using System.Collections;
using UnityEngine;

public class OvenStation : MonoBehaviour
{
    private Item currentItem;
    private float bakeTimer = 0f;
    private bool isBaking = false;

    public GameObject resultPrefab; // Bread

    public void Interact()
    {
        // If nothing in oven → try to add dough
        if (!isBaking)
        {
            TryInsertDough();
        }
        else
        {
            // Take out bread
            RemoveItem();
        }
    }

    private void TryInsertDough()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (var hit in hits)
        {
            Item item = hit.GetComponent<Item>();

            if (item != null && item.Name == "Dough")
            {
                currentItem = item;
                StartCoroutine(Bake());
                return;
            }
        }

        Debug.Log("No dough to bake!");
    }

    private IEnumerator Bake()
    {
        isBaking = true;
        bakeTimer = 0f;

        Debug.Log("Started baking...");

        while (true)
        {
            bakeTimer += Time.deltaTime;
            yield return null;
        }
    }

    private void RemoveItem()
    {
        StopAllCoroutines();

        Destroy(currentItem.gameObject);

        Instantiate(resultPrefab, transform.position, Quaternion.identity);

        Debug.Log("Removed after " + bakeTimer + " seconds");

        isBaking = false;
        currentItem = null;
    }
}