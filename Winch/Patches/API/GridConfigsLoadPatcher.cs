using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Winch.Core.API;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(DataLoader))]
    [HarmonyPatch(nameof(DataLoader.OnGridConfigDataAddressablesLoaded))]
    class GridConfigsLoadPatcher
    {
        public static void Prefix(DataLoader __instance, AsyncOperationHandle<IList<GridConfiguration>> handle)
        {
            DredgeEvent.AddressableEvents.GridConfigsLoaded.Trigger(__instance, handle, true);
        }

        public static void Postfix(DataLoader __instance, AsyncOperationHandle<IList<GridConfiguration>> handle)
        {
            DredgeEvent.AddressableEvents.GridConfigsLoaded.Trigger(__instance, handle, false);
        }
    }
}
