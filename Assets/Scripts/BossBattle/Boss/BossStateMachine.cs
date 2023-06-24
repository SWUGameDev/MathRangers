using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateMachine : StateMachine<BossStateMachine>
{
    private Boss boss;

    public Boss Boss{
        get {
            return this.boss;
        }
    }
    private void Awake() {
        base.states = new Dictionary<string, State<BossStateMachine>>{
            {"Idle",new BossIdle(this)},
            {"Move",new BossMove(this)},
            {"BeHit",new BossBeHit(this)},
            {"Call",new BossCall(this)},
            {"Attack",new BossAttack(this)},
            {"Skill",new BossSkill(this)}
        };
    }

    public void Initialize(string stateName,Boss boss)
    {
        base.Initialize(stateName);
        this.boss = boss;
    }
}
