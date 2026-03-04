using System.Collections.Generic;
using UnityEngine;

public class CustomerSystem : MonoBehaviour
{
    [Header("Customer Settings")]
    public List<Customer> activeCustomers = new List<Customer>();
    public float patienceTickRate = 1f; // seconds per tick
    public CustomerUI customerUIPrefab;
    public Transform uiParent; // where the UI elements will appear

    private float tickTimer = 0f;
    private Dictionary<Customer, CustomerUI> customerToUI = new Dictionary<Customer, CustomerUI>();
    //econ
    public BakingSystem bakingSystem;
    public int money = 0;

    void Update()
    {
        tickTimer += Time.deltaTime;
        if (tickTimer >= patienceTickRate)
        {
            tickTimer = 0f;
            TickPatience();
        }
    }

    public void AddCustomer(Customer newCustomer)
    {
        activeCustomers.Add(newCustomer);

        // Spawn UI
        CustomerUI ui = Instantiate(customerUIPrefab, uiParent);
        ui.Setup(newCustomer);

        customerToUI[newCustomer] = ui;
    }

    void TickPatience()
    {
        List<Customer> customersToRemove = new List<Customer>();

        foreach (Customer customer in activeCustomers)
        {
            customer.patience -= 1f; // decrease patience
            if (customer.patience <= 0f)
            {
                customersToRemove.Add(customer);
            }
        }

        foreach (Customer customer in customersToRemove)
        {
            RemoveCustomer(customer, false);
        }
    }

    public void RemoveCustomer(Customer customer, bool boughtBread)
    {
        if (activeCustomers.Contains(customer))
            activeCustomers.Remove(customer);

        if (customerToUI.ContainsKey(customer))
        {
            Destroy(customerToUI[customer].gameObject);
            customerToUI.Remove(customer);
        }

        if (!boughtBread)
            Debug.Log(customer.customerName + " ran out of patience and left!");
    }

    public void ServeCustomers()
    {
        foreach (Customer customer in new List<Customer>(activeCustomers))
        {
            Bread breadToSell = bakingSystem.breadsForSale
                .Find(b => b.recipe == customer.favoriteBread);

            if (breadToSell != null)
            {
                bakingSystem.breadsForSale.Remove(breadToSell);
                money += Mathf.RoundToInt(breadToSell.breadValue);

                Debug.Log(customer.customerName + 
                        " bought " + breadToSell.recipe.recipeName +
                        " for " + Mathf.RoundToInt(breadToSell.breadValue) + " coins!");

                RemoveCustomer(customer, true);
                return; // remove this line if you want to serve multiple at once
            }
        }

        Debug.Log("No matching breads available.");
    }

    
    public void CustomerBuys(Customer customer)
    {
        // Called when a customer successfully buys bread
        RemoveCustomer(customer, true);
        Debug.Log(customer.customerName + " has purchased their bread!");
    }
    
    /*
    public BakingSystem bakingSystem;
    public int money = 0;

    public void customerBuys(Customer customer)
    {

        Debug.Log("Customer wants: " + customer.favoriteBread);
        Debug.Log("Breads in shop: " + bakingSystem.breadsForSale.Count);
        //is the bread available
        Bread breadToSell = bakingSystem.breadsForSale.Find(b => b.recipe == customer.favoriteBread);
        if (breadToSell != null)
        {
            bakingSystem.breadsForSale.Remove(breadToSell);
            money += Mathf.RoundToInt(breadToSell.breadValue);

            Debug.Log(customer.customerName + " bought " + breadToSell.recipe.recipeName +
                      " for " + Mathf.RoundToInt(breadToSell.breadValue) + " coins!");
        }
        else
        {
            Debug.Log(customer.customerName + " didn't find their desired bread!");
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
}
