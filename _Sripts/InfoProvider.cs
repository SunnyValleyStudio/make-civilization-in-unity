using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoProvider : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    public Sprite Image => spriteRenderer.sprite;
    public string NameToDisplay => gameObject.name;

}
