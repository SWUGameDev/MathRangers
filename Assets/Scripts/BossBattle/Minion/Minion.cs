using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public partial class Minion : MonoBehaviour
{
    private MinionStateMachine minionStateMachine;
    private GameObject target;
    private GameObject boss;
    [SerializeField] private float minionMoveSpeed = 3f;
    public Vector3 targetDirection;
    public bool isTriggerTarget;
    public float minionHp;

    public MinionCreator minionCreator;
    private float maxHp = 300;

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
        this.minionCreator = boss.GetComponent<MinionCreator>();

        minionHp = maxHp;
        Minion.OnMinionDead = new UnityEvent();
        Minion.OnReturnBullet = new UnityEvent<GameObject>();
    }

    private void Start()
    {
        this.minionStateMachine.Initialize("Idle", this);
    }

    public void MoveToTarget()
    {
        this.targetDirection = (this.target.transform.position - this.transform.position).normalized;
        this.transform.position += this.targetDirection * this.minionMoveSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            this.StartCoroutine(CollideMinionWithPlayer());
        }

        if (collision.gameObject.tag == "Bullet")
        {
            Minion.OnReturnBullet?.Invoke(collision.gameObject);
            this.minionStateMachine.SetState("BeHit");
        }
    }

    public IEnumerator CollideMinionWithPlayer()
    {
        yield return new WaitForSeconds(1f);
        this.minionStateMachine.SetState("Dead");
    }


    public IEnumerator CollideMinionWithBullet()
    {

        Debug.Log("CollideMinionWithPlayer");

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

        if (this.minionHp <= 0)
        {
            Minion.OnMinionDead?.Invoke();
        }

        yield return new WaitForSeconds(1.0f);
    }

    public float GetMaxHp()
    {
        return this.maxHp;
    }
}
