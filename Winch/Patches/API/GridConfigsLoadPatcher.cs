using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Winch.Core.API;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(DataLoader))]
    [HarmonyPatch("OnGridConfigDataAddressablesLoaded")]
    class GridConfigsLoadPatcher
    {
        public static void Prefix(ItemManager __instance, AsyncOperationHandle<IList<GridConfiguration>> handle)
        {
            DredgeEvent.TriggerGridConfigsLoaded(__instance, handle, true);
        }

        public static void Postfix(ItemManager __instance, AsyncOperationHandle<IList<GridConfiguration>> handle)
        {
            DredgeEvent.TriggerGridConfigsLoaded(__instance, handle, false);
        }
    }
}
