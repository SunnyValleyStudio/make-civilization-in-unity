using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{
    [SerializeField]
    private Tilemap seaTilemap, fogOfWarTilemap;

    [SerializeField]
    private TileBase fowTile;

    void Awake()
    {
        fogOfWarTilemap.size = seaTilemap.size;

        fogOfWarTilemap.
            BoxFill(
            seaTilemap.cellBounds.min, 
            fowTile, 
            seaTilemap.cellBounds.min.x, 
            seaTilemap.cellBounds.min.y, 
            seaTilemap.cellBounds.max.x, 
            seaTilemap.cellBounds.max.y);

    }

    public void ClearFOW(List<Vector2> positionsToClear)
    {
        foreach (Vector2 position in positionsToClear)
        {
            fogOfWarTilemap.SetTile(fogOfWarTilemap.WorldToCell(position), null);
        }
    }
}
