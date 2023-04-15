using HarmonyLib;

namespace Winch.Patches
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("WaitForAllAsyncManagers")]
    class GameLoadPatcher
    {
        static void Postfix()
        {
            GameManager.Instance.BuildInfo.BuildNumber += "\nWinch Loaded!";
        }
    }
}
