using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIBuildSelectionHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private BuildDataSO buildData;
    public BuildDataSO BuildData { get => buildData; }

    private UIBuildButtonHandler buttonHandler;

    private UiHighlight uiHighlight;

    [SerializeField]
    private bool interactable = false;

    private void Awake()
    {
        buttonHandler = GetComponentInParent<UIBuildButtonHandler>();
        uiHighlight = GetComponent<UiHighlight>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (interactable == false)
        {
            buttonHandler.ResetBuildButton();
            return;
        }
        buttonHandler.PrepareBuildButton(this.buildData);
        uiHighlight.ToggleHighlight(true);
    }

    public void Reset()
    {
        uiHighlight.ToggleHighlight(false);
    }
}
