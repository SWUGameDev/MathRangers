using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyManager : MonoBehaviour
{
    [SerializeField] BattleAbilityDataManager battleAbilityDataManager;
    [SerializeField] Player player;

    // ex) n% 증가
    public void HpIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.Hp * (percentage / 100.0f);

        player.playerProperty.Hp += increaseAmount;
        Debug.Log("체력: " + player.playerProperty.Hp);
    }

    public void AttackPowerIncrease(float percentage)
    {
        float increaseAmountMin = player.playerProperty.MinAttackPower * (percentage / 100.0f);

        player.playerProperty.MinAttackPower += increaseAmountMin;

        float increaseAmountMax = player.playerProperty.MaxAttackPower * (percentage / 100.0f);

        player.playerProperty.MaxAttackPower += increaseAmountMax;

        Debug.Log("최소 공격력: " + player.playerProperty.MinAttackPower);
        Debug.Log("최대 공격력: " + player.playerProperty.MaxAttackPower);
    }

    public void DefensePowerIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.DefensePower * (percentage / 100.0f);

        player.playerProperty.DefensePower += increaseAmount;
        Debug.Log("방어력: " + player.playerProperty.DefensePower);
    }

    public void AttackSpeedIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.AttackSpeed * (percentage / 100.0f);

        player.playerProperty.AttackSpeed += increaseAmount;
        Debug.Log("공격 속도: " + player.playerProperty.AttackSpeed);
    }

    // n초 증가
    public void LimitTimeIncrease(float second)
    {
        player.playerProperty.LimitTime += second; 
        Debug.Log("제한 시간: " + player.playerProperty.LimitTime);
    }

    public void Buff214AttackIndex(int idx)
    {
        player.playerProperty.Buff214Index = idx;
    }
}
