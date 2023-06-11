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
            // ���� ���� �ѱ�
            Debug.Log("������ �մϴ�.");
        }
        else
        {
            // ���� ���� ����
            Debug.Log("������ ���ϴ�.");
        }
    }
}
