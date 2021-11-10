using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitData : MonoBehaviour
{
    [SerializeField]
    private UnitDataSO data;

    public UnitDataSO Data
    {
        get { return data; }
    }
}
