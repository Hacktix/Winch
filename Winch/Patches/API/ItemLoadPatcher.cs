using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Winch.Core.API;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(ItemManager))]
    [HarmonyPatch("OnItemDataAddressablesLoaded")]
    class ItemLoadPatcher
    {
        public static void Prefix(ItemManager __instance, AsyncOperationHandle<IList<ItemData>> handle)
        {
            DredgeEvent.AddressableEvents.ItemsLoaded.Trigger(__instance, handle, true);
        }

        public static void Postfix(ItemManager __instance, AsyncOperationHandle<IList<ItemData>> handle)
        {
            DredgeEvent.AddressableEvents.ItemsLoaded.Trigger(__instance, handle, false);
        }
    }
}
