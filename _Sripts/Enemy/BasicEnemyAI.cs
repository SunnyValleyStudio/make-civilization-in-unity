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

    [SerializeField] private FlashFeedback selectionFeedback;
    [SerializeField] private AgentOutlineFeedback outlineFeedback;

    private void Awake()
    {
        characterMovement = FindObjectOfType<CharacterMovement>();
        unit = GetComponent<Unit>();
        selectionFeedback = GetComponent<FlashFeedback>();
        outlineFeedback = GetComponent<AgentOutlineFeedback>();
    }

    public void StartTurn()
    {
        Debug.Log($"Enemy: {gameObject.name} takes turn.");

        selectionFeedback.PlayFeedback();
        outlineFeedback.Select();

        Dictionary<Vector2Int, Vector2Int?> movementRange
            = characterMovement.GetMovementRangeFor(unit);
        List<Vector2Int> path = GetPathToRandomPosition(movementRange);

        Queue<Vector2Int> pathQueue = new Queue<Vector2Int>(path);

        StartCoroutine(MoveUnit(pathQueue));
    }

    private List<Vector2Int> GetPathToRandomPosition(Dictionary<Vector2Int, Vector2Int?> movementRange)
    {
        List<Vector2Int> possibleDestionation = movementRange.Keys.ToList();
        possibleDestionation.Remove(Vector2Int.RoundToInt(transform.position));
        
        Vector2Int selectedDestination = 
            possibleDestionation[
                UnityEngine.Random.Range(0, possibleDestionation.Count)];

        List<Vector2Int> listToRetuen = 
            GetPathTo(selectedDestination, movementRange);

        return listToRetuen;
    }

    private List<Vector2Int> GetPathTo(Vector2Int destination, Dictionary<Vector2Int, Vector2Int?> movementRange)
    {
        List<Vector2Int> path = new List<Vector2Int>();

        path.Add(destination);

        while (movementRange[destination] != null)
        {
            path.Add(movementRange[destination].Value);
            destination = movementRange[destination].Value;
        }

        path.Reverse();

        return path.Skip(1).ToList();
    }

    private IEnumerator MoveUnit(Queue<Vector2Int> path)
    {
        yield return new WaitForSeconds(0.5f);
        if (unit.CanStillMove() == false || path.Count <= 0)
        {
            FinishMovement();
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
            FinishMovement();
        }
    }

    private void FinishMovement()
    {
        TurnFinished?.Invoke();
        selectionFeedback.StopFeedback();
        outlineFeedback.Deselect();
    }
}
