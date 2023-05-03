using System.Collections.Generic;

namespace Winch.Serialization.Item;

public class MessageItemDataConverter : NonSpatialItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "chronologicalOrder", new(0, o=> int.Parse(o.ToString())) },
        { "messageBodyKey", new( null, o=> CreateLocalizedString(ItemTableDefinition, o.ToString())) }
    };

    public MessageItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}