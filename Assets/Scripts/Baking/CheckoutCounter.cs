using UnityEngine;

public class CheckoutCounter : Interactable
{
    public ItemData bread;

    public override void Interact()
{
    Inventory inventory = FindFirstObjectByType<Inventory>();
    CustomerBehaviour customer = FindFirstObjectByType<CustomerBehaviour>();

    if (customer == null)
    {
        Debug.Log("No customer");
        return;
    }

    if (inventory.HasItem(bread, 1))
    {
        inventory.RemoveItem(bread, 1);
        customer.ReceiveBread(bread);
    }
    else
    {
        Debug.Log("No bread to sell");
    }
}
}