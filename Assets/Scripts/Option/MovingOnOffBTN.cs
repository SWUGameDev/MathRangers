using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovingOnOffBTN : MonoBehaviour
{
    // 버튼들을 배열로 관리
    public Button[] toggleButtons;
    public RectTransform[] buttonRectTransforms;
    public TextMeshProUGUI[] toggleTexts;

    private bool[] isOnArray; // 각 버튼의 On/Off 상태
    private Vector2[] onPositions; // 각 버튼의 On 상태일 때 위치
    private Vector2[] offPositions; // 각 버튼의 Off 상태일 때 위치
    private float moveSpeed = 300f; // 버튼의 이동 속도

    void Start()
    {
        // 배열 크기 초기화
        int buttonCount = toggleButtons.Length;
        isOnArray = new bool[buttonCount];
        onPositions = new Vector2[buttonCount];
        offPositions = new Vector2[buttonCount];

        // 각 버튼들의 초기 상태 설정
        for (int i = 0; i < buttonCount; i++)
        {
            // On/Off 상태에 따른 버튼 위치 설정
            onPositions[i] = buttonRectTransforms[i].localPosition;
            offPositions[i] = new Vector2(buttonRectTransforms[i].localPosition.x + buttonRectTransforms[i].rect.width, buttonRectTransforms[i].localPosition.y);

            // 초기 상태는 모두 On으로 설정
            isOnArray[i] = true;
        }

        // 각 버튼에 클릭 이벤트 핸들러 연결
        for (int i = 0; i < buttonCount; i++)
        {
            int index = i; // 클로저를 사용하여 인덱스 변수를 보존
            toggleButtons[i].onClick.AddListener(() => ToggleButton(index));
        }
    }

    public void ToggleButton(int buttonIndex)
    {
        isOnArray[buttonIndex] = !isOnArray[buttonIndex]; // On/Off 상태 반전

        // On/Off 상태에 따른 버튼 위치 이동
        Vector3 targetPosition = isOnArray[buttonIndex] ? onPositions[buttonIndex] : offPositions[buttonIndex];
        StartCoroutine(MoveButton(buttonIndex, targetPosition));
    }

    private IEnumerator MoveButton(int buttonIndex, Vector3 targetPosition)
    {
        // 버튼 이동 애니메이션
        while (buttonRectTransforms[buttonIndex].localPosition != targetPosition)
        {
            buttonRectTransforms[buttonIndex].localPosition = Vector2.MoveTowards(buttonRectTransforms[buttonIndex].localPosition, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        toggleTexts[buttonIndex].text = isOnArray[buttonIndex] ? "ON" : "OFF";
    }
}
