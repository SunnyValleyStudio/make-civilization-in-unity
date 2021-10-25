using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMovement : MonoBehaviour
{
    public float threshold = 0.5f;

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

        if (Vector2.Distance(endPosition, this.selectedUnit.transform.position) > threshold)
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
            this.selectedUnit.HandleMovement(direction, 10);
        }
    }

    
}
