using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovingOnOffBTN : MonoBehaviour
{
    // ��ư���� �迭�� ����
    public Button[] toggleButtons;
    public RectTransform[] buttonRectTransforms;
    public TextMeshProUGUI[] toggleTexts;

    private bool[] isOnArray; // �� ��ư�� On/Off ����
    private Vector2[] onPositions; // �� ��ư�� On ������ �� ��ġ
    private Vector2[] offPositions; // �� ��ư�� Off ������ �� ��ġ
    private float moveSpeed = 300f; // ��ư�� �̵� �ӵ�

    void Start()
    {
        // �迭 ũ�� �ʱ�ȭ
        int buttonCount = toggleButtons.Length;
        isOnArray = new bool[buttonCount];
        onPositions = new Vector2[buttonCount];
        offPositions = new Vector2[buttonCount];

        // �� ��ư���� �ʱ� ���� ����
        for (int i = 0; i < buttonCount; i++)
        {
            // On/Off ���¿� ���� ��ư ��ġ ����
            onPositions[i] = buttonRectTransforms[i].localPosition;
            offPositions[i] = new Vector2(buttonRectTransforms[i].localPosition.x + buttonRectTransforms[i].rect.width, buttonRectTransforms[i].localPosition.y);

            // �ʱ� ���´� ��� On���� ����
            isOnArray[i] = true;
        }

        // �� ��ư�� Ŭ�� �̺�Ʈ �ڵ鷯 ����
        for (int i = 0; i < buttonCount; i++)
        {
            int index = i; // Ŭ������ ����Ͽ� �ε��� ������ ����
            toggleButtons[i].onClick.AddListener(() => ToggleButton(index));
        }
    }

    public void ToggleButton(int buttonIndex)
    {
        isOnArray[buttonIndex] = !isOnArray[buttonIndex]; // On/Off ���� ����

        // On/Off ���¿� ���� ��ư ��ġ �̵�
        Vector3 targetPosition = isOnArray[buttonIndex] ? onPositions[buttonIndex] : offPositions[buttonIndex];
        StartCoroutine(MoveButton(buttonIndex, targetPosition));
    }

    private IEnumerator MoveButton(int buttonIndex, Vector3 targetPosition)
    {
        // ��ư �̵� �ִϸ��̼�
        while (buttonRectTransforms[buttonIndex].localPosition != targetPosition)
        {
            buttonRectTransforms[buttonIndex].localPosition = Vector2.MoveTowards(buttonRectTransforms[buttonIndex].localPosition, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        toggleTexts[buttonIndex].text = isOnArray[buttonIndex] ? "ON" : "OFF";
    }
}
