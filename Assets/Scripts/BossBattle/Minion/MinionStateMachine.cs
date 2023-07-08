using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionStateMachine : StateMachine<MinionStateMachine>
{
    private Minion minion;

    public Minion Minion { 
        get { 
            return this.minion; 
        } 
    }

    private void Awake()
    {
        base.states = new Dictionary<string, State<MinionStateMachine>>
        {
            { "Idle", new MinionIdle(this)},
            { "Move",new MinionMove(this)},
            { "BeHit",new MinionBeHit(this)},
            { "Dead",new MinionDead(this)},
        };
    }

    public void Initialize(string stateName, Minion minion)
    {
        base.Initialize(stateName);
        this.minion = minion;
    }
}
