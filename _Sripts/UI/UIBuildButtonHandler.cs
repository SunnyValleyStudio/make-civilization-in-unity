using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBuildButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Button buildBtn;

    private BuildDataSO buildData;

    [SerializeField]
    private Transform UiElementsParent;

    private List<UIBuildSelectionHandler> buildOptions;

    [SerializeField]
    private UnityEvent<BuildDataSO> OnBuildButtonClick;

    private void Start()
    {
        gameObject.SetActive(false);
        buildOptions = new List<UIBuildSelectionHandler>();
        foreach (Transform selectionHandler in UiElementsParent)
        {
            buildOptions.Add(selectionHandler.GetComponent<UIBuildSelectionHandler>());
        }
    }

    public void PrepareBuildButton(BuildDataSO buildData)
    {
        ResetUiElements();
        this.buildData = buildData;
        this.buildBtn.gameObject.SetActive(true);
    }

    public void ResetBuildButton()
    {
        this.buildData = null;
        this.buildBtn.gameObject.SetActive(false);

    }

    public void HandleButtonClick()
    {
        OnBuildButtonClick?.Invoke(this.buildData);
        ResetUiElements();
    }

    private void ResetUiElements()
    {
        foreach (UIBuildSelectionHandler selectionHandler in buildOptions)
        {
            selectionHandler.Reset();
        }
    }

    public void ToggleVisibility(bool val, ResourceManager resourceManager)
    {
        gameObject.SetActive(val);
        if (val == true)
        {
            PrepareBuildOptions(resourceManager);
            ResetBuildButton();
            ResetUiElements();
        }
    }

    private void PrepareBuildOptions(ResourceManager resourceManager)
    {
        foreach (UIBuildSelectionHandler buildItem in buildOptions)
        {
            if (buildItem.BuildData == null)
            {
                buildItem.ToggleActive(false);
                continue;
            }
            buildItem.ToggleActive(true);
            foreach (ResourceValue item in buildItem.BuildData.buildCost)
            {
                if (resourceManager.CheckResourceAvailability(item) == false)
                {
                    buildItem.ToggleActive(false);
                    break;
                }

            }
        }
    }
}
