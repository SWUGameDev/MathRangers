using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BarUIController : MonoBehaviour
{
    [SerializeField] private Image barImage;

    [SerializeField] private int fixedWidth = 50;

    [SerializeField] private int fixedHight = 200;

    [SerializeField] private TMP_Text text;

    public void SetDateText(string date)
    {
        this.text.text = date;
    }

    public void SetBarSize(float normalizedRate)
    {
        this.barImage.rectTransform.sizeDelta = new Vector2(this.fixedWidth,this.fixedHight * normalizedRate);
    }
}
