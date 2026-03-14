using UnityEngine;

public class Bread : MonoBehaviour
{
    public Recipe recipe;
    public float quality;
    public float breadValue;

    // constructor
    public void Initialize(Recipe recipe, float quality)
    {
        this.recipe = recipe;
        this.quality = quality;
        CalculateBreadValue();
    }

    void CalculateBreadValue()
    {
        breadValue = recipe.baseValue * quality; //exact value to be adjusted later
    }



}
