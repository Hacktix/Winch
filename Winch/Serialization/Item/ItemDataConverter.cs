using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using Winch.Util;

namespace Winch.Serialization.Item;

public class ItemDataConverter : DredgeTypeConverter<ItemData>
{
    internal const string ItemTableDefinition = "Items";

    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "id", new( Guid.NewGuid().ToString(), null) },
        { "itemNameKey", new( null, o=> CreateLocalizedString(ItemTableDefinition, o.ToString())) },
        { "itemDescriptionKey", new( null, o=> CreateLocalizedString(ItemTableDefinition, o.ToString())) },
        { "itemInsaneTitleKey", new(null, o=> CreateLocalizedString(ItemTableDefinition, o.ToString())) },
        { "itemInsaneDescriptionKey", new(null, o=> CreateLocalizedString(ItemTableDefinition, o.ToString())) },
        { "itemType", new(ItemType.GENERAL, o => DredgeTypeHelpers.GetEnumValue<ItemType>(o)) },
        { "itemSubtype", new(ItemSubtype.GENERAL, o => DredgeTypeHelpers.GetEnumValue<ItemSubtype>(o)) },
        { "tooltipTextColor", new(Color.white, o => DredgeTypeHelpers.GetColorFromJsonObject(o)) },
        { "tooltipNotesColor", new(Color.white, o => DredgeTypeHelpers.GetColorFromJsonObject(o)) },
        { "itemTypeIcon", new(null, o => TextureUtil.GetSprite(o.ToString())) },
        { "harvestParticlePrefab", new(null, null) },
        { "overrideHarvestParticleDepth", new(false, o=> bool.Parse(o.ToString())) },
        { "harvestParticleDepthOffset", new(-3f, o=> float.Parse(o.ToString())) },
        { "flattenParticleShape", new(false, o=> bool.Parse(o.ToString())) },
        { "availableInDemo", new(false, null) },
    };

    private readonly Dictionary<string, string> _reroutes = new()
    {
        { "itemInsaneTitleKey", "itemNameKey" }, { "itemInsaneDescriptionKey", "itemDescriptionKey" }
    };

    public ItemDataConverter()
    {
        AddDefinitions(_definitions);
        AddReroutes(_reroutes);
    }

    internal static LocalizedString CreateLocalizedString(string key, string value)
    {
        var keyValueTuple = (key, value);
        if (StringDefinitionCache.TryGetValue(keyValueTuple, out LocalizedString cached))
        {
            return cached;
        }
        var localizedString = new LocalizedString(key, value);
        StringDefinitionCache.Add(keyValueTuple, localizedString);
        return localizedString;
    }
}
