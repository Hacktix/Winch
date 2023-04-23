using System.Collections.Generic;
using UnityEngine;
// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization;

public class ItemDataConverter : DredgeTypeConverter<ItemData>
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "itemNameKey", new( "", null) },
        { "itemDescriptionKey", new( "", null) },
        { "itemInsaneTitleKey", new("", null) },
        { "itemInsaneDescriptionKey", new("", null) },
        { "itemType", new(ItemType.GENERAL, o => GetEnumValue<ItemType>(o)) },
        { "itemSubtype", new(ItemSubtype.GENERAL, o => GetEnumValue<ItemSubtype>(o)) },
        { "tooltipTextColor", new(Color.white, o => GetColorFromJsonObject(o)) },
        { "tooltipNotesColor", new(Color.white, o => GetColorFromJsonObject(o)) },
        { "itemTypeIcon", new(null, null) },
        { "harvestParticlePrefab", new(null, null) },
        { "overrideHarvestParticleDepth", new(null, null) },
    };
    
    public ItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}