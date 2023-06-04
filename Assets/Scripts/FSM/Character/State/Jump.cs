using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : IState
{
    private CharacterStateMachine stateMachine;

    public Jump(CharacterStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void OnEnter()
    {
        this.stateMachine.Character.Jump();
        this.stateMachine.SetState(new Idle(this.stateMachine));
    }
    public void OnUpdate()
    {

    }
    public void OnExit()
    {

    }
}
