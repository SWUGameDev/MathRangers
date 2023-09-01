using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFaint : BossState
{
    private float elapsedTime = 0f;

    private float escapeTime;
    public BossFaint(BossStateMachine stateMachine) : base(stateMachine)
    {
        
    }
    public override void OnEnter()
    {
        base.OnEnter();
        this.escapeTime = this.stateMachine.Boss.faintTime;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        this.elapsedTime += Time.deltaTime;

        if (this.elapsedTime < this.escapeTime)
        {
            this.stateMachine.Boss.BossFaint();
        }
        else
        {
            this.stateMachine.Boss.BossFaintEnd();
            this.stateMachine.SetState("Idle");
        }
    }
    public override void OnExit()
    {
        base.OnExit();

        this.elapsedTime = 0f;
    }
}
