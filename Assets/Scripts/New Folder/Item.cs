using UnityEngine;
using UnityEngine.UI;

// inventory items
[RequireComponent(typeof(Image))]
public class Item : MonoBehaviour
{
    public ItemData data;

    public int ID => data.ID;
    public string Name => data.itemName;

    public virtual void PickUp()
    {
        Sprite itemIcon = data.icon;

        if(ItemPickupUIController.Instance != null)
        {
            ItemPickupUIController.Instance.ShowItemPickup(Name, itemIcon);
        }
    }
}