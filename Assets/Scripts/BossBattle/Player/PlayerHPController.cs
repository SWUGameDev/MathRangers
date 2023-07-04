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

    public Color fullHealthColor = new Color(0f, 1f, 0f);
    public Color lowHealthColor = new Color(1f, 1f, 0f);
    public Color criticalHealthColor = new Color(1f, 0f, 0f);

    public float lowHealthThreshold = 0.5f;
    public float criticalHealthThreshold = 0.25f;

    private void Awake()
    {
        Player.OnBossDamage += OnBossDamage;
        playerHp = playerHpFull;
        fillImage.color = fullHealthColor;
    }

    private void Update()
    {
        // Debug.Log(playerHpGage.value);
    }
    private void OnDestroy()
    {
        Player.OnBossDamage -= OnBossDamage;
    }

    private void OnBossDamage()
    {
        playerHp--;
        setHp(playerHp/playerHpFull);
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
