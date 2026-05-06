using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;

    private void Awake()
    {
        itemDictionary = new  Dictionary<int, GameObject>();
        Debug.Log("Item prefab count: " + itemPrefabs.Count);

        // for (int i = 0; i < itemPrefabs.Count; i++)
        // {
        //     if(itemPrefabs[i] != null)
        //     {
        //         itemPrefabs[i].ID = i + 1;
        //     }
        // }
        
        foreach(Item item in itemPrefabs)
        {
            itemDictionary[item.ID] = item.gameObject;

            Debug.Log("Added item ID: " + item.ID + " Name: " + item.name);
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);
        if(prefab == null)
        {
            Debug.LogWarning($"Item with ID {itemID} not ound in dictionary");
        }
        return prefab;
    }
    
}
