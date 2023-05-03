using System.Collections.Generic;
using UnityEngine;

namespace Winch.Serialization.POI.Harvest;

public class CustomHarvestPoi
{
    public string id;
    public Vector3 location;
    public string harvestableParticlePrefab;
    public List<string> items;
    public List<string> nightItems;
    public int startStock = 3;
    public int maxStock = 5;
    public bool doesRestock = true;
    public bool useTimeSpecificStock = false;
    public bool isCurrentlySpecial = false;
}


