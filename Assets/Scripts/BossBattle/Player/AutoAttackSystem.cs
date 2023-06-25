using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackSystem : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float attackInterval = 1.0f; 

    private ObjectPool bulletPool;
    private GameObject player;
    private Transform firePoint;
    private float timer; 

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
            Shoot();
            timer = 0f;
        }
    }

    private void Shoot()
    {
        GameObject bulletObj = bulletPool.GetObject();

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        if (bullet != null)
        {
            bulletObj.transform.position = firePoint.position;
            bulletObj.transform.rotation = firePoint.rotation;
            //bullet.Fire(firePoint.up); // 플레이어의 전방으로 총알 발사

        }

        bulletObj.SetActive(true);
    }
}
