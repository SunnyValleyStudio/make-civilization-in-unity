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

    [SerializeField]
    ResourceManager resourceManager;

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
            unitBuildUI.ToggleVisibility(true, resourceManager);
            farmerUnit.FinishedMoving.AddListener(ResetBuildingSystem);
        }
        
    }

    private void ResetBuildingSystem()
    {
        if (farmerUnit != null)
            farmerUnit.FinishedMoving.RemoveListener(ResetBuildingSystem);
        farmerUnit = null;
        unitBuildUI.ToggleVisibility(false, resourceManager);
    }

    public void BuildStructure(BuildDataSO buildData)
    {
        if (map.IsPositionInvalid(this.farmerUnit.transform.position))
        {
            Debug.LogWarning("Structure already exists here");
            return;
        }

        resourceManager.SpendResource(buildData.buildCost);

        Debug.Log("Placing structure at " + this.farmerUnit.transform.position);
        GameObject structure = Instantiate(buildData.prefab, this.farmerUnit.transform.position, Quaternion.identity);

        ResourceProducer resourceProducer = structure.GetComponent<ResourceProducer>();
        if (resourceProducer != null)
            resourceProducer.Initialize(buildData);
        
        map.AddStructure(this.farmerUnit.transform.position, structure);

        audioSource.Play();

        if (buildData.prefab.name == "TownStructure")
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
