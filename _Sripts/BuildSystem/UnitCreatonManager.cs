using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCreatonManager : MonoBehaviour, ITurnDependant
{
    [SerializeField]
    private UIBuildButtonHandler townUi;

    private Town selectedTown = null;

    [SerializeField]
    private ResourceManager resourcemanager;

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
        townUi.ToggleVisibility(true, resourcemanager);
    }

    public void CreateUnity(BuildDataSO unitData)
    {
        if (selectedTown.InProduction)
        {
            Debug.Log("Town already is producing a unit");
            return;
        }
        resourcemanager.SpendResource(unitData.buildCost);
        selectedTown.AddUnitToProduction(unitData.prefab);
        ResetTownBuildUI();
    }

    private void ResetTownBuildUI()
    {
        townUi.ToggleVisibility(false, resourcemanager);
        selectedTown = null;
    }

    public void WaitTurn()
    {
        ResetTownBuildUI();
    }
}
