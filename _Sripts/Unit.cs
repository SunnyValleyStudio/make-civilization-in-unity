using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Unit : MonoBehaviour, ITurnDependant
{
    private int currentMovementPoints;

    public UnityEvent FinishedMoving;

    private UnitData unitData;

    public int CurrentMovementPoints { get => currentMovementPoints;}

    private void Awake()
    {
        unitData = GetComponent<UnitData>();
    }

    void Start()
    {
        ResetMovementPoints();
    }

    private void ResetMovementPoints()
    {
        currentMovementPoints = unitData.Data.movementRange;
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

    public void DestroyUnit()
    {
        FinishedMoving?.Invoke();
        Destroy(gameObject);
    }

    public void FinishMovement()
    {
        this.currentMovementPoints = 0;
        FinishedMoving?.Invoke();
    }
}
