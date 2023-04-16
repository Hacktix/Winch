using HarmonyLib;
using Winch.Core;

namespace Winch.Patches
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("AddTerminalCommands")]
    class BuildInfoFeaturePatcher
    {
        static bool Prefix() {
            WinchCore.Log.Debug("Disallowed Feature Enable Commands to be added to Terminal.");
            return false;
        }
    }
}
