using UnityEngine;

public class WorldItem : MonoBehaviour, Interactable
{
    public ItemData itemData;

    public void Interact()
    {
        InventoryController inventory =
            FindFirstObjectByType<InventoryController>();

        if(inventory == null)
        {
            Debug.LogError("No InventoryController found.");
            return;
        }

        if(itemData == null)
        {
            Debug.LogError("No ItemData assigned.");
            return;
        }

        bool added = inventory.AddItem(itemData.prefab);

        if(added)
        {
            Item item = itemData.prefab.GetComponent<Item>();

            if(item != null)
            {
                item.PickUp();
            }

            Destroy(gameObject);
        }
    }

    public bool CanInteract()
    {
        return itemData != null;
    }
}