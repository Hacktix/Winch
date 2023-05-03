using System.Collections.Generic;

namespace Winch.Serialization.Item;

public class DeployableItemDataConverter : HarvesterItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "catchRate", new(1f, o => float.Parse(o.ToString()))},
        { "gridConfig", new(null, null) },
        { "maxDurabilityDays", new(1f, o => float.Parse(o.ToString())) },
        { "timeBetweenCatchRolls", new(1f, o => float.Parse(o.ToString()))}
    };

    public DeployableItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}