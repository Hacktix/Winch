using HarmonyLib;
using Winch.Core.API;

namespace Randomizer.Patchers;

[HarmonyPatch(typeof(GameSceneInitializer))]
[HarmonyPatch("Start")]
public class GameSceneInitializerPatcher
{
    public static void Prefix(GameSceneInitializer __instance)
    {
        DredgeEvent.SceneEvents.GameSceneInitializerStart.Trigger(__instance, true);
    }
    public static void Postfix(GameSceneInitializer __instance)
    {
        DredgeEvent.SceneEvents.GameSceneInitializerStart.Trigger(__instance, false);
    }
}