using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Player.Ability101;
using AbilityId = System.Int32;

public partial class Player : MonoBehaviour
{
    [SerializeField] float playerSpeed = 15f;
    [SerializeField] private float jumpForce;
    [SerializeField] private Slider slider;
    [SerializeField] private BossSceneStop bossSceneStop;
    [SerializeField] private BossSceneUIManager bossSceneUIManager;
    [SerializeField] private PlayerUIController playerUIController;
    public BossSceneUIManager BossSceneUIManager { get {  return this.bossSceneUIManager; } }

    public bool isSkill1Being = false;

    private Rigidbody2D rb;

    private int jumpCount = 0;
    [SerializeField] GameObject charRed;

    public PropertyInfo playerProperty;
    bool isEnd = false;

    enum States
    {
        Run = 0,
        Jump = 1,
    }

    [SerializeField] Animator animator;
    string runGame = "RunGame";
    private void Awake()
    {
        this.playerProperty = new PropertyInfo();
        VirtualJoystick.OnProcessInput += OnProcessInput;

        Player.onAttackSucceeded = new UnityEngine.Events.UnityEvent<DamageType,int>();
        Player.OnBossDamaged = new UnityEngine.Events.UnityEvent<int>();
        Player.OnBossFaint = new UnityEngine.Events.UnityEvent();
        // 버프 테스트 
        this.AddBuff();
    }

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();

        bulletPool = new ObjectPool(bulletPrefab, 10, "BulletPool");
        firePoint = transform;

        Boss.OnBossAttacked.AddListener(this.OnBulletTriggered);
        Minion.OnReturnBullet.AddListener(this.OnReturnBullet);
        Boss.OnPlayerAttacked.AddListener(this.CalculateBossTriggerDamage);

        attackIndexBuff213 = this.playerProperty.Buff213Count;
        attackIndexBuff214 = this.playerProperty.Buff214Count;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            this.CreateBullet();

        if(this.playerProperty.Hp <= 0 && this.isEnd == false)
        {
            this.isEnd = true;
            bossSceneUIManager.GameResultMissionFail();
        }

    }

    private void OnDestroy()
    {
        VirtualJoystick.OnProcessInput -= OnProcessInput;

        Minion.OnReturnBullet.RemoveListener(this.OnReturnBullet);
        Boss.OnBossAttacked.RemoveListener(this.OnBulletTriggered);
    }

    private void OnProcessInput(Vector2 vdir)
    {
        transform.position += new Vector3(vdir.x, 0f, 0f) * playerSpeed * Time.deltaTime;



        if(vdir.x < 0)
        {
            for (int i = 0; i < playerSpriteRenderer.Length; i++)
            {
                charRed.transform.localScale = new Vector3(-1f, 1f, 1);
            }
        }
        else 
        {
            for (int i = 0; i < playerSpriteRenderer.Length; i++)
            {
                charRed.transform.localScale = new Vector3(1f, 1f, 1);
            }
        }
    }

    public void Jump()
    {
        if(jumpCount < 2)
        {
            if (jumpCount == 0)
            {
                SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_JUMP1);
            }
            else
            {
                SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_JUMP2);
            }

            jumpCount++;

            rb.velocity = Vector2.zero;
            this.rb.AddForce(new Vector2(0, this.jumpForce));

            animator.SetInteger(runGame, (int)States.Jump);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.gameObject.tag == "Ground") 
        { 
            jumpCount = 0;

            animator.SetInteger(runGame, (int)States.Run);
        }
    }
}
