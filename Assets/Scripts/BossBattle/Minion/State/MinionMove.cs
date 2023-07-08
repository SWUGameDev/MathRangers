using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMove : MinionState
{
    private float elapsedTime = 0f;

    private float escapeTime = 3f;
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

        this.elapsedTime += Time.deltaTime;

        if (this.stateMachine.Minion.isTriggerTarget == true)
        {
            Debug.Log("Ãæµ¹");
            if(this.elapsedTime < this.escapeTime)
            {
                this.stateMachine.Minion.MoveToTarget(Vector3.Reflect(this.stateMachine.Minion.targetDirection, Vector3.up));
            }
            else
            {
                this.elapsedTime = 0f;
                this.stateMachine.Minion.isTriggerTarget = false;
            }
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
