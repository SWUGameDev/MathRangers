using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 15f;

    private Rigidbody2D rb;
    [SerializeField]
    private float jumpForce = 350f;
    private bool isJumping;
    private bool isTriggerBoss;

    public static event Action OnDamaged;

    private void Awake()
    {
        VirtualJoystick.OnProcessInput += OnProcessInput;
    }

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        isJumping = false;
        isTriggerBoss = false;
    }

    private void Update()
    {
        if(isTriggerBoss == true)
        {
            OnDamaged?.Invoke();
        }
    }

    private void OnDestroy()
    {
        VirtualJoystick.OnProcessInput -= OnProcessInput;
    }

    private void OnProcessInput(Vector2 vdir)
    {
        transform.position += new Vector3(vdir.x, 0f, 0f) * playerSpeed * Time.deltaTime;
    }

    public void Jump()
    {
        if (isJumping == false)
        {
            isJumping = true;
            this.rb.AddForce(transform.up * this.jumpForce);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.transform.name == "Ground") 
        { 
            isJumping = false; 
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
