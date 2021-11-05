using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Town : MonoBehaviour, ITurnDependant
{
    public bool InProduction { get; private set; }
    private GameObject unitToCreate;

    public void AddUnitToProduction(GameObject unitToCreate)
    {
        this.unitToCreate = unitToCreate;
        InProduction = true;
    }

    public void CompleteProduction()
    {
        if (InProduction == false)
            return;
        InProduction = false;
        if (unitToCreate == null)
            return;
        Instantiate(unitToCreate, transform.position, Quaternion.identity);
    }

    public void WaitTurn()
    {
        CompleteProduction();
    }
}
