using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType
{
    Normal,
    Critical
}

public partial class BossSceneUIManager : MonoBehaviour
{
    [Header("Damage Effect")]
    [SerializeField] private GameObject damagePrefab;

    [SerializeField] private GameObject criticalDamagePrefab;

    private ObjectPool damageUIPool;

    private ObjectPool criticalDamageUIPool;

    private YieldInstruction waitForDamagedEffectSeconds = new WaitForSeconds(1);

    public void PlayDamageEffect(DamageType damageType,Vector3 startPosition,int damage)
    {
        Debug.Log("PlayDamageEffect Bullet");

        switch(damageType)
        {
            case DamageType.Normal:
                this.StartCoroutine(this.GetNormalDamageUI(startPosition,damage));
                break;
            case DamageType.Critical:
                this.StartCoroutine(this.GetCriticalDamageUI(startPosition,damage));
                break;

        }        
    }

    private IEnumerator GetNormalDamageUI(Vector3 startPosition,int damage)
    {
        if(this.damageUIPool == null)
        {
            this.damageUIPool = new ObjectPool(damagePrefab,15,"DamageUIPool");
        }

        GameObject effect = this.damageUIPool.GetObject();
        DamageUIInfo damageUIInfo = effect.GetComponent<DamageUIInfo>();
        damageUIInfo.InitializeDamageUIInfo(startPosition,damage);


        yield return this.waitForDamagedEffectSeconds;
        
        this.damageUIPool.ReturnObject(effect);

    }

    private IEnumerator GetCriticalDamageUI(Vector3 startPosition,int damage)
    {
        if(this.criticalDamageUIPool == null)
        {
            this.criticalDamageUIPool = new ObjectPool(criticalDamagePrefab,5,"CriticalDamageUIPool");
        }

        GameObject effect = this.criticalDamageUIPool.GetObject();
        DamageUIInfo damageUIInfo = effect.GetComponent<DamageUIInfo>();
        damageUIInfo.InitializeDamageUIInfo(startPosition, damage);

        yield return this.waitForDamagedEffectSeconds;

        this.criticalDamageUIPool.ReturnObject(effect);
    }

}
