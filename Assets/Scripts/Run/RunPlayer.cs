using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Tilemaps;


public partial class RunPlayer : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private int jumpCount = 0;
    private Rigidbody2D rb;

    public UnityEvent onEatCheese;
    public UnityEvent onCollisionEnemy;
    public UnityEvent onTriggerMath;

    private float maxPlayerHp = 10000;
    private float playerHp;
    private float enemyDamage = 400;

    [SerializeField] MathPanelUIController mathPanelUIController;
    [SerializeField] RunSceneUIManager runSceneUIManager;

    SpriteRenderer runPlayerSpriteRenderer;
    [SerializeField] Sprite slideSprite;
    [SerializeField] Sprite walkSprite;
    private BoxCollider2D[] colliders;
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
    }
    void Start()
    {

        this.rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        PlayerHp = MaxPlayerHp;
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
            PlayerHp -= enemyDamage;
            onCollisionEnemy?.Invoke();
            StartCoroutine(TransparentCycle());
        }

        if (collision.gameObject.tag == "Cheese")
        {
            collision.gameObject.SetActive(false);
            onEatCheese?.Invoke();
        }

        if (collision.gameObject.tag == "Math" && runSceneUIManager.windowScrolling.isScroll == true)
        {
            mathPanelUIController.SetMathPanelActive(true);
            Debug.Log("매쓰 충돌");
            onTriggerMath?.Invoke();
        }
    }
}
