using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KneadingStation : MonoBehaviour
{
    public List<Recipe> recipes;
    public float interactRadius = 1.5f;

    private bool isProcessing = false;

    // Call this from your interaction (E key)
    public void Interact()
    {
        if (isProcessing) return;

        // Find nearby items (you can tweak layer/tag if needed)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius);

        List<Item> nearbyItems = new List<Item>();
        foreach (var hit in hits)
        {
            Item item = hit.GetComponent<Item>();
            if (item != null)
                nearbyItems.Add(item);
        }

        Recipe match = FindMatchingRecipe(nearbyItems);
        if (match == null)
        {
            Debug.Log("No matching recipe.");
            return;
        }

        StartCoroutine(Process(match, nearbyItems));
    }

    private Recipe FindMatchingRecipe(List<Item> items)
    {
        foreach (var recipe in recipes)
        {
            if (Matches(recipe, items))
                return recipe;
        }
        return null;
    }

    private bool Matches(Recipe recipe, List<Item> items)
    {
        if (items.Count < recipe.ingredientNames.Count)
            return false;

        // Copy list so we can “use up” matches
        List<Item> temp = new List<Item>(items);

        foreach (var needed in recipe.ingredientNames)
        {
            bool found = false;

            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Name == needed)
                {
                    temp.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (!found) return false;
        }

        return true;
    }

    private IEnumerator Process(Recipe recipe, List<Item> items)
    {
        isProcessing = true;

        Debug.Log("kneading instantly...");

        ConsumeIngredients(recipe, items);

        // Spawn dough immediately
        GameObject dough = Instantiate(recipe.resultPrefab, transform.position, Quaternion.identity);
        BakingItem bakingItem = dough.GetComponent<BakingItem>();
        if (bakingItem != null)
        {
            bakingItem.recipe = recipe;
        }
        else
        {
            Debug.LogError("Dough prefab is missing BakingItem script!");
        }

        Debug.Log("Proofing...");
        yield return new WaitForSeconds(recipe.proofTime);

        Debug.Log("Dough finished proofing!");

        isProcessing = false;
    }

    private void ConsumeIngredients(Recipe recipe, List<Item> items)
    {
        List<Item> temp = new List<Item>(items);

        foreach (var needed in recipe.ingredientNames)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].Name == needed)
                {
                    Destroy(temp[i].gameObject);
                    temp.RemoveAt(i);
                    break;
                }
            }
        }
    }

    // Optional: visualize radius
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}