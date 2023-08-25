using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;

public partial class RunPlayer : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private int jumpCount = 0;
    private Rigidbody2D rb;

    public UnityEvent onEatCheese;
    public UnityEvent onSetHpGauge;
    public UnityEvent<bool> onTriggerMath;
    public UnityEvent onRunPlayerDead;

    private float maxPlayerHp = 10000;
    private float playerHp;
    private float enemyDamage = 300;
    private float fallDownDamage = 600;

    [SerializeField] MathPanelUIController mathPanelUIController;
    [SerializeField] RunSceneUIManager runSceneUIManager;

    SpriteRenderer runPlayerSpriteRenderer;
    [SerializeField] Sprite slideSprite;
    [SerializeField] Sprite walkSprite;
    private BoxCollider2D[] colliders;

    bool isArive;
    bool isSlide;
    public bool isRun;

    bool isJump = false;

    private Transform playerTransform;

    [SerializeField] Animator animator;
    string runGame = "RunGame";

    [SerializeField] SceneController sceneController;
    enum States
    {
        Run = 0,
        Jump = 1,
        Slide = 2,
        Idle = 3,
    }

    public float MaxPlayerHp
    {
        get { return maxPlayerHp; }
    }

    public float PlayerHp
    {
        get { return playerHp; }
        set { playerHp = value; }
    }

    private void Awake()
    {
        runPlayerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        this.colliders = this.GetComponents<BoxCollider2D>();
        colliders[0].enabled = true;
        colliders[1].enabled = false;
        isArive = true;
        isSlide = false;
        isRun = false;
        PlayerHp = MaxPlayerHp;
        playerTransform = this.transform;
    }
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        if(isSlide == true)
        {
            Slide();
        }
        else if(isRun == true && isJump == false)
        {
            Run();
        }
        else if(isRun == false)
        {
            PlayerIdle();
        }

        if(this.playerHp <= 0 && this.isArive == true)
        {
            this.isArive = false;
            onRunPlayerDead?.Invoke();
        }

        CheckFallDown();
    }

    void PlayerIdle()
    {
        animator.SetInteger(runGame, (int)States.Idle);
    }

    public void Jump()
    {
        isJump = true;

        if (jumpCount < 2)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_JUMP);
            jumpCount++;
            rb.velocity = Vector2.zero;
            this.rb.AddForce(new Vector2(0, this.jumpForce));

            animator.SetInteger(runGame, (int)States.Jump);
        }
    }

    public void Slide()
    {
        isSlide = true;
        runPlayerSpriteRenderer.sprite = slideSprite;
        colliders[0].enabled = false;
        colliders[1].enabled = true;

        animator.SetInteger(runGame, (int)States.Slide);
    }


    public void Run()
    {
        runPlayerSpriteRenderer.sprite = walkSprite;
        colliders[0].enabled = true;
        colliders[1].enabled = false;
        animator.SetInteger(runGame, (int)States.Run);
    }
    
    public void PointerDownSlide()
    {
        isSlide = true;
        Slide();
    }

    public void PointerUpSlide()
    {
        isSlide = false;
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Ground")
        {
            jumpCount = 0;
            isJump = false;
            animator.SetInteger(runGame, (int)States.Run);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isUnbeat == false)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_HURDLE);
            TakeDamageplayer(enemyDamage);
            animator.SetTrigger("Behit");
        }

        if (collision.gameObject.tag == "Cheese")
        {
            collision.gameObject.SetActive(false);
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_CHEESE);
            onEatCheese?.Invoke();
        }

        if (collision.gameObject.tag == "Math" && runSceneUIManager.windowScrolling.isScroll == true)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_HURDLE);
            mathPanelUIController.SetMathPanelActive(true);
            onTriggerMath?.Invoke(false);
        }

        if(collision.gameObject.tag == "End")
        {
            sceneController.LoadBossScene();
        }
    }

    void TakeDamageplayer(float damage)
    {
        PlayerHp -= damage;
        onSetHpGauge?.Invoke();
        StartCoroutine(TransparentCycle());
    }
}
