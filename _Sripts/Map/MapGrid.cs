using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid
{
    Dictionary<Vector2Int, TerrainSO> grid
        = new Dictionary<Vector2Int, TerrainSO>();

    public void AddToGrid(TerrainSO terrainType, List<Vector2Int> collection)
    {
        foreach (Vector2Int cell in collection)
        {
            grid[cell] = terrainType;
        }
    }
}
