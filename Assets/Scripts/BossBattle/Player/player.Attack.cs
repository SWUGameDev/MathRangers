using System;
using UnityEngine;
using UnityEngine.Events;
using Damage = System.Int32;
public partial class Player : MonoBehaviour
{
    [Header("Player Attack")]
    public GameObject bulletPrefab; 

    private ObjectPool bulletPool;
    private Transform firePoint;
    private float timer;

    [SerializeField] public GameObject monster;

    public static UnityEvent<DamageType,Damage> onAttackSucceeded;

    private readonly float criticalPercent = 1.5f;


    public void CreateBullet(float bulletSize = 1)
    {
        GameObject bulletObj = bulletPool.GetObject();

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bullet.Initialized(this);

        bulletObj.transform.position = firePoint.position;
        bulletObj.transform.localScale = Vector3.one * bulletSize;
        bulletObj.transform.rotation = firePoint.rotation;
        bullet.Shot();
    }

    private int GetPlayerDamage()
    {
        return (int) this.defaultPlayerDamage;
    }

    private bool GetIsCriticalAttack()
    {
        int rand = UnityEngine.Random.Range(0, 10);
        if (rand <= 3)
        {
            return true;
        }
        else {
            return false;
        }
    }

    private void OnBulletTriggered(GameObject bullet)
    {   
        this.bulletPool.ReturnObject(bullet);

        int damage = this.GetPlayerDamage();
       
        if(this.GetIsCriticalAttack())
        {
            Player.onAttackSucceeded?.Invoke(DamageType.Critical,  (int)(damage * this.criticalPercent));
        }else{
            Player.onAttackSucceeded?.Invoke(DamageType.Normal,damage);
        }
        
    }

    public ObjectPool GetBulletPool()
    {
        return this.bulletPool;
    }

}
