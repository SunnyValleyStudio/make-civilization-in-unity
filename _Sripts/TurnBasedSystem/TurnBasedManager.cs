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

        foreach (Unit unit in FindObjectsOfType<Unit>())
        {
            unit.WaitTurn();
            Debug.Log($"Unit {unit.name} is waiting");
        }

        Debug.Log("New turn ready!");
        OnUnblockPlayerInput?.Invoke();

    }
}
