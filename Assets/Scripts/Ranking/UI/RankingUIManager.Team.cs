using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using totalScore = System.Int64;
using System.Linq;
public partial class RankingUIManager : MonoBehaviour
{
    [Header("Team Tab UI")]

    [SerializeField] private List<GameObject> teamRankingPanels;

    private Dictionary<TeamType,TeamRackingUIItem> teamRackingUIItems;
    private void initializeTeamRankSettings()
    {
        this.teamRackingUIItems = new Dictionary<TeamType, TeamRackingUIItem>();

        foreach (var panel in this.teamRankingPanels)
        {
            TeamRackingUIItem panelController = panel.GetComponent<TeamRackingUIItem>();
            this.teamRackingUIItems[panelController.TeamType] = panelController;    
        }
    }

    public bool SetTeamRankUI(IOrderedEnumerable<KeyValuePair<TeamType, totalScore>> teamRackingInfos)
    {
        this.initializeTeamRankSettings();

        if(teamRackingInfos == null)
            return false;
        
        int rank = 0;
        foreach(var infos in teamRackingInfos)
        {
            if(this.teamRackingUIItems.ContainsKey(infos.Key) == false)
                continue;
            
            this.teamRackingUIItems[infos.Key]?.SetTeamRankBackGroundColor(this.rankBackgroundSprites[rank]);
            this.teamRackingUIItems[infos.Key]?.SetTeamRankText((++rank).ToString());
        }
        return true;
    }

}
