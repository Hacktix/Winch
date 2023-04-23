using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Winch.Serialization;

namespace Winch.Util;

internal static class ItemUtil
{
    internal static void AddItemFromMeta<T>(string metaPath) where T : ItemData
    {
        string metaFile = File.ReadAllText(metaPath);
        Dictionary<string, object> meta = JsonConvert.DeserializeObject<Dictionary<string, object>>(metaFile);
        meta["id"] = Path.GetFileNameWithoutExtension(metaPath);

        T item = ScriptableObject.CreateInstance<T>();
        Type itemType = typeof(T);
        
        //TODO: Think of a better way of doing this.... Maybe a dictionary of converters?
        switch(itemType)
        {
            case { } fishItemData when fishItemData == typeof(FishItemData):
                break;
            case { } spatialItemData when spatialItemData == typeof(SpatialItemData):
                break;
        };

        GameManager.Instance.ItemManager.allItems.Add(item);
    }
}