using System.Collections.Generic;

namespace Winch.Serialization.Item;

public class DredgeItemDataConverter : HarvesterItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "itemSubtype", new(ItemSubtype.DREDGE, null) },
        { "harvestableTypes", new(HarvestableType.DREDGE, null) },
    };

    public DredgeItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}