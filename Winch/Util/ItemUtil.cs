using System;
using System.Collections.Generic;
using Winch.Core;
using Winch.Serialization;
using Winch.Serialization.Item;

namespace Winch.Util;

internal static class ItemUtil
{
    private static Dictionary<Type, IDredgeTypeConverter> Converters = new()
    {
        { typeof(FishItemData), new FishItemDataConverter() },
        { typeof(SpatialItemData), new SpatialItemDataConverter() },
    };

    public static Dictionary<string, ItemData> HarvestableItemDataDict = new();
    public static Dictionary<string, FishItemData> CustomFish = new();

    public static void PopulateItemData()
    {
        foreach (var item in GameManager.Instance.ItemManager.allItems)
        {
            if (item is FishItemData or RelicItemData or HarvestableItemData)
            {
                HarvestableItemDataDict.Add(item.id, item);
            }
            WinchCore.Log.Debug($"Added item {item.id} to HarvestableItemDataDict");
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

        var item = UtilHelpers.GetScriptableObjectFromMeta<T>(meta, metaPath);

        if (!UtilHelpers.PopulateObjectFromMeta<T>(item, meta, Converters))
        {
            return;
        }

        switch(item)
        {
            case FishItemData fishItem:
                CustomFish.Add(fishItem.id, fishItem);
                break;
            case RelicItemData relicItem:
            case SpatialItemData spatialItem:
            default:
                break;
        };

        GameManager.Instance.ItemManager.allItems.Add(item);
    }
}
