using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class VariableProbabilityController
{
    List<float> currentPercentages;

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
            if (abilityInfo.abilityType == AbilityType.Active && abilityInfo.isSelected)
                continue;

            abilityInfos.Add(abilityInfo);
        }

        return this.GetRandomCombinations<AbilityInfo>(abilityInfos, 3);
    }


    private List<T> GetRandomCombinations<T>(List<T> elements, int m)
    {
        List<T> selectedElements = new List<T>();
        List<List<int>> combinations = new List<List<int>>();
        int n = elements.Count;

        GenerateCombinations(elements, combinations, new List<int>(), 0, n, m);

        int index = Random.Range(0, combinations.Count);
        Debug.Log(index);

        foreach (int selected in combinations[index])
        {
            selectedElements.Add(elements[selected]);
        }

        return selectedElements;
    }

    private void GenerateCombinations<T>(List<T> elements, List<List<int>> combinations, List<int> currentCombination, int start, int n, int m)
    {
        if (currentCombination.Count == m)
        {
            List<int> combination = new List<int>(currentCombination);
            combinations.Add(combination);
            return;
        }

        for (int i = start; i < n; i++)
        {
            currentCombination.Add(i);
            GenerateCombinations(elements, combinations, currentCombination, i + 1, n, m);
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }
    }


}
