using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Bakery/Recipe")]

public class Recipe : ScriptableObject
{
    public string recipeName;
    public int difficulty; // 1-5 stars (1 is lowest - 5 is highest)
    public float baseValue;
    // public List<IngredientsRequired> ingredients;
    // public Sprite icon; 
    // public string description; // for more info (and the actual text)
}