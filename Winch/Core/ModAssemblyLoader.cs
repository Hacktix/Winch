using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Winch.Util;

namespace Winch.Core
{
    class ModAssemblyLoader
    {
        public static Dictionary<string, ModAssembly> RegisteredAssemblies = new();
        public static Dictionary<string, bool> EnabledMods = new();
        public static List<string> LoadedMods = new();
        public static List<string> ErrorMods = new();

        internal static void LoadModAssemblies()
        {

            if (!Directory.Exists("Mods"))
                Directory.CreateDirectory("Mods");

            string[] modDirs = Directory.GetDirectories("Mods");
            WinchCore.Log.Info($"Loading {modDirs.Length} mod assemblies...");
            foreach (string modDir in modDirs)
                RegisterModAssembly(modDir);

            try
            {
                GetEnabledMods();
            }
            catch (Exception ex)
            {
                WinchCore.Log.Error($"Error fetching enabled mods: {ex}");
            }
        }

        private static void RegisterModAssembly(string path)
        {
            string modName = Path.GetFileName(path);
            WinchCore.Log.Debug($"Loading '{modName}'...");
            try
            {
                ModAssembly mod = ModAssembly.FromPath(path);
                mod.LoadAssembly();
                RegisteredAssemblies.Add(modName, mod);
            }
            catch(Exception ex)
            {
                ErrorMods.Add(modName);
                WinchCore.Log.Error($"Error loading {modName}: {ex}");
            }
        }

        internal static void ExecuteModAssemblies()
        {
            foreach (string modName in RegisteredAssemblies.Keys)
                ExecuteModAssembly(modName);
        }

        internal static void ExecuteModAssembly(string modName, string? minVersion = null)
        {
            if (LoadedMods.Contains(modName) || ErrorMods.Contains(modName))
                return;

            if (!RegisteredAssemblies.ContainsKey(modName))
            {
                ErrorMods.Add(modName);
                WinchCore.Log.Error($"Mod not loaded: {modName}");
                return;
            }

            if(minVersion != null)
            {
                if (!VersionUtil.IsSameOrNewer(RegisteredAssemblies[modName].Metadata["Version"].ToString(), minVersion))
                    throw new Exception($"Cannot satisfy minimum version constraint {minVersion} for {modName}");
            }

            var modGUID = (string)RegisteredAssemblies[modName].Metadata["ModGUID"];
            if (!EnabledMods[modGUID])
            {
                WinchCore.Log.Info($"Mod '{modName}' disabled.");
                return;
            }

            try
            {
                RegisteredAssemblies[modName].ExecuteAssembly();
                LoadedMods.Add(modName);
                WinchCore.Log.Info($"Successfully initialized {modName}.");
            }
            catch(Exception ex)
            {
                ErrorMods.Add(modName);
                WinchCore.Log.Error($"Error initializing {modName}: {ex}");
            }
        }

        internal static void GetEnabledMods()
        {
            string modListPath = Path.Combine(Directory.GetCurrentDirectory(), "mod_list.json");

            if (File.Exists(modListPath))
            {
                string modList = File.ReadAllText(modListPath);
                EnabledMods = JsonConvert.DeserializeObject<Dictionary<string, bool>>(modList)
                    ?? throw new InvalidOperationException("Unable to parse mod_list.json file.");
            }

            foreach (string mod in RegisteredAssemblies.Keys) 
            {
                string modGUID = (string) RegisteredAssemblies[mod].Metadata["ModGUID"];
                if (!EnabledMods.ContainsKey(modGUID))
                {
                    EnabledMods.Add(modGUID, true);
                }
            }

            string serializedEnabledMods = JsonConvert.SerializeObject(EnabledMods, Formatting.Indented);
            File.WriteAllText(modListPath, serializedEnabledMods);
        }
    }
}
