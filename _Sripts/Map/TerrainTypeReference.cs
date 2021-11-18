using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTypeReference : MonoBehaviour
{
    [SerializeField]
    private TerrainSO terrainData;

    public TerrainSO GetTerrainData() => terrainData;
}
