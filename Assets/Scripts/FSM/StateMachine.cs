using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public IState state;

    private bool isRunning;

    public bool IsRunning
    {
        get {return this.isRunning; }
        set { this.isRunning = value; }
    }

    public virtual void Initialize(IState state)
    {
        this.state = state;
        this.isRunning = true;
        this.state.OnEnter();
    }

    void Update()
    {

        if(this.state == null || this.isRunning == false)
            return;

        this.state.OnUpdate();
    }

    public void SetState(IState state)
    {
        if(this.isRunning == false)
            return;

        this.state.OnExit();
        this.state = state;
        this.state.OnEnter();
    }
}
