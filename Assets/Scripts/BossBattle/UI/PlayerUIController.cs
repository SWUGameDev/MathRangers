using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Player player;

    [SerializeField] private Image fillImage;
    Color fullHealthColor = new Color(0f, 1f, 0f);
    Color lowHealthColor = new Color(1f, 1f, 0f);
    Color criticalHealthColor = new Color(1f, 0f, 0f);

    float lowHealthThreshold = 0.5f;
    float criticalHealthThreshold = 0.25f;

    void Start()
    {
        
        fillImage.color = fullHealthColor;
    }

    public void ChangePlayerHpValue()
    {
        if (player.playerProperty.Hp < 0)
            return;

        this.slider.value = player.playerProperty.Hp / player.playerProperty.MaxHp;

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
