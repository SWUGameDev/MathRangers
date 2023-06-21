using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private BossStateMachine bossStateMachine;
    private void Awake() {
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
}
