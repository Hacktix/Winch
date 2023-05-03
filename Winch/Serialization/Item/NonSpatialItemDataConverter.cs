using System.Collections.Generic;

namespace Winch.Serialization.Item;

public class NonSpatialItemDataConverter : ItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "showInCabin", new(true, o => bool.Parse(o.ToString()))}
    };

    public NonSpatialItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}
