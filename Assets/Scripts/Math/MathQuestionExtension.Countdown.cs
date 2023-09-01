using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class MathQuestionExtension : MonoBehaviour
{

    [SerializeField] private float timerAnimTime = 2f;
    public static UnityEvent OnMathTimeOutDamage = new UnityEvent();

    private void SetTimerUIAnimation(float time)
    {
        if(time==this.timerAnimTime)
        {
            SoundManager.Instance?.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_TIME_LIMIT_TEN_REMAINED);

            this.mathPanelUIController.SetTimerTimeoutUI();
        }
    }

    private void SetTimeout()
    {
        SoundManager.Instance?.StopEffectAudioSource();

        OnQuestionSolved?.Invoke(this.currentQuestionIndex + 1, false);
        OnMathTimeOutDamage?.Invoke();

        this.SendLearningSelectAnswer(0,"N",false);
    }

    private void SendLearningSelectAnswer(int index,string answerCwYn,bool isCorrect)
    {
        isSolvingQuestion = false;
        currentQuestionIndex++;

        wj_connector.Learning_SelectAnswer(currentQuestionIndex, this.mathPanelUIController.textAnswers[index].text, answerCwYn, (int)(questionSolveTime * 1000));

        this.mathPanelUIController.SetResultImage(isCorrect);
        this.countdownController.StopCountdown();

        questionSolveTime = 0;
    }
}
