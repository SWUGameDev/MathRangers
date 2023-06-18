using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeamName = System.String;
using totalScore = System.Int64;
using System.Linq;
public partial class RankingUIManager : MonoBehaviour
{
    [Header("Team Tab UI")]

    [SerializeField] private List<GameObject> teamRankingPanels;

    private Dictionary<TeamName,TeamRackingUIItem> teamRackingUIItems;
    private void initializeTeamRankSettings()
    {
        this.teamRackingUIItems = new Dictionary<TeamName, TeamRackingUIItem>();

        foreach (var panel in this.teamRankingPanels)
        {
            TeamRackingUIItem panelController = panel.GetComponent<TeamRackingUIItem>();
            this.teamRackingUIItems[panelController.TeamName] = panelController;    
        }
    }

    public bool SetTeamRankUI(IOrderedEnumerable<KeyValuePair<TeamName, totalScore>> teamRackingInfos)
    {
        this.initializeTeamRankSettings();

        if(teamRackingInfos == null)
            return false;
        
        int rank = 0;
        foreach(var infos in teamRackingInfos)
        {
            this.teamRackingUIItems[infos.Key]?.SetTeamRankBackGroundColor(this.rankBackgroundColors[rank]);
            this.teamRackingUIItems[infos.Key]?.SetTeamRankText((++rank).ToString());
        }
        return true;
    }

}
