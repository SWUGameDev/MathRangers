using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCall : BossState
{

    private float elapsedTime = 0f;

    private float escapeTime = 1f;

    private bool isTimerRunning = false;

    public BossCall(BossStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();

        this.stateMachine.StartCoroutine(this.stateMachine.Boss.RushToTargetPosition(this.stateMachine.Boss.spawnPoint.position));

    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(this.stateMachine.Boss.isMoveToTargetPosition)
        {
            //TODO : Minion Call 로직 추가
            this.isTimerRunning = true;
        }

        if(this.isTimerRunning)
            this.elapsedTime += Time.deltaTime;

        if(this.elapsedTime>=this.escapeTime)
            this.stateMachine.SetState("Move");
        
    }
    public override void OnExit()
    {
        base.OnExit();

        this.elapsedTime = 0;
    }

}
