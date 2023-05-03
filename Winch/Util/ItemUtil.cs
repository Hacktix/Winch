using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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

    public static Dictionary<string, ItemData> HarvestableItemDataDict = new();

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

    internal static void AddItemFromMeta(Type itemType, string metaPath)
    {
        if (!itemType.IsSubclassOf(typeof(ItemData)))
            throw new ArgumentException($"Type {nameof(itemType)} must be a subclass of {nameof(ItemData)}");

        var meta = UtilHelpers.ParseMeta(metaPath);
        if (meta == null)
        {
            WinchCore.Log.Error($"Meta file {metaPath} is empty");
            return;
        }

        meta["id"] = Path.GetFileNameWithoutExtension(metaPath);
        var item = ScriptableObject.CreateInstance(itemType) as ItemData ?? throw new NullReferenceException($"Unable to create instance of {itemType}");

        if (UtilHelpers.PopulateObjectFromMeta(itemType, item, meta, Converters))
        {
            WinchCore.Log.Debug($"Added item {item.id} from meta {metaPath}");
            GameManager.Instance.ItemManager.allItems.Add(item);
        }
    }
    internal static void AddItemFromMeta<T>(string metaPath) where T : ItemData
    {
       AddItemFromMeta(typeof(T), metaPath);
    }
}
