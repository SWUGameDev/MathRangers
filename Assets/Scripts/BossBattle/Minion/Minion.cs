using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.Events;

public partial class Minion : MonoBehaviour
{
    private MinionStateMachine minionStateMachine;
    private GameObject target;
    private GameObject boss;
    private float minionMoveSpeed = 5f;
    public Vector3 targetDirection;
    public bool isTriggerTarget;
    public float minionHp;

    public MinionCreator minionCreator;
    private float maxHp = 1000;

    public static UnityEvent OnMinionDead;
    public static UnityEvent<GameObject> OnReturnBullet;


    private BossSceneUIManager bossSceneUIManager;

    private Player player;
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

        player = target.GetComponent<Player>(); 
        this.minionStateMachine = this.gameObject.AddComponent<MinionStateMachine>();
        bossSceneUIManager = FindObjectOfType<BossSceneUIManager>();
        if (boss != null)
        {
            this.minionCreator = boss.GetComponent<MinionCreator>();
        }
        else
        {
            Debug.LogError("Boss object not found!");
        }
        minionHp = maxHp;
        Minion.OnMinionDead = new UnityEvent();
        Minion.OnReturnBullet = new UnityEvent<GameObject>();
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
            // 함수로 바꾸기

            Minion.OnReturnBullet?.Invoke(collision.gameObject);

            int damage = UnityEngine.Random.Range(player.MinDamage, player.MaxDamage);

            this.minionHp -= damage;

            if (damage > player.CriticalDamage)
            {
                this.PlayDamageEffect(DamageType.Critical, damage);
            }
            else
            {
                this.PlayDamageEffect(DamageType.Normal, damage);
            }

            if(this.minionHp <= 0)
            {
                Minion.OnMinionDead?.Invoke();
            }
        }

    }


    public float GetMaxHp()
    {
        return this.maxHp;
    }
}
