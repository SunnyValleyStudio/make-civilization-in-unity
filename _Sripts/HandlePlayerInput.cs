using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandlePlayerInput : MonoBehaviour
{
    public Camera currentCamera;
    public LayerMask layerMask;

    public UnityEvent<GameObject> OnHandleMouseClick;
        public UnityEvent<Vector3> OnHandleMouseFinishDragging;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleMouseClick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            HandleMouseUp();
        }
    }

    private void HandleMouseUp()
    {
        Vector3 mouseInput = GetMousePosition();
        OnHandleMouseFinishDragging?.Invoke(mouseInput);
    }

    private void HandleMouseClick()
    {
        Vector3 mouseInput = GetMousePosition();
        Collider2D colider = Physics2D.OverlapPoint(mouseInput, layerMask);
        GameObject selectedObject = colider == null ? null : colider.gameObject;

        OnHandleMouseClick?.Invoke(selectedObject);
    }

    private Vector3 GetMousePosition()
    {
        Vector3 mouseInput = currentCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseInput.z = 0f;
        return mouseInput;
    }
}
