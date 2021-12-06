using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnTaker : MonoBehaviour
{
    private bool turnFinished;

    private IEnemyAI enemyAI;

    private void Start()
    {
        enemyAI = GetComponent<IEnemyAI>();
        enemyAI.TurnFinished += () => turnFinished = true;
    }

    public void TakeTurn()
    {
        enemyAI.StartTurn();
    }


    public bool IsFinished()
     => turnFinished;

    public void Reset()
    {
        turnFinished = false;
    }
}

public interface IEnemyAI
{
    event Action TurnFinished;

    void StartTurn();
}