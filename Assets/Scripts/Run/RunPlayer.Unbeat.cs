using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class RunPlayer : MonoBehaviour
{
    // ���� ���� ���� ��ũ��Ʈ
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
                Debug.Log("����");
            }
            else
            {
                Debug.Log("������");
            }
            // 1�� ��ٸ���
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void ChangeTransparent()
    {
        float transparentAlpha = isTransparent ? 1.0f : 0.5f; // �����̸� 1.0, �������̸� 0.5
        changeColor = playerSpriteRenderer.color;
        changeColor.a = transparentAlpha;
        playerSpriteRenderer.color = changeColor;
    }
}
