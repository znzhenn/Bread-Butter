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
        if (isProcessing) return;

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, interactRadius);

        List<Item> nearbyItems = new List<Item>();

        foreach (var hit in hits)
        {
            Item item = hit.GetComponent<Item>();

            if (item != null)
            {
                nearbyItems.Add(item);
            }
        }

        Recipe match = FindMatchingRecipe(nearbyItems);

        if (match == null)
        {
            Debug.Log("No matching recipe.");
            return;
        }

        StartCoroutine(Process(match, nearbyItems));
    }

    public bool CanInteract()
    {
        return !isProcessing;
    }

    private Recipe FindMatchingRecipe(List<Item> items)
    {
        foreach (Recipe recipe in recipes)
        {
            if (Matches(recipe, items))
            {
                return recipe;
            }
        }

        return null;
    }

    private bool Matches(Recipe recipe, List<Item> items)
    {
        if (items.Count < recipe.ingredients.Count)
            return false;

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

        Debug.Log("Kneading instantly...");

        ConsumeIngredients(recipe, items);

        GameObject dough =
            Instantiate(recipe.resultItem.prefab,
                        transform.position,
                        Quaternion.identity);

        BakingItem bakingItem = dough.GetComponent<BakingItem>();

        if (bakingItem != null)
        {
            bakingItem.recipe = recipe;
        }
        else
        {
            Debug.LogError("Dough prefab missing BakingItem!");
        }

        Debug.Log("Proofing...");

        yield return new WaitForSeconds(recipe.proofTime);

        Debug.Log("Dough finished proofing!");

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