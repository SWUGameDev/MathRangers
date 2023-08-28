using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyInfo : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private float minAttackPower = 100;
    [SerializeField] private float maxAttackPower = 150;
    [SerializeField] private float defensePower = 100;
    [SerializeField] private float attackSpeed = 1; // 기획서는 100인데 그렇게 하면 너무 큼
    [SerializeField] private float limitTime = 120;

    public float Hp { get { return hp; } set { hp = value; } }
    public float MinAttackPower { get { return minAttackPower; } set { minAttackPower = value; } }
    public float MaxAttackPower { get { return maxAttackPower; } set { maxAttackPower = value; } }
    public float DefensePower { get { return defensePower; } set { defensePower = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float LimitTime { get { return limitTime; } set { limitTime = value; } }   
}
