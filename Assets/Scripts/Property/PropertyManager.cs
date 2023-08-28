using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyManager : MonoBehaviour
{
    private PropertyInfo propertyInfo;

    private void Start()
    {
        this.propertyInfo = new PropertyInfo();
        this.propertyInfo.HP = 100;
        this.propertyInfo.AttackPower = 100;
        this.propertyInfo.DefensePower = 100;
        this.propertyInfo.AttackSpeed = 1; // ��ȹ���� 100�ε� �׷��� �ϸ� �ʹ� ŭ
        this.propertyInfo.LimitTime = 120;
    }

    // ex) 10% ���� �� ��ȯ
    public float HpIncrease(float percentage)
    {
        float increaseAmount = this.propertyInfo.HP * (percentage / 100.0f);

        return this.propertyInfo.HP + increaseAmount;
    }

    public float AttackPowerIncrease(float percentage)
    {
        float increaseAmount = this.propertyInfo.AttackPower * (percentage / 100.0f);

        return this.propertyInfo.AttackPower + increaseAmount;
    }

    public float DefensePowerIncrease(float percentage)
    {
        float increaseAmount = this.propertyInfo.DefensePower * (percentage / 100.0f);

        return this.propertyInfo.DefensePower + increaseAmount;
    }

    public float AttackSpeedIncrease(float percentage)
    {
        float increaseAmount = this.propertyInfo.AttackSpeed * (percentage / 100.0f);

        return this.propertyInfo.AttackSpeed + increaseAmount;
    }

    // n�� ����
    public float LimitTimeIncrease(float second)
    {
        return this.propertyInfo.LimitTime + second;
    }
}
