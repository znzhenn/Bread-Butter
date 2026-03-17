using UnityEngine;

public class MixingBowl : Interactable
{
    public ItemData flour;
    public ItemData water;
    public ItemData yeast;
    public ItemData dough;

    public override void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();

        if (inventory.HasItem(flour,1) &&
            inventory.HasItem(water,1) &&
            inventory.HasItem(yeast,1))
        {
            inventory.RemoveItem(flour,1);
            inventory.RemoveItem(water,1);
            inventory.RemoveItem(yeast,1);

            inventory.AddItem(dough,1);

            Debug.Log("Dough created!");
        }
        else
        {
            Debug.Log("Missing ingredients");
        }
    }
}