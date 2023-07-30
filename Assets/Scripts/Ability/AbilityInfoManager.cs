using System.Collections.Generic;
using UnityEngine;
using TMPro;
using AbilityId = System.Int32;
using SelectedIndex = System.Int32;

public class AbilityInfoManager : MonoBehaviour {

    public class selectedAbility
    {
        public int selectedIndex;
        public int selectedCount;

        public selectedAbility(int selectedIndex, int selectedCount)
        {
            this.selectedIndex = selectedIndex;
            this.selectedCount = selectedCount;
        }

        public  void AddSelectedCount()
        {
            this.selectedCount++;
        }
    }


    [SerializeField] private List<AbilityInfoUIController> abilityInfoUIControllers;

    [SerializeField] private List<AbilityInfo> abilityInfos;

    private Dictionary<int, AbilityInfo> abilityInfoDictionary;

    private Dictionary<AbilityId, selectedAbility> selectedAbilityDictionary;

    private VariableProbabilityController variableProbabilityController;

    private int selectedIndex = 0;

    private void Start()
    {
        this.InitializeAbilityUIInfo();
    }

    public void InitializeAbilityUIInfo()
    {

        if (this.variableProbabilityController == null)
            this.variableProbabilityController = new VariableProbabilityController();

        this.abilityInfoDictionary = new Dictionary<AbilityId, AbilityInfo>();

        foreach (AbilityInfo abilityInfo in this.abilityInfos)
        {
            this.abilityInfoDictionary[abilityInfo.abilityId] = abilityInfo;
        }

    }

    public void SetRandomAbilityInfo()
    {

        if (this.variableProbabilityController == null)
            this.variableProbabilityController = new VariableProbabilityController();

        List<AbilityInfo> selectedAbilityInfos = this.variableProbabilityController.GetCurrentRandomAbilityInfo(this.abilityInfos);

        for (int index = 0; index < this.abilityInfoUIControllers.Count; index++)
        {
            this.abilityInfoUIControllers[index].InitializeAbilityUIInfo(this, selectedAbilityInfos[index]);
        }
    }


    public Dictionary<AbilityId, selectedAbility> GetSelectedAbilityDictionary()
    {
        return this.selectedAbilityDictionary;
    }

    public void SelectAbility(int abliityId)
    {
        if (this.selectedAbilityDictionary == null)
            this.selectedAbilityDictionary = new Dictionary<AbilityId, selectedAbility>();

        if (this.selectedAbilityDictionary.ContainsKey(abliityId))
        {
            this.selectedAbilityDictionary[abliityId].AddSelectedCount();
        }
        else {
            this.selectedAbilityDictionary[abliityId] = new selectedAbility(this.selectedIndex, 1);
            this.abilityInfoDictionary[abliityId].isSelected = true;
            this.selectedIndex++;
        }

        this.PrintCurrentSelectedAbilityInfos();

    }

    // For Debug.
    private void PrintCurrentSelectedAbilityInfos()
    {
        foreach (KeyValuePair<int, selectedAbility> keyValuePair in this.selectedAbilityDictionary)
        {
            Debug.Log($" Ablility id {keyValuePair.Key}, index :  {keyValuePair.Value.selectedIndex} count : {keyValuePair.Value.selectedCount}");
        }
    }

   
}

