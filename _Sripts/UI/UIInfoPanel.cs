using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIInfoPanel : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private Image infoImage;

    public void SetData(Sprite sprite, string text)
    {
        this.nameText.text = text;
        this.infoImage.sprite = sprite;
    }

    public void ToggleVisibility(bool val)
    {
        gameObject.SetActive(val);
    }
}
