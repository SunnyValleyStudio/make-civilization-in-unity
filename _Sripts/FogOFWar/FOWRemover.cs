using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOWRemover : MonoBehaviour
{
    public static FogOfWar fow;
    public int range = 4;

    private Unit unit;

    void Start()
    {
        unit = GetComponent<Unit>();
        if (fow == null)
            fow = FindObjectOfType<FogOfWar>();
        ClearPositions();
        unit.OnMove += ClearPositions;
    }

    public void ClearPositions()
    {
        List<Vector2> positionsToClear = CalculatePositionsAround();
        fow.ClearFOW(positionsToClear);
    }

    private List<Vector2> CalculatePositionsAround()
    {
        List<Vector2> positions = new List<Vector2>();
        Vector2 centerPosition = 
            new Vector2(transform.position.x, transform.position.y);

        for (int y = -range; y <= range; y++)
        {
            for (int x = -range; x <= range; x++)
            {
                Vector2 tempPosition = centerPosition + new Vector2(x, y);
                if (Vector2.Distance(centerPosition, tempPosition) <= range)
                {
                    positions.Add(tempPosition);
                }
            }
        }
        return positions;
    }
}
