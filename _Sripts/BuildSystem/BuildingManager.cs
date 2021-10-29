using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour, ITurnDependant
{
    [SerializeField]
    private UIBuildButtonHandler unitBuildUI;
    private Unit farmerUnit;

    [SerializeField]
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HandleSelection(GameObject selectedObject)
    {
        ResetBuildingSystem();

        if (selectedObject == null)
            return;

        farmerUnit = selectedObject.GetComponent<Unit>();
        if (farmerUnit != null && farmerUnit.CanStillMove())
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

        audioSource.Play();

        if (structurePrefab.name == "TownStructure")
        {
            this.farmerUnit.DestroyUnit();
        }
        else
        {
            this.farmerUnit.FinishMovement();
        }

        ResetBuildingSystem();
    }

    public void WaitTurn()
    {
        ResetBuildingSystem();
    }
}
