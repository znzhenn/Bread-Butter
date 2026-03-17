using UnityEngine;

public class CustomerBehaviour : MonoBehaviour
{
    public Customer customerData;

    private bool hasOrdered = false;
    public DisplayCase displayCase;

    private float checkTimer = 2f;

    void Update()
    {
        if (customerData == null || displayCase == null) return;

        checkTimer -= Time.deltaTime;

        if (checkTimer <= 0)
        {
            TryBuy();
            checkTimer = 2f;
        }
    }

    void TryBuy()
    {
        string desired = customerData.favoriteBread.recipeName;

        ItemData bread = displayCase.TakeBread(desired);

        if (bread != null)
        {
            Debug.Log(customerData.customerName + " bought " + desired);
            Leave();
        }
        else
        {
            Debug.Log(customerData.customerName + " is waiting for " + desired);
        }
    }

    public void Setup(Customer customer)
    {
        customerData = customer;
    }

    
    public void PlaceOrder()
    {
        if (hasOrdered) return;

        hasOrdered = true;
        Debug.Log(customerData.customerName + " wants " + customerData.favoriteBread.recipeName);
    }

    public void ReceiveBread(ItemData bread)
    {
        //temp basic check
        if (bread.itemName == customerData.favoriteBread.recipeName)
        {
            Debug.Log("Correct order! Customer happy.");
        }
        else
        {
            Debug.Log("Wrong order!");
        }

        Leave();
    }

    void Leave()
    {
        Debug.Log(customerData.customerName + " left.");
        Destroy(gameObject);
    }
}