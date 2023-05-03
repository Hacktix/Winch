using System.Collections.Generic;

namespace Winch.Serialization.Item;

public class EngineItemDataConverter : SpatialItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "itemType", new(ItemType.EQUIPMENT, null) },
        { "itemSubtype", new(ItemSubtype.ENGINE, null) },
        { "speedBonus", new(0f, o => float.Parse(o.ToString())) }
    };

    public EngineItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}