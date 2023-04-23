using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Localization;
using Winch.Core;

// ReSharper disable HeapView.BoxingAllocation
// ReSharper disable HeapView.PossibleBoxingAllocation

namespace Winch.Serialization;

public class DredgeTypeConverter<T> : IDredgeTypeConverter
{
    private Dictionary<string, FieldDefinition> FieldDefinitions { get; } = new();
    private Dictionary<string, string> Reroutes { get; } = new();
    protected static Dictionary<(string TableId, string Text), LocalizedString> StringDefinitionCache { get; } = new();

    public void PopulateFields(object obj, Dictionary<string, object> data)
    {
        ProcessDictionaryEntries(obj, data);
        ProcessReroutes(obj, data);
    }

    private void ProcessDictionaryEntries(object obj, Dictionary<string, object> data)
    {
        Type itemType = obj.GetType();
        foreach (var field in itemType.GetRuntimeFields())
        {
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
                    if (FieldDefinitions.TryGetValue(field.Name, out var definition))
                    {
                        if (definition.DefaultValue != null)
                        {
                            field.SetValue(obj, definition.DefaultValue);
                        }
                    }
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

    private void ProcessReroutes(object obj, Dictionary<string, object> data)
    {
        Type objectType = obj.GetType();
        foreach (var rerouteKeyValPair in Reroutes)
        {
            var targetField = objectType.GetRuntimeFields().FirstOrDefault(field => field.Name == rerouteKeyValPair.Key);
            var sourceField = objectType.GetRuntimeFields().FirstOrDefault(field => field.Name == rerouteKeyValPair.Value);
            if (targetField != null && sourceField != null)
            {
                if (targetField.GetValue(obj) == null)
                {
                    targetField.SetValue(obj, sourceField.GetValue(obj));
                }
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
    
    protected void AddReroutes(Dictionary<string, string> reroutes)
    {
        foreach (var reroute in reroutes)
        {
            this.Reroutes.Add(reroute.Key, reroute.Value);
        }
    }
}
