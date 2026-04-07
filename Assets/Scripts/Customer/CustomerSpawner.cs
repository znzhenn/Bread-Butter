using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public CustomerSystem customerSystem;
    public ShopSystem shopSystem;
    public OrderSystem orderSystem;

    public List<Recipe> availableRecipes;

    public GameObject customerPrefab;
    public Transform spawnPoint;

    public float spawnInterval = 5f;
    private float timer;

    public string[] names = { "Alice", "Bob", "Charlie" };

    void Start()
    {
        Debug.Log("CustomerSpawner started.");
        SpawnCustomer();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            Debug.Log("Trying to spawn customer...");
            SpawnCustomer();
            timer = 0f;
        }
    }

    void SpawnCustomer()
    {
        if (availableRecipes == null || availableRecipes.Count == 0)
        {
            Debug.LogWarning("CustomerSpawner has no available recipes assigned.");
            return;
        }

        if (customerPrefab == null)
        {
            Debug.LogWarning("CustomerSpawner has no customerPrefab assigned.");
            return;
        }

        if (spawnPoint == null)
        {
            Debug.LogWarning("CustomerSpawner has no spawnPoint assigned.");
            return;
        }

        if (customerSystem == null)
        {
            Debug.LogWarning("CustomerSpawner has no CustomerSystem assigned.");
            return;
        }

        if (shopSystem == null)
        {
            Debug.LogWarning("CustomerSpawner has no ShopSystem assigned.");
            return;
        }

        if (orderSystem == null)
        {
            Debug.LogWarning("CustomerSpawner has no OrderSystem assigned.");
            return;
        }

        string name = names[Random.Range(0, names.Length)];
        Recipe recipe = availableRecipes[Random.Range(0, availableRecipes.Count)];

        Customer customer = new Customer(name, recipe, 1f, false);

        GameObject obj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Customer prefab instantiated at: " + spawnPoint.position);

        CustomerBehaviour behaviour = obj.GetComponent<CustomerBehaviour>();
        if (behaviour == null)
        {
            Debug.LogError("Customer prefab is missing CustomerBehaviour.");
            return;
        }

        behaviour.Setup(customer);
        behaviour.OnTryPurchase += shopSystem.TryServeCustomer;

        customerSystem.AddCustomer(customer);
        orderSystem.PlaceOrder(customer);

        Debug.Log(name + " spawned and wants " + recipe.recipeName);
    }
}