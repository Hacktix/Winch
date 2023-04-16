using HarmonyLib;
using Winch.Core;

namespace IntroSkipper
{
    [HarmonyPatch(typeof(SplashController))]
    [HarmonyPatch("OnEnable")]
    class SplashControllerPatcher
    {
        public static bool Prefix()
        {
            WinchCore.Log.Info("Skipping Splash Screen...");
            GameManager.Instance.Loader.LoadStartupFromSplash();
            return false;
        }
    }
}
