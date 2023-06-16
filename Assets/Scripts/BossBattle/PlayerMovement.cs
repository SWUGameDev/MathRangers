using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private RectTransform lever;
    [SerializeField]
    float speed = 10f;

    float distance = 10;
    Vector3 vPos;
    Vector3 vDist;
    Vector3 mousePosition;
    Vector3 vDir;
    public void Update()
    {
        Vector3 vPos = transform.position; // 현재 포지션

    }

    void OnMouseDrag()
    {
        Debug.Log("Drag!!");
        mousePosition = new Vector3(Input.mousePosition.x,
Input.mousePosition.y, distance);
        vDist = mousePosition - vPos;
        vDir = vDist.normalized; // 방향
        
        transform.position += vDir * speed * Time.deltaTime;
        //Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //transform.position = objPosition;
    }
}
