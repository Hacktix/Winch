using System.Collections.Generic;
// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.POI.Harvest;

public class HarvestPoiConverter : PoiConverter
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "cachedDaytimeCheckTime", new( 0f, o=> float.Parse(o.ToString())) },
        { "cachedWasDaytime", new( false, null) },
        { "harvestable", new( null, null) }, // Redirects from harvestPOIData
        { "harvestParticlePrefab", new( null, null) },
        { "harvestParticles", new( null, null) },
        { "harvestPOIData", new( null, null) },
        { "isCurrentlySpecial", new( false, null) },
        { "poiCollider", new( null, null) },
        { "prevRealTimeOfStockCheck", new( 0f, o => float.Parse(o.ToString())) },
        { "realTimeBetweenStockChecksSec", new( 0f, o => float.Parse(o.ToString())) },
        { "shouldUpdateSpecialParticles", new( false, null) },
    };
    
    public HarvestPoiConverter()
    {
        AddDefinitions(_definitions);
    }
}
