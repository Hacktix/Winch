using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Winch.Core;

// ReSharper disable HeapView.BoxingAllocation
// ReSharper disable HeapView.PossibleBoxingAllocation

namespace Winch.Serialization;

public class DredgeTypeConverter<T> : IDredgeTypeConverter
{
    private Dictionary<string, FieldDefinition> FieldDefinitions { get; } = new();

    public void PopulateFields(object obj, Dictionary<string, object> data)
    {
        Type itemType = obj.GetType();
        WinchCore.Log.Debug($"Processing {itemType}...");
        foreach (var field in itemType.GetRuntimeFields())
        {
            WinchCore.Log.Debug($"    Field: {field.Name}");
            try
            {
                if (data.TryGetValue(field.Name, out var value))
                {
                    field.SetValue(obj,
                        FieldDefinitions[field.Name].Parser != null
                            ? FieldDefinitions[field.Name].Parser(value)
                            : value);
                }
                else
                {
                    field.SetValue(obj,
                        FieldDefinitions.TryGetValue(field.Name, out var definition) ? definition.DefaultValue : null);
                }
            }
            catch(Exception ex)
            {
                string configuredValue = data.TryGetValue(field.Name, out var value) ? value.ToString() : "UNDEFINED";
                WinchCore.Log.Error($"Exception occurred while processing field '{field.Name}' (Configured: '{configuredValue}'): {ex}");
                throw;
            }
        }
    }

    protected void AddDefinitions(Dictionary<string, FieldDefinition> definitions)
    {
        foreach (var fieldDefinitionEntry in definitions)
        {
            this.FieldDefinitions.Add(fieldDefinitionEntry.Key, fieldDefinitionEntry.Value);
        }
    }
    
    protected static TEnum GetEnumValue<TEnum>(object value) where TEnum : Enum
    {
        return (TEnum)Enum.Parse(typeof(TEnum), value.ToString());
    }

    protected static Color GetColorFromJsonObject(object value)
    {
        return GetColorFromObject(JsonConvert.DeserializeObject<Dictionary<string, int>>(value.ToString()));
    }

    private static Color GetColorFromObject(Dictionary<string, int> color)
    {
        int r = 0, g = 0, b = 0, a = 255;
        if (color.TryGetValue("r", out var value)) r = value;
        if (color.TryGetValue("g", out var value1)) g = value1;
        if (color.TryGetValue("b", out var value2)) b = value2;
        if (color.TryGetValue("a", out var value3)) a = value3;
        return new Color(r/255f, g/255f, b/255f, a/255f);
    }
    
    protected static List<Vector2Int> ParseDimensions(JArray dimensions)
    {
        WinchCore.Log.Debug($"Parsing dimensions: {dimensions}");
        var parsed = new List<Vector2Int>();
        for(int y = 0; y < dimensions.Count; y++)
        {
            string line = dimensions[y].ToString();
            for(int x = 0; x < line.Length; x++)
            {
                char pos = line[x];
                if (pos != ' ')
                    parsed.Add(new Vector2Int(x, y));
            }
        }

        foreach (var dim in parsed)
        {
            WinchCore.Log.Debug($"DimEntry : {dim.x}, {dim.y}");
        }

        return parsed;
    }
}
