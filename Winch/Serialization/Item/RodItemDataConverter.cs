using System.Collections.Generic;

namespace Winch.Serialization.Item;
public class RodItemDataConverter : HarvesterItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "itemSubtype", new(ItemSubtype.ROD, null) },
        { "fishingSpeedModifier", new(1f, o => float.Parse(o.ToString())) },
        { "aberrationCatchBonus", new(0f, o => float.Parse(o.ToString())) }
    };

    public RodItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}
