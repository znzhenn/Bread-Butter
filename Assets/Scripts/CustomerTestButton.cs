using UnityEngine;

public class CustomerTest : MonoBehaviour
{
    public CustomerSystem customerManager;
    public Recipe testRecipe;   // assign in Inspector

    private Customer testCustomer;

    void Start()
    {
        testCustomer = new Customer(
            "Test Customer",
            testRecipe,
            0.8f,
            false
        );
    }

    public void OnTestCustomerClick()
    {
        customerManager.customerBuys(testCustomer);
    }
}