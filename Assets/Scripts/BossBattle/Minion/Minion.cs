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

        //minionPosArr[0] = new Vector3(-1.71f, 2.11f);
        //minionPosArr[1] = new Vector3(-1.8f, 1.01f);
        //minionPosArr[2] = new Vector3(-2.59f, 1.34f);
        //minionPosArr[3] = new Vector3(-3.2f, 1.97f);
        //minionPosArr[4] = new Vector3(-2.71f, 0.3f);
        //minionPosArr[5] = new Vector3(-3.28f, 1.1f);
        //minionPosArr[6] = new Vector3(-2.58f, 2.38f);
        //minionPosArr[7] = new Vector3(-2.2f, 0.16f);
        //minionPosArr[8] = new Vector3(-3.79f, 1.55f);
        //minionPosArr[9] = new Vector3(-2.26f, 0.95f);
        //minionPosArr[10] = new Vector3(-2.28f, 1.94f);
        //minionPosArr[11] = new Vector3(-3.33f, 0.35f);
        //minionPosArr[12] = new Vector3(-3.78f, 2.45f);
        //minionPosArr[13] = new Vector3(-3.88f, 0.78f);
        //minionPosArr[14] = new Vector3(-3.17f, 2.81f);
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
        if (other.gameObject.tag == "Player")
            isTriggerTarget = !isTriggerTarget;

        if (other.gameObject.tag == "Bullet")
            // 여기서 setstate 해야하는지?
            OnMinionAttacked?.Invoke();
    }

    void MinionHpDecrease()
    {
        // TO DO : - 플레이어의 공격력으로 수정
        minionHp--;
    }
}
