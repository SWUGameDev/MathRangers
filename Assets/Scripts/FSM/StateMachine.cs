using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected IState state;

    protected bool isRunning;

    public bool isRunning
    {
        get {return this.isRunning; }
        set { this.isRunning = value; }
    }

    public StateMachine(IState state)
    {
        this.state = state;
        this.isRunning = true;
        this.state.OnEnter();
    }

    Update()
    {
        if(this.state == null || this.isRunning == false)
            return;

        this.state.OnUpdate();
    }

    public SetState(IState state)
    {
        if(this.isRunning == false)
            return;

        this.state.OnExit();
        this.state = state;
        this.state.OnEnter();
    }
}
