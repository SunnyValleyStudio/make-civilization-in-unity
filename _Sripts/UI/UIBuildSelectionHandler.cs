using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBuildSelectionHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private GameObject structurePrefab;

    private UIBuildButtonHandler buttonHandler;

    [SerializeField]
    private bool interactable = false;

    private void Awake()
    {
        buttonHandler = GetComponentInParent<UIBuildButtonHandler>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (interactable == false)
        {
            buttonHandler.ResetBuildButton();
            return;
        }
        buttonHandler.PrepareBuildButton(this.structurePrefab);
    }
}