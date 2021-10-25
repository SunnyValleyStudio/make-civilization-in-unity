using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, ITurnDependant
{
    [SerializeField]
    private int maxMovementPoints = 20;

    private int currentMovementPoints;

    public UnityEvent FinishedMoving;

    void Start()
    {
        ResetMovementPoints();
    }

    private void ResetMovementPoints()
    {
        currentMovementPoints = maxMovementPoints;
    }

    public bool CanStillMove()
    {
        return currentMovementPoints > 0;
    }

    public void WaitTurn()
    {
        ResetMovementPoints();
    }

    public void HandleMovement(Vector3 cardinalDirection, int movementCost)
    {
        if (currentMovementPoints - movementCost < 0)
        {
            Debug.LogError($"Not enough movement points {currentMovementPoints} to move {movementCost}.");
            return;
        }

        currentMovementPoints -= movementCost;

        if (currentMovementPoints <= 0)
            FinishedMoving?.Invoke();

        transform.position += cardinalDirection;
    }
}
