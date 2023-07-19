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
    public static UnityEvent<int> OnBossDamaged;


    [Header("Damage Info")]
    [SerializeField] private int minDamage = 200;
    [SerializeField] private int maxDamage = 1001;

    [SerializeField] private int criticalDamage = 800;

    public int MinDamage
    {
        get { return minDamage; }
    }
    public int MaxDamage
    {
        get { return maxDamage; }
    }
    public int CriticalDamage
    {
        get { return criticalDamage; }
    }

    public void CreateBullet()
    {
        GameObject bulletObj = bulletPool.GetObject();

        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bullet.Initialized(this);

        bulletObj.transform.position = firePoint.position;
        bulletObj.transform.rotation = firePoint.rotation;
        bullet.Shot();
    }

    private void OnBulletTriggered(GameObject bullet)
    {   
        this.bulletPool.ReturnObject(bullet);

        int damage = UnityEngine.Random.Range(this.minDamage,this.maxDamage);

        Player.OnBossDamaged?.Invoke(damage);

        if(damage>this.criticalDamage)
        {
            Player.onAttackSucceeded?.Invoke(DamageType.Critical,damage);
        }else{
            Player.onAttackSucceeded?.Invoke(DamageType.Normal,damage);
        }
        
    }

    private void OnReturnBullet(GameObject bullet)
    {
        this.bulletPool.ReturnObject(bullet);
    }

    public ObjectPool GetBulletPool()
    {
        return this.bulletPool;
    }

}
