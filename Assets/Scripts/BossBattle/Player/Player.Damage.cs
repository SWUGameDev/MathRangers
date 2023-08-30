using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    // [SerializeField] int BossTriggerDamage = 1000;

    int bossTriggerDamage = 1000;
    int minionTriggerDamage = 500;
    public int BossTriggerDamage { get { return bossTriggerDamage; } set { bossTriggerDamage = value; } }


    private void CalculateBossTriggerDamage()
    {
        int result = (int)(bossTriggerDamage * (1 / playerProperty.DefensePower));
        playerProperty.Hp -= result;

        playerUIController.ChangePlayerHpValue();
    }
}
