using UnityEngine;

public class BakeButton : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public Recipe recipeToBake;      
    public BakingSystem bakingSystem; 

    public void onPressBakeButton()
    {
        if (bakingSystem == null)
        {
            Debug.LogError("BakeButton: BakingSystem reference not assigned!");
            return;
        }

        if (recipeToBake == null)
        {
            Debug.LogError("BakeButton: RecipeToBake is not assigned!");
            return;
        }

        bakingSystem.BakeBread(recipeToBake);
    }
}