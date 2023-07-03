using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPController : MonoBehaviour
{
    public Slider playerHpGage;
    [SerializeField] private float playerHp;
    private float playerHpFull = 300;

    private void Awake()
    {
        Player.OnBossDamage += OnBossDamage;
        playerHp = playerHpFull;
    }

    private void Update()
    {
        Debug.Log(playerHpGage.value);
    }
    private void OnDestroy()
    {
        Player.OnBossDamage -= OnBossDamage;
    }

    private void OnBossDamage()
    {
        playerHp--;
        setPower(playerHp/playerHpFull);
    }

    public void setPower(float hp)
    {
        playerHpGage.value = hp;
    }
}
