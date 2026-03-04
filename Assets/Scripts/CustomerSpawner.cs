using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerUIPrefab;
    public CustomerSystem customerSystem;
    public List<Recipe> availableRecipes;

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

        // Pick a random name
        string name = sampleNames[Random.Range(0, sampleNames.Length)];

        // Pick a random recipe
        Recipe favorite = availableRecipes[Random.Range(0, availableRecipes.Count)];

        // Random mood & returning status
        float mood = Random.Range(0.5f, 1f);
        bool isReturning = Random.value > 0.5f;

        Customer newCustomer = new Customer(name, favorite, mood, isReturning);
        customerSystem.AddCustomer(newCustomer);

        Debug.Log(name + " entered the shop wanting " + favorite.recipeName);
    }
}