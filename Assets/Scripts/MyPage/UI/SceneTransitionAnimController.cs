using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SceneTransitionAnimController : MonoBehaviour
{
    [SerializeField] private GameObject mask;

    [SerializeField] private float speed;

    [SerializeField] private float defaultScale;

    void Start()
    {
        this.OutIn();
    }

    public void InOut(Action OnCompleted)
    {
        this.transform.gameObject.SetActive(true);
        this.StartCoroutine(this.ScaleInOut(OnCompleted));
    }

    public void OutIn()
    {
        this.transform.gameObject.SetActive(true);
        this.StartCoroutine(this.ScaleOutIn());
    }

    private IEnumerator ScaleInOut(Action OnCompleted)
    {
        float value = this.defaultScale;
        while(value>0)
        {
            value -= (0.1f * speed);
            this.mask.transform.localScale = new Vector3(value,value,0);
            yield return null;
        }
        this.transform.gameObject.SetActive(false);
        OnCompleted?.Invoke();
    }

    private IEnumerator ScaleOutIn()
    {
        this.transform.gameObject.SetActive(true);
        float value = 0;
        while(value<this.defaultScale)
        {
            value += (0.1f * speed);
            this.mask.transform.localScale = new Vector3(value,value,0);
            yield return null;
        }
        this.transform.gameObject.SetActive(false);
    }
}
