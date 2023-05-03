using System.Collections.Generic;

namespace Winch.Serialization.Item;

public class ResearchableItemDataConverter : NonSpatialItemDataConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "completedDescriptionKey", new( null, o=> CreateLocalizedString(ItemTableDefinition, o.ToString())) },
        { "daysToResearch", new( 1f, o => float.Parse(o.ToString())) },
        { "researchBenefitType", new(ResearchBenefitType.MOVEMENT_SPEED, o=> DredgeTypeHelpers.GetEnumValue<ResearchBenefitType>(o)) },
        { "researchBenefitValue", new(0.05f, o => float.Parse(o.ToString())) }
    };

    public ResearchableItemDataConverter()
    {
        AddDefinitions(_definitions);
    }
}