using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIResourcesManager : MonoBehaviour
{
    Dictionary<ResourceType, UIResource> resourceUiDictionary 
        = new Dictionary<ResourceType, UIResource>();

    private void Awake()
    {
        PrepareResourceDictionary();
    }

    private void PrepareResourceDictionary()
    {
        foreach (UIResource uiResourceReference 
            in GetComponentsInChildren<UIResource>())
        {
            if (resourceUiDictionary.ContainsKey(uiResourceReference.ResourceType))
                throw new ArgumentException
                    ("Dictionary already contains a " + 
                    uiResourceReference.ResourceType.ToString());
            resourceUiDictionary[uiResourceReference.ResourceType] = 
                uiResourceReference;
            SetResource(uiResourceReference.ResourceType, 0);
        }
    }

    public void SetResource(ResourceType resourceType, int val)
    {
        try
        {
            resourceUiDictionary[resourceType].SetValue(val);
        }
        catch (Exception)
        {

            throw new Exception
                ("Dictionary doesn't containr a UiReference for " + resourceType);
        }

    }
}
