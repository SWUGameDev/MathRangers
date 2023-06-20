using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private static PlayerMovement instance = null;

    private void Awake()
    {
        if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static PlayerMovement Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }

    [SerializeField]
    float playerSpeed = 10f;

    public void OnPlayerMove(Vector2 vdir)
    {
        transform.position += new Vector3(vdir.x, vdir.y, 0f) * playerSpeed * Time.deltaTime;
    }
}
