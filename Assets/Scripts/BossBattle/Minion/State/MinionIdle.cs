using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionIdle : MinionState
{
    private float elapsedTime = 0f;

    private float escapeTime = 1f;
    public MinionIdle(MinionStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        this.elapsedTime += Time.deltaTime;

        if (this.elapsedTime >= this.escapeTime)
        {
            this.stateMachine.SetState("Move");
        }

        if (this.stateMachine.Minion.minionHp == 0)
        {
            this.stateMachine.SetState("Dead");
        }
    }
    public override void OnExit()
    {
        base.OnExit();

        this.elapsedTime = 0f;
    }
}
