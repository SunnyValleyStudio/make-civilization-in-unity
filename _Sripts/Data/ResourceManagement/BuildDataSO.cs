using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Build Data", 
    menuName = "EconomyData/BuildCostData")]
public class BuildDataSO : ScriptableObject
{
    public GameObject prefab;
    public List<ResourceValue> buildCost;
    public List<ResourceValue> producedResources;
}
