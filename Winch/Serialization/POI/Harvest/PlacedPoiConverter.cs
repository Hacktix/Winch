using System.Collections.Generic;
// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.POI.Harvest;

public class PlacedPoiConverter : HarvestPoiConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "animator", new( null, null) },
        { "brokenObj", new( null, null) },
        { "didJustBreak", new( false, null) },
        { "idleObj", new( null, null) },
        { "isDeployed", new( false, null) },
        { "readyObj", new( null, null) },
    };
    
    public PlacedPoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
