using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFeedback : MonoBehaviour
{
    public SpriteRenderer spriteRendrer;
    [SerializeField]
    private float invisibleTime, visibleTime;

    public void PlayFeedback()
    {
        if (spriteRendrer == null)
            return;
        StopFeedback();
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        Color spriteColor = spriteRendrer.color;
        spriteColor.a = 0;
        spriteRendrer.color = spriteColor;
        yield return new WaitForSeconds(invisibleTime);

        spriteColor.a = 1;
        spriteRendrer.color = spriteColor;
        yield return new WaitForSeconds(visibleTime);

        StartCoroutine(FlashCoroutine());
    }

    public void StopFeedback()
    {
        StopAllCoroutines();
        Color spriteColor = spriteRendrer.color;
        spriteColor.a = 1;
        spriteRendrer.color = spriteColor;
    }
}
