using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SimpleLevelUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text levelText;
    // Start is called before the first frame update
    void Start()
    {
        int level = PlayerPrefs.GetInt(PlayerPrefManager.PlayerLevelKey) == 0 ? 1 : PlayerPrefs.GetInt(PlayerPrefManager.PlayerLevelKey) ;
        this.levelText.text = $"Lv {level}";
    }


}
