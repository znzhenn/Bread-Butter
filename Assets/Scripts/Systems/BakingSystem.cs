using System.Collections.Generic;
using UnityEngine;

public class BakingSystem : MonoBehaviour
{
    public List<Bread> breadsForSale = new();
    public int BreadCount => breadsForSale.Count;

    public void Tick(float dt)
    {
        // baking timers to be added later
    }

    public void BakeBread(Recipe recipe)
    {
        float quality = Random.Range(0.5f, 1f);

        Bread bread = new Bread(recipe, quality);
        breadsForSale.Add(bread);

        Debug.Log($"Baked {recipe.recipeName} | Value: {bread.breadValue}");
    }

    public ItemData TakeBreadAsItem()
    {
        if (breadsForSale.Count == 0) return null;

        Bread bread = breadsForSale[0];
        breadsForSale.RemoveAt(0);

        return bread.recipe.resultPrefab.GetComponent<Item>().data;
    }
}