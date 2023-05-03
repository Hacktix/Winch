using System.Collections.Generic;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.POI.Harvest;

public class HarvestPoiDataModelConverter : PoiDataModelConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "doesRestock", new( true, null) },
        { "items", new( new List<FishItemData>(), null) },
        { "maxStock", new( 0f, o => float.Parse(o.ToString())) },
        { "nightItems", new( new List<FishItemData>(), null) },
        { "overriddenDaytimeSpecialChance", new( 0f, o => float.Parse(o.ToString())) },
        { "overriddenNighttimeSpecialChance", new( 0f, o => float.Parse(o.ToString())) },
        { "overrideDefaultDaySpecialChance", new( false, null) },
        { "overrideDefaultNightSpecialChance", new( false, null) },
        { "startStock", new( 0f, o => float.Parse(o.ToString())) },
        { "usesTimeSpecificStock", new( false, null) },
    };
    
    public HarvestPoiDataModelConverter()
    {
        AddDefinitions(_definitions);
    }
}
