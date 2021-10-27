using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject unitBuildUI;
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
        unitBuildUI.SetActive(true);
    }

    private void ResetBuildingSystem()
    {
        farmerUnit = null;
        unitBuildUI.SetActive(false);
    }

    public void BuildStructure()
    {
        Debug.Log("Placing structure at " + this.farmerUnit.transform.position);
    }
}
