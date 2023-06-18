using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamName = System.String;
using totalScore = System.Int64;
using System.Linq;
public partial class RankingUIManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> teamRankingPanels;

    private Dictionary<TeamName,TeamRackingUIItem> teamRackingUIItems;
    public void initializeTeamRankSettings()
    {
        this.teamRackingUIItems = new Dictionary<TeamName, TeamRackingUIItem>();

        foreach (var panel in this.teamRankingPanels)
        {
            TeamRackingUIItem panelController = panel.GetComponent<TeamRackingUIItem>();
            this.teamRackingUIItems[panelController.TeamName] = panelController;    
        }
    }

    public void SetTeamRankUI(IOrderedEnumerable<KeyValuePair<TeamName, totalScore>> teamRackingInfos)
    {
        int rank = 0;
        foreach(var infos in teamRackingInfos)
        {
            //TODO : Background Color 설정
            this.teamRackingUIItems[infos.Key]?.SetTeamRankText((++rank).ToString());
        }
    }

}
