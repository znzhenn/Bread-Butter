using UnityEngine;

public class CustomerSystem : MonoBehaviour
{

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
        
    }
}
