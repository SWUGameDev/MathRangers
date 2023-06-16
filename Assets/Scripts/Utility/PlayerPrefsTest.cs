using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class PlayerPrefsTest : MonoBehaviour
{
    [MenuItem("PlayerPrefs/DeleteAll")]
    public static void DeleteAll() 
    {
        PlayerPrefs.DeleteAll();
    }    // Start is called before the first frame update

}
