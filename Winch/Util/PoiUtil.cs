using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using HarmonyLib;
using UnityEngine;
using Winch.Core;
using Winch.Serialization;

using Winch.Serialization.POI;
using Winch.Serialization.POI.Conversation;
using Winch.Serialization.POI.Harvest;

namespace Winch.Util;

internal static class PoiUtil
{
    private static Dictionary<Type, IDredgeTypeConverter> Converters = new()
    {
        //{ typeof(POI), new PoiConverter() },
        //{ typeof(DockPOI), new DockPoiConverter() },
        //{ typeof(ItemPOI), new ItemPoiConverter() },
        //{ typeof(HarvestPOI), new HarvestPoiConverter() },
        //{ typeof(HarvestPOIDataModel), new HarvestPoiDataModelConverter() },
        //{ typeof(BaitHarvestPOI), new BaitPoiConverter() },
        //{ typeof(PlacedHarvestPOI), new PlacedPoiConverter() },
        //{ typeof(ConversationPOI), new ConversationPoiConverter() },
        //{ typeof(AutoMovePOI), new AutoMovePoiConverter() },
        //{ typeof(ConversationPOI), new ConversationPoiConverter() },
        //{ typeof(ExplosivePOI), new ExplosivePoiConverter() },
        { typeof(CustomHarvestPoi), new CustomHarvestPoiConverter()}
    };

    public static List<CustomHarvestPoi> CustomHarvestPois = new();
    public static Dictionary<string, IHarvestable> Harvestables = new();
    public static Dictionary<string, GameObject> HarvestParticlePrefabs = new();

    public static void PopulateHarvestablesAndHarvestParticlePrefabs()
    {
        var allHarvestPOIs = Traverse.Create(GameManager.Instance.HarvestPOIManager).Field("allHarvestPOIs").GetValue() as List<HarvestPOI>;
        foreach (var harvestPoi in allHarvestPOIs)
        {
            try
            {
                if (!Harvestables.ContainsKey(harvestPoi.Harvestable.GetId()))
                    Harvestables.Add(harvestPoi.Harvestable.GetId(), harvestPoi.Harvestable);
                var prefab = harvestPoi.Harvestable.GetParticlePrefab();
                if (!HarvestParticlePrefabs.ContainsKey(prefab.name))
                    HarvestParticlePrefabs.Add(prefab.name, prefab);
            }
            catch (Exception ex)
            {
                WinchCore.Log.Error($"Unable to add harvestable {harvestPoi.Harvestable.GetId()} to Harvestables and HarvestParticlePrefabs: {ex}");
            }
        }
    }

    public static GameObject CreateGameObjectFromCustomHarvestPoi(CustomHarvestPoi customHarvestPoi)
    {
        GameObject customPoi = new GameObject();
        customPoi.transform.SetParent(GameSceneInitializer.Instance.HarvestPoiContainer.transform);
        customPoi.transform.position = customHarvestPoi.location;
        customPoi.name = customHarvestPoi.id;
        var harvestPoi = customPoi.AddComponent<HarvestPOI>();

        var harvestPoiDataModel = new HarvestPOIDataModel();
        harvestPoiDataModel.doesRestock = customHarvestPoi.doesRestock;
        harvestPoiDataModel.startStock = customHarvestPoi.startStock;
        harvestPoiDataModel.usesTimeSpecificStock = customHarvestPoi.useTimeSpecificStock;

        foreach (var item in customHarvestPoi.items)
        {
            if (ItemUtil.HarvestableItemDataDict.TryGetValue(item, out var value))
                harvestPoiDataModel.items.Add((HarvestableItemData)value);
        }

        foreach (var item in customHarvestPoi.nightItems)
        {
            if (ItemUtil.HarvestableItemDataDict.TryGetValue(item, out var value))
                harvestPoiDataModel.nightItems.Add((HarvestableItemData)value);
        }

        harvestPoiDataModel.id = customHarvestPoi.id;

        harvestPoi.HarvestPOIData = harvestPoiDataModel;
        harvestPoi.Harvestable = harvestPoiDataModel;

        GameObject harvestParticlePrefab = null;
        if (HarvestParticlePrefabs.TryGetValue(customHarvestPoi.harvestableParticlePrefab, out var prefab))
        {
            harvestParticlePrefab = prefab;
        }

        Traverse.Create(harvestPoi).Field("harvestParticlePrefab").SetValue(harvestParticlePrefab);

        // Default Harvest POI Sphere Collider
        var sphereCollider = customPoi.AddComponent<SphereCollider>();
        sphereCollider.radius = 2;
        sphereCollider.enabled = true;
        sphereCollider.contactOffset = 0.01f;

        Traverse.Create(harvestPoi).Field("poiCollider").SetValue(sphereCollider);

        // This needs to be added to the GameManager.Instance.CullingBrain
        var cullable = customPoi.AddComponent<Cullable>();
        GameManager.Instance.CullingBrain.AddCullable(cullable);

        // No setup needed
        customPoi.AddComponent<SimpleBuoyantObject>();

        var allPOIs = Traverse.Create(GameManager.Instance.HarvestPOIManager).Field("allHarvestPOIs").GetValue() as List<HarvestPOI>;
        allPOIs.Add(harvestPoi);

        customPoi.layer = LayerMask.NameToLayer("POI");
        return customPoi;
    }

    internal static void AddCustomHarvestPoiFromMetadata(string metaPath)
    {
        var meta = UtilHelpers.ParseMeta(metaPath);

        if (meta == null)
        {
            WinchCore.Log.Error($"Meta file {metaPath} is empty");
            return;
        }

        meta["id"] = Path.GetFileNameWithoutExtension(metaPath);
        CustomHarvestPoi item = new();

        if (UtilHelpers.PopulateObjectFromMeta(item, meta, Converters))
        {
            CustomHarvestPois.Add(item);
        }
        else
        {
            WinchCore.Log.Error($"No converter found for type {typeof(CustomHarvestPoi)}");
        }
    }
    
}
