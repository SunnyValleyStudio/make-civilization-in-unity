using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Camera currentCamera;
    public LayerMask layerMask;
    public float threshold = 0.5f;

    private GameObject selectedObject;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            HandleMovement();
        }
    }

    private void HandleSelection()
    {
        Vector3 mouseInput = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseInput.z = 0f;
        Collider2D colider = Physics2D.OverlapPoint(mouseInput, layerMask);
        selectedObject = colider == null ? null : colider.gameObject;
    }

    private void HandleMovement()
    {
        if (selectedObject == null)
            return;

        Vector3 endPosition = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        endPosition.z = 0f;

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
