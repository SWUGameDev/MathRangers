using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropertyInfo : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float attackPower;
    [SerializeField] private float defensePower;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float limitTime;

    public float HP { get { return hp; } set { hp = value; } }
    public float AttackPower { get { return attackPower; } set { attackPower = value; } }
    public float DefensePower { get { return defensePower; } set { defensePower = value; } }
    public float AttackSpeed { get { return attackSpeed; } set { attackSpeed = value; } }
    public float LimitTime { get { return limitTime; } set { limitTime = value; } }   
}
