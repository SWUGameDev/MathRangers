using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using AbilityId = System.Int32;

public class BattleAbilityDataManager : MonoBehaviour
{
    [SerializeField] private List<AbilityInfo> abilityInfoList;

    Dictionary<AbilityId,AbilityInfo> abilityInfoDictionary;

    Dictionary<AbilityId, selectedAbility> selectedAbilityDictionary;

    private void Awake() {

        this.InitializeAbilityData();
        
    }

    private void InitializeAbilityData()
    {
        this.ConvertAbilityListToDictionary();

        this.LoadSelectedAbilityDictionary();
    }

    private void ConvertAbilityListToDictionary()
    {
        this.abilityInfoDictionary = new Dictionary<int,AbilityInfo>();

        foreach(AbilityInfo info in this.abilityInfoList)
        {
            this.abilityInfoDictionary[info.abilityId] = info;
        }
    }

    private void LoadSelectedAbilityDictionary()
    {
        if(!PlayerPrefs.HasKey(AbilityInfoManager.serializedAbilityInfoDictionaryKey))
            return;

        string serializedSelectedAbilityDictionary = PlayerPrefs.GetString(AbilityInfoManager.serializedAbilityInfoDictionaryKey);

        this.selectedAbilityDictionary = JsonConvert.DeserializeObject<Dictionary<int,selectedAbility>>(serializedSelectedAbilityDictionary);
    }

    public Dictionary<AbilityId, selectedAbility> GetLoadSelectedAbilityDictionary()
    {
        if (this.selectedAbilityDictionary == null)
        {
            this.selectedAbilityDictionary = new Dictionary<AbilityId, selectedAbility>();

            LoadSelectedAbilityDictionary();
        }

        return this.selectedAbilityDictionary;
    }
}
