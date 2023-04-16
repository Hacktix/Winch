using HarmonyLib;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;
using Winch.Core.API;

namespace Winch.Patches.API
{
    [HarmonyPatch(typeof(AchievementManager))]
    [HarmonyPatch("OnAchievementDataAddressablesLoaded")]
    class AchievementLoadPatcher
    {
        public static void Prefix(ItemManager __instance, AsyncOperationHandle<IList<AchievementData>> handle)
        {
            DredgeEvent.TriggerAchievementsLoaded(__instance, handle, true);
        }

        public static void Postfix(ItemManager __instance, AsyncOperationHandle<IList<AchievementData>> handle)
        {
            DredgeEvent.TriggerAchievementsLoaded(__instance, handle, false);
        }
    }
}
