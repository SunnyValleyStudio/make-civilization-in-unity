using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnBasedManager : MonoBehaviour
{
    public UnityEvent OnBlockPlayerInput, OnUnblockPlayerInput;

    public void NextTurn()
    {
        Debug.Log("Waiting ... ");
        OnBlockPlayerInput?.Invoke();

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