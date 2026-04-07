using UnityEngine;
using System.Collections.Generic;

public class OrderSystem : MonoBehaviour
{
    public List<Order> activeOrders = new();
    public CustomerSystem customerSystem;

    public void PlaceOrder(Customer customer)
    {
        Order order = new Order(customer, customer.favoriteBread);
        activeOrders.Add(order);

        Debug.Log(customer.customerName + " placed order for " + order.requestedRecipe.recipeName);
    }

    public Order FindMatchingOrder(Bread bread)
    {
        foreach (var order in activeOrders)
        {
            if (!order.isCompleted && order.requestedRecipe == bread.recipe)
            {
                return order;
            }
        }

        return null;
    }

    public Order FindOrderForCustomer(Customer customer)
    {
        foreach (var order in activeOrders)
        {
            if (order.customer == customer)
            {
                return order;
            }
        }

        return null;
    }

    public void CompleteOrder(Order order)
    {
        order.isCompleted = true;
        activeOrders.Remove(order);

        Debug.Log("Order completed for " + order.customer.customerName);
    }
}