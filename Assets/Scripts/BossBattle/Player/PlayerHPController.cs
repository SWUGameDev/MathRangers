using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPController : MonoBehaviour
{
    public Slider playerHpGage;
    public Image fillImage;
    [SerializeField] private float playerHp;
    private float playerHpFull = 500;

    Color fullHealthColor = new Color(0f, 1f, 0f);
    Color lowHealthColor = new Color(1f, 1f, 0f);
    Color criticalHealthColor = new Color(1f, 0f, 0f);

    float lowHealthThreshold = 0.5f;
    float criticalHealthThreshold = 0.25f;

    private void Awake()
    {
        playerHp = playerHpFull;
        fillImage.color = fullHealthColor;
    }

    public void setHp(float hp)
    {
        playerHpGage.value = hp;

        if(hp >= lowHealthThreshold)
        {
            fillImage.color = fullHealthColor;
        }
        else if(hp < lowHealthThreshold && hp >= criticalHealthThreshold)
        {
            fillImage.color = lowHealthColor;
        }
        else if (hp < criticalHealthThreshold)
        {
            fillImage.color = criticalHealthColor;
        }
    }
}
