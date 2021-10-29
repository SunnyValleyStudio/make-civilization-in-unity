using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    Dictionary<Vector3Int, GameObject> buildings = new Dictionary<Vector3Int, GameObject>();
    [SerializeField]
    private Tilemap tilemap;

    private Vector3Int GetCellPositionFor(Vector3 worldPosition)
    {
        return Vector3Int.CeilToInt(tilemap.CellToWorld(tilemap.WorldToCell(worldPosition)));
    }

    public void AddStructure(Vector3 worldPosition, GameObject structure)
    {
        Vector3Int position = GetCellPositionFor(worldPosition);

        if (buildings.ContainsKey(position))
        {
            Debug.LogError($"There is a structure already at this position {worldPosition}");
            return;
        }

        buildings[position] = structure;
    }

    public bool IsPositionInvalid(Vector3 worldPosition)
    {
        return buildings.ContainsKey(GetCellPositionFor(worldPosition));
    }
}
