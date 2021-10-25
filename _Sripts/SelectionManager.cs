using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    FlashFeedback flashFeedback;

    public void HandleSelection(GameObject detectedColldier)
    {
        DeselectOldObject();

        if (detectedColldier == null)
            return;

        Unit unit = detectedColldier.GetComponent<Unit>();
        if(unit != null)
        {
            if (unit.CanStillMove() == false)
                return;
        }

        flashFeedback = detectedColldier.GetComponent<FlashFeedback>();
        flashFeedback.PlayFeedback();
    }

    private void DeselectOldObject()
    {
        if (flashFeedback == null)
            return;
        flashFeedback.StopFeedback();
        flashFeedback = null;
    }

}
