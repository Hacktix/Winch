using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Winch.Core.API;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(DataLoader))]
    [HarmonyPatch("OnWorldEventDataAddressablesLoaded")]
    class WorldEventLoadPatcher
    {
        public static void Prefix(DataLoader __instance, AsyncOperationHandle<IList<WorldEventData>> handle)
        {
            DredgeEvent.AddressableEvents.WorldEventsLoaded.Trigger(__instance, handle, true);
        }

        public static void Postfix(DataLoader __instance, AsyncOperationHandle<IList<WorldEventData>> handle)
        {
            DredgeEvent.AddressableEvents.WorldEventsLoaded.Trigger(__instance, handle, false);
        }
    }
}
