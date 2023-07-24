using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CommandOperator{
    Mutiple,
    Add,
    Minus
}

public enum BuffType{
    Passive,
    Skill
}

[Serializable]
public class BuffName{
    public string koreanName;
    public string englishName;
}

[Serializable]
public class buffCommand {
    public string fieldName;

    public string target;

    public List<float> amountForLevel;

    public CommandOperator commandOperator;

} 

[CreateAssetMenu(fileName = "BuffInfo", menuName = "~/MathRangers/Assets/Scripts/Buff/BuffInfo.cs/BuffInfo", order = 0)]
public class BuffInfo : ScriptableObject {
    public int buffId;
    public BuffName buffName;

    public Sprite buffIcon;

    public BuffType buffType;
    public List<buffCommand> buffCommands;

    public List<float> buffStat;
}
