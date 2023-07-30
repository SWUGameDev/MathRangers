using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MathQuestionExtension : MonoBehaviour
{
    [SerializeField] private MathPanelUIController mathPanelUIController;

    [SerializeField] private CountdownController countdownController;

    [SerializeField] private WJ_Connector wj_connector;

    [SerializeField] CurrentStatus currentStatus;
    public CurrentStatus  CurrentStatus => currentStatus;

    [Header("Status")]
    int     currentQuestionIndex;
    bool    isSolvingQuestion;
    float   questionSolveTime;

    private void Start() 
    {
        this.Setup();

        this.GetNewQuestionSet();
    }
    private void Setup()
    {
        if (wj_connector != null)
        {
            this.wj_connector.onGetLearning = new UnityEngine.Events.UnityEvent();
            this.wj_connector.onGetLearning.AddListener(() => GetLearning(0));
            
        }
    }

    private void GetNewQuestionSet()
    {
        this.wj_connector.GetQuestionByExtension();
    }

    private void Update()
    {
        if (isSolvingQuestion) 
            questionSolveTime += Time.deltaTime;
    }

    private void OnEnable() {

        if(this.currentQuestionIndex>=8)
        {
            this.mathPanelUIController.transform.gameObject.SetActive(false);
            return;
        }

        if(this.currentQuestionIndex!=0)
            this.GetLearning(this.currentQuestionIndex);
    }

    private void GetLearning(int _index)
    {
        if (_index == 0) currentQuestionIndex = 0;

        this.countdownController.StartCountdown(this.SetTimeout,this.SetTimerUIAnimation);

        this.isSolvingQuestion =  mathPanelUIController.MakeQuestion(wj_connector.cLearnSet.data.qsts[_index].textCn,
                    wj_connector.cLearnSet.data.qsts[_index].qstCn,
                    wj_connector.cLearnSet.data.qsts[_index].qstCransr,
                    wj_connector.cLearnSet.data.qsts[_index].qstWransr);
    }

    public void SelectAnswer(int selectedIndex)
    {
        bool isCorrect  = this.mathPanelUIController.textAnswers[selectedIndex].text.CompareTo(wj_connector.cLearnSet.data.qsts[currentQuestionIndex].qstCransr) == 0 ? true : false;
        string answerCwYn = isCorrect ? "Y" : "N";

        this.SendLearningSelectAnswer(selectedIndex,answerCwYn,isCorrect);

        Debug.Log("문제풀이 중"+ this.mathPanelUIController.textAnswers[selectedIndex].text + answerCwYn+ questionSolveTime + " 초");

        if (this.currentQuestionIndex >= 8) 
        {
            Debug.Log("8 문제 전체 풀이 완료");
        }
        else {
            Debug.Log($"문제풀이 진행 상황 ... {this.currentQuestionIndex} 문제 진행");
        }
        
    }
}
