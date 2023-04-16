using HarmonyLib;

namespace IntroSkipper
{
    [HarmonyPatch(typeof(SplashController))]
    [HarmonyPatch("OnEnable")]
    class SplashControllerPatcher
    {
        public static bool Prefix()
        {
            GameManager.Instance.Loader.LoadStartupFromSplash();
            return false;
        }
    }
}
