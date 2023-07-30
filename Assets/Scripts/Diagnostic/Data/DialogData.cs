using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogData 
{
    [SerializeField] public int activeTalker {get; private set;}
    [SerializeField] public bool isSelection  {get; private set;}

    [SerializeField] public string talkerName  {get; private set;}

    [SerializeField] public int spriteType  {get; private set;}

    [SerializeField] public int isMultiContent  {get; private set;}

    [SerializeField] public string content  {get; private set;}

    public DialogData(int activeTalker,bool isSelection,string talkerName,int spriteType,int isMultiContent,string content)
    {
        this.activeTalker = activeTalker;
        this.isSelection = isSelection;
        this.talkerName = talkerName;
        this.spriteType = spriteType;
        this.isMultiContent = isMultiContent;
        this.content = content;
    }
}
