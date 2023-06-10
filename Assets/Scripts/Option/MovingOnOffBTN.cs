using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovingOnOffBTN : MonoBehaviour
{
    public Button toggleButton; // On/Off 토글 버튼
    public RectTransform buttonRectTransform; // 버튼의 RectTransform 컴포넌트
    public TextMeshProUGUI toggleText;

    private bool isOn = true; // On/Off 상태
    private Vector2 onPosition; // On 상태일 때 버튼의 위치
    private Vector2 offPosition; // Off 상태일 때 버튼의 위치
    private float moveSpeed = 100f; // 버튼의 이동 속도

    // Start is called before the first frame update
    void Start()
    {
        // On/Off 상태에 따른 버튼 위치 설정
        onPosition = buttonRectTransform.localPosition;
        offPosition = new Vector2(buttonRectTransform.localPosition.x + buttonRectTransform.rect.width, buttonRectTransform.localPosition.y);

    }

    public void ToggleButton()
    {
        isOn = !isOn; // On/Off 상태 반전

        // On/Off 상태에 따른 버튼 위치 이동
        Vector3 targetPosition = isOn ? onPosition : offPosition;
        StartCoroutine(MoveButton(targetPosition));
        
    }

    private System.Collections.IEnumerator MoveButton(Vector3 targetPosition)
    {
        // 버튼 이동 애니메이션
        while (buttonRectTransform.localPosition != targetPosition)
        {
            buttonRectTransform.localPosition = Vector2.MoveTowards(buttonRectTransform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        toggleText.text = isOn ? "ON" : "OFF";
    }
}
