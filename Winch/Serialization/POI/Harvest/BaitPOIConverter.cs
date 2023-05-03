using System.Collections.Generic;

namespace Winch.Serialization.POI.Harvest;

public class BaitPoiConverter : HarvestPoiConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "itemStock", new( new Stack<FishItemData>(), null) },
    };
    
    public BaitPoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
