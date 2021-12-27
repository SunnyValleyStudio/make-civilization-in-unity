using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnBasedManager : MonoBehaviour
{
    Queue<EnemyTurnTaker> enemyQueue = new Queue<EnemyTurnTaker>();

    public UnityEvent OnBlockPlayerInput, OnUnblockPlayerInput;

    public void NextTurn()
    {
        Debug.Log("Waiting ... ");
        OnBlockPlayerInput?.Invoke();
        SystemsTurn();
        EnemiesTurn();

    }

    private void SystemsTurn()
    {
        foreach (SystemTurnTaker turnTaker in FindObjectsOfType<SystemTurnTaker>())
        {
            turnTaker.WaitTurn();
        }
    }

    private void EnemiesTurn()
    {
        enemyQueue 
            = new Queue<EnemyTurnTaker>(FindObjectsOfType<EnemyTurnTaker>());
        StartCoroutine(EnemyTakeTurn(enemyQueue));
    }

    private IEnumerator EnemyTakeTurn(Queue<EnemyTurnTaker> enemyQueue)
    {
        while (enemyQueue.Count > 0)
        {

            EnemyTurnTaker turnTaker = enemyQueue.Dequeue();
            turnTaker.TakeTurn();
            yield return new WaitUntil(turnTaker.IsFinished);
            turnTaker.Reset();
        }
        Debug.Log("PLAYERS turn begin");
        PlayerTurn();
    }

    private void PlayerTurn()
    {
        foreach (PlayerTurnTaker turnTaker in FindObjectsOfType<PlayerTurnTaker>())
        {
            turnTaker.WaitTurn();
            Debug.Log($"Unit {turnTaker.name} is waiting");
        }

        Debug.Log("New turn ready!");
        OnUnblockPlayerInput?.Invoke();
    }
}


public interface ITurnDependant
{
    void WaitTurn();
}