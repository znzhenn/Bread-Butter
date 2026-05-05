using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Bakery/Recipe")]
public class Recipe : ScriptableObject
{
    public string recipeName;
    public int difficulty; // 1–5
    public float baseValue;
    public Sprite icon;

    [Header("Ingredients")]
    public List<ItemData> ingredients;

    [Header("Processing Times")]
    public float kneadTime = 5f;
    public float proofTime = 5f;
    public float bakeTime = 20f;

    [Header("Stages")]
    public ItemData doughItem;     // after kneading
    public ItemData resultItem;     // after baking
}