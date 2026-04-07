using System.Collections.Generic;
using UnityEngine;

public class CustomerSystem : MonoBehaviour
{
    public List<Customer> activeCustomers = new();

    public float patienceTickRate = 1f;
    private float tickTimer;

    public void Tick(float dt)
    {
        tickTimer += dt;

        if (tickTimer >= patienceTickRate)
        {
            tickTimer = 0f;
            TickPatience();
        }
    }

    public void AddCustomer(Customer customer)
    {
        activeCustomers.Add(customer);
    }

    void TickPatience()
    {
        List<Customer> toRemove = new();

        foreach (var c in activeCustomers)
        {
            c.TickPatience(1f);

            if (c.IsImpatient())
                toRemove.Add(c);
        }

        foreach (var c in toRemove)
        {
            RemoveCustomer(c);
        }
    }

    public void RemoveCustomer(Customer customer)
    {
        activeCustomers.Remove(customer);
        Debug.Log(customer.customerName + " left (impatient)");
    }
}

    // public void ServeCustomers()
    // {
    //     foreach (Customer customer in new List<Customer>(activeCustomers))
    //     {
    //         Bread breadToSell = bakingSystem.breadsForSale
    //             .Find(b => b.recipe == customer.favoriteBread);

    //         if (breadToSell != null)
    //         {
    //             bakingSystem.breadsForSale.Remove(breadToSell);
    //             money += Mathf.RoundToInt(breadToSell.breadValue);
    //             moneyUI.UpdateMoney();

    //             Debug.Log(customer.customerName + 
    //                     " bought " + breadToSell.recipe.recipeName +
    //                     " for " + Mathf.RoundToInt(breadToSell.breadValue) + " coins!");

    //             RemoveCustomer(customer, true);
    //             return; // remove this line if you want to serve multiple at once
    //         }
    //     }

    //     Debug.Log("No matching breads available.");
    // }

    /* no longer needed
    public void CustomerBuys(Customer customer)
    {
        // Called when a customer successfully buys bread
        RemoveCustomer(customer, true);
        Debug.Log(customer.customerName + " has purchased their bread!");
    }*/
    
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