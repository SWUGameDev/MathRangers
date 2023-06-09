using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : State<CharacterStateMachine>
{

    public Idle(CharacterStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnUpdate()
    {
        if(Input.GetAxis("Horizontal")!=0)
            this.stateMachine.SetState("Move");
        else if(Input.GetKeyDown(KeyCode.Space))
            this.stateMachine.SetState("Jump");
    }
}
