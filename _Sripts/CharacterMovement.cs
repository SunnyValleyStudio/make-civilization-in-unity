using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private Map map;

    private Unit selectedUnit;

    private List<Vector2Int> movementRange;

    [SerializeField]
    private MovementRangeHighlight rangeHighlight;

    public void HandleSelection(GameObject detectedObject)
    {
        if (detectedObject == null)
        {
            ResetCharacterMovement();
            return;
        }
        if (detectedObject.CompareTag("Player"))
            this.selectedUnit = detectedObject.GetComponent<Unit>();
        else
            this.selectedUnit = null;

        if (this.selectedUnit == null)
            return;

        if (this.selectedUnit.CanStillMove())
            PrepareMovementRange();
        else
            rangeHighlight.ClearHighlight();

        //foreach (Vector2Int position in movementRange)
        //{
        //    Debug.Log(position);
        //}
    }

    private void PrepareMovementRange()
    {
        movementRange = GetMovementRangeFor(this.selectedUnit).Keys.ToList();
        rangeHighlight.HighlightTiles(movementRange);
    }

    public Dictionary<Vector2Int, Vector2Int?> GetMovementRangeFor(Unit selectedUnit)
    {
        return map.GetMovementRange(
                            selectedUnit.transform.position,
                            selectedUnit.CurrentMovementPoints
                            );
    }

    public void ResetCharacterMovement()
    {
        if(rangeHighlight != null)
            rangeHighlight.ClearHighlight();
        this.selectedUnit = null;
    }

    public void HandleMovement(Vector3 endPosition)
    {
        if (this.selectedUnit == null)
            return;

        if (this.selectedUnit.CanStillMove() == false)
            return;

        Vector2 direction = CalculateMovementDirection(endPosition);

        Vector2Int unitTilePosition = 
            Vector2Int.FloorToInt(
                (Vector2)this.selectedUnit.transform.position + direction);

        if (movementRange.Contains(unitTilePosition))
        {
            int cost = map.GetMovementCost(unitTilePosition);
            this.selectedUnit.HandleMovement(direction, cost);
            if (this.selectedUnit.CanStillMove())
            {
                PrepareMovementRange();
            }
            else
            {
                rangeHighlight.ClearHighlight();
            }
        }
        else
        {
            Debug.Log($"Cant move in the direction {direction}");
        }

        

    }

    private Vector2 CalculateMovementDirection(Vector3 endPosition)
    {
        Vector2 direction = (endPosition - this.selectedUnit.transform.position);

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            float sign = Mathf.Sign(direction.x);
            direction = Vector2.right * sign;
        }
        else
        {
            float sign = Mathf.Sign(direction.y);
            direction = Vector2.up * sign;
        }

        return direction;
    }
}
