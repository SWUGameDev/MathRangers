using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RunPlayer : MonoBehaviour
{
    // 무적 상태 관련 스크립트
    private Color changeColor;
    [SerializeField] SpriteRenderer[] playerSpriteRenderer;
    private bool isTransparent = false;
    private bool isUnbeat = false;

    private float transparentAlpha;
    private float transparent = 0.5f;
    private float normal = 1.0f;

    private IEnumerator TransparentCycle()
    {
        isUnbeat = true;
        for (int i = 0; i < 6; i++)
        {
            isTransparent = !isTransparent;
            ChangeTransparent();
            yield return new WaitForSeconds(0.3f);
        }
        isUnbeat = false;
    }

    private void ChangeTransparent()
    {
        transparentAlpha = isTransparent ? transparent : normal;
        for (int i = 0; i < playerSpriteRenderer.Length; i++)
        {
            changeColor = playerSpriteRenderer[i].color;
            changeColor.a = transparentAlpha;
            playerSpriteRenderer[i].color = changeColor;
        }
    }
}
