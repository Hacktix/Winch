using HarmonyLib;
using Winch.Core;

namespace Winch.Patches
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.WaitForAllAsyncManagers))]
    class GameLoadPatcher
    {
        static void Postfix()
        {
            WinchCore.Log.Info("Game Managers completed loading, initializing Winch...");
            Initializer.Initialize();
            WinchCore.Log.Info("Winch initialized successfully.");
        }
    }
}
