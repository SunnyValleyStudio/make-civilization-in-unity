using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIResource : MonoBehaviour
{
    [SerializeField]
    private TMP_Text resourceAmount;
    [SerializeField]
    private ResourceType resourceType;

    public ResourceType ResourceType { get => resourceType; }

    private void Start()
    {
        if (resourceType == ResourceType.None)
            throw new System.ArgumentException
                ("Resource type can't be None! in " + gameObject.name);
    }

    public void SetValue(int val)
    {
        resourceAmount.text = val.ToString();
    }
}

public enum ResourceType
{
    None,
    Wood,
    Food,
    Gold
}
