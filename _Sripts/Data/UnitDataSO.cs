using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Units/UnitData")]
public class UnitDataSO : ScriptableObject
{
    public int movementRange = 10;
    public int health = 1;
    public int attackStrength = 1;
}
