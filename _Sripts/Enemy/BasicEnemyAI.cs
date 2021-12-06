using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IEnemyAI
{
    public event Action TurnFinished;

    public void StartTurn()
    {
        Debug.Log($"Enemy: {gameObject.name} takes turn.");
        StartCoroutine(TestCoroutine());
    }

    private IEnumerator TestCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"Enemy: {gameObject.name} finished moving.");
        TurnFinished?.Invoke();
    }
}
