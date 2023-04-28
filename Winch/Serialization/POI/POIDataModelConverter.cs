using System.Collections.Generic;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.POI;

public class PoiDataModelConverter : DredgeTypeConverter<POIDataModel>
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "id", new( null, null) },
        { "lastUpdate", new( 0f, o => float.Parse(o.ToString())) },
    };
    
    public PoiDataModelConverter()
    {
        AddDefinitions(_definitions);
    }
    
}
