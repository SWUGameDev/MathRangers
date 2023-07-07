using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossRush : BossState
{
    private Coroutine rushCoroutine;
    public BossRush(BossStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();

        this.rushCoroutine = this.stateMachine.StartCoroutine(this.stateMachine.Boss.RushToTarget());
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(!this.stateMachine.Boss.isRushRunning)
        {
            this.stateMachine.SetState("Move");
        }
    }
    public override void OnExit()
    {
        base.OnExit();
    }
}