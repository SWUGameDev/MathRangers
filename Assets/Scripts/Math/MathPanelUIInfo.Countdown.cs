using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public partial class MathPanelUIInfo : MonoBehaviour
{
    [Header("Timer UI Panels")]

    [SerializeField] private TMP_Text clockText;


    [SerializeField] private Animator clockAnimator;

    private readonly string clockAnimKey = "IsSwinging";


    public void SetTimerTimeoutUI()
    {
        this.clockText.color = Color.red;

        this.clockAnimator.SetTrigger(this.clockAnimKey);
    }   

    private void ResetTimerUI()
    {
        this.clockText.color = Color.black;
    }


}
