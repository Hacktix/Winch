using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;
// ReSharper disable HeapView.BoxingAllocation

namespace Winch.Serialization.POI.Harvest;

public class CustomHarvestPoiConverter : DredgeTypeConverter<CustomHarvestPoi>
{
    private readonly Dictionary<string, FieldDefinition> _definitions = new()
    {
        { "id", new( null, null) },
        { "location", new( new Vector3(0,0,0), o=> DredgeTypeHelpers.ParseVector3(o)) },
        { "harvestableParticlePrefab", new( null, null) },
        { "items", new( null, o=>JarrayToList((JArray)o)) },
        { "nightItems", new( null, o=>JarrayToList((JArray)o)) },
        { "startStock", new( 3, o=> int.Parse(o.ToString())) },
        { "maxStock", new( 5 , o=> int.Parse(o.ToString())) },
        { "doesRestock", new( true, null) },
        { "useTimeSpecificStock", new( false, null) },
        { "isCurrentlySpecial", new( false, null) },
    };
    
    public CustomHarvestPoiConverter()
    {
        AddDefinitions(_definitions);
    }
    
    public static List<string> JarrayToList(JArray jArray)
    {
        var list = new List<string>();
        foreach (var item in jArray)
        {
            list.Add(item.ToString());
        }
        return list;
    }
}
