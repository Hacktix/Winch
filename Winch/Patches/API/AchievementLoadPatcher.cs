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
        public static void Prefix(AchievementManager __instance, AsyncOperationHandle<IList<AchievementData>> handle)
        {
            DredgeEvent.AddressableEvents.AchievementsLoaded.Trigger(__instance, handle, true);
        }

        public static void Postfix(AchievementManager __instance, AsyncOperationHandle<IList<AchievementData>> handle)
        {
            DredgeEvent.AddressableEvents.AchievementsLoaded.Trigger(__instance, handle, false);
        }
    }
}
