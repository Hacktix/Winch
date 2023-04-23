using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Winch.Util;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization;

public class SpatialItemDataConverter : ItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "canBeSoldByPlayer", new(true, null)},
        { "canBeSoldInBulkAction", new(true, null)},
        { "value", new(decimal.Zero, o => decimal.Parse(o.ToString())) },
        { "hasSellOverride", new(false, null)},
        { "sellOverrideValue", new(decimal.Zero, o => decimal.Parse(o.ToString()))},
        { "sprite", new(null, o => TextureUtil.GetSprite(o.ToString())) },
        { "platformSpecificSpriteOverrides", new(null, null) },
        { "itemColor", new(new Color(65f, 65f, 65f, 255f), o=> DredgeTypeHelpers.GetColorFromJsonObject(o))},
        { "canBeDiscardedByPlayer", new(true, null)},
        { "canBeDiscardedDuringQuestPickup", new(true, null)},
        { "damageMode", new(DamageMode.NONE, o=> DredgeTypeHelpers.GetEnumValue<DamageMode>(o))},
        { "moveMode", new(MoveMode.FREE, o=> DredgeTypeHelpers.GetEnumValue<MoveMode>(o))},
        { "ignoreDamageWhenPlacing", new(false, null)},
        { "isUnderlayItem", new(false, null)},
        { "forbidStorageTray", new(false, null)},
        { "dimensions", new(new List<Vector2Int>(){new Vector2Int(1,1)}, o => DredgeTypeHelpers.ParseDimensions((JArray)o)) },
        { "squishFactor", new(1f, o => float.Parse(o.ToString())) },
        { "itemOwnPrerequisites", new(null, null)},
        { "researchPrerequisites", new(null, null) },
        { "researchPointsRequired", new(0, o => int.Parse(o.ToString())) },
        { "buyableWithoutResearch", new(true, null) },
    };

    public SpatialItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}
