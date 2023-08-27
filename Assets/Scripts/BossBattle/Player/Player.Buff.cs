using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityId = System.Int32;

public partial class Player : MonoBehaviour
{
    [SerializeField] BattleAbilityDataManager battleAbilityDataManager;
    private static Dictionary<AbilityId, selectedAbility> playerAbilityInfoDictionary = new Dictionary<AbilityId, selectedAbility>();

    public interface IAbility
    {
        void ApplyAbility(AbilityId abilityId);

    }

    public class Ability101 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("개교 기념일");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
            
        }
    }

    public class Ability102 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("에너지바");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability103 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("103");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability204 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("204");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability205 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("205");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability206 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("206");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability207 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("207");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability208 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("208");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability209 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("209");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability210 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("210");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability211 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("211");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability212 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("212");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability213 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("213");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability214 : IAbility
    {
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("214");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    void AddBuff()
    {
        playerAbilityInfoDictionary = battleAbilityDataManager.GetLoadSelectedAbilityDictionary();

        foreach (var kvp in playerAbilityInfoDictionary)
        {
            AbilityId abilityId = kvp.Key;
            selectedAbility ability = kvp.Value;

            IAbility abilityImplementation = GetAbilityImplementation(abilityId);
            if (abilityImplementation != null)
            {
                abilityImplementation.ApplyAbility(abilityId);
            }
        }
    }

    IAbility GetAbilityImplementation(AbilityId abilityId)
    {
        switch (abilityId)
        {
            case 101: return new Ability101();
            case 102: return new Ability102();
            case 103: return new Ability103();
            case 204: return new Ability204();
            case 205: return new Ability205();
            case 206: return new Ability206();
            case 207: return new Ability207();
            case 208: return new Ability208();
            case 209: return new Ability209();
            case 210: return new Ability210();
            case 211: return new Ability211();
            case 212: return new Ability212();
            case 213: return new Ability213();
            case 214: return new Ability214();

            default: return null;
        }
    }
}

