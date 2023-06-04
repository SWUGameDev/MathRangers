using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    private CharacterStateMachine stateMachine;

    public Idle(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void OnEnter()
    {

    }
    public void OnUpdate()
    {
        if(Input.GetAxis("Horizontal")!=0)
            this.stateMachine.SetState(new Move(this.stateMachine));
        else if(Input.GetKeyDown(KeyCode.Space))
            this.stateMachine.SetState(new Jump(this.stateMachine));
    }
    public void OnExit()
    {

    }
}
