using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackSystem : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float attackInterval = 1.0f; 

    private ObjectPool bulletPool;
    private Transform firePoint;
    private float timer;

    [SerializeField] public GameObject monster;
    private void Start()
    {
        bulletPool = new ObjectPool(bulletPrefab, 10, "BulletPool");
        firePoint = transform;
    }

    
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= attackInterval)
        {
            CreateBullet();
            timer = 0f;
        }
    }

    private void CreateBullet()
    {
        GameObject bulletObj = bulletPool.GetObject();

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bullet.Initialized(this);

        bulletObj.transform.position = firePoint.position;
        bulletObj.transform.rotation = firePoint.rotation;
        bullet.Shot();
    }

    public ObjectPool GetBulletPool()
    {
        return this.bulletPool;
    }
}
