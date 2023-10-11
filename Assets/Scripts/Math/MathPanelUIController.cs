using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public partial class MathPanelUIController : MonoBehaviour
{

    [Header("Question Panels")]

    [SerializeField] private TMP_Text descriptionText; 

    [SerializeField] Text  equationText;

    [SerializeField] Button[] answerButtons = new Button[4]; 

    [SerializeField] Text[] TEXDrawAnswerText;

    public Text[] textAnswers {  get => this.TEXDrawAnswerText;}

    [SerializeField] private Image resultImage;

    [SerializeField] private Sprite[] resultSprites;

    [SerializeField] private Animator mathPanelAnimator;

    private readonly string exitAnimKey = "IsExited";

    [SerializeField] private BuffSelectPanelUIController buffSelectPanelUIController;

    [SerializeField] private RunSceneUIManager runSceneUIManager;
    public static UnityEvent<bool> OnSolveWrong = new UnityEvent<bool>();
    public static UnityEvent OnMathDamage = new UnityEvent();

    public void SetMathPanelActive(bool isActive)
    {
        SoundManager.Instance?.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_POPUP);
        
        this.gameObject.SetActive(isActive);
    }

    public bool MakeQuestion(string questionDescription, string questionText, string questionAnswers, string questionWrongAnswer)
    {
        Debug.Log(questionDescription);
        string      correctAnswer =  questionAnswers;
        string[]    wrongAnswers = questionWrongAnswer.Split(',');

        this.descriptionText.text = "Q. " + questionDescription;
        questionText = questionText.Replace("align","flalign");
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

        Debug.Log("answerIndex"+answerIndex);

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

    public void SetResultImage(bool isCorrect)
    {
        this.resultImage.gameObject.SetActive(true);

        if(isCorrect)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_DIAGNOSTIC_O);

            this.resultImage.sprite = this.resultSprites[1];
        }else{

            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_DIAGNOSTIC_X);

            this.resultImage.sprite = this.resultSprites[0];
        }

        this.StartCoroutine(this.SetResultImageCoroutine(isCorrect));
    }


    public IEnumerator SetResultImageCoroutine(bool isCorrect)
    {
        this.SetAnswerButtonActive(false);
        
        yield return new WaitForSeconds(0.5f);

        this.mathPanelAnimator.SetTrigger(this.exitAnimKey);

        yield return new WaitForSeconds(0.7f);

        this.buffSelectPanelUIController.SetBuffPanelActive(isCorrect);

        this.ResetUISetting();

        this.ResetTimerUI();

        if(!isCorrect)
        {
            this.transform.gameObject.SetActive(false);

            OnSolveWrong?.Invoke(true);
            OnMathDamage?.Invoke();
        }
    }

    private void SetAnswerButtonActive(bool isActable)
    {
        foreach (var button in this.answerButtons)
        {
            button.interactable = isActable;
        }
    }

    public void ResetUISetting()
    {
        this.SetAnswerButtonActive(true);

        this.resultImage.gameObject.SetActive(false);
    }

}
