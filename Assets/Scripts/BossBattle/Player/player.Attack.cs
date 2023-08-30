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

    private int attackIndex;
    private int attackCount = 0;

    [Header("Damage Info")]

    [SerializeField] private int criticalDamage = 800;

    public int CriticalDamage
    {
        get { return criticalDamage; }
    }

    public void CreateBullet()
    {
        attackCount++;

        GameObject bulletObj = bulletPool.GetObject();
        Bullet bullet = bulletObj.GetComponent<Bullet>();

        bullet.Initialized(this);

        bulletObj.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        bulletObj.transform.position = firePoint.position;
        bulletObj.transform.rotation = firePoint.rotation;

        if(attackCount % attackIndex == 0)
        {
            bullet.isBuff214 = true;
            bulletObj.transform.localScale = new Vector3(0.6f, 0.6f, 1);
        }

        bullet.Shot();
    }

    private void OnBulletTriggered(GameObject bulletObj)
    {
        Bullet bullet = bulletObj.GetComponent<Bullet>();
        this.bulletPool.ReturnObject(bulletObj);

        int minDamage = (int)this.playerProperty.MinAttackPower;
        int maxDamage = (int)this.playerProperty.MaxAttackPower;

        if(bullet.isBuff214 == true)
        {
            minDamage *= 10;
            maxDamage *= 10;
        }
        int damage = UnityEngine.Random.Range(minDamage, maxDamage);

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
