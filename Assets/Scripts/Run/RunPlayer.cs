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
    bool isWalk;
    bool isFall = false;

    private Transform playerTransform;
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
        isWalk = true;
        PlayerHp = MaxPlayerHp;
        playerTransform = this.transform;
    }
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        
    }

    void Update()
    {
        // 테스트 용
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        if(Input.GetKey(KeyCode.Z))
        {
            Slide();
        }
        else
        {
            Walk();
        }

        if(isSlide == true)
        {
            Slide();
        }

        if(this.playerHp <= 0 && this.isArive == true)
        {
            this.isArive = false;
            onRunPlayerDead?.Invoke();
        }

        CheckFallDown();
    }

    public void Jump()
    {
        if (jumpCount < 2)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_JUMP);
            jumpCount++;
            rb.velocity = Vector2.zero;
            this.rb.AddForce(new Vector2(0, this.jumpForce));

            if(jumpCount == 2)
            {
                // 애니메이션 변경
            }
        }
    }

    public void Slide()
    {
        runPlayerSpriteRenderer.sprite = slideSprite;
        colliders[0].enabled = false;
        colliders[1].enabled = true;
    }


    public void Walk()
    {
        runPlayerSpriteRenderer.sprite = walkSprite;
        colliders[0].enabled = true;
        colliders[1].enabled = false;
    }

    public void PointerDownSlide()
    {
        isSlide = true;
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && isUnbeat == false)
        {
            TakeDamageplayer(enemyDamage);
        }

        if (collision.gameObject.tag == "Cheese")
        {
            collision.gameObject.SetActive(false);
            onEatCheese?.Invoke();
        }

        if (collision.gameObject.tag == "Math" && runSceneUIManager.windowScrolling.isScroll == true)
        {
            mathPanelUIController.SetMathPanelActive(true);
            onTriggerMath?.Invoke(false);
        }
    }
    
    void CheckFallDown()
    {
        if(this.gameObject.transform.position.y <= runSceneUIManager.MinY && isFall == false)
        {
            this.StartCoroutine(this.LiftUpPlayer());
        }
    }

    IEnumerator LiftUpPlayer() 
    {
        isFall = true;
        runSceneUIManager.SetAllScroll(false);
        runSceneUIManager.SetAllReverse(true);

        TakeDamageplayer(fallDownDamage);
        yield return new WaitForSeconds(1.0f);
        Vector3 liftUpPosition = playerTransform.position + Vector3.up * 12.0f;
        playerTransform.position = liftUpPosition;
        runSceneUIManager.SetAllReverse(false);
        yield return new WaitForSeconds(1.0f);
        runSceneUIManager.SetAllScroll(true);
        isFall = false;
    }

    void TakeDamageplayer(float damage)
    {
        PlayerHp -= damage;
        onSetHpGauge?.Invoke();
        StartCoroutine(TransparentCycle());
    }
}
