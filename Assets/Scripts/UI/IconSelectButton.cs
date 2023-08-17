using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IconSelectButton : MonoBehaviour
{
    private Button button;
    private void Awake() {
        this.button = transform.GetComponent<Button>();
    }

    public void SetIndex(int index)
    {
        this.button.onClick.AddListener(()=>{
                
            PlayerPrefManager.SetInt(IconSelectPanel.userIconKey,index);

            IconSelectPanel.OnProfileChanged.Invoke(index);
        });
    }

}
