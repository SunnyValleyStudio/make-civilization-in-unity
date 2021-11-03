using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionIndicatorFeedback : MonoBehaviour, ITurnDependant
{
    int defaultSortingLayer;
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private int layerToUse;

    private void Start()
    {
        layerToUse = SortingLayer.NameToID("SelectedObject");
        defaultSortingLayer = spriteRenderer.sortingLayerID;
    }

    private void ToggleSelection(bool val)
    {
        if (val)
        {
            spriteRenderer.sortingLayerID = layerToUse;
        }
        else
        {
            spriteRenderer.sortingLayerID = defaultSortingLayer;
        }
    }

    public void Select()
    {
        ToggleSelection(true);
    }

    public void Deselect()
    {
        ToggleSelection(false);
    }

    public void WaitTurn()
    {
        Deselect();
    }
}
