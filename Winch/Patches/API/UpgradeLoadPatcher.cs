using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Winch.Core.API;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(UpgradeManager))]
    [HarmonyPatch("OnUpgradeDataAddressablesLoaded")]
    class UpgradeLoadPatcher
    {
        public static void Prefix(UpgradeManager __instance, AsyncOperationHandle<IList<UpgradeData>> handle)
        {
            DredgeEvent.AddressableEvents.UpgradesLoaded.Trigger(__instance, handle, true);
        }

        public static void Postfix(UpgradeManager __instance, AsyncOperationHandle<IList<UpgradeData>> handle)
        {
            DredgeEvent.AddressableEvents.UpgradesLoaded.Trigger(__instance, handle, false);
        }
    }
}
