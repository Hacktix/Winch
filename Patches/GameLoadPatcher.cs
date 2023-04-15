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
            Initializer.Initialize();
        }
    }
}
