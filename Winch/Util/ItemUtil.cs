using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Winch.Core;
using Winch.Serialization;
using Winch.Serialization.Item;

namespace Winch.Util;

internal static class ItemUtil
{
    private static readonly Dictionary<Type, IDredgeTypeConverter> Converters = new()
    {
        { typeof(NonSpatialItemData), new NonSpatialItemDataConverter() },
        { typeof(MessageItemData), new MessageItemDataConverter() },
        { typeof(ResearchableItemData), new ResearchableItemDataConverter() },
        { typeof(SpatialItemData), new SpatialItemDataConverter() },
        { typeof(EngineItemData), new EngineItemDataConverter() },
        { typeof(FishItemData), new FishItemDataConverter() },
        { typeof(RelicItemData), new RelicItemDataConverter() },
        { typeof(DeployableItemData), new DeployableItemDataConverter() },
        { typeof(DredgeItemData), new DredgeItemDataConverter() },
        { typeof(RodItemData), new RodItemDataConverter() },
        { typeof(LightItemData), new LightItemDataConverter() },
        { typeof(DamageItemData), new DamageItemDataConverter() },
    };

    public static Dictionary<string, ItemData> harvestableItemDataDict = new();
    public static Dictionary<string, ItemData> allItemDataDict = new();

    public static void PopulateItemData()
    {
        foreach (var item in GameManager.Instance.ItemManager.allItems)
        {
            if (item is FishItemData or RelicItemData or HarvestableItemData)
            {
                harvestableItemDataDict.Add(item.id, item);
                WinchCore.Log.Debug($"Added item {item.id} to harvestableItemDataDict");
            }
            allItemDataDict.Add(item.id, item);
            WinchCore.Log.Debug($"Added item {item.id} to allItemDataDict");
        }
    }

    internal static void AddItemFromMeta<T>(string metaPath) where T : ItemData
    {
        var meta = UtilHelpers.ParseMeta(metaPath);
        if (meta == null)
        {
            WinchCore.Log.Error($"Meta file {metaPath} is empty");
            return;
        }
        if (allItemDataDict.ContainsKey((string)meta["id"]))
        {
            WinchCore.Log.Error($"Duplicate item {(string)meta["id"]} at {metaPath} failed to load");
            return;
        }
        var item = UtilHelpers.GetScriptableObjectFromMeta<T>(meta, metaPath);
        if (UtilHelpers.PopulateObjectFromMeta<T>(item, meta, Converters))
        {
            GameManager.Instance.ItemManager.allItems.Add(item);
            allItemDataDict.Add(item.id, item);
        }
    }
}
