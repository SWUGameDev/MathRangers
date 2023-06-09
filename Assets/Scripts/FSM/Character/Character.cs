using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Character : MonoBehaviour
{
    private CharacterStateMachine stateMachine;

    [SerializeField] private LayerMask groundLayerMask;

    [SerializeField] private float speed = 5.0f;
    
    [SerializeField] private float jumpForce = 1.0f;

    [SerializeField] private Rigidbody2D rigidBody;

    [SerializeField] private float characterRadius;
    public bool RigidBody
    {
        get {return this.rigidBody;}
    }

    private bool isOnGround;

    public bool IsOnGround
    {
        get {return this.isOnGround;}
    }

    void Awake()
    {
        this.stateMachine = this.gameObject.AddComponent<CharacterStateMachine>();
        this.characterRadius = this.GetComponent<BoxCollider2D>().bounds.size.y/2;
    }

    public void Update()
    {
        Debug.Log(this.stateMachine.currentState);
    }

    void Start()
    {
        this.stateMachine.Initialize("Idle",this);
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

    public bool CheckGroundCollision()
    {
        Vector2 startPos = transform.position;
        Vector2 endPos = new Vector2(startPos.x, startPos.y - this.characterRadius - 0.05f);

        RaycastHit2D hit = Physics2D.Linecast(startPos, endPos, this.groundLayerMask);

        return this.isOnGround = hit.collider != null ? true : false;
    }

}
