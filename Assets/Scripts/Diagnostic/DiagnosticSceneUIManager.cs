using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagnosticSceneUIManager : MonoBehaviour
{
    [SerializeField] private GameObject noticePanel;
    public void ClickOkayButton()
    {
        this.StartCoroutine(this.PlayShowDown());
    }

    private IEnumerator PlayShowDown()
    {
        Animator animator =  this.noticePanel.transform.GetComponent<Animator>();
        animator.SetTrigger("PlayShowDown");

        yield return new WaitForSeconds(1.5f);

        this.noticePanel.SetActive(false);
    }

}
