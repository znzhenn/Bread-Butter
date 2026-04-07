using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public CustomerSystem customerSystem;
    public ShopSystem shopSystem;

    public List<Recipe> availableRecipes;

    public GameObject customerPrefab;
    public Transform spawnPoint;
    public OrderSystem orderSystem;

    public float spawnInterval = 5f;
    private float timer;

    public string[] names = { "Alice", "Bob", "Charlie" };

    
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCustomer();
            timer = 0f;
        }
    }

    void SpawnCustomer()
    {
        string name = names[Random.Range(0, names.Length)];
        Recipe recipe = availableRecipes[Random.Range(0, availableRecipes.Count)];

        Customer customer = new Customer(name, recipe, 1f, false);

        GameObject obj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

        var behaviour = obj.GetComponent<CustomerBehaviour>();
        behaviour.Setup(customer);

        behaviour.OnTryPurchase += shopSystem.TryServeCustomer;
        customerSystem.AddCustomer(customer);
        orderSystem.PlaceOrder(customer);
    }
}