using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunUIBackGroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;

    float leftPosX = 0f;
    float rightPosX = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;

    void Start()
    {
        // yScreenHalfSize = Camera.main.orthographicSize;
        // xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

        // leftPosX = -(xScreenHalfSize * 2);
        // rightPosX = xScreenHalfSize * 2 * backgrounds.Length;
    }

    void Update()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
    }



}


