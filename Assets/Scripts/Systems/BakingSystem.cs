using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BakingSystem : MonoBehaviour
{
    public List<Bread> breadsForSale = new List<Bread>();
    public void BakeBread(Recipe recipe)
    {
        float accuracy = RunTimingMinigame(recipe.difficulty);
        float quality = CalculateQuality(accuracy);

        Bread newBread = new Bread(recipe, quality);
        breadsForSale.Add(newBread);

        Debug.Log(recipe.recipeName + " has been baked!" + quality + " star quality " + newBread.breadValue + "cost");
    }

    float RunTimingMinigame(int difficulty)
    {
        //temp placeholder
        return Random.Range(05f, 1.0f);
    }

    float CalculateQuality(float accuracy)
    {
        // exact quality calculations added later
        return accuracy;
    }
 
}
