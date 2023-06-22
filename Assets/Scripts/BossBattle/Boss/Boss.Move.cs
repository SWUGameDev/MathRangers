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
    public void FollowTarget()
    {
        if(this.target == null)
            return;

        if(Vector2.Distance(this.target.transform.position , this.transform.position)< this.minDistance)
            return;

        Vector3 direction = this.target.transform.position - this.transform.position;
        direction.Normalize(); 

        transform.position += direction * this.bossMoveSpeed * Time.deltaTime;
    }
}
