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
    public event Action OnMove;

    [SerializeField]
    private LayerMask enemyDetectionLayer;

    private AudioSource stepSound;

    private void Awake()
    {
        unitData = GetComponent<UnitData>();
        stepSound = GetComponent<AudioSource>();
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

        GameObject enemyUnity = CheckIfEnemyUnitInDirection(cardinalDirection);
        if(enemyUnity == null)
        {
            stepSound.Play();
            transform.position += cardinalDirection;
            OnMove?.Invoke();
        }
        else
        {
            PerformAttack(enemyUnity.GetComponent<Health>());
        }

        if (currentMovementPoints <= 0)
            FinishedMoving?.Invoke();

        
    }

    private void PerformAttack(Health health)
    {
        health.GetHit(unitData.Data.attackStrength);
        currentMovementPoints = 0;
    }

    private GameObject CheckIfEnemyUnitInDirection(Vector3 cardinalDirection)
    {
        RaycastHit2D hit =
            Physics2D.Raycast(transform.position, cardinalDirection, 1, enemyDetectionLayer);
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }
        return null;
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
