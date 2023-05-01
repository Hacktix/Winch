using System.Collections.Generic;

namespace Winch.Serialization.Item;

public class HarvestableItemDataConverter : SpatialItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "harvestMinigameType", new( HarvestMinigameType.DREDGE_RADIAL, o => DredgeTypeHelpers.GetEnumValue<HarvestMinigameType>(o)) },
        { "perSpotMin", new( 1, o => int.Parse(o.ToString())) },
        { "perSpotMax", new(1, o => int.Parse(o.ToString())) },
        { "harvestItemWeight", new(1f, o => float.Parse(o.ToString())) },
        { "regenHarvestSpotOnDestroy", new(false, o => bool.Parse(o.ToString())) },
        { "harvestPOICategory", new(HarvestPOICategory.FISH_SMALL, o => DredgeTypeHelpers.GetEnumValue<HarvestPOICategory>(o)) },
        { "harvestableType", new(HarvestableType.SHALLOW, o => DredgeTypeHelpers.GetEnumValue<HarvestableType>(o)) },
        { "harvestDifficulty", new(HarvestDifficulty.EASY, o => DredgeTypeHelpers.GetEnumValue<HarvestDifficulty>(o)) },
        { "canBeReplacedWithResearchItem", new(false, o => bool.Parse(o.ToString())) },
        { "canBeCaughtByRod", new(true, o => bool.Parse(o.ToString())) },
        { "canBeCaughtByPot", new(false, o => bool.Parse(o.ToString())) },
        { "canBeCaughtByNet", new(false, o => bool.Parse(o.ToString())) },
        { "affectedByFishingSustain", new(true, o => bool.Parse(o.ToString())) },
        { "hasMinDepth", new(false, o => bool.Parse(o.ToString())) },
        { "minDepth", new(DepthEnum.SHALLOW, o => DredgeTypeHelpers.GetEnumValue<DepthEnum>(o)) },
        { "hasMaxDepth", new(false, o => bool.Parse(o.ToString())) },
        { "maxDepth", new(DepthEnum.VERY_DEEP, o => DredgeTypeHelpers.GetEnumValue<DepthEnum>(o)) },
        { "zonesFoundIn", new(ZoneEnum.ALL, o => DredgeTypeHelpers.GetEnumValue<ZoneEnum>(o)) },
    };
    
    public HarvestableItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}
