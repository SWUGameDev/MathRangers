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
            {"Rush",new BossRush(this)},
            {"BeHit",new BossBeHit(this)},
            {"Call",new BossCall(this)},
            {"Swing",new BossSwing(this)},
            {"Faint",new BossFaint(this)}
        };
    }

    public void Initialize(string stateName,Boss boss)
    {
        base.Initialize(stateName);
        this.boss = boss;
    }
}
