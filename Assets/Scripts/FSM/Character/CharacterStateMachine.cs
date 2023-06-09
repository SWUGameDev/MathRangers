using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine<CharacterStateMachine>
{
    private Character character;

    public Character Character 
    {
        get {return this.character; }
    }

    private void Awake() {
        base.states = new Dictionary<string, State<CharacterStateMachine>>{
            {"Idle",new Idle(this)},
            {"Jump", new Jump(this)},
            {"Move", new Move(this)}
        };
    }

    public void Initialize(string stateName,Character character)
    {
        base.Initialize(stateName);
        this.character = character;
    }
    
}
