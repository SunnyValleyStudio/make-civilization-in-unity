using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceProducer : MonoBehaviour, ITurnDependant
{
    private static ResourceManager resourceManager;
    private BuildDataSO myBuildData;

    private void Awake()
    {
        if (resourceManager == null)
            resourceManager = FindObjectOfType<ResourceManager>();
    }

    public void Initialize(BuildDataSO data)
    {
        this.myBuildData = data;
    }

    public void WaitTurn()
    {
        resourceManager.AddResource(myBuildData.producedResources);
    }
}
