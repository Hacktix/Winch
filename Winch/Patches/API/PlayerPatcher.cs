using System;
using HarmonyLib;
using UnityEngine;
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
            WinchCore.Log.Debug("populating harvestables...");
            PoiUtil.PopulateHarvestablesAndHarvestParticlePrefabs();
            WinchCore.Log.Debug("Harvestables populated");
            WinchCore.Log.Debug("populating items...");
            ItemUtil.PopulateItemData();
            WinchCore.Log.Debug("Items Populated");
            foreach (var customHarvestPoi in PoiUtil.CustomHarvestPois)
            {
                WinchCore.Log.Debug($"Adding custom harvest poi {customHarvestPoi.id}");
                var poiGameObject = PoiUtil.CreateGameObjectFromCustomHarvestPoi(customHarvestPoi);
                poiGameObject.transform.SetParent(GameSceneInitializer.Instance.HarvestPoiContainer.transform);
                WinchCore.Log.Debug($"{customHarvestPoi.id} has been added at {poiGameObject.transform.position}");
            }
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in {nameof(PlayerPatch)}: exception {ex}");
        }
        WinchCore.Log.Debug("Finished adding all custom pois.");
    }
}

public class PatchActions
{
    public static void Main()
    {
        WinchCore.Log.Debug("Awake Prefix");
        try
        {
            WinchCore.Log.Debug("populating harvestables...");
            PoiUtil.PopulateHarvestablesAndHarvestParticlePrefabs();
            WinchCore.Log.Debug("Harvestables populated");
            WinchCore.Log.Debug("populating items...");
            ItemUtil.PopulateItemData();
            WinchCore.Log.Debug("Items Populated");
            foreach (var customHarvestPoi in PoiUtil.CustomHarvestPois)
            {
                var poiGameObject = PoiUtil.CreateGameObjectFromCustomHarvestPoi(customHarvestPoi);
                
            }
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Error in {nameof(PlayerPatch)}: exception {ex}");
        }
    }
}
