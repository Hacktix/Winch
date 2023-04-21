using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Winch.Core.API;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(DataLoader))]
    [HarmonyPatch("OnQuestDataAddressablesLoaded")]
    class QuestLoadPatcher
    {
        public static void Prefix(DataLoader __instance, AsyncOperationHandle<IList<QuestData>> handle)
        {
            DredgeEvent.AddressableEvents.QuestsLoaded.Trigger(__instance, handle, true);
        }

        public static void Postfix(DataLoader __instance, AsyncOperationHandle<IList<QuestData>> handle)
        {
            DredgeEvent.AddressableEvents.QuestsLoaded.Trigger(__instance, handle, false);
        }
    }
}
