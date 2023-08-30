using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    int bossTriggerDamage = 1200;
    int minionTriggerDamage = 1000;
    int bossRushDamage = 5000;
    int bossSwingDamage = 8000;
    int bossListeningDamage = 60000;
    public int BossTriggerDamage { get { return bossTriggerDamage; } set { bossTriggerDamage = value; } }
    public int MinionTriggerDamage { get { return minionTriggerDamage; } set { minionTriggerDamage = value; } }
    public int BossRushDamage { get { return bossRushDamage; } set { bossRushDamage = value; } }
    public int BossSwingDamage { get { return bossSwingDamage; } set { bossSwingDamage = value; } }
    public int BossListeningDamage { get { return bossListeningDamage; } set { bossListeningDamage = value; } }


    private void CalculateBossTriggerDamage()
    {
        int result = (int)(bossTriggerDamage * (1 / playerProperty.DefensePower));
        playerProperty.Hp -= result;

        playerUIController.ChangePlayerHpValue();
    }
}
