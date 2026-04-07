using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public DisplayCase displayCase;
    public CustomerSystem customerSystem;
    public OrderSystem orderSystem;

    public int money = 0;

    public void TryServeCustomer(CustomerBehaviour customerBehaviour)
    {
        if (customerBehaviour == null || customerBehaviour.customerData == null)
            return;

        Customer customer = customerBehaviour.customerData;
        string desired = customer.favoriteBread.recipeName;

        ItemData bread = displayCase.TakeBread(desired);

        if (bread != null)
        {
            Order matchingOrder = orderSystem.FindOrderForCustomer(customer);

            if (matchingOrder != null)
            {
                money += Mathf.RoundToInt(customer.favoriteBread.baseValue);

                orderSystem.CompleteOrder(matchingOrder);
                customerSystem.RemoveCustomer(customer);

                Debug.Log(customer.customerName + " bought " + desired + "!");
                Debug.Log("Gold: " + money);

                Destroy(customerBehaviour.gameObject);
            }
            else
            {
                Debug.LogWarning("Bread found, but no order was found for " + customer.customerName);
            }
        }
        else
        {
            Debug.Log(customer.customerName + " is still waiting for " + desired);
        }
    }
}