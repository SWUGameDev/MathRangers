using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public class RunSceneUIManager : UI_Base
{
    [SerializeField] GameObject playerGameObject;
    private RunPlayer player;
    private int eatCheeseNumber = 0;
    [SerializeField] TMP_Text eatCheeseNumberText;

    enum Texts
    {
        correctAnswerRate,
        // eatCheeseNumberText,
    }

    private void Awake()
    {
        player = playerGameObject.GetComponent<RunPlayer>();
        player.onEatCheese.AddListener(this.EatCheeseNumber);
    }

    private void Start()
    {
        // Bind<TMP_Text>(typeof(Texts));
        // Get<TMP_Text>((int)Texts.eatCheeseNumberText).text = eatCheeseNumber.ToString();
        eatCheeseNumberText.text = eatCheeseNumber.ToString();
    }

    public void EatCheeseNumber()
    {
        Debug.Log("eatCheeseNumber");
        eatCheeseNumber++;
        // Get<TMP_Text>((int)Texts.eatCheeseNumberText).text = eatCheeseNumber.ToString();
        eatCheeseNumberText.text = eatCheeseNumber.ToString();
    }
}
