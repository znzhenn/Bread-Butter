using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KneadingStation : MonoBehaviour, Interactable
{
    public List<Recipe> recipes;

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
        if (isProcessing)
            return;

        InventoryController inventory =
            FindFirstObjectByType<InventoryController>();

        if (inventory == null)
        {
            Debug.LogError("No InventoryController found!");
            return;
        }

        if (!inventory.HasIngredients(recipe.ingredients))
        {
            Debug.Log("Missing ingredients for " + recipe.recipeName);
            return;
        }

        StartCoroutine(Process(recipe, inventory));
    }

    private IEnumerator Process(
        Recipe recipe,
        InventoryController inventory)
    {
        isProcessing = true;

        Debug.Log("Crafting recipe: " + recipe.recipeName);

        inventory.ConsumeIngredients(recipe.ingredients);

        Debug.Log("Proofing...");

        yield return new WaitForSeconds(recipe.proofTime);

        GameObject dough =
            Instantiate(
                recipe.doughItem.prefab,
                transform.position,
                Quaternion.identity
            );

        BakingItem bakingItem =
            dough.GetComponent<BakingItem>();

        if (bakingItem != null)
        {
            bakingItem.recipe = recipe;
        }
        else
        {
            Debug.LogWarning("Dough missing BakingItem script!");
        }

        Debug.Log(recipe.recipeName + " dough finished!");

        isProcessing = false;
    }
}