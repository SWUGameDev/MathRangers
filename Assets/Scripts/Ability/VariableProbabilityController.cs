using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class VariableProbabilityController 
{
    List<float> currentPercentages;

    public VariableProbabilityController(List<float> initialPercentages)
    {
        this.currentPercentages = initialPercentages;
    }

    public int GetRandomIndex()
    {
        float percentageSum = this.currentPercentages.Sum();
        float target = Random.Range(0,percentageSum);

        float targetSum = 0;
        for(int index = 0;index<this.currentPercentages.Count;index++)
        {
            targetSum += this.currentPercentages[index];
            if(target<=targetSum)
                return index;
        }

        return this.currentPercentages.Count - 1;
    }

    private void ChangeProbability()
    {

    }
}
