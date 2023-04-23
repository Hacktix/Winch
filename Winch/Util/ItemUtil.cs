using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Winch.Core;
using Winch.Serialization;

namespace Winch.Util;

internal static class ItemUtil
{
    private static Dictionary<Type, IDredgeTypeConverter> Converters = new()
    {
        { typeof(FishItemData), new FishItemDataConverter() },
        { typeof(SpatialItemData), new SpatialItemDataConverter() },
    };

    internal static void AddItemFromMeta<T>(string metaPath) where T : ItemData
    {
        string metaFile = File.ReadAllText(metaPath);
        Dictionary<string, object> meta = JsonConvert.DeserializeObject<Dictionary<string, object>>(metaFile);
        meta["id"] = Path.GetFileNameWithoutExtension(metaPath);

        if (!meta.ContainsKey("itemInsaneTitleKey"))
        {
            WinchCore.Log.Debug("WRITING INSANETITLE KEY");
            //meta["itemInsaneTitleKey"] = meta["itemNameKey"];
        }
        if (!meta.ContainsKey("itemInsaneDescriptionKey"))
        {
            WinchCore.Log.Debug("WRITING INSANEDESC KEY");
            //meta["itemInsaneDescriptionKey"] = meta["itemDescriptionKey"];
        }

        T item = ScriptableObject.CreateInstance<T>();
        Type itemType = typeof(T);
        if (Converters.TryGetValue(itemType, out var converter))
        {
            converter.PopulateFields(item, meta);
            GameManager.Instance.ItemManager.allItems.Add(item);
        }
        else
        {
            WinchCore.Log.Error($"No converter found for type {itemType}");
        }
    }
}
