using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffSelectPanelUIController : MonoBehaviour
{
    [SerializeField] private GameObject mathPanelGameObject;
    [SerializeField] private Animator animator;

    private readonly string BuffPanelExitKey = "IsExited"; 


    public void SetBuffPanelActive(bool isActive)
    {
        this.transform.gameObject.SetActive(isActive);
    }

    public void SetUnActive()
    {
        this.StartCoroutine(this.SetUnActiveCoroutine());
    }

    private IEnumerator SetUnActiveCoroutine()
    {
        this.animator.SetTrigger(this.BuffPanelExitKey);

        yield return new WaitForSeconds(1);

        this.transform.gameObject.SetActive(false);

        this.mathPanelGameObject?.SetActive(false);
    }
}
