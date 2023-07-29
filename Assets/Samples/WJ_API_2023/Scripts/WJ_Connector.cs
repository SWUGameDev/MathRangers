using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using WjChallenge;

public class WJ_Connector : MonoBehaviour
{
    [Header("My Info")]
    public string strGameCD;        //�����ڵ�
    public string strGameKey;       //����Ű(Api Key)

    private string strAuthorization;
    private string strMBR_ID;       //��� ID
    private string strDeviceNm;     //����̽� �̸�
    private string strOsScnCd;      //OS
    private string strGameVer;      //���� ����

    #region StoredData
    [HideInInspector]
    public DN_Response                  cDiagnotics     = null; //���� - ���� Ǯ���ִ� ������ ����
    [HideInInspector]
    public Response_Learning_Setting    cLearnSet       = null; //�н� - �н� ���� ��û �� �޾ƿ� �н� ������
    [HideInInspector]
    public Response_Learning_Progress   cLearnProg      = null; //�н� - �н� �Ϸ� �� �޾ƿ� ���
    [HideInInspector]
    public List<Learning_MyAnsr>        cMyAnsrs        = null;
    [HideInInspector]
    public Request_DN_Progress          result          = null;
    [HideInInspector]
    public string                       qstCransr       = "";

    public static readonly string userPlayerPrefsMBRKey = "userMBRKey";

    public static readonly string userPlayerPrefsAuthorizationKey = "userAuthorizationKey";

    #endregion

    #region UnityEvents
    [HideInInspector]
    public UnityEvent onGetDiagnosis;
    [HideInInspector]
    public UnityEvent onGetLearning;
    #endregion

    private void Awake()
    {
        if (SystemInfo.deviceType == DeviceType.Desktop) 
            strDeviceNm = "PC";
        else 
            strDeviceNm = SystemInfo.deviceModel;

        strOsScnCd      = SystemInfo.operatingSystem;
        strGameVer      = Application.version;

        if (strOsScnCd.Length >= 15) strOsScnCd = strOsScnCd.Substring(0, 14);

        Setting_MBR_ID();
    }

    //���� �ð��� �������� MBR ID ����
    private void Setting_MBR_ID()
    {
        if(PlayerPrefs.HasKey(WJ_Connector.userPlayerPrefsMBRKey))
        {
            this.strMBR_ID = PlayerPrefs.GetString(WJ_Connector.userPlayerPrefsMBRKey);
        }else{
            DateTime dt = DateTime.Now;
            strMBR_ID = string.Format("{0}{1:0000}{2:00}{3:00}{4:00}{5:00}{6:00}{7:000}", strGameCD, dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, dt.Millisecond);
            PlayerPrefs.SetString(WJ_Connector.userPlayerPrefsMBRKey,strMBR_ID);
        }
    }

    #region Function Progress

    /// <summary>
    /// ������ ù ���� �� ������ ���
    /// </summary>
    private IEnumerator Send_Diagnosis(int level)
    {
        Request_DN_Setting request = new Request_DN_Setting();

        request.gameCd = strGameCD;
        request.mbrId = strMBR_ID;
        request.deviceNm = strDeviceNm;
        request.gameVer = strGameVer;
        request.osScnCd = strOsScnCd;
        request.langCd = "KO";
        request.timeZone = TimeZoneInfo.Local.BaseUtcOffset.Hours;

        switch (level)
        {
            case 0: request.bgnLvl = "A"; break;
            case 1: request.bgnLvl = "B"; break;
            case 2: request.bgnLvl = "C"; break;
            case 3: request.bgnLvl = "D"; break;
            default: request.bgnLvl = "A"; break;
        }

        yield return StartCoroutine(UWR_Post<Request_DN_Setting, DN_Response>(request, "https://prd-brs-relay-model.mathpid.com/api/v1/contest/diagnosis/setting", false));

        onGetDiagnosis.Invoke();

        yield return null;
    }

    /// <summary>
    /// ������ �� ���� Ǯ�� �� ������ ���
    /// </summary>
    private IEnumerator SendProgress_Diagnosis(string _prgsCd, string _qstCd, string _qstCransr, string _ansrCwYn, long _sid, long _nQstDelayTime)
    {
        Request_DN_Progress request = new Request_DN_Progress();
        request.mbrId = strMBR_ID;
        request.gameCd = strGameCD;
        request.prgsCd = _prgsCd;// "W";    // W: ���� ����    E: ���� �Ϸ�    X: ��Ÿ ���?
        request.qstCd = _qstCd;             // ���� �ڵ�
        request.qstCransr = _qstCransr;     // �Է��� �䳻��
        request.ansrCwYn = _ansrCwYn;//"Y"; // ���� ����
        request.sid = _sid;                 // ���� ID
        request.slvTime = _nQstDelayTime;//5000;

        yield return StartCoroutine(UWR_Post<Request_DN_Progress, DN_Response>(request, "https://prd-brs-relay-model.mathpid.com/api/v1/contest/diagnosis/progress", true));

        onGetDiagnosis.Invoke();

        yield return null;
    }

    /// <summary>
    /// �н� ������ �޾ƿ��� ���� ������ ���
    /// </summary>
    private IEnumerator Send_Learning()
    {
        Request_Learning_Setting request = new Request_Learning_Setting();

        request.gameCd = strGameCD;
        request.mbrId = strMBR_ID;
        request.gameVer = strGameVer;
        request.osScnCd = strOsScnCd;
        request.deviceNm = strDeviceNm;
        request.langCd = "KO";
        request.timeZone = TimeZoneInfo.Local.BaseUtcOffset.Hours;

        request.mathpidId = "";

        yield return StartCoroutine(UWR_Post<Request_Learning_Setting, Response_Learning_Setting>(request, "https://prd-brs-relay-model.mathpid.com/api/v1/contest/learning/setting", true));

        onGetLearning.Invoke();

        cMyAnsrs = new List<Learning_MyAnsr>();

        yield return null;
    }

    private IEnumerator Send_Learning_Running()
    {
        Request_Learning_Setting request = new Request_Learning_Setting();

        request.gameCd = strGameCD;
        request.mbrId = strMBR_ID;
        request.gameVer = strGameVer;
        request.osScnCd = strOsScnCd;
        request.deviceNm = strDeviceNm;
        request.langCd = "KO";
        request.timeZone = TimeZoneInfo.Local.BaseUtcOffset.Hours;

        request.mathpidId = strMBR_ID;

        yield return StartCoroutine(UWR_Post<Request_Learning_Setting, Response_Learning_Setting>(request, "https://prd-brs-relay-model.mathpid.com/api/v1/contest/learning/setting", true));

        onGetLearning.Invoke();

        cMyAnsrs = new List<Learning_MyAnsr>();

        yield return null;
    }

    /// <summary>
    /// �н� 8���� �Ϸ��Ҷ����� ������ ����Ͽ� �н� ����� �޾ƿ�
    /// </summary>
    private IEnumerator SendProgress_Learning()
    {
        Request_Learning_Progress request = new Request_Learning_Progress();

        request.gameCd = strGameCD;
        request.mbrId = strMBR_ID;
        request.prgsCd = "E";
        request.sid = cLearnSet.data.sid;
        request.bgnDt = cLearnSet.data.bgnDt;

        request.data = cMyAnsrs;

        yield return StartCoroutine(UWR_Post<Request_Learning_Progress, Response_Learning_Progress>(request, "https://prd-brs-relay-model.mathpid.com/api/v1/contest/learning/progress", true));

        yield return null;
    }

    /// <summary>
    /// UnityWebRequest�� ����Ͽ� ������ ������ ���
    /// </summary>
    /// <typeparam name="TRequest"> ������ ���ϴ� Ÿ�� </typeparam>
    /// <typeparam name="TResponse"> �޾ƿ� Ÿ�� </typeparam>
    /// <param name="request"> ������ �� </param>
    /// <param name="url"> URL </param>
    /// <param name="isSendAuth"> Authorization ��� ������ </param>
    /// <returns></returns>
    private IEnumerator UWR_Post<TRequest, TResponse>(TRequest request, string url, bool isSendAuth)
    where TRequest : class
    where TResponse : class
    {
        string strBody = JsonUtility.ToJson(request);

        using (UnityWebRequest uwr = UnityWebRequest.Post(url, string.Empty))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(strBody);
            uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            uwr.SetRequestHeader("Content-Type", "application/json");
            uwr.SetRequestHeader("x-api-key", strGameKey);

            Debug.Log(strAuthorization);

            if (isSendAuth)
            {
                if(PlayerPrefs.HasKey(WJ_Connector.userPlayerPrefsAuthorizationKey))
                {
                    this.strAuthorization = PlayerPrefs.GetString(WJ_Connector.userPlayerPrefsAuthorizationKey);
                }
                uwr.SetRequestHeader("Authorization", strAuthorization);
            }

            uwr.timeout = 5;

            yield return uwr.SendWebRequest();

            Debug.Log($"Request => {strBody}");

            if (uwr.error == null)
            {
                TResponse output = default;

                try
                {
                    output = JsonUtility.FromJson<TResponse>(uwr.downloadHandler.text);
                }
                catch (Exception e) { Debug.LogError(e.Message); }

                cDiagnotics = null;
                cLearnSet = null;
                cLearnProg = null;

                switch (output)
                {
                    case DN_Response dnResponse:
                        cDiagnotics = dnResponse;
                        qstCransr = cDiagnotics.data.qstCransr;
                        break;

                    case Response_Learning_Setting ResponselearnSet:
                        cLearnSet = ResponselearnSet;
                        break;

                    case Response_Learning_Progress ResponselearnProg:
                    {
                            cLearnProg = ResponselearnProg;
                            this.UploaGameResult(this.cLearnProg);
                            break;
                    }

                    default:
                        Debug.LogError("type error - output type : " + output.GetType().ToString());
                        break;
                }

                if (uwr.GetResponseHeaders().ContainsKey("Authorization")) 
                {
                    strAuthorization = uwr.GetResponseHeader("Authorization");
                    PlayerPrefs.SetString(WJ_Connector.userPlayerPrefsAuthorizationKey,this.strAuthorization);
                }
            }
            else
            {
                Debug.LogError(uwr.error.ToString());
            }
            Debug.Log($"Authorization => {uwr.GetResponseHeader("Authorization")}");
            Debug.Log($"Response => {uwr.downloadHandler.text}");
            uwr.Dispose();
        }
    }

    private void UploaGameResult(Response_Learning_Progress response_Learning_Progress)
    { 
        //FirebaseRealtimeDatabaseManager.Instance.UploadUserInfo
    }
    #endregion

    #region Public Method

    public void FirstRun_Diagnosis(int diff)
    {
        StartCoroutine(Send_Diagnosis(diff));
    }

    public void Learning_GetQuestion()
    {
        StartCoroutine(Send_Learning());
    }

    public void GetQuestionByExtension()
    {
        StartCoroutine(Send_Learning_Running());   
    }

    /// <summary>
    /// ���� - ���� ������ ���� �� ����
    /// </summary>
    public void Diagnosis_SelectAnswer(string _cransr, string _ansrYn, long _slvTime = 5000)
    {
        long sid            = cDiagnotics.data.sid;
        string prgsCd       = cDiagnotics.data.prgsCd;
        string qstCd        = cDiagnotics.data.qstCd;

        string qstCransr    = _cransr;
        string ansrCwYn     = _ansrYn;
        long slvTime        = _slvTime;

        StartCoroutine(SendProgress_Diagnosis(prgsCd, qstCd, qstCransr, ansrCwYn, sid, slvTime));
    }

    /// <summary>
    /// �н� - ���� ������ ����, Ǭ ������ 8���� �Ǹ� ����
    /// </summary>
    public void Learning_SelectAnswer(int _index, string _cransr, string _ansrYn, long _slvTime = 5000)
    {
        if(cMyAnsrs == null) cMyAnsrs = new List<Learning_MyAnsr>();

        cMyAnsrs.Add(new Learning_MyAnsr(cLearnSet.data.qsts[_index - 1].qstCd, _cransr, _ansrYn, 0));

        if(cMyAnsrs.Count >= 8)
        {
            StartCoroutine(SendProgress_Learning());
        }
    }
    
    #endregion

    #region ForTest
    public void Diagnosis_SelectAnswer_Forced()
    {
        long sid = cDiagnotics.data.sid;
        string prgsCd = cDiagnotics.data.prgsCd;
        string qstCd = cDiagnotics.data.qstCd;

        string qstCransr = cDiagnotics.data.qstCransr;
        string ansrCwYn = "Y";
        long slvTime = 5000;

        StartCoroutine(SendProgress_Diagnosis(prgsCd, qstCd, qstCransr, ansrCwYn, sid, slvTime));
    }
    #endregion


}
