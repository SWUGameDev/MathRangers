using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VibrationController : MonoBehaviourSingleton<VibrationController>
{
    private bool isVibrationEnabled = true;

    public void ToggleVibration()
    {
        isVibrationEnabled = !isVibrationEnabled;

        if (isVibrationEnabled)
        {
            // ���� ���� �ѱ�
            Debug.Log("������ �մϴ�.");
        }
        else
        {
            // ���� ���� ����
            Debug.Log("������ ���ϴ�.");
        }
    }

    public void Vibration()
    {
        if(this.isActiveAndEnabled)
            Handheld.Vibrate();
    }
}
