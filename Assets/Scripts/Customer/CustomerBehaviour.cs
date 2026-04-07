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

    void Update()
    {
        if (customerData == null) return;

        checkTimer -= Time.deltaTime;

        if (checkTimer <= 0f)
        {
            OnTryPurchase?.Invoke(this);
            checkTimer = 2f;
        }
    }
}