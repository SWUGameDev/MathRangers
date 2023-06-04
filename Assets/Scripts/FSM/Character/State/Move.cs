using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : IState
{
    private CharacterStateMachine stateMachine;

    public Move(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void OnEnter()
    {

    }
    public void OnUpdate()
    {
        this.stateMachine.Character.Move();
        if(Input.GetAxis("Horizontal")==0)
            this.stateMachine.SetState(new Idle(this.stateMachine));
    }
    public void OnExit()
    {

    }
}
