using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogData 
{
    [SerializeField] private int activeTalker;
    [SerializeField] private bool isSelection;

    [SerializeField] private string talkerName;

    [SerializeField] private int spriteType;

    [SerializeField] private string content;
}
