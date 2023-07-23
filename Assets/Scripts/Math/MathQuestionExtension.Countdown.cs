using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MathQuestionExtension : MonoBehaviour
{

    [SerializeField] private float timerAnimTime = 2f;

    private void SetTimerUIAnimation(float time)
    {
        if(time==this.timerAnimTime)
        {
            this.mathPanelUIController.SetTimerTimeoutUI();
        }
    }

    private void SetTimeout()
    {
        this.SendLearningSelectAnswer(0,"N");
    }

    private void SendLearningSelectAnswer(int index,string answerCwYn)
    {
        isSolvingQuestion = false;
        currentQuestionIndex++;

        wj_connector.Learning_SelectAnswer(currentQuestionIndex, this.mathPanelUIController.textAnswers[index].text, answerCwYn, (int)(questionSolveTime * 1000));

        questionSolveTime = 0;
    }
}
