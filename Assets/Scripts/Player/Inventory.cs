using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<ItemData, int> items = new Dictionary<ItemData, int>();

    public ItemData flour;
    public ItemData water;
    public ItemData yeast;

    void Start()
    {
        AddItem(flour, 5);
        AddItem(water, 5);
        AddItem(yeast, 5);
    }

    public void AddItem(ItemData item, int amount)
    {
        if (!items.ContainsKey(item))
            items[item] = 0;

        items[item] += amount;

        Debug.Log("Added " + item.itemName);
    }

    public bool HasItem(ItemData item, int amount)
    {
        return items.ContainsKey(item) && items[item] >= amount;
    }

    public void RemoveItem(ItemData item, int amount)
    {
        if (!items.ContainsKey(item))
            return;

        items[item] -= amount;

        if (items[item] <= 0)
            items.Remove(item);
    }
}