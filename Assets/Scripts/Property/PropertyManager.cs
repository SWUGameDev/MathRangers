using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class PropertyManager : MonoBehaviour
{
    [SerializeField] BattleAbilityDataManager battleAbilityDataManager;
    [SerializeField] Player player;

    // ex) n% ����

    public void HpIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.Hp * (percentage / 100.0f);

        player.playerProperty.Hp += increaseAmount;
        Debug.Log("ü��: " + player.playerProperty.Hp);
    }

    public void AttackPowerIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.AttackPower * (percentage / 100.0f);

        player.playerProperty.AttackPower += increaseAmount;
        Debug.Log("���ݷ�: " + player.playerProperty.AttackPower);
    }

    public void DefensePowerIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.DefensePower * (percentage / 100.0f);

        player.playerProperty.DefensePower += increaseAmount;
        Debug.Log("����: " + player.playerProperty.DefensePower);
    }

    public void AttackSpeedIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.AttackSpeed * (percentage / 100.0f);

        player.playerProperty.AttackSpeed += increaseAmount;
        Debug.Log("���� �ӵ�: " + player.playerProperty.AttackSpeed);
    }

    // n�� ����
    public void LimitTimeIncrease(float second)
    {
        player.playerProperty.LimitTime += second; 
        Debug.Log("���� �ð�: " + player.playerProperty.LimitTime);
    }
}
