using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public partial class DiagnosticSceneUIManager : MonoBehaviour
{
    [SerializeField] private GameObject noticePanel;

    [SerializeField] private Image blackPanel;

    [SerializeField] private GameObject DiagnosticCanvas;

    [SerializeField] private SceneController sceneController;

    private YieldInstruction waitForFadeSeconds;

    private void Start() {
        DialogSystem.onDialogEnded -= this.SetDiagnosticCanvasActive;
        DialogSystem.onDialogEnded += this.SetDiagnosticCanvasActive;
    }

    private void OnDestroy() {
        DialogSystem.onDialogEnded -= this.SetDiagnosticCanvasActive;
    }

    private void SetDiagnosticCanvasActive(int dataIndex)
    {
        if(dataIndex == 0)
        {
            this.DiagnosticCanvas.SetActive(true);
        }
        else if(dataIndex == 1)
        {
            this.StartCoroutine(this.FadeOut(this.sceneController.LoadMainScene));
        }

    }
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

    private IEnumerator FadeOut(Action onCompleted)
    {
        if(this.blackPanel == null)
            yield break;

        if(this.waitForFadeSeconds == null)
            this.waitForFadeSeconds = new WaitForSeconds(0.1f);

        this.blackPanel.gameObject.SetActive(true);

        Color color = new Color(0,0,0,0);
        this.blackPanel.color = color; 
        while(color.a < 1)
        {
            color.a += 0.1f;
            this.blackPanel.color = color;
            yield return this.waitForFadeSeconds;
        }

        onCompleted?.Invoke();

    }


}
