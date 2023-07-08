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

        if(this.stateMachine.Minion.isTriggerTarget)
        {
            this.stateMachine.Minion.MoveToTarget(Vector3.Reflect(this.stateMachine.Minion.targetDirection, Vector3.up));
        }
        else
        {
            this.stateMachine.Minion.MoveToTarget(this.stateMachine.Minion.targetDirection);
        }

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
