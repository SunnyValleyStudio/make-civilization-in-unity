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

    public void HandleSelection(GameObject detectedObject)
    {
        if (detectedObject == null)
        {
            this.selectedUnit = null;
            return;
        }
        this.selectedUnit = detectedObject.GetComponent<Unit>();
        movementRange = map.GetMovementRange(
            this.selectedUnit.transform.position,
            this.selectedUnit.CurrentMovementPoints
            ).Keys.ToList();

        //foreach (Vector2Int position in movementRange)
        //{
        //    Debug.Log(position);
        //}
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
