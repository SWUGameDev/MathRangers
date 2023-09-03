using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossState
{
    private float elapsedTime = 0f;

    private float escapeTime = 1f;
    public BossIdle(BossStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        // this.stateMachine.Boss.setBossAnim(0);

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
