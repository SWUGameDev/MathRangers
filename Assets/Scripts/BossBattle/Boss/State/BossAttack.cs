using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : BossState
{
    private float elapsedTime = 0f;

    private float escapeTime = 1f;
    public BossAttack(BossStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();

        this.stateMachine.Boss.PlayAttackEffect();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        this.elapsedTime += Time.deltaTime;

        if(this.elapsedTime >= this.escapeTime)
        {
            this.stateMachine.SetState("Move");
        }
    }
    public override void OnExit()
    {
        base.OnExit();

        this.elapsedTime = 0f;
    }

}
