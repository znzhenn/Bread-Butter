using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UnityEngine.UI.Image))]
public class Item : MonoBehaviour
{
    public int ID;
    public string Name;
    public ItemData data;

    public virtual void pickUp()
    {
        Sprite itemIcon = GetComponent<UnityEngine.UI.Image>()?.sprite;
        if(ItemPickupUIController.Instance != null)
        {
            ItemPickupUIController.Instance.ShowItemPickup(Name, itemIcon);
        }
    }
}
