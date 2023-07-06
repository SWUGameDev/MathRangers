using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Boss : MonoBehaviour
{
    private Animator bossAnimator;

    [SerializeField] private GameObject weapon;

    private YieldInstruction waitForSeconds = new WaitForSeconds(1.0f);

    public bool isAttacked {get; private set;}

    public IEnumerator SwingWeapon()
    {
        this.isAttacked = true;

        this.weapon.SetActive(true);

        yield return this.waitForSeconds;

        this.weapon.SetActive(false);

        this.isAttacked = false;
    }
}
