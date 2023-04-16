using HarmonyLib;
using Winch.Core;

namespace IntroSkipper
{
    public class Loader
    {
        public static void Initialize()
        {
            WinchCore.Log.Info("Applying Harmony Patches...");
            var harmony = new Harmony("com.dredge.introskipper");
            harmony.PatchAll();
        }
    }
}
