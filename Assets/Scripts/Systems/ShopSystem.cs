using Unity.VisualScripting;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public DisplayCase displayCase;
    public CustomerSystem customerSystem;
    public OrderSystem orderSystem;

    public int money;

    public void TryServeBread(Bread bread)
    {
        Order order = orderSystem.FindMatchingOrder(bread); 

        if (order != null)
        {
            money += Mathf.RoundToInt(bread.breadValue);

            orderSystem.CompleteOrder(order);
            customerSystem.RemoveCustomer(order.customer);

            Debug.Log("Served correct order!");
        }
        else
        {
            Debug.Log("No one wants this bread!");
        }
    }
    public void TryServeCustomer(CustomerBehaviour customer)
    {
        string desired = customer.customerData.favoriteBread.recipeName;

        ItemData bread = displayCase.TakeBread(desired);

        if (bread != null)
        {
            money += 10; // temp value

            Debug.Log(customer.customerData.customerName + " bought " + desired);

            customerSystem.RemoveCustomer(customer.customerData);
            Destroy(customer.gameObject);
        }
        else
        {
            Debug.Log(customer.customerData.customerName + " still waiting...");
        }
    }
}