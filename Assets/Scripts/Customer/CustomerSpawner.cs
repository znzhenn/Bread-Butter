using UnityEngine;
using System.Collections.Generic;

public class CustomerSpawner : MonoBehaviour
{
    public CustomerSystem customerSystem;
    public ShopSystem shopSystem;
    public OrderSystem orderSystem;

    public List<CustomerProfile> customerProfiles = new();

    public GameObject customerPrefab;
    public Transform spawnPoint;

    public float spawnInterval = 5f;
    private float timer;

    private int nextCustomerIndex = 0;

    void Start()
    {
        Debug.Log("CustomerSpawner started.");
    }

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
        if (customerProfiles == null || customerProfiles.Count == 0)
        {
            Debug.LogWarning("CustomerSpawner has no customer profiles assigned.");
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

        CustomerProfile profile = customerProfiles[nextCustomerIndex];

        if (profile == null)
        {
            Debug.LogWarning("Customer profile at index " + nextCustomerIndex + " is null.");
            AdvanceIndex();
            return;
        }

        if (profile.favoriteBread == null)
        {
            Debug.LogWarning(profile.customerName + " has no favorite bread assigned.");
            AdvanceIndex();
            return;
        }

        Customer customer = new Customer(
            profile.customerName,
            profile.favoriteBread,
            profile.mood,
            profile.isReturning,
            profile.maxPatience
        );

        GameObject obj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

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

        Debug.Log(customer.customerName + " spawned and wants " + customer.favoriteBread.recipeName);

        AdvanceIndex();
    }

    void AdvanceIndex()
    {
        nextCustomerIndex++;

        if (nextCustomerIndex >= customerProfiles.Count)
        {
            Debug.Log("All predefined customers have been spawned.");
            enabled = false;
        }
    }

    // void AdvanceIndex()
    // {
    //     nextCustomerIndex++;

    //     if (nextCustomerIndex >= customerProfiles.Count)
    //     {
    //         nextCustomerIndex = 0;
    //     }
    //}
}