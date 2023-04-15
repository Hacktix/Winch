using HarmonyLib;
using System.IO;

namespace Winch.Patches
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("WaitForAllAsyncManagers")]
    class GameLoadHook
    {
        static void Postfix()
        {
            GameManager.Instance.BuildInfo.BuildNumber += "\nWinch Loaded!";
        }
    }
}
