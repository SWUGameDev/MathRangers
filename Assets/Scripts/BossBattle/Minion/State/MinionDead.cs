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
        base.OnEnter();

        this.stateMachine.Minion.minionManager.GetMinionPool().ReturnObject(this.stateMachine.Minion.gameObject);
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
