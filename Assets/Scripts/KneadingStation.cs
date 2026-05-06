using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KneadingStation : MonoBehaviour, Interactable
{
    public List<Recipe> recipes;
    public float interactRadius = 1.5f;

    private bool isProcessing = false;

    public void Interact()
    {
        if (isProcessing)
        {
            Debug.Log("Kneading station is already processing.");
            return;
        }

        if (KneadingRecipeMenu.Instance == null)
        {
            Debug.LogError("No KneadingRecipeMenu found in scene.");
            return;
        }

        KneadingRecipeMenu.Instance.Open(this, recipes);
    }

    public bool CanInteract()
    {
        return !isProcessing;
    }

    public void CraftRecipe(Recipe recipe)
    {
        if (isProcessing) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRadius);

        List<Item> nearbyItems = new List<Item>();

        foreach (Collider2D hit in hits)
        {
            Item item = hit.GetComponent<Item>();

            if (item != null)
            {
                nearbyItems.Add(item);
            }
        }

        if (!HasIngredients(recipe, nearbyItems))
        {
            Debug.Log("Missing ingredients for " + recipe.recipeName);
            return;
        }

        StartCoroutine(Process(recipe, nearbyItems));
    }

    private bool HasIngredients(Recipe recipe, List<Item> items)
    {
        List<Item> temp = new List<Item>(items);

        foreach (ItemData needed in recipe.ingredients)
        {
            bool found = false;

            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].data == needed)
                {
                    temp.RemoveAt(i);
                    found = true;
                    break;
                }
            }

            if (!found)
                return false;
        }

        return true;
    }

    private IEnumerator Process(Recipe recipe, List<Item> items)
    {
        isProcessing = true;

        Debug.Log("Crafting recipe: " + recipe.recipeName);

        ConsumeIngredients(recipe, items);

        Debug.Log("Proofing...");
        yield return new WaitForSeconds(recipe.proofTime);

        GameObject result = Instantiate(recipe.resultItem.prefab, transform.position, Quaternion.identity);

        BakingItem bakingItem = result.GetComponent<BakingItem>();
        if (bakingItem != null)
        {
            bakingItem.recipe = recipe;
        }

        Debug.Log(recipe.recipeName + " finished!");

        isProcessing = false;
    }

    private void ConsumeIngredients(Recipe recipe, List<Item> items)
    {
        List<Item> temp = new List<Item>(items);

        foreach (ItemData needed in recipe.ingredients)
        {
            for (int i = 0; i < temp.Count; i++)
            {
                if (temp[i].data == needed)
                {
                    Destroy(temp[i].gameObject);
                    temp.RemoveAt(i);
                    break;
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}