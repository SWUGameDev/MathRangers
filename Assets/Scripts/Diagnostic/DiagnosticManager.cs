using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CurrentStatus { WAITING, DIAGNOSIS, LEARNING }

public class DiagnosticManager : MonoBehaviour
{
    [SerializeField] WJ_Connector       wj_conn;
    [SerializeField] CurrentStatus      currentStatus;
    public CurrentStatus                CurrentStatus => currentStatus;

    [Header("Panels")]
    [SerializeField] GameObject         panel_diag_chooseDiff;  //난이도 선택 패널
    [SerializeField] GameObject         panel_question;         //문제 패널(진단,학습)

    [SerializeField] TMP_Text   textDescription;        //문제 설명 텍스트
    [SerializeField] TEXDraw   textEquation;           //문제 텍스트(※TextDraw로 변경 필요)
    [SerializeField] Button[]           btAnsr = new Button[4]; //정답 버튼들
    TEXDraw[]                textAnsr;                  //정답 버튼들 텍스트(※TextDraw로 변경 필요)

    [Header("Status")]
    int     currentQuestionIndex;
    bool    isSolvingQuestion;
    float   questionSolveTime;

    //[Header("For Debug")]
    //[SerializeField] WJ_DisplayText     wj_displayText;         //텍스트 표시용(필수X)
    //[SerializeField] Button             getLearningButton;      //문제 받아오기 버튼

    private void Awake()
    {
        textAnsr = new TEXDraw[btAnsr.Length];
        for (int i = 0; i < btAnsr.Length; ++i)

            textAnsr[i] = btAnsr[i].GetComponentInChildren<TEXDraw>();

        //wj_displayText.SetState("대기중", "", "", "");
    }

    private void OnEnable()
    {
        Setup();
    }

    private void Setup()
    {
        switch (currentStatus)
        {
            case CurrentStatus.WAITING:
                panel_diag_chooseDiff.SetActive(true);
                break;
        }

        if (wj_conn != null)
        {
            wj_conn.onGetDiagnosis.AddListener(() => GetDiagnosis());
            wj_conn.onGetLearning.AddListener(() => GetLearning(0));
        }
        else Debug.LogError("Cannot find Connector");
    }

    private void Update()
    {
        if (isSolvingQuestion) questionSolveTime += Time.deltaTime;
    }

    /// <summary>
    /// 진단평가 문제 받아오기
    /// </summary>
    public GameObject diagnosticEndPanel;
    private void GetDiagnosis()
    {
        switch (wj_conn.cDiagnotics.data.prgsCd)
        {
            case "W":
                MakeQuestion(wj_conn.cDiagnotics.data.textCn, 
                            wj_conn.cDiagnotics.data.qstCn, 
                            wj_conn.cDiagnotics.data.qstCransr, 
                            wj_conn.cDiagnotics.data.qstWransr);
                break;
            case "E":
                Debug.Log("진단평가 끝! 학습 단계로 넘어갑니다.");
                PlayerPrefs.SetInt("DiagnosticCompleted",1);
                this.InitializeUserInfo();
                currentStatus = CurrentStatus.LEARNING;
                diagnosticEndPanel.SetActive(true);
                break;
        }
    }

    [SerializeField] private TeamMatchManager teamMatchManager;

    private void InitializeUserInfo()
    {
        string nickName = PlayerPrefs.GetString(NicknameUIManager.NicknamePlayerPrefsKey);
        PlayerPrefs.SetString(MathQuestionExtension.AuthorizationPlayerPrefsKey,this.wj_conn.GetAuthorization());
        
        UserInfo userInfo = new UserInfo(FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserEmail(),nickName, this.wj_conn.GetAuthorization(),this.teamMatchManager.GetSelectedTeam());
        string serializedData = JsonUtility.ToJson(userInfo);
        Debug.Log($"[InitializeUserInfo] ${serializedData}");

        string userId = FirebaseRealtimeDatabaseManager.Instance.GetCurrentUserId();
        FirebaseRealtimeDatabaseManager.Instance.UploadUserInfo(userId,serializedData);
    }

    /// <summary>
    ///  n 번째 학습 문제 받아오기
    /// </summary>
    private void GetLearning(int _index)
    {
        if (_index == 0) currentQuestionIndex = 0;

        MakeQuestion(wj_conn.cLearnSet.data.qsts[_index].textCn,
                    wj_conn.cLearnSet.data.qsts[_index].qstCn,
                    wj_conn.cLearnSet.data.qsts[_index].qstCransr,
                    wj_conn.cLearnSet.data.qsts[_index].qstWransr);
    }

    /// <summary>
    /// 받아온 데이터를 가지고 문제를 표시
    /// </summary>
    private void MakeQuestion(string textCn, string qstCn, string qstCransr, string qstWransr)
    {
        panel_diag_chooseDiff.SetActive(false);
        panel_question.SetActive(true);

        string      correctAnswer;
        string[]    wrongAnswers;

        textDescription.text = textCn;
        textEquation.text = qstCn;

        correctAnswer = qstCransr;
        wrongAnswers    = qstWransr.Split(',');

        int ansrCount = Mathf.Clamp(wrongAnswers.Length, 0, 3) + 1;

        for(int i=0; i<btAnsr.Length; i++)
        {
            if (i < ansrCount)
                btAnsr[i].gameObject.SetActive(true);
            else
                btAnsr[i].gameObject.SetActive(false);
        }

        int ansrIndex = Random.Range(0, ansrCount);

        for(int i = 0, q = 0; i < ansrCount; ++i, ++q)
        {
            if (i == ansrIndex)
            {
                textAnsr[i].text = correctAnswer;
                --q;
            }
            else
                textAnsr[i].text = wrongAnswers[q];
        }
        isSolvingQuestion = true;
    }

    /// <summary>
    /// 답을 고르고 맞았는 지 체크
    /// </summary>
    public void SelectAnswer(int _idx)
    {
        bool isCorrect;
        string ansrCwYn = "N";

        switch (currentStatus)
        {
            case CurrentStatus.DIAGNOSIS:
                isCorrect   = textAnsr[_idx].text.CompareTo(wj_conn.cDiagnotics.data.qstCransr) == 0 ? true : false;
                ansrCwYn    = isCorrect ? "Y" : "N";

                isSolvingQuestion = false;

                wj_conn.Diagnosis_SelectAnswer(textAnsr[_idx].text, ansrCwYn, (int)(questionSolveTime * 1000));

                //wj_displayText.SetState("진단평가 중", textAnsr[_idx].text, ansrCwYn, questionSolveTime + " 초");
                this.DisplayAnswer(isCorrect,_idx);

                questionSolveTime = 0;
                break;

            case CurrentStatus.LEARNING:
                isCorrect   = textAnsr[_idx].text.CompareTo(wj_conn.cLearnSet.data.qsts[currentQuestionIndex].qstCransr) == 0 ? true : false;
                ansrCwYn    = isCorrect ? "Y" : "N";

                isSolvingQuestion = false;
                currentQuestionIndex++;

                wj_conn.Learning_SelectAnswer(currentQuestionIndex, textAnsr[_idx].text, ansrCwYn, (int)(questionSolveTime * 1000));

                //wj_displayText.SetState("문제풀이 중", textAnsr[_idx].text, ansrCwYn, questionSolveTime + " 초");

                if (currentQuestionIndex >= 8) 
                {
                    panel_question.SetActive(false);
                    //wj_displayText.SetState("문제풀이 완료", "", "", "");
                }
                else GetLearning(currentQuestionIndex);

                questionSolveTime = 0;
                break;
        }
    }

    public Image characterImage;
    public Image OXImage;
    public Image OXBackgroundImage;
    public Sprite[] characterAnswerImages;
    public Sprite[] OXImages;

    private void DisplayAnswer(bool isCorrect,int index)
    {
        this.OXBackgroundImage.gameObject.SetActive(true);

        if(isCorrect)
        {
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_DIAGNOSTIC_O);

            this.OXImage.sprite = this.OXImages[0];
            this.characterImage.sprite = this.characterAnswerImages[1];
        }else{
            SoundManager.Instance.PlayAffectSoundOneShot(effectsAudioSourceType.SFX_DIAGNOSTIC_X);
            
            this.OXImage.sprite = this.OXImages[1];
            this.characterImage.sprite = this.characterAnswerImages[2];
        }
        this.StartCoroutine(this.Pass(isCorrect,index));
    }

    private IEnumerator Pass(bool isCorrect,int index)
    {
        yield return new WaitForSeconds(0.7f);
        this.characterImage.sprite = this.characterAnswerImages[0];
        this.OXBackgroundImage.gameObject.SetActive(false);
        panel_question.SetActive(false);

        wj_conn.Diagnosis_SelectAnswer(textAnsr[index].text, isCorrect ? "Y" : "N", (int)(questionSolveTime * 1000));
    }


    public void DisplayCurrentState(string state, string myAnswer, string isCorrect, string svTime)
    {
        //if (wj_displayText == null) return;
        Debug.Log($"DisplayCurrentState {state}");
        //wj_displayText.SetState(state, myAnswer, isCorrect, svTime);
    }

    #region Unity ButtonEvent
    public void ButtonEvent_ChooseDifficulty(int a)
    {
        currentStatus = CurrentStatus.DIAGNOSIS;
        wj_conn.FirstRun_Diagnosis(a);
    }
    public void ButtonEvent_GetLearning()
    {
        wj_conn.Learning_GetQuestion();
        //wj_displayText.SetState("문제풀이 중", "-", "-", "-");
    }
    #endregion
}
