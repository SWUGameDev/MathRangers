using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class RunUIBackGroundScrolling : MonoBehaviour
{
    public float scrollSpeed;
    public Transform[] backgrounds;

    Vector3 endPos;
    Vector3 startPos;
    [SerializeField] bool isRepeat;
    [SerializeField] bool isDistance;
    public bool isScroll;
    [SerializeField] GameObject backgoundObject;
    SpriteRenderer objectSpriteRenderer;
    private float objectWidth;

    float leftPosX = 0f;
    float rightPosX = 0f;
    float xScreenHalfSize;
    float yScreenHalfSize;

    private void Awake()
    {
        isScroll = false;
        if (this.isRepeat == true)
        {
            objectSpriteRenderer = backgoundObject.GetComponent<SpriteRenderer>();
            objectWidth = objectSpriteRenderer.bounds.size.x;

            int startIndex = 0;
            startPos = backgrounds[startIndex].position;

            int endIndex = backgrounds.Length - 2;
            endPos = backgrounds[endIndex].position;
        }

        if(this.isDistance == true) 
        {
            yScreenHalfSize = Camera.main.orthographicSize;
            xScreenHalfSize = yScreenHalfSize * Camera.main.aspect;

            leftPosX = -(xScreenHalfSize * 2);
            rightPosX = xScreenHalfSize * 2 * backgrounds.Length;
        }
    }

    void Update()
    {
        if(this.isScroll == true)
        {
            ScrollBackground();
        }

        if (this.isRepeat == true)
        {
            WarpBackground();
        }

        if(this.isDistance == true)
        {
            WarpBackgroundDistance();
        }
    }

    void ScrollBackground()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += new Vector3(-scrollSpeed, 0, 0) * Time.deltaTime;
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

    void WarpBackgroundDistance()
    {
        for (int i = 0; i < backgrounds.Length; i++)
        {
            if (backgrounds[i].position.x < leftPosX)
            {
                Vector3 nextPos = backgrounds[i].position;
                nextPos = new Vector3(nextPos.x + rightPosX, nextPos.y, nextPos.z);
                backgrounds[i].position = nextPos;
            }
        }
    }

    public void SetisScroll()
    {
        this.isScroll = !this.isScroll;
    }
}


