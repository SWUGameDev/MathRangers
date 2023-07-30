using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : BossState
{
    private float elapsedTime = 0f;

    private float escapeTime = 4f;
    public BossMove(BossStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    public override void OnUpdate()
    {
        base.OnUpdate();


        this.elapsedTime += Time.deltaTime;

        if(this.elapsedTime >= this.escapeTime)
        {
            int randomIndex = Random.Range(0,3);

            switch (randomIndex)
            {
                case 0:
                    this.stateMachine.SetState("Rush");
                    break;
                case 1:
                    this.stateMachine.SetState("Swing");
                    break;
                case 2:
                    this.stateMachine.SetState("Call");
                    break;
            }

        }
    }
    public override void OnExit()
    {
        base.OnExit();

        this.elapsedTime = 0f;
    }

}
