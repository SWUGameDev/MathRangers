using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSwing : BossState
{
    [SerializeField] private int minSwingCount = 2;

    [SerializeField] private int swingDistance = 6;

    private bool isSwingStarted;
    public BossSwing(BossStateMachine stateMachine) : base(stateMachine)
    {

    }
   public override void OnEnter()
    {
        base.OnEnter();

        this.stateMachine.StartCoroutine(this.stateMachine.Boss.RushToTargetRange(this.swingDistance));

    }
    public override void OnUpdate()
    {
        base.OnUpdate();

        if(this.stateMachine.Boss.isMoveToTargetPosition)
        {
            if(isSwingStarted==false)
            {

                this.stateMachine.StartCoroutine(this.stateMachine.Boss.Swing(this.GetRandomSwingCount()));

                this.isSwingStarted = true;
            }
            
        }

        if(this.isSwingStarted && this.stateMachine.Boss.isAttacked == false)
        {
            this.stateMachine.SetState("Move");
        }

    }
    public override void OnExit()
    {
        base.OnExit();

        this.isSwingStarted = false;
    }

    private int GetRandomSwingCount()
    {
        return this.minSwingCount + Random.Range(0,2);
    }
}
