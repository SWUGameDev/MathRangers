using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CommandOperator{
    Multiple,
    Add,
    Minus,
    Plus,
    Default
}

public enum AbilityType{
    Passive,
    Active
}

[Serializable]
public class AbilityName{
    public string koreanName;
    public string englishName;
}

[Serializable]
public class BuffDescription
{
    public string koreanDescription;
    public string englishDescription;
}

[Serializable]
public class AbilityCommand {
    public string fieldName;

    public string target;

    public List<float> amountForLevel;

    public CommandOperator commandOperator;

} 

[CreateAssetMenu(fileName = "_AbilityInfo", menuName = "MathRangers/Assets/AbilityInfo", order = 0)]
public class AbilityInfo : ScriptableObject {
    public int abilityId;

    public AbilityName abilityName;

    public BuffDescription abilityDescription;

    public Sprite abilityIcon;

    public AbilityType abilityType;

    public bool isSelected;

    public List<AbilityCommand> abilityCommands;
}
