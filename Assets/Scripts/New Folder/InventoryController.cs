using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    private ItemDictionary itemDictionary;

    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public GameObject[] itemPrefabs;
    public int slotCount;

    void Start()
    {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();

        for (int i = 0; i < slotCount; i++)
        {
            Slot slot =
                Instantiate(slotPrefab, inventoryPanel.transform)
                .GetComponent<Slot>();

            if (i < itemPrefabs.Length)
            {
                GameObject item =
                    Instantiate(itemPrefabs[i], slot.transform);

                RectTransform rt =
                    item.GetComponent<RectTransform>();

                rt.localPosition = Vector3.zero;
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one;

                slot.currentItem = item;
            }
        }
    }

    public bool AddItem(GameObject itemPrefab)
    {
        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot =
                slotTransform.GetComponent<Slot>();

            if (slot != null && slot.currentItem == null)
            {
                GameObject item =
                    Instantiate(itemPrefab, slotTransform);

                RectTransform rt =
                    item.GetComponent<RectTransform>();

                rt.localPosition = Vector3.zero;
                rt.anchoredPosition = Vector2.zero;
                rt.localScale = Vector3.one;

                slot.currentItem = item;

                return true;
            }
        }

        Debug.Log("Inventory is full!");

        return false;
    }

    public List<Item> GetAllItems()
    {
        List<Item> items = new List<Item>();

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();

            if (slot != null && slot.currentItem != null)
            {
                Item item =
                    slot.currentItem.GetComponent<Item>();

                if (item != null)
                {
                    items.Add(item);
                }
            }
        }

        return items;
    }

    public bool HasIngredients(List<ItemData> neededIngredients)
    {
        List<Item> temp =
            new List<Item>(GetAllItems());

        foreach (ItemData needed in neededIngredients)
        {
            bool found = false;

            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].data == needed)
                {
                    temp.RemoveAt(i);

                    found = true;

                    break;
                }
            }

            if (!found)
            {
                return false;
            }
        }

        return true;
    }

    public void ConsumeIngredients(List<ItemData> neededIngredients)
    {
        foreach (ItemData needed in neededIngredients)
        {
            foreach (Transform slotTransform in inventoryPanel.transform)
            {
                Slot slot =
                    slotTransform.GetComponent<Slot>();

                if (slot != null &&
                    slot.currentItem != null)
                {
                    Item item =
                        slot.currentItem.GetComponent<Item>();

                    if (item != null &&
                        item.data == needed)
                    {
                        Destroy(slot.currentItem);

                        slot.currentItem = null;

                        break;
                    }
                }
            }
        }
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData =
            new List<InventorySaveData>();

        foreach (Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot =
                slotTransform.GetComponent<Slot>();

            if (slot.currentItem != null)
            {
                Item item =
                    slot.currentItem.GetComponent<Item>();

                invData.Add(new InventorySaveData
                {
                    itemID = item.ID,
                    slotIndex = slotTransform.GetSiblingIndex()
                });
            }
        }

        return invData;
    }

    public void SetInventoryItems(
        List<InventorySaveData> inventorySaveData)
    {
        foreach (InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex <
                inventoryPanel.transform.childCount)
            {
                Slot slot =
                    inventoryPanel.transform
                    .GetChild(data.slotIndex)
                    .GetComponent<Slot>();

                if (slot.currentItem != null)
                {
                    Destroy(slot.currentItem);

                    slot.currentItem = null;
                }

                GameObject itemPrefab =
                    itemDictionary.GetItemPrefab(data.itemID);

                if (itemPrefab != null)
                {
                    GameObject item =
                        Instantiate(itemPrefab, slot.transform);

                    RectTransform rt =
                        item.GetComponent<RectTransform>();

                    rt.localPosition = Vector3.zero;
                    rt.anchoredPosition = Vector2.zero;
                    rt.localScale = Vector3.one;

                    slot.currentItem = item;
                }
            }
        }
    }
}