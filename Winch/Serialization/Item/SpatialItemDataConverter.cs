using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
using Winch.Util;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.Item;

public class SpatialItemDataConverter : ItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "canBeSoldByPlayer", new(true, o => bool.Parse(o.ToString())) },
        { "canBeSoldInBulkAction", new(true, o => bool.Parse(o.ToString())) },
        { "value", new(decimal.Zero, o => decimal.Parse(o.ToString())) },
        { "hasSellOverride", new(false, o => bool.Parse(o.ToString())) },
        { "sellOverrideValue", new(decimal.Zero, o => decimal.Parse(o.ToString())) },
        { "sprite", new(null, o => TextureUtil.GetSprite(o.ToString())) },
        { "platformSpecificSpriteOverrides", new(null, null) },
        { "itemColor", new(new Color(0.1922f, 0.1922f, 0.1922f, 255), o=> DredgeTypeHelpers.GetColorFromJsonObject(o)) }, // default game uses
        { "canBeDiscardedByPlayer", new(true, o => bool.Parse(o.ToString())) },
        { "canBeDiscardedDuringQuestPickup", new(true, o => bool.Parse(o.ToString())) },
        { "damageMode", new(DamageMode.NONE, o=> DredgeTypeHelpers.GetEnumValue<DamageMode>(o)) },
        { "moveMode", new(MoveMode.FREE, o=> DredgeTypeHelpers.GetEnumValue<MoveMode>(o)) },
        { "ignoreDamageWhenPlacing", new(false, o => bool.Parse(o.ToString())) },
        { "isUnderlayItem", new(false, null) },
        { "forbidStorageTray", new(false, null) },
        { "dimensions", new(new List<Vector2Int>(){ new Vector2Int(0,0) }, o => DredgeTypeHelpers.ParseDimensions((JArray)o)) },
        { "squishFactor", new(1f, o => float.Parse(o.ToString())) },
        { "itemOwnPrerequisites", new(null, null) },
        { "researchPrerequisites", new(null, null) },
        { "researchPointsRequired", new(0, o => int.Parse(o.ToString())) },
        { "buyableWithoutResearch", new(true, o => bool.Parse(o.ToString())) }
    };

    public SpatialItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}
