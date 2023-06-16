using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(0f, 30f)]
    private float leverRange = 25f;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        var inputDir = eventData.position -rectTransform.anchoredPosition - new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        //lever.anchoredPosition = inputDir;

        //var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ?
            inputDir : inputDir.normalized * leverRange;

        lever.anchoredPosition = clampedDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        //var inputDir = eventData.position - rectTransform.anchoredPosition - new Vector2(rectTransform.rect.width, rectTransform.rect.height);
        //lever.anchoredPosition = inputDir;
        var inputDir = eventData.position - rectTransform.anchoredPosition;
        var clampedDir = inputDir.magnitude < leverRange ? inputDir : inputDir.normalized * leverRange;
        Debug.Log(clampedDir.x + " " + clampedDir.y);

        lever.anchoredPosition = clampedDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
    }

}
