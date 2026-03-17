using System.Collections.Generic;
using UnityEngine;

public class DisplayCase : Interactable
{
    public List<ItemData> storedBread = new List<ItemData>();

    public override void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();

        // Try to add bread from player inventory
        foreach (var item in new List<ItemData>(inventory.items.Keys))
        {
            if (item.itemName.Contains("Bread"))
            {
                inventory.RemoveItem(item, 1);
                storedBread.Add(item);

                Debug.Log("Added bread to display: " + item.itemName);
                return;
            }
        }

        Debug.Log("No bread to display");
    }

    public ItemData TakeBread(string requestedName)
    {
        foreach (var bread in storedBread)
        {
            if (bread.itemName == requestedName)
            {
                storedBread.Remove(bread);
                return bread;
            }
        }

        return null;
    }
}