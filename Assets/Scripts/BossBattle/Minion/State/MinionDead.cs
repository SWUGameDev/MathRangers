using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionDead : MinionState
{
    public MinionDead(MinionStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnter()
    {
        Debug.Log("dead");
        base.OnEnter();
        this.stateMachine.SetState("Idle");
        this.stateMachine.Minion.minionHp = this.stateMachine.Minion.GetMaxHp();
        this.stateMachine.Minion.minionCreator.GetMinionPool().ReturnObject(this.stateMachine.Minion.gameObject);
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
    }

    public override void OnExit()
    {
        base.OnExit();
    }
}
