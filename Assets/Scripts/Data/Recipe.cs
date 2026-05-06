using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Bakery/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;

    [Header("Ingredients")]
    public List<ItemData> ingredients;

    [Header("Result")]
    public ItemData resultItem;

    [Header("Economy")]
    public float baseValue;

    [Header("Timing")]
    public float proofTime; 
    public float bakeTime;
}