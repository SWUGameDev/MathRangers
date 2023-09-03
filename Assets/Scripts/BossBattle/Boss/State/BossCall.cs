using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.VersionControl.Asset;

public class BossCall : BossState
{

    private float elapsedTime = 0f;

    private float escapeTime = 1f;

    private bool isTimerRunning = false;

    private bool isCalled = false;

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

        this.stateMachine.Boss.setBossAnim(1);

        if (this.stateMachine.Boss.isMoveToTargetPosition)
        {
            if(this.isCalled == false)
            {
                this.stateMachine.Boss.GetMinionCreator().CreateMinion();
                this.isCalled = true;
            }
            

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

        this.isCalled = false;

        this.isTimerRunning = false;

        this.elapsedTime = 0;
    }

}
