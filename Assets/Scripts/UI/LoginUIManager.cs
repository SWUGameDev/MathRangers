using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LoginUIManager : MonoBehaviour
{
    public static LoginUIManager Instance;

    private void Awake()
    {
        this.waitForSeconds = new WaitForSeconds(this.popupTime);

        this.noticeObjectPool = new ObjectPool(this.noticePrefab,1,"Notice");

        LoginUIManager.Instance = this;
    }

}
