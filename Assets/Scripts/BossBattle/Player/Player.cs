using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
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
    private Rigidbody2D rb;

    private bool isTriggerBoss;
    private int jumpCount = 0;

    public static event Action OnDamaged;
    public PropertyInfo playerProperty;
    private void Awake()
    {
        this.playerProperty = new PropertyInfo();
        VirtualJoystick.OnProcessInput += OnProcessInput;

        Player.onAttackSucceeded = new UnityEngine.Events.UnityEvent<DamageType,int>();
        Player.OnBossDamaged = new UnityEngine.Events.UnityEvent<int>();

        // 버프 테스트
        this.AddBuff();
    }

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();

        isTriggerBoss = false;

        bulletPool = new ObjectPool(bulletPrefab, 10, "BulletPool");
        firePoint = transform;

        Boss.OnBossAttacked.AddListener(this.OnBulletTriggered);
        Minion.OnReturnBullet.AddListener(this.OnReturnBullet);
    }

    private void Update()
    {
        if(isTriggerBoss == true)
        {
            OnDamaged?.Invoke();
        }

        if(Input.GetKeyDown(KeyCode.Space))
            this.CreateBullet();

        if(this.slider.value <= 0)
        {
            bossSceneStop.GameEnd();
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
    }



    public void Jump()
    {
        if(jumpCount < 2)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_JUMP);
            jumpCount++;
            this.rb.AddForce(transform.up * this.jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.gameObject.tag == "Ground") 
        { 
            jumpCount = 0;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnDamaged?.Invoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggerBoss = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isTriggerBoss = false;   
    }
}
