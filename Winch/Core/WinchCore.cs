using HarmonyLib;
using System;
using Winch.Logging;
using Winch.Util;

namespace Winch.Core
{
    public class WinchCore
    {
        public static Logger Log = new Logger();

        public static void Main()
        {
            string version = VersionUtil.GetVersion();
            Log.Info($"Winch {version} booting up...");

            ModAssemblyLoader.LoadModAssemblies();

            var harmony = new Harmony("com.dredge.winch");
            Log.Debug("Created Harmony Instance 'com.dredge.winch'. Patching...");
            harmony.PatchAll();

            foreach(ModAssembly modAssembly in ModAssemblyLoader.RegisteredAssemblies.Values)
            {
                try
                {
                    bool hasPatches = modAssembly.Metadata.ContainsKey("ApplyPatches") && (bool)modAssembly.Metadata["ApplyPatches"] == true;
                    if (modAssembly.LoadedAssembly != null && hasPatches)
                    {
                        Log.Debug($"Patching from {modAssembly.LoadedAssembly.GetName().Name}...");
                        harmony.PatchAll(modAssembly.LoadedAssembly);
                    }
                }
                catch(Exception ex)
                {
                    Log.Error($"Failed to apply patches for {modAssembly.BasePath}: {ex}");
                }
            }

            Log.Debug("Harmony Patching complete.");
        }
    }
}
