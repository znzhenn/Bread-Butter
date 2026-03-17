using UnityEngine;

public abstract class KitchenStation : Interactable
{
    public ItemData inputItem;
    public ItemData outputItem;

    protected Inventory inventory;

    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    public override void Interact()
    {
        Process();
    }

    protected abstract void Process();
}