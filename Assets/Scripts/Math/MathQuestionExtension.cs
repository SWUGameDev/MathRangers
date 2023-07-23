using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathQuestionExtension : MonoBehaviour
{
    [SerializeField]
    private MathPanelUIController mathPanelUIController;

    [SerializeField]
    private CountdownController countdownController;

    [SerializeField]
    private WJ_Connector wj_conn;

    [SerializeField] CurrentStatus      currentStatus;
    public CurrentStatus  CurrentStatus => currentStatus;

    [Header("Status")]
    int     currentQuestionIndex;
    bool    isSolvingQuestion;
    float   questionSolveTime;

    public static readonly string AuthorizationPlayerPrefsKey = "AuthorizationPlayerPrefsKey";


    private void Start() {

        Debug.Log("Test");

        this.Setup();

        this.GetQuestionSet();
    }
    private void Setup()
    {
        if (wj_conn != null)
        {
            this.wj_conn.onGetLearning = new UnityEngine.Events.UnityEvent();
            this.wj_conn.onGetLearning.AddListener(() => GetLearning(0));
            this.wj_conn.SetAuthorization(PlayerPrefs.GetString(MathQuestionExtension.AuthorizationPlayerPrefsKey));
            this.wj_conn.SetAuthorization("Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJjaG4iOiJFMTUiLCJtYnIiOiJFMTUyMDIzMDcyMjE2NTgzOTcyMyIsImlhdCI6MTY1MTU2MDYxNywiZXhwIjozNTQ2ODA2NTE1fQ.uI1rQddXMLwM26HWfxR9yFPnTKA_wt0b1rfmx_elrpY");
            
        }
    }

    private void GetQuestionSet()
    {
        this.wj_conn.GetQuestionByExtension();
    }

    private void Update()
    {
        if (isSolvingQuestion) 
            questionSolveTime += Time.deltaTime;
    }

    private void OnEnable() {

        if(this.currentQuestionIndex!=0)
            this.GetLearning(this.currentQuestionIndex);
    }

    // 문제 셋팅 8번 반복되어야함
    private void GetLearning(int _index)
    {
        if (_index == 0) currentQuestionIndex = 0;

        this.countdownController.StartCountdown(this.SetTimeout,this.SetTimerUIAnimation);

        this.isSolvingQuestion =  mathPanelUIController.MakeQuestion(wj_conn.cLearnSet.data.qsts[_index].textCn,
                    wj_conn.cLearnSet.data.qsts[_index].qstCn,
                    wj_conn.cLearnSet.data.qsts[_index].qstCransr,
                    wj_conn.cLearnSet.data.qsts[_index].qstWransr);
    }

    public void SelectAnswer(int _idx)
    {
        bool isCorrect  = this.mathPanelUIController.textAnswers[_idx].text.CompareTo(wj_conn.cLearnSet.data.qsts[currentQuestionIndex].qstCransr) == 0 ? true : false;
        string ansrCwYn = isCorrect ? "Y" : "N";

        isSolvingQuestion = false;
        currentQuestionIndex++;

        wj_conn.Learning_SelectAnswer(currentQuestionIndex, this.mathPanelUIController.textAnswers[_idx].text, ansrCwYn, (int)(questionSolveTime * 1000));

        Debug.Log("문제풀이 중"+ this.mathPanelUIController.textAnswers[_idx].text + ansrCwYn+ questionSolveTime + " 초");

        this.mathPanelUIController.SetResultImage(isCorrect);

        this.countdownController.StopCountdown();

        if (currentQuestionIndex >= 8) 
        {
            Debug.Log("8 문제 전체 풀이 완료");
        }
        else {
            Debug.Log($"문제풀이 진행 상황 ... {this.currentQuestionIndex} 문제 진행");
        }

        questionSolveTime = 0;
        
    }

    private float timerAnimTime = 2f;

    private void SetTimerUIAnimation(float time)
    {
        if(time==this.timerAnimTime)
        {
            this.mathPanelUIController.SetTimerTimeoutUI();
        }
    }

    private void SetTimeout()
    {
        isSolvingQuestion = false;
        currentQuestionIndex++;

        wj_conn.Learning_SelectAnswer(currentQuestionIndex, this.mathPanelUIController.textAnswers[0].text, "N", (int)(questionSolveTime * 1000));
        this.mathPanelUIController.SetResultImage(false);

        questionSolveTime = 0;
    }
}