using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIBuildButtonHandler : MonoBehaviour
{
    [SerializeField]
    private Button buildBtn;

    private GameObject structurePrefab;

    [SerializeField]
    private UnityEvent<GameObject> OnBuildButtonClick;

    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void PrepareBuildButton(GameObject structurePrefab)
    {
        this.structurePrefab = structurePrefab;
        this.buildBtn.gameObject.SetActive(true);
    }

    public void ResetBuildButton()
    {
        this.structurePrefab = null;
        this.buildBtn.gameObject.SetActive(false);

    }

    public void HandleButtonClick()
    {
        OnBuildButtonClick?.Invoke(this.structurePrefab);
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
