using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Winch.Config;

namespace Winch.Core
{
    class ModAssembly
    {
        public readonly string BasePath;
        public Dictionary<string, object> Metadata;
        public Assembly LoadedAssembly;

        private ModAssembly(string basePath) {
            BasePath = basePath;

            string metaPath = Path.Combine(basePath, "mod_meta.json");
            if (!File.Exists(metaPath))
                throw new FileNotFoundException("Missing mod_meta.json file.");

            string metaText = File.ReadAllText(metaPath);
            Metadata = JsonConvert.DeserializeObject<Dictionary<string, object>>(metaText);
        }

        internal static ModAssembly FromPath(string path)
        {
            return new ModAssembly(path);
        }



        internal void LoadAssembly()
        {
            if (!Metadata.ContainsKey("ModAssembly"))
                throw new MissingFieldException("Property 'ModAssembly' not found in mod_meta.json");

            string assemblyName = Metadata["ModAssembly"].ToString();
            string assemblyPath = Path.Combine(BasePath, assemblyName);
            if(!File.Exists(assemblyPath))
                throw new FileNotFoundException($"Could not find mod assembly '{assemblyPath}'");

            LoadedAssembly = Assembly.LoadFrom(assemblyPath);

            WinchCore.Log.Debug($"Loaded Assembly '{LoadedAssembly.GetName().Name}'.");
        }

        internal void ExecuteAssembly()
        {
            if (LoadedAssembly == null)
                throw new NullReferenceException("Cannot execute assembly as LoadedAssembly is null");

            WinchCore.Log.Debug($"Initializing ModAssembly {LoadedAssembly.GetName().Name}...");

            if (Metadata.ContainsKey("DefaultConfig"))
                ProcessDefaultConfig();

            if (Metadata.ContainsKey("Dependencies"))
                ProcessDependencies();

            if (Metadata.ContainsKey("Entrypoint"))
                ProcessEntrypoint();
        }



        private void ProcessDependencies()
        {
            string[] deps = ((JArray)Metadata["Dependencies"]).ToObject<string[]>();
            foreach (string dep in deps)
            {
                WinchCore.Log.Debug($"Processing dependency {dep}");
                ModAssemblyLoader.ExecuteModAssembly(dep);
            }
        }

        private void ProcessDefaultConfig()
        {
            string defaultConfig = JsonConvert.SerializeObject(Metadata["DefaultConfig"], Formatting.Indented);
            string modName = Path.GetFileName(BasePath);
            ModConfig.RegisterDefaultConfig(modName, defaultConfig);
        }

        private void ProcessEntrypoint()
        {
            string entrypointSetting = Metadata["Entrypoint"].ToString();
            if (!entrypointSetting.Contains("/"))
                throw new ArgumentException("Malformed Entrypoint in mod_meta.json");

            string entrypointTypeName = entrypointSetting.Split('/')[0];
            string entrypointMethodName = entrypointSetting.Split('/')[1];

            Type entrypointType = LoadedAssembly.GetType(entrypointTypeName);
            if (entrypointType == null)
                throw new EntryPointNotFoundException($"Could not find type {entrypointTypeName} in Mod Assembly");

            MethodInfo entrypoint = entrypointType.GetMethod(entrypointMethodName);
            if (entrypoint == null)
                throw new EntryPointNotFoundException($"Could not find method {entrypointTypeName} in type {entrypointTypeName} in Mod Assembly");

            WinchCore.Log.Debug($"Invoking entrypoint {entrypointType}.{entrypointMethodName}...");
            entrypoint.Invoke(null, new object[0]);
        }
    }
}
