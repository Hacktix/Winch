using System.Collections.Generic;

namespace Winch.Serialization.Item;

public class LightItemDataConverter : SpatialItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "itemType", new(ItemType.EQUIPMENT, null) },
        { "itemSubtype", new(ItemSubtype.LIGHT, null) },
        { "lumens", new(500f, o => float.Parse(o.ToString())) },
        { "range", new(10f, o => float.Parse(o.ToString())) }
    };

    public LightItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}