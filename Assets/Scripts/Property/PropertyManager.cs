using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        player.BossSceneUIManager.ActiveBuffUI(5);
    }

    public void AttackPowerIncrease(float percentage)
    {
        float increaseAmountMin = player.playerProperty.MinAttackPower * (percentage / 100.0f);

        player.playerProperty.MinAttackPower += increaseAmountMin;

        float increaseAmountMax = player.playerProperty.MaxAttackPower * (percentage / 100.0f);

        player.playerProperty.MaxAttackPower += increaseAmountMax;

        Debug.Log("최소 공격력: " + player.playerProperty.MinAttackPower);
        Debug.Log("최대 공격력: " + player.playerProperty.MaxAttackPower);
        player.BossSceneUIManager.ActiveBuffUI(3);
    }

    public void DefensePowerIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.DefensePower * (percentage / 100.0f);

        player.playerProperty.DefensePower += increaseAmount;
        Debug.Log("방어력: " + player.playerProperty.DefensePower);
        player.BossSceneUIManager.ActiveBuffUI(4);
    }

    public void AttackSpeedIncrease(float percentage)
    {
        float increaseAmount = player.playerProperty.AttackSpeed * (percentage / 100.0f);

        player.playerProperty.AttackSpeed += increaseAmount;
        Debug.Log("공격 속도: " + player.playerProperty.AttackSpeed);
        player.BossSceneUIManager.ActiveBuffUI(6);
    }

    // n초 증가
    public void LimitTimeIncrease(float second)
    {
        player.playerProperty.LimitTime += second; 
        Debug.Log("제한 시간: " + player.playerProperty.LimitTime);
        player.BossSceneUIManager.ActiveBuffUI(7);
    }

    public void ActiveSkillUnbeat(float time)
    {
        player.playerProperty.Buff101UnbeatTime = time;

        player.BossSceneUIManager.ActiveSkillUI(0);
        player.BossSceneUIManager.ActiveBuffUI(0);
    }

    public void ActiveSkillEnergy(float percentage)
    {
        float increaseAmount = player.playerProperty.Hp * (percentage / 100.0f);
        player.playerProperty.EnergyHp = increaseAmount;

        player.BossSceneUIManager.ActiveSkillUI(1);
        player.BossSceneUIManager.ActiveBuffUI(1);
    }

    public void ActiveSkillSchoolNo1(int second, float attack, float speed)
    {
        player.playerProperty.Buff103Time = second;

        float increaseAmountMin = player.playerProperty.MinAttackPower * (attack / 100.0f);

        player.playerProperty.Buff103MinAttackPower = increaseAmountMin;

        float increaseAmountMax = player.playerProperty.MaxAttackPower * (attack / 100.0f);

        player.playerProperty.Buff103MaxAttackPower = increaseAmountMax;

        float increaseAmount = player.playerProperty.AttackSpeed * (speed / 100.0f);

        player.playerProperty.Buff103AttackSpeed = increaseAmount;

        player.BossSceneUIManager.ActiveSkillUI(2);
        player.BossSceneUIManager.ActiveBuffUI(2);
    }

    public void Buff213Attack(float n, float m)
    {
        player.playerProperty.Buff213Count = (int)n;
        player.playerProperty.Buff213FaintTime = m;
        player.BossSceneUIManager.ActiveBuffUI(12);
    }
    
    public void Buff214AttackIndex(int idx)
    {
        player.playerProperty.Buff214Count = idx;
        player.BossSceneUIManager.ActiveBuffUI(13);
    }
}
