using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LanguageManager : MonoBehaviour
{

    public TextMeshProUGUI languageTexts;
    private bool isKorean;

    void Start()
    {
        isKorean = true;
        languageTexts.text = "�ѱ���";
    }
    public void LanguageChange()
    {
        isKorean = !isKorean;
        if(isKorean)
        {
            languageTexts.text = "�ѱ���";
        }
        else
        {
            languageTexts.text = "English";
        }
    }


}
