using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;
    //public GameObject player;

    [SerializeField, Range(0f, 30f)]
    private float leverRange = 25f;

    private Vector2 inputVector;  
    private bool isInput;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }

    public void Update()
    {
        //playerPos = player.transform.position;
        if (isInput)
        {
            InputControlVector();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //var inputDir = eventData.position -rectTransform.anchoredPosition - new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        //var clampedDir = inputDir.magnitude < leverRange ?
            //inputDir : inputDir.normalized * leverRange;

        //lever.anchoredPosition = clampedDir;

        ControlJoystickLever(eventData);  // 추가
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //var inputDir = eventData.position - rectTransform.anchoredPosition - new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        //var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        //Debug.Log(clampedDir.x + " " + clampedDir.y);

        //lever.anchoredPosition = clampedDir;

        //Vector3 moveDirection = new Vector3(clampedDir.x, clampedDir.y, clampedDir.y).normalized;
        //Vector3 movePosition = playerPos + moveDirection * moveSpeed * Time.deltaTime;
        //player.transform.position = movePosition;
        ControlJoystickLever(eventData);    // 추가
        isInput = false;    // 추가
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
    }

    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition - new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }
    private void InputControlVector()
    {
        //Debug.Log(inputDirection.x + " / " + inputDirection.y);
        // 캐릭터에게 입력벡터를 전달
    }
}
