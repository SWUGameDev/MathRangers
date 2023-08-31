using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AbilityId = System.Int32;

public partial class Player : MonoBehaviour
{
    [SerializeField] PropertyManager propertyManager;
    [SerializeField] BattleAbilityDataManager battleAbilityDataManager;
    private static Dictionary<AbilityId, selectedAbility> playerAbilityInfoDictionary = new Dictionary<AbilityId, selectedAbility>();
    
    static List<AbilityInfo> playerAbilityInfoList;
    public List<AbilityInfo> PlayerAbilityInfoList { get { return playerAbilityInfoList; } }

    public interface IAbility
    {
        void ApplyAbility(AbilityId abilityId);
    }

    public class Ability101 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability101(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("[스킬] 개교 기념일: n초 동안 무적");

            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float amout = playerAbilityInfoList[0].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.ActiveSkillUnbeat(amout);
        }
    }

    public class Ability102 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability102(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("에너지바");
            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float amout = playerAbilityInfoList[1].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.ActiveSkillUnbeat(amout);

        }
    }

    public class Ability103 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability103(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("103");
            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float amout = playerAbilityInfoList[2].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.ActiveSkillUnbeat(amout);
        }
    }

    public class Ability204 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability204(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("시험기간: 공격력이 n% 증가");

            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            Debug.Log("레벨: " + level);
            float amout = playerAbilityInfoList[3].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.AttackPowerIncrease(amout);
        }
    }

    public class Ability205 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability205(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("자습시간: 방어력이 n% 증가");

            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float amout = playerAbilityInfoList[4].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.DefensePowerIncrease(amout);
        }
    }

    public class Ability206 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability206(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("국민체조: 최대 체력이 n% 증가");
            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float amout = playerAbilityInfoList[5].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.HpIncrease(amout);
        }
    }

    public class Ability207 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability207(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("점심시간: 공격 속도 n& 증가");
            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float amout = playerAbilityInfoList[6].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.AttackSpeedIncrease(amout);
        }
    }

    public class Ability208 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability208(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("스터디 플래너: 제한 시간 n초 증가");
            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float amout = playerAbilityInfoList[7].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.LimitTimeIncrease(amout);
        }
    }

    public class Ability209 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability209(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("209");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability210 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability210(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("210");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability211 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability211(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("211");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability212 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability212(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("212");
            Debug.Log(playerAbilityInfoDictionary[abilityId].selectedCount);
        }
    }

    public class Ability213 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability213(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("교내행사: 일반 공격 n번마다 보스를 m초 간 기절");
            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float n = playerAbilityInfoList[12].abilityCommands[0].amountForLevel[level - 1];
            float m = playerAbilityInfoList[12].abilityCommands[1].amountForLevel[level - 1];

            this.propertyManager.Buff213Attack(n, m);
        }
    }

    public class Ability214 : IAbility
    {
        private PropertyManager propertyManager;

        public Ability214(PropertyManager propertyManager)
        {
            this.propertyManager = propertyManager;
        }
        public void ApplyAbility(AbilityId abilityId)
        {
            Debug.Log("복습은 철저히: 일반 공격 n번마다 위력 10 배");
            int level = playerAbilityInfoDictionary[abilityId].selectedCount;
            float amout = playerAbilityInfoList[13].abilityCommands[0].amountForLevel[level - 1];

            this.propertyManager.Buff214AttackIndex((int)amout);

        }
    }

    void AddBuff()
    {
        playerAbilityInfoDictionary = battleAbilityDataManager.GetLoadSelectedAbilityDictionary();
        playerAbilityInfoList = battleAbilityDataManager.GetAbilityInfoList();
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
            case 101: return new Ability101(this.propertyManager);
            case 102: return new Ability102(this.propertyManager);
            case 103: return new Ability103(this.propertyManager);
            case 204: return new Ability204(this.propertyManager);
            case 205: return new Ability205(this.propertyManager);
            case 206: return new Ability206(this.propertyManager);
            case 207: return new Ability207(this.propertyManager);
            case 208: return new Ability208(this.propertyManager);
            case 209: return new Ability209(this.propertyManager);
            case 210: return new Ability210(this.propertyManager);
            case 211: return new Ability211(this.propertyManager);
            case 212: return new Ability212(this.propertyManager);
            case 213: return new Ability213(this.propertyManager);
            case 214: return new Ability214(this.propertyManager);

            default: return null;
        }
    }
}

