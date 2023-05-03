using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using Winch.Core;
using Winch.Serialization;
// ReSharper disable HeapView.PossibleBoxingAllocation

namespace Winch.Util;

public static class UtilHelpers
{
    public static Dictionary<string, object>? ParseMeta(string metaPath)
    {
        try
        {
            string metaFile = File.ReadAllText(metaPath);
            Dictionary<string, object>? meta = JsonConvert.DeserializeObject<Dictionary<string, object>>(metaFile);
            return meta;
        }
        catch (Exception ex)
        {
            WinchCore.Log.Error($"Unable to read meta file {metaPath}: {ex.Message}");
        }

        return null;
    }

    public static ScriptableObject GetScriptableObjectFromMeta(Type scriptableObjectType, Dictionary<string, object> meta, string metaPath)
    {
        if (!scriptableObjectType.IsSubclassOf(typeof(ScriptableObject)))
            throw new ArgumentException($"Type {nameof(scriptableObjectType)} must be a subclass of {nameof(ScriptableObject)}");

        meta["id"] = Path.GetFileNameWithoutExtension(metaPath);
        ScriptableObject item = ScriptableObject.CreateInstance(scriptableObjectType);
        return item;
    }

    public static T GetScriptableObjectFromMeta<T>(Dictionary<string, object> meta, string metaPath) where T : ScriptableObject
    {
        return GetScriptableObjectFromMeta(typeof(T), meta, metaPath) as T;
    }

    public static bool PopulateObjectFromMeta(Type itemType, object item, Dictionary<string, object> meta,
        Dictionary<Type, IDredgeTypeConverter> converters)
    {
        if (item == null) throw new ArgumentNullException($"{nameof(item)} is null");

        if (converters.TryGetValue(itemType, out var converter))
        {
            converter.PopulateFields(item, meta);
        }
        else
        {
            WinchCore.Log.Error($"No converter found for type {itemType}");
            return false;
        }
        return true;
    }

    public static bool PopulateObjectFromMeta<T>(T item, Dictionary<string, object> meta, Dictionary<Type, IDredgeTypeConverter> converters)
    {
        return PopulateObjectFromMeta(typeof(T), item, meta, converters);
    }
}
