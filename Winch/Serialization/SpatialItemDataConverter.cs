using System.Collections.Generic;
using Newtonsoft.Json;
using Winch.Util;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization;

public class SpatialItemDataConverter : DredgeTypeConverter<SpatialItemData>
{
    private SpatialItemDataConverter instance;
    public SpatialItemDataConverter Instance
    {
        get { return instance ??= new(); }
    }

    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "canBeSoldByPlayer", new(true, null)},
        { "canBeSoldInBulkAction", new(true, null)},
        { "value", new(decimal.Zero, o => decimal.Parse(o.ToString())) },
        { "hasSellOverride", new(false, null)},
        { "sellOverrideValue", new(0, o => decimal.Parse(o.ToString()))},
        { "sprite", new("", o => TextureUtil.GetSprite(o.ToString())) },
        { "platformSpecificSpriteOverrides", new("", null) },
        { "itemColor", new("", o=> GetColorFromJsonObject(o))},
        { "canBeDiscardedByPlayer", new(true, null)},
        { "canBeDiscardedDuringQuestPickup", new(true, null)},
        { "damageMode", new(DamageMode.DESTROY, o=> GetEnumValue<DamageMode>(o))},
        { "moveMode", new(MoveMode.FREE, o=> GetEnumValue<MoveMode>(o))},
        { "ignoreDamageWhenPlacing", new(false, null)},
        { "isUnderlayItem", new(false, null)},
        { "forbidStorageTray", new(false, null)},
        { "dimensions", new("", null) },
        { "squishFactor", new(1f, o => float.Parse(o.ToString())) },
        { "itemOwnPrerequisites", new(ItemType.GENERAL, o => GetEnumValue<ItemType>(o)) },
        { "researchPrerequisites", new(ItemType.GENERAL, o => GetEnumValue<ItemType>(o)) },
        { "researchPointsRequired", new(0, o => int.Parse(o.ToString())) },
        { "buyableWithoutResearch", new(true, null) },
    };

    public SpatialItemDataConverter()
    {
        AddDefinitions(_definitions);
    }

}