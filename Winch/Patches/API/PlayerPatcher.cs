using System;
using HarmonyLib;
using Winch.Core;
using Winch.Util;

namespace Winch.Patches.API;

[HarmonyPatch(typeof(Player))]
[HarmonyPatch("OnEnable")]
public class PlayerPatch
{
    public static void Postfix(Player __instance)
    {
        WinchCore.Log.Debug("Awake Prefix");
        try
        {
            ItemUtil.PopulateItemData();
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in {nameof(PlayerPatch)}: exception {ex}");
        }
    }
}
