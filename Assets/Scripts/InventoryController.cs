using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemDictionary = FindObjectOfType<ItemDictionary>();

<<<<<<< Updated upstream
        for(int i = 0; i< slotCount; i++)
=======
        /*
        for(int i = 0; i <slotCount; i++)
>>>>>>> Stashed changes
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        //preload if it already exists
        string savePath = System.IO.Path.Combine(Application.persistentDataPath, "saveData.json");
        if(System.IO.File.Exists(savePath))
        {
           Debug.Log("No Save found --> preloading test");
           for(int i = 0; i <slotCount; i++)
            {
                if(i< itemPrefabs.Length)
                {
                    Slot slot = inventoryPanel.transform.GetChild(i).GetComponent<Slot>();
                    GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
<<<<<<< Updated upstream
        }
        // for(int i = 0; i <slotCount; i++)
        // {
        //     Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
        //     if(i < itemPrefabs.Length)
        //     {
        //         GameObject item = Instantiate(itemPrefabs[i], slot.transform);
        //         item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //         slot.currentItem = item;
        //     }
        // }
=======
        }*/
>>>>>>> Stashed changes
        
    }
    
    // public bool AddItem(GameObject itemPrefab)
    // {
    //     foreach(Transform slotTransform in inventoryPanel.transform)
    //     {
    //         Slot slot = slotTransform.GetComponent<Slot>();
    //         if(slot != null && slot.currentItem == null)
    //         {
    //             GameObject item = Instantiate(itemPrefab, slotTransform);
    //             item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    //             slot.currentItem = item;
    //             return true; // item added
    //         }
    //     }
    //     Debug.Log("Inventory is full!");
    //     return false; // full inventory
    // }


    public List<InventorySaveData> GetInventoryItems()
        {
            List<InventorySaveData> invData = new List<InventorySaveData>();
            foreach(Transform slotTransform in inventoryPanel.transform)
            {
                Slot slot = slotTransform.GetComponent<Slot>();
                if (slot.currentItem != null)
                {
                    Item item = slot.currentItem.GetComponent<Item>();
                    invData.Add(new InventorySaveData {itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex()});
                }
            }
            return invData;
        }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        foreach(Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i <slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        foreach(InventorySaveData data in inventorySaveData)
        {
            if(data.slotIndex < slotCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);
                if(itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
        }
    }
}
