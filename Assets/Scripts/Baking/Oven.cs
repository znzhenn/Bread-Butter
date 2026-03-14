using UnityEngine;
using System.Collections;

public class Oven : Interactable
{
    public ItemData dough;
    public ItemData bread;

    public float bakeTime = 5f;

    public override void Interact()
    {
        Inventory inventory = FindObjectOfType<Inventory>();

        if (inventory.HasItem(dough,1))
        {
            inventory.RemoveItem(dough,1);
            StartCoroutine(Bake(inventory));
        }
    }

    IEnumerator Bake(Inventory inventory)
    {
        Debug.Log("Baking...");
        yield return new WaitForSeconds(bakeTime);

        inventory.AddItem(bread,1);

        Debug.Log("Bread finished!");
    }
}