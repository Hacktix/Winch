using HarmonyLib;
using Winch.Core;

namespace Winch.Patches
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("WaitForAllAsyncManagers")]
    class GameLoadPatcher
    {
        static void Postfix()
        {
            WinchCore.Log.Debug("'GameManager.WaitForAllAsyncManagers' Postfix called, running Initializer.");
            Initializer.Initialize();
        }
    }
}
