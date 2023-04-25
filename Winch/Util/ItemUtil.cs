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
    private static Dictionary<Type, IDredgeTypeConverter> Converters = new()
    {
        { typeof(FishItemData), new FishItemDataConverter() },
        { typeof(SpatialItemData), new SpatialItemDataConverter() },
    };

    internal static void AddItemFromMeta<T>(string metaPath) where T : ItemData
    {
        var meta = UtilHelpers.ParseMeta(metaPath);

        if (meta == null)
        {
            WinchCore.Log.Error($"Meta file {metaPath} is empty");
            return;
        }

        var item = UtilHelpers.GetScriptableObjectFromMeta<T>(meta, metaPath);

        if (UtilHelpers.PopulateObjectFromMeta<T>(item, meta, Converters))
            GameManager.Instance.ItemManager.allItems.Add(item);
    }
}
