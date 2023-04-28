using System;
using System.Collections.Generic;
using UnityEngine;

namespace Winch.Serialization.POI;

public class DockPoiConverter : PoiConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "dock", new( null, null) },
        { "dockSlots", new( Array.Empty<Transform>(), null) },
    };
    
    public DockPoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
