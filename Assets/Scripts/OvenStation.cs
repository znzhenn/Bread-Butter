using System.Collections;
using UnityEngine;

public class OvenStation : MonoBehaviour
{
    public float bakeTime = 20f; // 👈 FIXES bakeTime error

    private bool isBaking = false;

    public void Interact()
    {
        if (isBaking) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (var hit in hits)
        {
            Item item = hit.GetComponent<Item>();

            if (item != null && item.Name == "Dough")
            {
                StartCoroutine(Bake(item)); // 👈 this now exists
                return;
            }
        }

        Debug.Log("No dough found!");
    }

    private IEnumerator Bake(Item doughItem) // 👈 FIXES Bake() error
    {
        isBaking = true;

        BakingItem bakingItem = doughItem.GetComponent<BakingItem>();

        if (bakingItem == null)
        {
            Debug.LogError("No BakingItem on dough!");
            isBaking = false;
            yield break;
        }

        Recipe recipe = bakingItem.recipe;

        Debug.Log("Baking " + recipe.recipeName);

        yield return new WaitForSeconds(bakeTime);

        Destroy(doughItem.gameObject);

        Instantiate(recipe.resultPrefab, transform.position, Quaternion.identity);

        Debug.Log(recipe.recipeName + " finished!");

        isBaking = false;
    }
}