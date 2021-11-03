using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour, ITurnDependant
{
    [SerializeField]
    private UIInfoPanel infoPanel;

    private void Start()
    {
        HideInfoPanel();
    }
    public void HandleSelection(GameObject selectedObject)
    {
        HideInfoPanel();
        if (selectedObject == null)
        {
            return;
        }
        InfoProvider infoProvider = selectedObject.GetComponent<InfoProvider>();
        if (infoProvider == null)
        {
            return;
        }
        ShowInfoPanel(infoProvider);
    }

    private void ShowInfoPanel(InfoProvider infoProvider)
    {
        infoPanel.ToggleVisibility(true);
        infoPanel.SetData(infoProvider.Image, infoProvider.NameToDisplay);
    }

    public void HideInfoPanel()
    {
        infoPanel.ToggleVisibility(false);
    }

    public void WaitTurn()
    {
        HideInfoPanel();
    }
}
