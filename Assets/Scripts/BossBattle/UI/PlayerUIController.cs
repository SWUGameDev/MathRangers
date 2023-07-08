using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    //TODO : 선언 위치 이동 시키기
    [SerializeField] private float damage = 0.01f;

    [SerializeField] private Image fillImage;
    Color fullHealthColor = new Color(0f, 1f, 0f);
    Color lowHealthColor = new Color(1f, 1f, 0f);
    Color criticalHealthColor = new Color(1f, 0f, 0f);

    float lowHealthThreshold = 0.5f;
    float criticalHealthThreshold = 0.25f;
    void Start()
    {
        Boss.OnPlayerAttacked.AddListener(this.ChangePlayerHpValue);
        fillImage.color = fullHealthColor;
    }

    private void ChangePlayerHpValue()
    {
        if(this.slider.value<0)
            return;

        this.slider.value -= damage;
        if (this.slider.value >= lowHealthThreshold)
        {
            fillImage.color = fullHealthColor;
        }
        else if (this.slider.value < lowHealthThreshold && this.slider.value >= criticalHealthThreshold)
        {
            fillImage.color = lowHealthColor;
        }
        else if (this.slider.value < criticalHealthThreshold)
        {
            fillImage.color = criticalHealthColor;
        }
    }

    private void OnDestroy() {
        Boss.OnPlayerAttacked.RemoveAllListeners();
    }
}
