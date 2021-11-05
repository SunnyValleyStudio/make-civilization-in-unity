using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreatonManager : MonoBehaviour, ITurnDependant
{
    [SerializeField]
    private UIBuildButtonHandler townUi;

    private Town selectedTown = null;

    public void HandleSelection(GameObject selectedObject)
    {
        ResetTownBuildUI();

        if (selectedObject == null)
            return;

        selectedTown = selectedObject.GetComponent<Town>();
        if (selectedTown != null)
        {
            HandleTown(selectedTown);
        }
    }

    private void HandleTown(Town selectedTown)
    {
        townUi.ToggleVisibility(true);
    }

    public void CreateUnity(GameObject unitToCreate)
    {
        Instantiate(unitToCreate, selectedTown.transform.position, Quaternion.identity);
    }

    private void ResetTownBuildUI()
    {
        townUi.ToggleVisibility(false);
        selectedTown = null;
    }

    public void WaitTurn()
    {
        ResetTownBuildUI();
    }
}
