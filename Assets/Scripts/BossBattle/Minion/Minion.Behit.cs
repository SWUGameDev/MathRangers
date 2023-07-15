using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Minion : MonoBehaviour
{
    private void PlayDamageEffect(DamageType damageType, int damage)
    {
        SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_BOSS_BEHIT);

        this.bossSceneUIManager.PlayDamageEffect(damageType, this.GetRandomDamageRange(), damage);
    }

    private Vector3 GetRandomDamageRange()
    {
        float horizontalWeight = Random.Range(-1.0f, 1.0f);
        float verticalWeight = Random.Range(1.0f, 2.0f);
        return this.transform.localPosition + Vector3.up * verticalWeight + Vector3.right * horizontalWeight;
    }

}
