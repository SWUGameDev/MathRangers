using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyInfo : MonoBehaviour
{
    [SerializeField] private float hp = 1000;
    [SerializeField] private float maxHp = 1000;
    [SerializeField] private float minAttackPower = 100;
    [SerializeField] private float maxAttackPower = 150;
    [SerializeField] private float defensePower = 100;
    [SerializeField] private float attackSpeed = 1; // 배수
    [SerializeField] private float limitTime = 120;

    [SerializeField] private float buff101UnbeatTime;

    // 버프가 없을 때는 발동되는 것이 불가능 할만큼 큰 값으로 설정
    [SerializeField] private float buff213FaintTime = 5000f;
    [SerializeField] private int buff213Count = 1000;
    [SerializeField] private int buff214Count = 1000;

    public float Hp { get { return hp; } set { hp = value; } }
    public float MaxHp { get { return maxHp; } set { maxHp = value; } }
    public float MinAttackPower { get { return minAttackPower; } set { minAttackPower = value; } }
    public float MaxAttackPower { get { return maxAttackPower; } set { maxAttackPower = value; } }
    public float DefensePower { get { return defensePower; } set { defensePower = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float LimitTime { get { return limitTime; } set { limitTime = value; } }   
    public float Buff101UnbeatTime { get { return buff101UnbeatTime; } set { buff101UnbeatTime = value; } }
    public float Buff213FaintTime { get { return buff213FaintTime; } set { buff213FaintTime = value; } }
    public int Buff213Count { get { return buff213Count; } set { buff213Count = value; } }
    public int Buff214Count { get { return buff214Count; } set { buff214Count = value; } }
}
