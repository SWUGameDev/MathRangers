using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MathPanelUIInfo : MonoBehaviour
{

    [Header("Question Panels")]

    [SerializeField] private TMP_Text descriptionText; 

    [SerializeField] TEXDraw  equationText;

    [SerializeField] Button[] answerButtons = new Button[4]; 

    [SerializeField] TEXDraw[] TEXDrawAnswerText;

    public void SetMathPanelActive(bool isActive)
    {
        this.gameObject.SetActive(isActive);
    }

    public bool MakeQuestion(string questionDescription, string questionText, string questionAnswers, string questionWrongAnswer)
    {
        string      correctAnswer =  questionAnswers;
        string[]    wrongAnswers = questionWrongAnswer.Split(',');

        this.descriptionText.text = questionDescription;
        this.equationText.text = questionText;

        int answerCount = Mathf.Clamp(wrongAnswers.Length, 0, 3) + 1;

        for(int index=0; index<this.answerButtons.Length; index++)
        {
            if (index < answerCount)
                this.answerButtons[index].gameObject.SetActive(true);
            else
                this.answerButtons[index].gameObject.SetActive(false);
        }

        int answerIndex = Random.Range(0, answerCount);

        for(int i = 0, q = 0; i < answerCount; ++i, ++q)
        {
            if (i == answerIndex)
            {
                this.TEXDrawAnswerText[i].text = correctAnswer;
                --q;
            }
            else
                this.TEXDrawAnswerText[i].text = wrongAnswers[q];
        }
        
        return true;
    }

}
