using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunBossItemManager : MonoBehaviourSingleton<RunBossItemManager>
{
    private int cheeseFromRunGame;

    public int CheeseFromRunGame
    {
        get { return cheeseFromRunGame; }
        set { cheeseFromRunGame = value; }
    }
}
