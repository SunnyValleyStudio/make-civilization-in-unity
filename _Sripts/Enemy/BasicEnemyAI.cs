using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BasicEnemyAI : MonoBehaviour, IEnemyAI
{
    public event Action TurnFinished;

    private Unit unit;
    private CharacterMovement characterMovement;

    private void Awake()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();
        unit = GetComponent<Unit>();
    }

    public void StartTurn()
    {
        Debug.Log($"Enemy: {gameObject.name} takes turn.");

        Dictionary<Vector2Int, Vector2Int?> movementRange
            = characterMovement.GetMovementRangeFor(unit);
        List<Vector2Int> path = GetPathToRandomPosition(movementRange);

        Queue<Vector2Int> pathQueue = new Queue<Vector2Int>(path);

        StartCoroutine(MoveUnit(pathQueue));
    }

    private List<Vector2Int> GetPathToRandomPosition(Dictionary<Vector2Int, Vector2Int?> movementRange)
    {
        Debug.Log(movementRange.Keys.ToList()[2]);
        return new List<Vector2Int> { movementRange.Keys.ToList()[2] };
    }

    private IEnumerator MoveUnit(Queue<Vector2Int> path)
    {
        yield return new WaitForSeconds(0.5f);
        if (unit.CanStillMove() == false || path.Count <= 0)
        {
            TurnFinished?.Invoke();
            yield break;
        }
        Vector2Int pos = path.Dequeue();
        Vector3Int direction
            = Vector3Int.RoundToInt(
                new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0) - transform.position);
        unit.HandleMovement(direction, 0);
        yield return new WaitForSeconds(0.3f);
        if(path.Count > 0)
        {
            StartCoroutine(MoveUnit(path));
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            TurnFinished?.Invoke();
        }
    }
}
