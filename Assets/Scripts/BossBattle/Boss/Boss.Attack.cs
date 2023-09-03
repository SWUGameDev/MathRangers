using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss : MonoBehaviour
{
    // private Animator bossAnimator;

    [SerializeField] private GameObject weapon;

    private YieldInstruction waitForSeconds = new WaitForSeconds(1.0f);

    public bool isAttacked {get; private set;}

    public IEnumerator Swing(int count)
    {
        this.isAttacked = true;

        for(int i = 0;i < count; i ++)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_SWING);

            yield return this.SwingWeapon();
        }
        
        this.isAttacked = false;
    }

    private IEnumerator SwingWeapon()
    {
        this.weapon.SetActive(true);

        yield return this.waitForSeconds;

        this.weapon.SetActive(false);
    }

    public MinionCreator GetMinionCreator()
    {
        return this.minionCreator;
    }
}
