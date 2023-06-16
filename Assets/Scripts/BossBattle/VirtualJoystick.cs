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
        if (isInput)
        {
            PlayerMovement.Instance.OnPlayerMove(inputVector);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position -rectTransform.anchoredPosition - new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;

        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;  
    }

    public void ControlJoystickLever(PointerEventData eventData)
    {
        var inputDir = eventData.position - rectTransform.anchoredPosition - new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        lever.anchoredPosition = clampedDir;
        inputVector = clampedDir / leverRange;
    }
}
