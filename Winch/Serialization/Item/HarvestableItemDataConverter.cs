using System.Collections.Generic;

// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.Item;

public class HarvestableItemDataConverter : SpatialItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "harvestMinigameType", new( HarvestMinigameType.DREDGE_RADIAL, o => DredgeTypeHelpers.GetEnumValue<HarvestMinigameType>(o)) },
        { "perSpotMin", new( 1, o => int.Parse(o.ToString())) },
        { "perSpotMax", new(1, o => int.Parse(o.ToString())) },
        { "harvestItemWeight", new(1f, o => float.Parse(o.ToString())) },
        { "regenHarvestSpotOnDestroy", new(false, null) },
        { "harvestPOICategory", new(HarvestPOICategory.FISH_SMALL, o => DredgeTypeHelpers.GetEnumValue<HarvestPOICategory>(o)) },
        { "harvestableType", new(HarvestableType.SHALLOW, o => DredgeTypeHelpers.GetEnumValue<HarvestableType>(o)) },
        { "harvestDifficulty", new(HarvestDifficulty.EASY, o => DredgeTypeHelpers.GetEnumValue<HarvestDifficulty>(o)) },
        { "canBeReplacedWithResearchItem", new(false, null) },
        { "canBeCaughtByRod", new(true, null) },
        { "canBeCaughtByPot", new(false, null) },
        { "canBeCaughtByNet", new(false, null) },
        { "affectedByFishingSustain", new(false, null) },
        { "hasMinDepth", new(false, null) },
        { "minDepth", new(DepthEnum.SHALLOW, o => DredgeTypeHelpers.GetEnumValue<DepthEnum>(o)) },
        { "hasMaxDepth", new(false, null) },
        { "maxDepth", new(DepthEnum.VERY_DEEP, o => DredgeTypeHelpers.GetEnumValue<DepthEnum>(o)) },
        { "zonesFoundIn", new(ZoneEnum.ALL, o => DredgeTypeHelpers.GetEnumValue<ZoneEnum>(o)) },
    };
    
    public HarvestableItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}
