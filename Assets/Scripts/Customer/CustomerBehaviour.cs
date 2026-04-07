using UnityEngine;
using System;

public class CustomerBehaviour : MonoBehaviour
{
    public Customer customerData;

    public Action<CustomerBehaviour> OnTryPurchase;

    private float checkTimer = 2f;

    public void Setup(Customer customer)
    {
        customerData = customer;
    }

    public void PlaceOrder()
    {
        Debug.Log(customerData.customerName + " is waiting for " + customerData.favoriteBread.recipeName);
    }
}