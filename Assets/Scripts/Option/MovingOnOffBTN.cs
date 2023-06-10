using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovingOnOffBTN : MonoBehaviour
{
    public Button toggleButton; // On/Off ��� ��ư
    public RectTransform buttonRectTransform; // ��ư�� RectTransform ������Ʈ
    public TextMeshProUGUI toggleText;

    private bool isOn = true; // On/Off ����
    private Vector2 onPosition; // On ������ �� ��ư�� ��ġ
    private Vector2 offPosition; // Off ������ �� ��ư�� ��ġ
    private float moveSpeed = 100f; // ��ư�� �̵� �ӵ�

    // Start is called before the first frame update
    void Start()
    {
        // On/Off ���¿� ���� ��ư ��ġ ����
        onPosition = buttonRectTransform.localPosition;
        offPosition = new Vector2(buttonRectTransform.localPosition.x + buttonRectTransform.rect.width, buttonRectTransform.localPosition.y);

    }

    public void ToggleButton()
    {
        isOn = !isOn; // On/Off ���� ����

        // On/Off ���¿� ���� ��ư ��ġ �̵�
        Vector3 targetPosition = isOn ? onPosition : offPosition;
        StartCoroutine(MoveButton(targetPosition));
        
    }

    private System.Collections.IEnumerator MoveButton(Vector3 targetPosition)
    {
        // ��ư �̵� �ִϸ��̼�
        while (buttonRectTransform.localPosition != targetPosition)
        {
            buttonRectTransform.localPosition = Vector2.MoveTowards(buttonRectTransform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
        toggleText.text = isOn ? "ON" : "OFF";
    }
}
