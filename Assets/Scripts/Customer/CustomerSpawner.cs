using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public CustomerSystem customerSystem;
    public List<Recipe> availableRecipes;
    public GameObject customerPrefab;
    public Transform spawnPoint;
    public DisplayCase displayCase;

    public float spawnInterval = 5f; // seconds between spawns
    private float spawnTimer = 0f;

    private float timer;

    public string[] sampleNames = { "Alice", "Bob", "Charlie", "Diana", "Ethan" };

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
        if (availableRecipes.Count == 0) return;

        string name = sampleNames[Random.Range(0, sampleNames.Length)];
        Recipe favorite = availableRecipes[Random.Range(0, availableRecipes.Count)];

        float mood = Random.Range(0.5f, 1f);
        bool isReturning = Random.value > 0.5f;

        Customer newCustomer = new Customer(name, favorite, mood, isReturning);

        GameObject obj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        obj.name = "Customer_" + name;

        CustomerBehaviour behaviour = obj.GetComponent<CustomerBehaviour>();
        behaviour.Setup(newCustomer);
        behaviour.displayCase = displayCase;

        Debug.Log(name + " spawned in world");
        Debug.Log("Spawned at: " + spawnPoint.position);
    }
}