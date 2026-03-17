using UnityEngine;
using System.Collections;

public class Oven : Interactable
{
    public ItemData shapedDough;
    public ItemData bread;

    public float bakeTime = 5f;

    public override void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();

        if (inventory.HasItem(shapedDough, 1))
        {
            inventory.RemoveItem(shapedDough, 1);
            StartCoroutine(Bake(inventory));
        }
        else
        {
            Debug.Log("No shaped dough to bake");
        }
    }

    IEnumerator Bake(Inventory inventory)
    {
        Debug.Log("Baking...");
        yield return new WaitForSeconds(bakeTime);

        inventory.AddItem(bread, 1);

        Debug.Log("Bread finished!");
    }
}