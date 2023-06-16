using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class LoginUIManager
{
    [Header("Notice Message")]

    [SerializeField] private GameObject noticePrefab;

    [SerializeField] private Transform canvasTransform;

    [SerializeField] float popupTime = 1;

    [SerializeField] Vector2 noticePosition;

    private YieldInstruction waitForSeconds;

    private ObjectPool noticeObjectPool;

    public void PopUpMessage(string message)
    {
        
        GameObject notice = this.noticeObjectPool.GetObject();
        notice.transform.SetParent(this.canvasTransform);
        notice.transform.localPosition = noticePosition;
        NoticePanel panel = notice.transform.GetComponent<NoticePanel>();
        panel.text.text = message;
        this.StartCoroutine(this.PopDown(notice));

    }

    private IEnumerator PopDown(GameObject notice)
    {
        yield return this.waitForSeconds;
        this.noticeObjectPool.ReturnObject(notice);
    }

}
