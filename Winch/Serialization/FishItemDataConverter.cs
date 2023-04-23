using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization;

public class FishItemDataConverter : HarvestableItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "minSizeCentimeters", new( 0f, o => float.Parse(o.ToString())) },
        { "maxSizeCentimeters", new( 0f, o => float.Parse(o.ToString())) },
        { "aberrations", new( null, null) },
        { "isAberration", new( false, null) },
        { "nonAberrationParent", new( null, null) },
        { "minWorldPhaseRequired", new( 0, o => int.Parse(o.ToString())) },
        { "locationHiddenUntilCaught", new( false, null) },
        { "day", new( true, null) },
        { "night", new( true, null) },
        { "canAppearInBaitBalls", new( true, null) },
        { "canBeInfected", new( false, null) },
        { "cellsExcludedFromDisplayingInfection", new( new List<Vector2Int>(){new(0,0)}, o => DredgeTypeHelpers.ParseDimensions((JArray)o)) },
    };
    
    public FishItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}
