using System.Collections.Generic;
using UnityEngine;

public class DisplayCase : Interactable
{
    public ItemData[] acceptableBreads;
    public List<ItemData> storedBread = new List<ItemData>();

    public override void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();

        foreach (var item in new List<ItemData>(inventory.items.Keys))
        {
            if (System.Array.Exists(acceptableBreads, b => b == item))
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
        for (int i = 0; i < storedBread.Count; i++)
        {
            ItemData bread = storedBread[i];

            if (bread != null && bread.itemName == requestedName)
            {
                storedBread.RemoveAt(i);
                return bread;
            }
        }

        return null;
    }
}