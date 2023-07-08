using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss : MonoBehaviour
{
    [SerializeField] private GameObject target;

    [SerializeField] private float bossMoveSpeed;

    [SerializeField] private float minDistance = 0.1f;

    public GameObject Target
    {
        get{
            return this.target;
        }
    }

    private void TurnToTarget() {

        if(this.target == null)
            return;

        Vector2 currentDirection = (this.target.transform.position - this.transform.position).normalized.x < 0 ? Vector2.left : Vector2.right; 
        
        if(this.direction == currentDirection)
            return;

        this.direction = currentDirection;
        
        if(this.direction == Vector2.left)
        {
            this.transform.localEulerAngles = new Vector3(0,180,0);
        }else if(this.direction == Vector2.right)
        {
            this.transform.localEulerAngles = Vector3.zero;
        }

    }

    [SerializeField] private int maxRushCount = 3;

    private YieldInstruction rushWaitForSeconds = new WaitForSeconds(1.5f);

    public bool isRushRunning {get; private set;}

    public bool isMoveToTargetPosition {get; private set;}

    public IEnumerator RushToTarget()
    {
        this.isRushRunning = true;

        for(int index = 0;index<this.maxRushCount;index++)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_RUSH);

            Vector3 targetPosition = new Vector3(this.target.transform.position.x,-1,this.target.transform.position.z);

            yield return this.RushToTargetPosition(targetPosition);

            yield return this.rushWaitForSeconds;
        }

        this.isRushRunning = false;
    }

    public IEnumerator RushToTargetRange(float distance)
    {
        this.isMoveToTargetPosition = false;

        if(this.target == null)
            yield break;

        while(Vector2.Distance(this.target.transform.position, this.transform.position) > distance)
        {

            Vector3 direction = (this.target.transform.position - this.transform.position).normalized;
    
            transform.position += direction * this.bossMoveSpeed * Time.deltaTime;

            yield return null;
        }

        this.isMoveToTargetPosition = true;

    }

    

    public IEnumerator RushToTargetPosition(Vector3 targetPosition)
    {
        this.isMoveToTargetPosition = false;

        if(this.target == null)
            yield break;

        while(Vector2.Distance(targetPosition, this.transform.position) > this.minDistance)
        {

            Vector3 direction = (targetPosition - this.transform.position).normalized;
    
            transform.position += direction * this.bossMoveSpeed * Time.deltaTime;

            yield return null;
        }

        this.isMoveToTargetPosition = true;

    }
}
