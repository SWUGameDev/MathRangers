using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SelectInfoData
{
    public List<SelectInfo> selectInfoList {get; private set;}

    public class SelectInfo
    {
        [SerializeField] public int type  {get; private set;}
        [SerializeField] public string content  {get; private set;}

        public SelectInfo (int type, string content)
        {
            this.type = type;
            this.content = content;
        }
    }


    public SelectInfoData(List<SelectInfo> selectInfoList)
    {
        this.selectInfoList = selectInfoList;
    }
}
