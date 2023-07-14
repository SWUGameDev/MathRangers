using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Minion : MonoBehaviour
{
    private MinionStateMachine minionStateMachine;
    private GameObject target;
    private GameObject boss;
    private float minionMoveSpeed = 5f;
    public Vector3 targetDirection;
    public bool isTriggerTarget;
    public float minionHp;

    public MinionCreator minionCreator;
    private float maxHp = 1;

    public static UnityEvent OnMinionAttacked;

    public GameObject Target
    {
        get
        {
            return this.target;
        }
    }

    private void Awake()
    {
        target = GameObject.Find("Player");
        boss = GameObject.Find("Boss");
        this.minionStateMachine = this.gameObject.AddComponent<MinionStateMachine>();
        if (boss != null)
        {
            this.minionCreator = boss.GetComponent<MinionCreator>();
        }
        else
        {
            Debug.LogError("Boss object not found!");
        }
        minionHp = maxHp;
        Minion.OnMinionAttacked = new UnityEvent();
    }

    private void Start()
    {
        this.minionStateMachine.Initialize("Idle", this);
        isTriggerTarget = false;
    }

    private void Update()
    {
        this.DirectionToTarget();
    }


    private void DirectionToTarget()
    {
        this.targetDirection = (this.target.transform.position - this.transform.position).normalized;
    }

    public void MoveToTarget(Vector3 direction)
    {
        this.transform.position += direction * this.minionMoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.isTriggerTarget = !this.isTriggerTarget;
        }

        if (collision.gameObject.tag == "Bullet")
        {
            // 여기서 setstate 해야하는지?
            Debug.Log($"{this.transform.gameObject.name}, ontriggerenter");
            this.minionHp--;
            Minion.OnMinionAttacked?.Invoke();
        }

    }


    public float GetMaxHp()
    {
        return this.maxHp;
    }
}
