using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{ 
    Dictionary<Vector3Int, GameObject> buildings 
        = new Dictionary<Vector3Int, GameObject>();

    [SerializeField]
    private Tilemap islandColldiersTilemap, seaTilemap, 
        forestTilemap, mountainsTilemap;

    private List<Vector2Int> islandTiles, forestTiles, 
        mountainTiles, emptyTiles;

    [SerializeField]
    private bool showEmpty, showMountains, showForest;

    public Dictionary<Vector2Int, Vector2Int?> GetMovementRange(Vector3 worldPosition, int currentMovementPoints)
    {
        Vector3Int cellWorldPosition = GetCellWorldPositionFor(worldPosition);
        return GraphSearch.BFS(mapGrid, (Vector2Int)cellWorldPosition, currentMovementPoints);
    }

    private MapGrid mapGrid;

    private void Awake()
    {
        forestTiles = GetTilemapWorldPositionsFrom(forestTilemap);
        mountainTiles = GetTilemapWorldPositionsFrom(mountainsTilemap);
        islandTiles = GetTilemapWorldPositionsFrom(islandColldiersTilemap);
        emptyTiles =
            GetEmptyTiles(islandTiles, forestTiles.Concat(mountainTiles).ToList());
        
        PrepareMapGrid();
    }

    private void PrepareMapGrid()
    {
        mapGrid = new MapGrid();

        mapGrid.AddToGrid(
            forestTilemap.GetComponent<TerrainTypeReference>().GetTerrainData(),
            forestTiles);
        mapGrid.AddToGrid(
            mountainsTilemap.GetComponent<TerrainTypeReference>().GetTerrainData(),
            mountainTiles);
        mapGrid.AddToGrid(
            islandColldiersTilemap.GetComponent<TerrainTypeReference>().GetTerrainData(),
            emptyTiles);
    }

    public int GetMovementCost(Vector2Int cellWorldPosition)
    {
        return mapGrid.GetMovementCost(cellWorldPosition);
    }

    //public bool CanIMoveTo(Vector2 unitPosition, Vector2 direction)
    //{
    //    Vector2Int unitTilePosition = Vector2Int.FloorToInt(unitPosition + direction);

    //    List<Vector2Int> neighbours 
    //        = mapGrid.GetNeighboursFor(Vector2Int.FloorToInt(unitPosition));

    //    foreach (Vector2Int cellPosition in neighbours)
    //    {
    //        Debug.Log(cellPosition);
    //    }

    //    return neighbours.Contains(unitTilePosition) 
    //        && mapGrid.CheckIfPositionIsValid(unitTilePosition);
    //}

    private List<Vector2Int> GetEmptyTiles(List<Vector2Int> islandTiles, List<Vector2Int> nonEmptyTiles)
    {
        HashSet<Vector2Int> emptyTilesHashset 
            = new HashSet<Vector2Int>(islandTiles);
        emptyTilesHashset.ExceptWith(nonEmptyTiles);
        return new List<Vector2Int>(emptyTilesHashset);
    }


    private List<Vector2Int> GetTilemapWorldPositionsFrom(Tilemap tilemap)
    {
        List<Vector2Int> tempList = new List<Vector2Int>();
        foreach (Vector2Int cellPosition in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.HasTile((Vector3Int)cellPosition) == false)
                continue;

            Vector3Int worldPosition = GetWorldPositionFor(cellPosition);
            tempList.Add((Vector2Int)worldPosition);
        }
        return tempList;
    }

    private Vector3Int GetCellWorldPositionFor(Vector3 worldPosition)
    {
        return Vector3Int.CeilToInt(islandColldiersTilemap
            .CellToWorld(islandColldiersTilemap.WorldToCell(worldPosition)));
    }

    private Vector3Int GetWorldPositionFor(Vector2Int cellPosition)
    {
        return Vector3Int.CeilToInt(islandColldiersTilemap
            .CellToWorld((Vector3Int)cellPosition));
    }

    public void AddStructure(Vector3 worldPosition, GameObject structure)
    {
        Vector3Int position = GetCellWorldPositionFor(worldPosition);

        if (buildings.ContainsKey(position))
        {
            Debug.LogError($"There is a structure already at this position {worldPosition}");
            return;
        }

        buildings[position] = structure;
    }

    public bool IsPositionInvalid(Vector3 worldPosition)
    {
        return buildings.ContainsKey(GetCellWorldPositionFor(worldPosition));
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;
        DrawGizomOf(emptyTiles, Color.white, showEmpty);
        DrawGizomOf(forestTiles, Color.yellow, showForest);
        DrawGizomOf(mountainTiles, Color.red, showMountains);
    }

    private void DrawGizomOf(List<Vector2Int> tiles, Color color, bool isShowing)
    {
        if (isShowing)
        {
            Gizmos.color = color;
            foreach (Vector2Int pos in tiles)
            {
                Gizmos.DrawSphere(new Vector3(pos.x+0.5f, pos.y+0.5f, 0), 0.3f);
            }
        }
    }
}
