using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ImageNoticeUIController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject character;

    [SerializeField] private Image image;

    [SerializeField] private Sprite[] sprites;
    
    public void SetActive(int index)
    {
        if(index==-1)
            this.image.sprite = sprites[1];
        else
            this.image.sprite = sprites[index];
        this.image.gameObject.SetActive(true);
        this.character.SetActive(false);
    }

    public void SetUnActive()
    {
        this.image.gameObject.SetActive(false);
        this.character.SetActive(true);
    }
}
