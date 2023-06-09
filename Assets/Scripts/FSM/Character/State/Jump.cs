using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : State<CharacterStateMachine>
{
    public Jump(CharacterStateMachine stateMachine):base(stateMachine)
    {

    }
    public override void OnEnter()
    {
        base.stateMachine.Character.Jump();

    }

    public override void OnUpdate()
    {
        if(base.stateMachine.Character.CheckGroundCollision())
            this.stateMachine.SetState("Idle");
    }
    
}
