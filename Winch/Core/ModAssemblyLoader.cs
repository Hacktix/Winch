using System;
using System.IO;

namespace Winch.Core
{
    class ModAssemblyLoader
    {
        public static int LoadedAssemblies = 0;

        public static void LoadModAssemblies()
        {

            if (!Directory.Exists("Mods"))
                Directory.CreateDirectory("Mods");

            string[] modDirs = Directory.GetDirectories("Mods");
            WinchCore.Log.Debug($"Loading {modDirs.Length} mod assemblies...");
            foreach (string modDir in modDirs)
            {
                bool success = LoadModFromPath(modDir);
                if (success)
                {
                    LoadedAssemblies++;
                }
            }
        }

        private static bool LoadModFromPath(string path)
        {
            string modName = Path.GetFileName(path);
            WinchCore.Log.Info($"Loading '{modName}'...");
            try
            {
                ModAssembly mod = ModAssembly.FromPath(path);
                mod.LoadAssembly();
                mod.ExecuteAssembly();
            }
            catch(Exception ex)
            {
                WinchCore.Log.Error($"Error loading {modName}: {ex.ToString()}");
                return false;
            }

            WinchCore.Log.Info($"Successfully loaded {modName}.");
            return true;
        }
    }
}
