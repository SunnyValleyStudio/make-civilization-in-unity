using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private int maxMovementPoints = 20;

    private int currentMovementPoints;

    void Start()
    {
        currentMovementPoints = maxMovementPoints;
    }

    public bool CanStillMove()
    {
        return currentMovementPoints > 0;
    }

    public void HandleMovement(Vector3 cardinalDirection, int movementCost)
    {
        if (currentMovementPoints - movementCost < 0)
        {
            Debug.LogError($"Not enough movement points {currentMovementPoints} to move {movementCost}.");
            return;
        }

        currentMovementPoints -= movementCost;

        transform.position += cardinalDirection;
    }
}
