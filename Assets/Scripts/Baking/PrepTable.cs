using UnityEngine;

public class PrepTable : Interactable
{
    public ItemData dough;
    public ItemData shapedDough;

    public override void Interact()
    {
        Inventory inventory = FindFirstObjectByType<Inventory>();

        if (inventory.HasItem(dough, 1))
        {
            inventory.RemoveItem(dough, 1);
            inventory.AddItem(shapedDough, 1);

            Debug.Log("Dough shaped!");
        }
        else
        {
            Debug.Log("No dough to shape");
        }
    }
}