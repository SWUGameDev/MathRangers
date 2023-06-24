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
    }

    void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
            Boss.OnPlayerAttacked?.Invoke();
    }
}
