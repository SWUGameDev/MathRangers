using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VariableProbabilityController
{
    List<float> currentPercentages;

    private HashSet<AbilityInfo> selectedActiveAbility;

    public VariableProbabilityController(List<float> initialPercentages)
    {
        this.currentPercentages = initialPercentages;
    }
    public VariableProbabilityController()
    {

    }

    public int GetRandomIndex(List<float> percentList)
    {
        this.currentPercentages = percentList;

        float percentageSum = this.currentPercentages.Sum();
        float target = Random.Range(0, percentageSum);

        float targetSum = 0;
        for (int index = 0; index < this.currentPercentages.Count; index++)
        {
            targetSum += this.currentPercentages[index];
            if (target <= targetSum)
                return index;
        }
        return this.currentPercentages.Count - 1;
    }

    public List<AbilityInfo> GetCurrentRandomAbilityInfo(List<AbilityInfo> currentAbilityInfos)
    {

        List<AbilityInfo> abilityInfos = new List<AbilityInfo>();
        
        foreach (AbilityInfo abilityInfo in currentAbilityInfos)
        {

            if(abilityInfo.abilityType == AbilityType.Active)
            {
                if( this.IsActiveAbilityContain(abilityInfo))
                    continue;
            }

            abilityInfos.Add(abilityInfo);
        }

        return this.GetRandomCombinations<AbilityInfo>(abilityInfos, 3);
    }

    private bool IsActiveAbilityContain(AbilityInfo abilityInfo)
    {
        if(this.selectedActiveAbility == null)
            this.selectedActiveAbility = new HashSet<AbilityInfo>();

        if (abilityInfo.isSelected)
        {
            if(this.selectedActiveAbility.Count != 2)
                this.selectedActiveAbility.Add(abilityInfo);
        }

        if (this.selectedActiveAbility.Count == 2)
        {
            if(!this.selectedActiveAbility.Contains(abilityInfo))
                return false;
        }

        return true;
    }


    private List<T> GetRandomCombinations<T>(List<T> elements, int m)
    {
        List<T> selectedElements = new List<T>();
        List<List<int>> combinations = new List<List<int>>();
        int n = elements.Count;

        combinations = GenerateCombinations(elements.Count, m);
        int index = Random.Range(0, combinations.Count);

        foreach (int selected in combinations[index])
        {
            Debug.Log($"selected {selected-1}");

            selectedElements.Add(elements[selected-1]);
        }

        return selectedElements;
    }

    public static List<List<int>> GenerateCombinations(int n, int m)
    {
        List<List<int>> combinations = new List<List<int>>();
        List<int> currentCombination = new List<int>();
        return GenerateCombinationsHelper(combinations, currentCombination, 1, n, m);
    }

    private static List<List<int>> GenerateCombinationsHelper(List<List<int>> combinations, List<int> currentCombination, int start, int n, int m)
    {
        if (m == 0)
        {
            combinations.Add(new List<int>(currentCombination));
            return combinations;
        }

        for (int i = start; i <= n; i++)
        {
            currentCombination.Add(i);
            GenerateCombinationsHelper(combinations, currentCombination, i + 1, n, m - 1);
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }

        return combinations;
    }


}
