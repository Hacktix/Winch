using System.Collections.Generic;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.POI;

public class PoiConverter : DredgeTypeConverter<global::POI>
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "canBeGhostWindTarget", new( false, null) },
        { "ghostWindTargetTransform", new( null, null) },
        { "interactPointTargetTransform", new( null, null) },
    };

    public PoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
