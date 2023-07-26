using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RunPlayer : MonoBehaviour
{
    // 무적 상태 관련 스크립트
    private Color changeColor;
    private SpriteRenderer playerSpriteRenderer;
    private bool isTransparent = false;
    private float transparentAlpha;
    private float transparent = 0.5f;
    private float normal = 1.0f;

    private IEnumerator TransparentCycle()
    {
        for(int i = 0; i < 7; i++)
        {
            ChangeTransparent();
            isTransparent = !isTransparent;

            yield return new WaitForSeconds(0.3f);
        }
    }

    private void ChangeTransparent()
    {
        transparentAlpha = isTransparent ? transparent : normal;
        changeColor = playerSpriteRenderer.color;
        changeColor.a = transparentAlpha;
        playerSpriteRenderer.color = changeColor;
    }
}
