using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VibrationController : MonoBehaviour
{
    public bool isVibrationEnabled = true;

    public void ToggleVibration()
    {
        isVibrationEnabled = !isVibrationEnabled;

        if (isVibrationEnabled)
        {
            // 진동 반응 켜기
            Debug.Log("진동을 켭니다.");
        }
        else
        {
            // 진동 반응 끄기
            Debug.Log("진동을 끕니다.");
        }
    }
}
