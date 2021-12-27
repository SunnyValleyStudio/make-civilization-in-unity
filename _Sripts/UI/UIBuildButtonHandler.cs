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
    private UnityEvent<BuildDataSO> OnBuildButtonClick;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void PrepareBuildButton(BuildDataSO buildData)
    {
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
    }

    public void ToggleVisibility(bool val)
    {
        gameObject.SetActive(val);
        if (val == true)
        {
            ResetBuildButton();
        }
    }
}
