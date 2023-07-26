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

    private IEnumerator TransparentCycle()
    {
        for(int i = 0; i < 6; i++)
        {
            ChangeTransparent();
            isTransparent = !isTransparent;
            if(isTransparent)
            {
                Debug.Log("투명");
            }
            else
            {
                Debug.Log("불투명");
            }
            // 1초 기다리기
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void ChangeTransparent()
    {
        float transparentAlpha = isTransparent ? 1.0f : 0.5f; // 투명이면 1.0, 반투명이면 0.5
        changeColor = playerSpriteRenderer.color;
        changeColor.a = transparentAlpha;
        playerSpriteRenderer.color = changeColor;
    }
}
