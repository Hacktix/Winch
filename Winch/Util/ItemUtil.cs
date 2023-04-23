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

        WinchCore.Log.Debug($"Adding Item of type {typeof(T)}");

        T item = ScriptableObject.CreateInstance<T>();
        Type itemType = typeof(T);
        WinchCore.Log.Debug($"Typeof is still {itemType}");
        if (Converters.TryGetValue(itemType, out var converter))
        {
            converter.PopulateFields(item, meta);
            WinchCore.Log.Debug($"Item {itemType} fields:");
            foreach (var f in itemType.GetFields())
            {
                WinchCore.Log.Debug($"    {f.Name} : {f.GetValue(item)}");
                if (f.FieldType == typeof(List<Vector2Int>))
                {
                    foreach (var dim in (IEnumerable)f.GetValue(item))
                    {
                        var castDim = (Vector2Int)dim;
                        WinchCore.Log.Debug($"        [{castDim.x}, {castDim.y}]");
                    }
                }
            }
            GameManager.Instance.ItemManager.allItems.Add(item);
        }
        else
        {
            WinchCore.Log.Error($"No converter found for type {itemType}");
        }
    }
}
