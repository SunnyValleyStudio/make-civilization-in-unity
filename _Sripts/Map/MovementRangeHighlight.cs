using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementRangeHighlight : MonoBehaviour
{
    [SerializeField]
    private Tilemap highlightTilemap;
    [SerializeField]
    private TileBase highlightTile;

    public void ClearHighlight()
    {
        highlightTilemap.ClearAllTiles();
    }

    public void HighlightTiles(IEnumerable<Vector2Int> cellPositions)
    {
        ClearHighlight();
        foreach (Vector2Int tilePosition in cellPositions)
        {
            highlightTilemap.SetTile((Vector3Int)tilePosition, highlightTile);
        }
    }
}
