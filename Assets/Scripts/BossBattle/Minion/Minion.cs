using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    private MinionStateMachine minionStateMachine;
    private GameObject target;
    private GameObject boss;
    private float minionMoveSpeed = 5f;
    public Vector3 targetDirection;
    public bool isTriggerTarget;
    public float minionHp;
    //public static event Action OnMinionAttacked;
    public MinionCreator minionCreator;
    private float maxHp = 1;

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
        //OnMinionAttacked += MinionHpDecrease;
        minionHp = maxHp;
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

    private void OnDestroy()
    {
        //OnMinionAttacked-= MinionHpDecrease;
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
            //OnMinionAttacked?.Invoke();
            this.minionHp--;
        }

    }
    void MinionHpDecrease()
    {
        // TO DO : - 플레이어의 공격력으로 수정
        
    }

    public float GetMaxHp()
    {
        return this.maxHp;
    }
}
