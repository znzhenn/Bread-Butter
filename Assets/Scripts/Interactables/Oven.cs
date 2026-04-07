using UnityEngine;
using System.Collections;

public class Oven : Interactable
{
    public ItemData shapedDough;
    public ItemData[] possibleBreads;
    public float bakeTime = 5f;

    private bool isBaking = false;

    public override void Interact()
    {
        if (isBaking)
        {
            Debug.Log("The oven is already baking.");
            return;
        }

        Inventory inventory = FindFirstObjectByType<Inventory>();

        if (inventory == null)
        {
            Debug.LogWarning("No Inventory found.");
            return;
        }

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
        isBaking = true;
        Debug.Log("Baking...");

        yield return new WaitForSeconds(bakeTime);

        if (possibleBreads == null || possibleBreads.Length == 0)
        {
            Debug.LogWarning("No possible breads assigned to Oven.");
            isBaking = false;
            yield break;
        }

        ItemData result = possibleBreads[Random.Range(0, possibleBreads.Length)];
        inventory.AddItem(result, 1);

        Debug.Log("Bread finished: " + result.itemName);
        isBaking = false;
    }
}