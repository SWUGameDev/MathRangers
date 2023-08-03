using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunUITilemapScrolling : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;

    void Update()
    {
        gameObject.transform.position += new Vector3(-speed, 0, 0) * Time.deltaTime;
    }
}