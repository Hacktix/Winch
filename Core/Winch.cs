using HarmonyLib;
using Winch.Logging;
using Winch.Util;

namespace Winch.Core
{
    public class Winch
    {
        public static Logger Log = new Logger();

        public static void Main()
        {
            string version = VersionUtil.GetVersion();
            Log.Info($"Winch {version} booting up...");

            var harmony = new Harmony("com.dredge.winch");
            harmony.PatchAll();
        }
    }
}
