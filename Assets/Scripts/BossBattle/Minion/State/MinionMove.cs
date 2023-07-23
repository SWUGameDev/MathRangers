using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMove : MinionState
{
    public MinionMove(MinionStateMachine stateMachine) : base(stateMachine) 
    {
    
    }

    public override void OnEnter()
    {
        base.OnEnter();
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        this.stateMachine.Minion.MoveToTarget();

        if(this.stateMachine.Minion.minionHp == 0)
        {
            this.stateMachine.SetState("Dead");
        }
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
