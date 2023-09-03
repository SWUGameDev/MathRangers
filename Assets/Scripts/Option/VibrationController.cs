using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VibrationController : MonoBehaviourSingleton<VibrationController>
{
    public bool isVibrationEnabled = true;

    public void ToggleVibration()
    {
        isVibrationEnabled = !isVibrationEnabled;

        if (isVibrationEnabled)
        {
            // 진동 반응 켜기
            Debug.Log("진동 반응 켜기");
        }
        else
        {
            // // 진동 반응 끄기
            Debug.Log("진동 반응 끄기");
        }
    }

    public void Vibration()
    {
        if(this.isActiveAndEnabled == true && this.isVibrationEnabled == true)
            Handheld.Vibrate();
    }
}
