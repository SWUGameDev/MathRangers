using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionBeHit : MinionState
{
    public MinionBeHit(MinionStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnter()
    {
        base.OnEnter();
        Debug.Log("MinionBeHit");

        this.stateMachine.StartCoroutine(this.stateMachine.Minion.CollideMinionWithBullet());

        base.OnUpdate();
        if (this.stateMachine.Minion.minionHp <= 0)
        {
            this.stateMachine.SetState("Dead");
        }
        else if (this.stateMachine.Minion.minionHp > 0)
        {
            this.stateMachine.SetState("Move");
        }
    }

    public override void OnUpdate()
    {

    }

    public override void OnExit()
    {
        base.OnExit();
    }

}
