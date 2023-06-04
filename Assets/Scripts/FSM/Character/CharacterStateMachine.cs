using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    private Character character;

    public Character Character 
    {
        get {return this.character; }
    }

    public void Initialize(IState state,Character character)
    {
        this.character = character;
        base.Initialize(state);
    }
    
}
