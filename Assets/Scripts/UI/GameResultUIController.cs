using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Newtonsoft.Json;
using WjChallenge;

public enum GameResultType
{
    EmergencyAbortOfMission,
    MissionFail,
    MissionSuccess
}

public class GameResultContent
{
    public string gameResultTitle;

    public List<GameResultNPCContent> gameResultContentList;

    public GameResultContent()
    {

    }
}

[Serializable]
public class GameResultNPCContent
{
    public string lrnPrgsStsCd;

    public string DescriptionKorean;

    public string DescriptionEnglish;

    public GameResultNPCContent()
    {

    }
}

[Serializable]
public class GameResultData
{
    public long damage;
    public int minionCount;
    public int cheeseCount;

    public GameResultData()
    {

    }

    public GameResultData(int cheeseCount)
    {
        this.cheeseCount = cheeseCount;
        this.damage = 0;
        this.minionCount = 0;
    }

    public GameResultData(long damage,int minionCount,int cheeseCount)
    {
        this.cheeseCount = cheeseCount;
        this.damage = damage;
        this.minionCount = minionCount;
    }
}

public partial class GameResultUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private Image npcImage;

    [SerializeField] private Sprite[] npcSprites;

    [SerializeField] private TMP_Text npcChatText;

    [SerializeField] private TMP_Text explanationResultText;

    [SerializeField] private TMP_Text accuracyText;

    [SerializeField] private TMP_Text explanationSpeedText;

    [SerializeField] private TMP_Text damageScoreText;

    [SerializeField] private TMP_Text minionCountText;

    [SerializeField] private TMP_Text earningText;

    [SerializeField] private TextAsset[] gameResultContentText;

    [SerializeField] private string[] gameResultTitleContents;

    [SerializeField] private GameObject[] runGameResultGameObject;

    [SerializeField] private GameObject particlePrefab;

    private Dictionary<GameResultType,GameResultContent> gameResultContentDictionary;

    public static readonly string responseLearningProgressDataKey = "responseLearningProgressDataKey";


    private void Awake()
    {
        this.InitializeGameResultData();
    }

    private void InitializeGameResultData()
    {
        this.gameResultContentDictionary = new Dictionary<GameResultType,GameResultContent>();

        for(int index = 0;index<this.gameResultContentText.Length;index++)
        {
            List<GameResultNPCContent> list = JsonConvert.DeserializeObject<List<GameResultNPCContent>>(this.gameResultContentText[index].text);

            var gameResultContent = new GameResultContent();
            gameResultContent.gameResultContentList = list;
            gameResultContent.gameResultTitle = this.gameResultTitleContents[index];

            gameResultContentDictionary[(GameResultType)index] = gameResultContent;
        }
    }

    // TODO : 테스트 코드입니다. 호출시 아래 테스트 코드와 같이 진행되면 됩니다.
    public void ResultTest()
    {
        // TODO: UI 예외처리 필요
        if(!PlayerPrefs.HasKey(GameResultUIController.responseLearningProgressDataKey)) 
            return;

        string data = PlayerPrefs.GetString(GameResultUIController.responseLearningProgressDataKey);
        Response_Learning_ProgressData response_Learning_ProgressData = JsonConvert.DeserializeObject<Response_Learning_ProgressData>(data);

        this.SetResult(GameResultType.MissionSuccess, new GameResultData(5455315487300,3245,433),response_Learning_ProgressData);
    }

    public void SetPanelUnActive()
    {
        this.transform.gameObject.SetActive(false);
    }

    public void SetResult(GameResultType type,GameResultData gameResultData,Response_Learning_ProgressData progressData)
    {
        this.transform.gameObject.SetActive(true);

        if(type == GameResultType.EmergencyAbortOfMission)
        {
            foreach (GameObject item in this.runGameResultGameObject)
            {
                item.SetActive(false);
            }
        }

        if(type == GameResultType.MissionSuccess)
        {
            this.PlayParticle();

            this.npcImage.sprite = this.npcSprites[0];
        }

        this.SetNPCContent(type, progressData.lrnPrgsStsCd);

        this.SetResponseLearningProgressData(progressData);

        this.damageScoreText.text = gameResultData.damage.ToString("N0");
        this.minionCountText.text = gameResultData.minionCount.ToString("N0");
        this.earningText.text = gameResultData.cheeseCount.ToString("N0");

        this.titleText.text = this.gameResultContentDictionary[type].gameResultTitle;

        this.SaveGameResultData(type,gameResultData,progressData);
    }

    private void SaveGameResultData(GameResultType type,GameResultData gameResultData,Response_Learning_ProgressData progressData)
    {

        GameResultInfo gameResultInfo = new GameResultInfo(type,gameResultData,progressData);
        string gameResultInfoString = JsonConvert.SerializeObject(gameResultInfo);

        FirebaseRealtimeDatabaseManager firebaseRealtimeDatabaseManager = FirebaseRealtimeDatabaseManager.Instance;

        string userId = firebaseRealtimeDatabaseManager.GetCurrentUserId();
        
        this.SaveUserHighScore(firebaseRealtimeDatabaseManager,userId,gameResultData.damage);

        firebaseRealtimeDatabaseManager.UploadGameResultInfo(userId,gameResultInfoString,()=>{this.UploadGameResultDebug(gameResultInfoString);});

    }

    public static readonly string UserHighScoreKey = "Key_UserHighScore";

    private bool SaveUserHighScore(FirebaseRealtimeDatabaseManager firebaseRealtimeDatabaseManager,string userId,long currentScore)
    {
        long prevHighScore = 0;

        if(PlayerPrefs.HasKey(GameResultUIController.UserHighScoreKey))
        {
            prevHighScore = long.Parse(PlayerPrefs.GetString(GameResultUIController.UserHighScoreKey));
        }else{
            PlayerPrefs.SetString(GameResultUIController.UserHighScoreKey,currentScore.ToString());

            int iconIndex = PlayerPrefs.GetInt(IconSelectPanel.userIconKey);
            string nickName = PlayerPrefs.GetString(NicknameUIManager.NicknamePlayerPrefsKey);
            int teamType = PlayerPrefs.GetInt(DiagnosticManager.TeamTypeKey);

            UserRankInfo userRankInfo = new UserRankInfo(userId,iconIndex,nickName,currentScore,teamType);
            string serializedUserRankInfo = JsonConvert.SerializeObject(userRankInfo);

            firebaseRealtimeDatabaseManager.UploadInitializedUserRankInfo(userId,serializedUserRankInfo);
        }

        if(prevHighScore<currentScore)
        {
            PlayerPrefs.SetString(GameResultUIController.UserHighScoreKey,currentScore.ToString());

            firebaseRealtimeDatabaseManager.UpdateUserScoreInfo(userId,currentScore.ToString());

            return true;
        }

        return false;
    }

    private void UploadGameResultDebug(string data)
    {
        Debug.Log($"Upload Data : {data}");
    }

    private void PlayParticle()
    {
        var particleCanvas = GameObject.Instantiate(this.particlePrefab);
        Canvas canvas = particleCanvas.GetComponent<Canvas>();
        canvas.transform.SetParent(this.transform);
        canvas.worldCamera = Camera.main;
    }
    
}
