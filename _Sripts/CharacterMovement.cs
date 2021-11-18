using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    [SerializeField]
    private Map map;

    private Unit selectedUnit;

    public void HandleSelection(GameObject detectedObject)
    {
        if (detectedObject == null)
        {
            this.selectedUnit = null;
            return;
        }
        this.selectedUnit = detectedObject.GetComponent<Unit>();
    }

    public void HandleMovement(Vector3 endPosition)
    {
        if (this.selectedUnit == null)
            return;

        if (this.selectedUnit.CanStillMove() == false)
            return;

        Vector2 direction = CalculateMovementDirection(endPosition);

        if(map.CanIMoveTo(this.selectedUnit.transform.position, direction))
        {
            this.selectedUnit.HandleMovement(direction, 10);
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
