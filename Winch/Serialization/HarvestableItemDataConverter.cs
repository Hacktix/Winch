using System.Collections.Generic;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization;

public class HarvestableItemDataConverter : SpatialItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "harvestMinigameType", new( HarvestMinigameType.DREDGE_RADIAL, o => GetEnumValue<HarvestMinigameType>(o)) },
        { "perSpotMin", new( 1, o => int.Parse(o.ToString())) },
        { "perSpotMax", new(1, o => int.Parse(o.ToString())) },
        { "harvestItemWeight", new(1f, o => float.Parse(o.ToString())) },
        { "regenHarvestSpotOnDestroy", new(false, null) },
        { "harvestPOICategory", new(HarvestPOICategory.FISH_SMALL, o => GetEnumValue<HarvestPOICategory>(o)) },
        { "harvestableType", new(HarvestableType.SHALLOW, o => GetEnumValue<HarvestableType>(o)) },
        { "harvestDifficulty", new(HarvestDifficulty.EASY, o => GetEnumValue<HarvestDifficulty>(o)) },
        { "canBeReplacedWithResearchItem", new(false, null) },
        { "canBeCaughtByRod", new(true, null) },
        { "canBeCaughtByPot", new(false, null) },
        { "canBeCaughtByNet", new(false, null) },
        { "affectedByFishingSustain", new(false, null) },
        { "hasMinDepth", new(false, null) },
        { "minDepth", new(DepthEnum.SHALLOW, o => GetEnumValue<DepthEnum>(o)) },
        { "hasMaxDepth", new(false, null) },
        { "maxDepth", new(DepthEnum.VERY_DEEP, o => GetEnumValue<DepthEnum>(o)) },
        { "zonesFoundIn", new(ZoneEnum.ALL, o => GetEnumValue<ZoneEnum>(o)) },
    };
    
    public HarvestableItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}