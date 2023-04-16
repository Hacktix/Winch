using HarmonyLib;
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
            Log.Debug("Harmony Patching complete.");
        }
    }
}
