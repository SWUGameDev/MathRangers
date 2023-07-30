using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RunPlayer : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    private int jumpCount = 0;
    private Rigidbody2D rb;

    public UnityEvent onEatCheese;
    void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger");
    }
}
