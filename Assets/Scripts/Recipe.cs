using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NeweRecipe", menuName = "Bakery/Recipe")]

public class Recipe : ScriptableObject
{
    public string recipeName;
    public int difficulty;
    public float baseValue;
    // public List<IngredientsRequired> ingredients;
}