using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
public partial class DialogSystem : MonoBehaviour
{
    [Header("Animation UI")]

    [SerializeField]
    private Image blackPanel;

    private YieldInstruction waitForFadeSeconds;

    private IEnumerator FadeIn()
    {
        if(this.blackPanel == null)
            yield break;

        if(this.waitForFadeSeconds == null)
            this.waitForFadeSeconds = new WaitForSeconds(0.1f);

        this.blackPanel.gameObject.SetActive(true);

        Color color = new Color(0,0,0,1);
        this.blackPanel.color = color; 
        while(color.a > 0)
        {
            color.a -= 0.1f;
            this.blackPanel.color = color;
            yield return this.waitForFadeSeconds;
        }

        this.blackPanel.gameObject.SetActive(false);

    }

    private void DoText(TMP_Text text)
    {
        text.maxVisibleCharacters = 0;
        DOTween.To(x => text.maxVisibleCharacters = (int)x , 0f, text.text.Length, this.textAnimationDuration);
    }




}
