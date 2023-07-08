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
    public static event Action OnMinionAttacked;
    public MinionManager minionManager;

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
        this.minionManager = boss.GetComponent<MinionManager>();

        OnMinionAttacked += MinionHpDecrease;
    }

    private void Start()
    {
        this.minionStateMachine.Initialize("Idle", this);
        isTriggerTarget = false;
    }

    private void Update()
    {
        Debug.Log(this.minionStateMachine.currentState);
        this.DirectionToTarget();
    }

    private void OnDestroy()
    {
        OnMinionAttacked-= MinionHpDecrease;
    }

    private void DirectionToTarget()
    {
        this.targetDirection = (this.target.transform.position - this.transform.position).normalized;
    }

    public void MoveToTarget(Vector3 direction)
    {
        this.transform.position += direction * this.minionMoveSpeed * Time.deltaTime;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            // 여기서 setstate 해야하는지?
            OnMinionAttacked?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("플레이어");

            this.isTriggerTarget = !this.isTriggerTarget;
        }
    }
    void MinionHpDecrease()
    {
        // TO DO : - 플레이어의 공격력으로 수정
        minionHp--;
    }
}
