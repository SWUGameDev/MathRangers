using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Player;

public partial class Boss : MonoBehaviour
{
    private BossStateMachine bossStateMachine;

    [SerializeField] private BoxCollider2D boxCollider2D;

    [SerializeField] public Transform spawnPoint;

    [SerializeField] private MinionCreator minionCreator;
    private Vector2 direction = Vector2.left;

    public static UnityEvent OnPlayerAttacked;

    public static UnityEvent<GameObject> OnBossAttacked;

    [SerializeField] BossSceneUIManager bossSceneUIManager;
    [SerializeField] Player player;

    private float maxBossHp = 20000;
    private float bossHp;

    public enum States
    {
        Walk = 0,
        Call = 1,
        Swing = 2,
        Faint = 3,
        Die = 4,
        Run = 5,
    }

    [SerializeField] public Animator bossNewAnimator;
    public string bossGame = "BossGame";

    private void Awake() {
        bossNewAnimator.SetInteger(bossGame, (int)States.Walk);
        Boss.OnPlayerAttacked = new UnityEvent();

        Boss.OnBossAttacked = new UnityEvent<GameObject>();

        this.bossStateMachine = this.gameObject.AddComponent<BossStateMachine>();

        //TODO : 이후에 위치 변경하기
        SoundManager.Instance.ChangeBackgroundAudioSource(backgroundAudioSourceType.BGM_BOSS_BATTLE);
        originalColor = bossSpriteRenderer.color;
        
    }
    void Start()
    {
        this.bossStateMachine.Initialize("Idle",this);
        bossNewAnimator.SetInteger(bossGame, (int)States.Run);
        Player.onAttackSucceeded.AddListener(this.PlayDamageEffect);
        Player.OnBossFaint.AddListener(this.BossStateFaint);

        faintTime = player.playerProperty.Buff213FaintTime;
    }

    void Update()
    {
        this.TurnToTarget();
    }

    void OnDestroy()
    {
        Player.onAttackSucceeded.RemoveListener(this.PlayDamageEffect);
        Player.OnBossFaint.RemoveListener(this.BossStateFaint);
    }

    void OnTriggerStay2D(Collider2D other) {

        if(other.gameObject.tag == "Player" && player.isUnbeat == false)
        {
            Boss.OnPlayerAttacked?.Invoke();
        }
    }
    void OnTriggerEnter2D(Collider2D other) {

        if(other.gameObject.tag == "Bullet")
        {
            Boss.OnBossAttacked?.Invoke(other.gameObject);
        }
    }

    public void setBossAnim(int state)
    {
        bossNewAnimator.SetInteger(bossGame, state); ;
    }

    public float MaxBossHp
    {
        get { return maxBossHp; } 
    }

    public float BossHp
    {
        get { return bossHp; }
        set { bossHp = value; }
    }

    void BossStateFaint()
    {
        this.bossStateMachine.Initialize("Faint", this);
    }
}
