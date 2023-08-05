using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RunUIBackGroundScrolling : MonoBehaviour
{
    public float speed;
    public Transform[] backgrounds;

    Vector3 endPos;
    Vector3 startPos;
    [SerializeField] bool isRepeat;
    [SerializeField] GameObject backgoundObject;
    SpriteRenderer objectSpriteRenderer;
    private float objectWidth;

    private void Awake()
    { 
        if (this.isRepeat == true)
        {
            objectSpriteRenderer = backgoundObject.GetComponent<SpriteRenderer>();
            objectWidth = objectSpriteRenderer.bounds.size.x;

            int startIndex = 0;
            startPos = backgrounds[startIndex].position;

            int endIndex = backgrounds.Length - 2;
            endPos = backgrounds[endIndex].position;
        }
    }

    void Update()
    {
        ScrollBackground();

        if (this.isRepeat == true)
        {
            WarpBackground();
        }
    }

    void ScrollBackground()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-speed, 0, 0) * Time.deltaTime;
        }
    }

    void WarpBackground()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (backgrounds[i].position.x + objectWidth < startPos.x)
            {
                backgrounds[i].position = endPos;
            }
        }
    }
}


