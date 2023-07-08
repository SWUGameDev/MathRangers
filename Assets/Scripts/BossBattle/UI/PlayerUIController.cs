using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    //TODO : 선언 위치 이동 시키기
    [SerializeField] private float damage = 0.01f;

    void Start()
    {
        Boss.OnPlayerAttacked.AddListener(this.ChangePlayerHpValue);
    }

    private void ChangePlayerHpValue()
    {
        if(this.slider.value<0)
            return;

        this.slider.value -= damage;
    }

    private void OnDestroy() {
        Boss.OnPlayerAttacked.RemoveAllListeners();
    }
}
