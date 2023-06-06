using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StateName = System.String;
public abstract class StateMachine<T> : MonoBehaviour
{
    public State<T> currentState;
    protected Dictionary<StateName,State<T>> states;

    private bool isRunning;

    public bool IsRunning
    {
        get {return this.isRunning; }
        set { this.isRunning = value; }
    }

    public virtual void Initialize(string stateName)
    {
        this.currentState = this.states[stateName];
        this.isRunning = true;
        this.currentState.OnEnter();
    }

    void Update()
    {

        if(this.currentState == null || this.isRunning == false)
            return;

        this.currentState.OnUpdate();
    }

    public void SetState(string stateName)
    {
        if(this.currentState == null || this.isRunning == false)
            return;

        this.currentState.OnExit();
        this.currentState = this.states[stateName];
        this.currentState.OnEnter();
    }
}
