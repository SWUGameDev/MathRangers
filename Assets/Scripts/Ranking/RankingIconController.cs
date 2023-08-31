using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankingIconController : MonoBehaviour
{
    [SerializeField] private Sprite[] iconSprites;

        public Sprite[] GetIconSprites()
    {
        return this.iconSprites;
    }


}
