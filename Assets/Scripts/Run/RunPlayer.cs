using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
    }

    public void Jump()
    {
        if (jumpCount < 2)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_JUMP);
            jumpCount++;
            this.rb.AddForce(transform.up * this.jumpForce);
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
            Debug.Log("Cheese");
            col.gameObject.SetActive(false);
            onEatCheese?.Invoke();
        }

        if (col.gameObject.tag == "Enemy")
        {
            PlayerHp -= enemyDamage;
            onCollisionEnemy?.Invoke();
            StartCoroutine(TransparentCycle());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
    }
}
