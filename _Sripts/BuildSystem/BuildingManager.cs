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

    [SerializeField]
    private Map map;

    [SerializeField]
    private InfoManager infoManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void HandleSelection(GameObject selectedObject)
    {
        ResetBuildingSystem();

        if (selectedObject == null)
            return;

        Worker worker = selectedObject.GetComponent<Worker>();
        if (worker != null)
        {
            HandleUnitSelection(worker);
        }

    }

    private void HandleUnitSelection(Worker worker)
    {

        farmerUnit = worker.GetComponent<Unit>();
        if (farmerUnit != null && farmerUnit.CanStillMove())
        {
            unitBuildUI.ToggleVisibility(true);
            farmerUnit.FinishedMoving.AddListener(ResetBuildingSystem);
        }
        
    }

    private void ResetBuildingSystem()
    {
        if (farmerUnit != null)
            farmerUnit.FinishedMoving.RemoveListener(ResetBuildingSystem);
        farmerUnit = null;
        unitBuildUI.ToggleVisibility(false);
    }

    public void BuildStructure(GameObject structurePrefab)
    {
        if (map.IsPositionInvalid(this.farmerUnit.transform.position))
        {
            Debug.LogWarning("Structure already exists here");
            return;
        }

        Debug.Log("Placing structure at " + this.farmerUnit.transform.position);
        GameObject structure = Instantiate(structurePrefab, this.farmerUnit.transform.position, Quaternion.identity);

        map.AddStructure(this.farmerUnit.transform.position, structure);

        audioSource.Play();

        if (structurePrefab.name == "TownStructure")
        {
            this.farmerUnit.DestroyUnit();
            infoManager.HideInfoPanel();
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
