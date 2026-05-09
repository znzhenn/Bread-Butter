using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab;

    public Transform spawnPoint;

    public Recipe[] possibleRequests;

    public float spawnInterval = 10f;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer),
                        2f,
                        spawnInterval);
    }

    void SpawnCustomer()
    {
        GameObject customerObj =
            Instantiate(customerPrefab,
                        spawnPoint.position,
                        Quaternion.identity);

        Customer customer =
            customerObj.GetComponent<Customer>();

        customer.favoriteBread =
            possibleRequests[
                Random.Range(0, possibleRequests.Length)
            ];

        Debug.Log("Customer wants: "
                  + customer.favoriteBread.recipeName);
    }
}