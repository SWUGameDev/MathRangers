using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
    private CharacterStateMachine stateMachine;

    [SerializeField]
    private float speed = 5.0f;
    
    [SerializeField]
    private float jumpForce = 1.0f;

    [SerializeField]
    private Rigidbody2D rigidBody;
    public bool RigidBody
    {
        get {return this.rigidBody;}
    }

    [SerializeField]
    private bool isOnGround;

    public bool IsOnGround
    {
        get {return this.isOnGround;}
    }

    void Awake()
    {
        this.stateMachine = this.gameObject.AddComponent<CharacterStateMachine>();
    }

    public void Update()
    {
        Debug.Log(this.stateMachine.state);
    }

    void Start()
    {
        this.stateMachine.Initialize(new Idle(this.stateMachine),this);
    }

    public void Move()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0f);
        this.rigidBody.velocity = new Vector2(movement.x * speed, rigidBody.velocity.y);
    }

    public void Jump()
    {
        this.rigidBody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
    }

}
