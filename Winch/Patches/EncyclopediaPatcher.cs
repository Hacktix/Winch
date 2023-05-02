using System;
using System.Collections.Generic;
using HarmonyLib;
using Winch.Core;
using Winch.Util;

namespace Winch.Patches;

[HarmonyPatch(typeof(Encyclopedia))]
[HarmonyPatch("Awake")]
public class EncyclopediaPatcher
{
    public static void Prefix(Encyclopedia __instance)
    {
        var fishList = Traverse.Create(__instance).Field("allFish").GetValue() as List<FishItemData>;
        foreach (var customFish in ItemUtil.CustomFish)
        {
            try
            {
                fishList?.Add(customFish.Value);
            }
            catch (Exception ex)
            {
                WinchCore.Log.Error($"Unable to add custom fish {customFish.Key} to encyclopedia: {ex}");
            }
        }
    }
}
