using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Winch.Core.API;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(DataLoader))]
    [HarmonyPatch("OnQuestGridDataAddressablesLoaded")]
    class QuestGridLoadPatcher
    {
        public static void Prefix(DataLoader __instance, AsyncOperationHandle<IList<QuestGridConfig>> handle)
        {
            DredgeEvent.AddressableEvents.QuestGridConfigsLoaded.Trigger(__instance, handle, true);
        }

        public static void Postfix(DataLoader __instance, AsyncOperationHandle<IList<QuestGridConfig>> handle)
        {
            DredgeEvent.AddressableEvents.QuestGridConfigsLoaded.Trigger(__instance, handle, false);
        }
    }
}
