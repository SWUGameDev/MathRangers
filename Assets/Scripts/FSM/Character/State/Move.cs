using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : State<CharacterStateMachine>
{
    public Move(CharacterStateMachine stateMachine):base(stateMachine)
    {
    }

    public override void OnUpdate()
    {
        this.stateMachine.Character.Move();
        if(Input.GetAxis("Horizontal")==0)
            this.stateMachine.SetState("Idle");
    }
    
}
