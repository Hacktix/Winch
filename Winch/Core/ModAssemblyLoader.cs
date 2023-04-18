using System;
using System.Collections.Generic;
using System.IO;

namespace Winch.Core
{
    class ModAssemblyLoader
    {
        public static Dictionary<string, ModAssembly> RegisteredAssemblies = new Dictionary<string, ModAssembly>();
        public static List<string> LoadedMods = new List<string>();
        public static List<string> ErrorMods = new List<string>();

        public static void LoadModAssemblies()
        {

            if (!Directory.Exists("Mods"))
                Directory.CreateDirectory("Mods");

            string[] modDirs = Directory.GetDirectories("Mods");
            WinchCore.Log.Info($"Loading {modDirs.Length} mod assemblies...");
            foreach (string modDir in modDirs)
                RegisterModAssembly(modDir);
            ExecuteModAssemblies();
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
                WinchCore.Log.Error($"Error loading {modName}: {ex.ToString()}");
                return;
            }
        }

        private static void ExecuteModAssemblies()
        {
            foreach (string modName in RegisteredAssemblies.Keys)
                ExecuteModAssembly(modName);
        }

        internal static void ExecuteModAssembly(string modName)
        {
            if (LoadedMods.Contains(modName) || ErrorMods.Contains(modName))
                return;

            if (!RegisteredAssemblies.ContainsKey(modName))
            {
                ErrorMods.Add(modName);
                WinchCore.Log.Error($"Mod not loaded: {modName}");
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
                WinchCore.Log.Error($"Error initializing {modName}: {ex.ToString()}");
                return;
            }
        }
    }
}
