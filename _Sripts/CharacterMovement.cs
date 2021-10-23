using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float threshold = 0.5f;

    private GameObject selectedObject;

    private FlashFeedback flashFeedback;

    public void HandleSelection(GameObject detectedObject)
    {
        if (detectedObject != null)
        {
            this.selectedObject = detectedObject;
            flashFeedback = selectedObject.GetComponent<FlashFeedback>();
            flashFeedback.PlayFeedback();
        }
            
    }

    public void HandleMovement(Vector3 endPosition)
    {
        if (selectedObject == null)
            return;

        flashFeedback.StopFeedback();

        if (Vector2.Distance(endPosition, selectedObject.transform.position) > threshold)
        {
            Vector2 direction = (endPosition - selectedObject.transform.position);

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
            selectedObject.transform.position += (Vector3)direction;
        }
    }

    
}
