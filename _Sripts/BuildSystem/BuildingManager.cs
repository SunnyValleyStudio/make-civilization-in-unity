using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private UIBuildButtonHandler unitBuildUI;
    private Unit farmerUnit;

    public void HandleSelection(GameObject selectedObject)
    {
        ResetBuildingSystem();

        if (selectedObject == null)
            return;

        farmerUnit = selectedObject.GetComponent<Unit>();
        if (farmerUnit != null)
        {
            HandleUnitSelection();
        }
    }

    private void HandleUnitSelection()
    {
        unitBuildUI.ToggleVisibility(true);
    }

    private void ResetBuildingSystem()
    {
        farmerUnit = null;
        unitBuildUI.ToggleVisibility(false);
    }

    public void BuildStructure(GameObject structurePrefab)
    {
        Debug.Log("Placing structure at " + this.farmerUnit.transform.position);
        Instantiate(structurePrefab, this.farmerUnit.transform.position, Quaternion.identity);

        this.farmerUnit.DestroyUnit();

        ResetBuildingSystem();
    }
}
