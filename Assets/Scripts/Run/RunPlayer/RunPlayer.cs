using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using System.Drawing.Drawing2D;

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
    private float mathDamage = 1000;

    [SerializeField] MathPanelUIController mathPanelUIController;
    [SerializeField] RunSceneUIManager runSceneUIManager;

    SpriteRenderer runPlayerSpriteRenderer;
    [SerializeField] Sprite slideSprite;
    [SerializeField] Sprite walkSprite;
    // [SerializeField] BoxCollider2D[] colliders;

    bool isArive;
    bool isSlide;
    public bool isRun;
    bool isEnd = false;
    bool isJump = false;

    private Transform playerTransform;

    [SerializeField] Animator animator;
    string runGame = "RunGame";

    [SerializeField] BoxCollider2D playerCollider;
    private Vector3 runColliderOffset = new Vector3(0.0f, 2.7f, 1.0f);
    private Vector3 runColliderSize = new Vector3(5.0f, 7.0f, 0.0f);

    private Vector3 sliderColliderOffset = new Vector3(0.0f, 0.0f, 1.0f);
    private Vector3 sliderColliderSize = new Vector3(5.0f, 2.0f, 0.0f);

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

        playerCollider.offset = runColliderOffset;
        playerCollider.size = runColliderSize;

        isArive = true;
        isSlide = false;
        isRun = false;
        PlayerHp = MaxPlayerHp;
        playerTransform = this.transform;

        MathPanelUIController.OnMathDamage.AddListener(this.MathDamagePlayer);
        MathQuestionExtension.OnMathTimeOutDamage.AddListener(this.MathDamagePlayer);
    }

    private void OnDestroy()
    {
        MathPanelUIController.OnMathDamage.RemoveListener(this.MathDamagePlayer);
        MathQuestionExtension.OnMathTimeOutDamage.RemoveListener(this.MathDamagePlayer);
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

        this.CheckFallDown();
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


    public void Slide()
    {
        isSlide = true;
        runPlayerSpriteRenderer.sprite = slideSprite;
        playerCollider.offset = sliderColliderOffset;
        playerCollider.size = sliderColliderSize;

        animator.SetInteger(runGame, (int)States.Slide);
    }


    public void Run()
    {
        runPlayerSpriteRenderer.sprite = walkSprite;
        playerCollider.offset = runColliderOffset;
        playerCollider.size = runColliderSize;
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
            Run();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isUnbeat == false)
        {
            VibrationController.Instance.Vibration();
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_HURDLE);
            this.TakeDamageplayer(enemyDamage);
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
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (collision.gameObject.tag == "End")
        {
            if (this.isArive == true)
            {
                sceneController.LoadBossScene();
                runSceneUIManager.SaveEatCheese();
            }
            else if (this.isArive == false && this.isEnd == false)
            {
                this.isEnd = true;
                runSceneUIManager.GameResultEmergencyAbortOfMission();
                runSceneUIManager.SetAllScroll(false);
            }
        }


    }

    void TakeDamageplayer(float damage)
    {
        PlayerHp -= damage;
        onSetHpGauge?.Invoke();
        StartCoroutine(TransparentCycle());
    }

    void MathDamagePlayer()
    {
        PlayerHp -= mathDamage;
        onSetHpGauge?.Invoke();
    }
}
