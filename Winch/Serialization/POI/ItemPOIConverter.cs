using System.Collections.Generic;

namespace Winch.Serialization.POI;

public class ItemPoiConverter : PoiConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "harvestable", new( null, null) }, // Redirects from itemPOIData
        { "harvestParticles", new( null, null) },
        { "itemPOIData", new( null, null) },
        { "intermittentSFXPlayer", new( null, null) },
        { "poiCollider", new( null, null) },
    };
    
    public ItemPoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
