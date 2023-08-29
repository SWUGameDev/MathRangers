using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuffSelectPanelUIController : MonoBehaviour
{
    [SerializeField] private GameObject mathPanelGameObject;
    [SerializeField] private Animator animator;
    [SerializeField] private AbilityInfoManager abilityInfoManager;
    [SerializeField] private RunSceneUIManager runSceneUIManager;
    private readonly string BuffPanelExitKey = "IsExited";
    public static UnityEvent<bool> OnSolvedCorrect = new UnityEvent<bool>();

    public void SetBuffPanelActive(bool isActive)
    {
        this.transform.gameObject.SetActive(isActive);

        if(isActive)
            this.abilityInfoManager.SetRandomAbilityInfo();
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

        OnSolvedCorrect?.Invoke(true);
    }
}
