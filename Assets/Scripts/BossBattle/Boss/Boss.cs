using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class Boss : MonoBehaviour
{
    private BossStateMachine bossStateMachine;

    [SerializeField] private BoxCollider2D boxCollider2D;

    public static UnityEvent OnPlayerAttacked;
    private void Awake() {
       // this.bossAnimator = this.gameObject.GetComponent<Animator>();
        Boss.OnPlayerAttacked = new UnityEvent();

        this.bossStateMachine = this.gameObject.AddComponent<BossStateMachine>();
    }
    void Start()
    {
        this.bossStateMachine.Initialize("Idle",this);
    }

    void Update()
    {
        Debug.Log(this.bossStateMachine.currentState);

        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, this.boxCollider2D.size,0,Vector2.zero);
        if(hit.collider != null && hit.collider.tag == "Player")
        {
            Boss.OnPlayerAttacked?.Invoke();
        }
    }

}
