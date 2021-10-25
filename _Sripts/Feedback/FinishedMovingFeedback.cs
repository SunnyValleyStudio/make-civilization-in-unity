using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishedMovingFeedback : MonoBehaviour, ITurnDependant
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private Color darkColor;
    private Color originalColor;

    private void Start()
    {
        originalColor = spriteRenderer.color;
    }

    public void PlayFeedback()
    {
        spriteRenderer.color = darkColor;
    }

    public void StopFeedback()
    {
        spriteRenderer.color = originalColor;
    }

    public void WaitTurn()
    {
        StopFeedback();
    }
}
