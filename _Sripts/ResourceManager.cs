using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    UIResourcesManager resourceUI;

    Dictionary<ResourceType, int> resourceDictionary 
        = new Dictionary<ResourceType, int>();

    public List<ResourceValue> initialResources = new List<ResourceValue>();

    private void Start()
    {
        resourceUI = FindObjectOfType<UIResourcesManager>();
        PrepareResoureDictionary();
        SetInitialResourceValues();
        UpdateUI();
    }

    private void UpdateUI()
    {
        foreach (ResourceType resourceType in resourceDictionary.Keys)
        {
            UpdateUI(resourceType);
        }
    }

    private void UpdateUI(ResourceType resourceType)
    {
        resourceUI.SetResource(resourceType, resourceDictionary[resourceType]);
    }

    private void SetInitialResourceValues()
    {
        foreach (ResourceValue initialResourceValue in initialResources)
        {
            if (initialResourceValue.resourceType == ResourceType.None)
                throw new ArgumentException("Resource can't be None!");
            resourceDictionary[initialResourceValue.resourceType] 
                = initialResourceValue.resourceAmount;
        }
    }

    public void AddResource(List<ResourceValue> producedResources)
    {
        foreach (ResourceValue resourseVal in producedResources)
        {
            AddResourse(resourseVal.resourceType, resourseVal.resourceAmount);
        }
    }

    public void AddResourse(ResourceType resourceType, int resourceAmount)
    {
        resourceDictionary[resourceType] += resourceAmount;
        VerifyResourceAmount(resourceType);
        UpdateUI(resourceType);
    }

    private void PrepareResoureDictionary()
    {
        foreach (ResourceType resourceType in Enum.GetValues(typeof(ResourceType)))
        {
            if (resourceType == ResourceType.None)
                continue;
            resourceDictionary[resourceType] = 0;
        }
    }

    public bool CheckResourceAvailability(ResourceValue resourceRecquired)
    {
        return resourceDictionary[resourceRecquired.resourceType] 
            >= resourceRecquired.resourceAmount;
    }

    public void SpendResource(List<ResourceValue> buildCost)
    {
        foreach (ResourceValue resourceValue in buildCost)
        {
            SpendResource(resourceValue.resourceType, resourceValue.resourceAmount);
        }
    }

    public void SpendResource(ResourceType resourceType, int resourceAmount)
    {
        resourceDictionary[resourceType] -= resourceAmount;
        VerifyResourceAmount(resourceType);
        UpdateUI(resourceType);
    }

    private void VerifyResourceAmount(ResourceType resourceType)
    {
        if (resourceDictionary[resourceType] < 0)
            throw new 
                InvalidOperationException(
                "Can't have resource less than 0 " + resourceType);
    }
}

[Serializable]
public struct ResourceValue
{
    public ResourceType resourceType;
    [Min(0)]
    public int resourceAmount;
}
