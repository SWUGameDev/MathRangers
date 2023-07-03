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
    private void Awake()
    {
        VirtualJoystick.OnProcessInput += OnProcessInput;
    }

    private void Start()
    {
        this.rb = GetComponent<Rigidbody2D>();
        isJumping = false;
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

        Debug.Log("�ݸ���");

    }


    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("�ݸ��� ������");
        if (collision.transform.name == "Boss")
        {
            Debug.Log("��z��");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("����");

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log("���� �����");

    }
}
