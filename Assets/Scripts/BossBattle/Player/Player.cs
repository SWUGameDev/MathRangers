using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    float playerSpeed = 10f;

    private void Awake()
    {
        VirtualJoystick.OnProcessInput += OnProcessInput;
    }

    private void OnDestroy()
    {
        VirtualJoystick.OnProcessInput -= OnProcessInput;
    }

    private void OnProcessInput(Vector2 vdir)
    {
        transform.position += new Vector3(vdir.x, vdir.y, 0f) * playerSpeed * Time.deltaTime;
    }
}
