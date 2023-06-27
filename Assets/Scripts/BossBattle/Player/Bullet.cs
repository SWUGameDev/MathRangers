using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float time = 0f;
    public float timeInterval = 1f;
    [SerializeField] private float bulletSpeed = 13.0f;
    private Vector3 startPosition;
    Rigidbody2D rigid;

    private AutoAttackSystem autoAttackSystem;

    // 오토액션에 대해 들고잇어야함
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void Shot()
    {
        startPosition = transform.position;

        
         Vector3 monsterDir = autoAttackSystem.monster.transform.position - transform.position;

        monsterDir = monsterDir == Vector3.zero ? Vector3.up : monsterDir;
        rigid.velocity = monsterDir.normalized * bulletSpeed;
    }

    public void Initialized(AutoAttackSystem autoAttackSystem)
    {
        this.autoAttackSystem = autoAttackSystem;

    }

    private void Update()
    {
        float currentDistance = Vector3.Distance(startPosition, transform.position);

        time += Time.deltaTime;
        if (time >= timeInterval)
        {
            Debug.Log("디버그");
            // 총알을 오브젝트 풀로 반환?
            // ReturnObject로 하고 싶은데.?
            // gameObject.SetActive(false);
            this.autoAttackSystem.GetBulletPool().ReturnObject(this.gameObject);
            time = 0f;
        }
    }
}
