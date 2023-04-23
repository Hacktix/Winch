using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using Winch.Util;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization;

public class ItemDataConverter : DredgeTypeConverter<ItemData>
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "id", new( Guid.NewGuid().ToString(), null) },
        { "itemNameKey", new( null, o=> CreateLocalizedString("Items", o.ToString())) },
        { "itemDescriptionKey", new( null, o=> CreateLocalizedString("Items", o.ToString())) },
        { "itemInsaneTitleKey", new(null, o=> CreateLocalizedString("Items", o.ToString())) },
        { "itemInsaneDescriptionKey", new(null, o=> CreateLocalizedString("Items", o.ToString())) },
        { "itemType", new(ItemType.GENERAL, o => GetEnumValue<ItemType>(o)) },
        { "itemSubtype", new(ItemSubtype.GENERAL, o => GetEnumValue<ItemSubtype>(o)) },
        { "tooltipTextColor", new(Color.white, o => GetColorFromJsonObject(o)) },
        { "tooltipNotesColor", new(Color.white, o => GetColorFromJsonObject(o)) },
        { "itemTypeIcon", new(null, o => TextureUtil.GetSprite(o.ToString())) },
        { "harvestParticlePrefab", new(null, null) },
        { "overrideHarvestParticleDepth", new(false, null) },
        { "harvestParticleDepthOffset", new(-3f, o=> float.Parse(o.ToString())) },
        { "flattenParticleShape", new(false, null) },
        { "availableInDemo", new(false, null) },
    };
    
    public ItemDataConverter()
    {
        AddDefinitions(_definitions);
    }

    private static LocalizedString CreateLocalizedString(string key, string value)
    {
        return new (key, value);
    }
}
