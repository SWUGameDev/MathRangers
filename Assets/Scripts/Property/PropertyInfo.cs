using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyInfo : MonoBehaviour
{
    [SerializeField] private float hp = 100;
    [SerializeField] private float attackPower = 100;
    [SerializeField] private float defensePower = 100;
    [SerializeField] private float attackSpeed = 1; // ��ȹ���� 100�ε� �׷��� �ϸ� �ʹ� ŭ
    [SerializeField] private float limitTime = 120;

    public float Hp { get { return hp; } set { hp = value; } }
    public float AttackPower { get { return attackPower; } set { attackPower = value; } }
    public float DefensePower { get { return defensePower; } set { defensePower = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float LimitTime { get { return limitTime; } set { limitTime = value; } }   
}
