using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LoginUIManager : MonoBehaviourSingleton<LoginUIManager>
{

    void Awake()
    {
        base.Awake();

        this.waitForSeconds = new WaitForSeconds(this.popupTime);

        this.noticeObjectPool = new ObjectPool(this.noticePrefab,1,"Notice");
    }

}
