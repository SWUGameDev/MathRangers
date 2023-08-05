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

    private float maxPlayerHp = 10000;
    private float playerHp;
    private float enemyDamage = 400;

    [SerializeField] GameObject cheeseTilemapGameObject;
    Tilemap cheeseTilemap;
    Grid grid;

    public float MaxPlayerHp
    {
        get { return maxPlayerHp; }
    }

    public float PlayerHp
    {
        get { return playerHp; }
        set { playerHp = value; }
    }

    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        playerSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        PlayerHp = MaxPlayerHp;
        cheeseTilemap = cheeseTilemapGameObject.GetComponent<Tilemap>();

        grid = cheeseTilemapGameObject.GetComponentInParent<Grid>();


        Vector3Int testPosition = new Vector3Int(14, -10, 0);
        cheeseTilemap.SetTile(testPosition, null);
    }

    void Update()
    {
        // �׽�Ʈ ��
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
                // �ִϸ��̼� ����
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision");

        if (col.gameObject.tag == "Ground")
        {
            jumpCount = 0;
        }

        if (col.gameObject.tag == "Cheese")
        {
            onEatCheese?.Invoke();
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
    }
}