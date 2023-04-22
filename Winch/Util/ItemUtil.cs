using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
using UnityEngine.Localization;
using Winch.Core;

namespace Winch.Util
{
    internal static class ItemUtil
    {
        internal static void AddItemFromMeta<T>(string metaPath) where T : ItemData
        {
            string metaFile = File.ReadAllText(metaPath);
            Dictionary<string, object> meta = JsonConvert.DeserializeObject<Dictionary<string, object>>(metaFile);
            meta["id"] = Path.GetFileNameWithoutExtension(metaPath);

            T item = ScriptableObject.CreateInstance<T>();
            Type itemType = typeof(T);
            foreach(FieldInfo field in itemType.GetRuntimeFields())
            {
                try
                {
                    if (meta.ContainsKey(field.Name))
                    {
                        field.SetValue(item, ProcessFieldData(field.Name, meta[field.Name]));
                    }
                    else
                    {
                        object defaultVal = GetDefaultFieldData(field.Name, meta);
                        if (defaultVal != null)
                            field.SetValue(item, defaultVal);
                    }
                }
                catch(Exception ex)
                {
                    string configuredValue = meta.ContainsKey(field.Name) ? ProcessFieldData(field.Name, meta[field.Name]).ToString() : "null";
                    WinchCore.Log.Error($"Exception occurred while processing field '{field.Name}' (Configured: '{configuredValue}'): {ex}");
                    throw ex;
                }
            }

            GameManager.Instance.ItemManager.allItems.Add(item);
        }

        private static object GetDefaultFieldData(string fieldName, Dictionary<string, object> meta)
        {
            switch(fieldName)
            {
                // ItemData
                case "itemInsaneTitleKey":           return meta.ContainsKey("itemNameKey") ? ProcessFieldData("itemInsaneTitleKey", meta["itemNameKey"]) : null;
                case "itemInsaneDescriptionKey":     return meta.ContainsKey("itemDescriptionKey") ? ProcessFieldData("itemInsaneDescriptionKey", meta["itemDescriptionKey"]) : null;
                case "itemType":                     return ItemType.GENERAL;
                case "itemSubtype":                  return ItemSubtype.GENERAL;
                case "tooltipTextColor":             return Color.white;
                case "tooltipNotesColor":            return Color.white;
                case "itemTypeIcon":                 return null; // TODO
                case "harvestParticlePrefab":        return null; // TODO
                case "overrideHarvestParticleDepth": return null; // TODO
                case "harvestParticleDepthOffset":   return null; // TODO
                case "flattenParticleShape":         return null; // TODO
                case "availableInDemo":              return false;

                // SpatialItemData
                case "canBeSoldByPlayer":               return true;
                case "canBeSoldInBulkAction":           return true;
                case "value":                           return decimal.Zero;
                case "hasSellOverride":                 return false;
                case "sellOverrideValue":               return decimal.Zero;
                case "sprite":                          return null; // TODO: Placeholder Sprite?
                case "platformSpecificSpriteOverrides": return null;
                case "itemColor":                       return new Color(65 / 255f, 65 / 255f, 65 / 255f, 1f);
                case "canBeDiscardedByPlayer":          return true;
                case "canBeDiscardedDuringQuestPickup": return true;
                case "damageMode":                      return DamageMode.NONE;
                case "moveMode":                        return MoveMode.FREE;
                case "ignoreDamageWhenPlacing":         return false;
                case "isUnderlayItem":                  return false;
                case "forbidStorageTray":               return false;
                case "dimensions":                      return new List<Vector2Int>() { new Vector2Int(0, 0) };
                case "squishFactor":                    return 1f;
                case "itemOwnPrerequisites":            return null;
                case "researchPrerequisites":           return null;
                case "researchPointsRequired":          return 0;
                case "buyableWithoutResearch":          return true;

                // If nothing above matches return null for no change
                default:
                    return null;
            }
        }

        private static object ProcessFieldData(string fieldName, object value)
        {
            switch(fieldName)
            {
                // ItemData
                case "itemNameKey":
                case "itemDescriptionKey":
                case "itemInsaneTitleKey":
                case "itemInsaneDescriptionKey": return new LocalizedString("Items", value.ToString());
                case "itemType":                 return GetEnumValue<ItemType>(value);
                case "itemSubtype":              return GetEnumValue<ItemSubtype>(value);
                case "tooltipTextColor":
                case "tooltipNotesColor":        return GetColorFromObject(JsonConvert.DeserializeObject<Dictionary<string, int>>(value.ToString()));
                case "itemTypeIcon":             WinchCore.Log.Warn($"Item Metadata defined unsupported field {fieldName} and was ignored."); return null; // TODO
                case "damageMode":               return GetEnumValue<DamageMode>(value);
                case "moveMode":                 return GetEnumValue<MoveMode>(value);
                case "harvestParticlePrefab":    WinchCore.Log.Warn($"Item Metadata defined unsupported field {fieldName} and was ignored."); return null; // TODO

                // SpatialItemData
                case "value":
                case "sellOverrideValue":               return decimal.Parse(value.ToString());
                case "sprite":                          return TextureUtil.GetSprite(value.ToString());
                case "platformSpecificSpriteOverrides": return null; // TODO
                case "itemColor":                       return GetColorFromObject(JsonConvert.DeserializeObject<Dictionary<string, int>>(value.ToString()));
                case "dimensions":                      return ParseDimensions((JArray)value);
                case "itemOwnPrerequisites":            WinchCore.Log.Warn($"Item Metadata defined unsupported field {fieldName} and was ignored."); return null; // TODO
                case "researchPrerequisites":           WinchCore.Log.Warn($"Item Metadata defined unsupported field {fieldName} and was ignored."); return null; // TODO

                // If nothing above matches processing not required - return unprocessed value
                default: return value;
            }
        }



        private static List<Vector2Int> ParseDimensions(JArray dimensions)
        {
            List<Vector2Int> parsed = new List<Vector2Int>();
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
            return parsed;
        }
        
        private static T GetEnumValue<T>(object value) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value.ToString());
        }

        private static Color GetColorFromObject(Dictionary<string, int> color)
        {
            int r = 0, g = 0, b = 0, a = 255;
            if (color.ContainsKey("r")) r = color["r"];
            if (color.ContainsKey("g")) g = color["g"];
            if (color.ContainsKey("b")) b = color["b"];
            if (color.ContainsKey("a")) a = color["a"];
            return new Color(r/255f, g/255f, b/255f, a/255f);
        }
    }
}
