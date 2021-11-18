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

    public readonly static List<Vector2Int> neighbours4Directions 
        = new List<Vector2Int>
    {
        new Vector2Int(0,1),
        new Vector2Int(1,0),
        new Vector2Int(-1,0),
        new Vector2Int(0,-1)
    };

    public bool CheckIfPositionIsValid(Vector2Int tileWorldPosition)
    {
        return grid.ContainsKey(tileWorldPosition) 
            && grid[tileWorldPosition].wakable;
    }

    public int GetMovementCost(Vector2Int tileWorldPosition)
    {
        return grid[tileWorldPosition].movementCost;
    }

    public List<Vector2Int> GetNeighboursFor(Vector2Int tileWorldPosition)
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        foreach (Vector2Int direction in neighbours4Directions)
        {
            Vector2Int tempPosition = tileWorldPosition + direction;
            if (grid.ContainsKey(tempPosition))
                positions.Add(tempPosition);
        }
        return positions;
    }
}
