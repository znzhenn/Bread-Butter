using System.Collections;
using UnityEngine;

public class OvenStation : MonoBehaviour, Interactable
{
    public float bakeTime = 20f;

    private bool isBaking = false;

    public void Interact()
    {
        if (isBaking) return;

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, 1.5f);

        foreach (var hit in hits)
        {
            Item item = hit.GetComponent<Item>();

            if (item != null && item.data.itemName == "Dough")
            {
                StartCoroutine(Bake(item));
                return;
            }
        }

        Debug.Log("No dough found!");
    }

    public bool CanInteract()
    {
        return !isBaking;
    }

    private IEnumerator Bake(Item doughItem)
    {
        isBaking = true;

        BakingItem bakingItem =
            doughItem.GetComponent<BakingItem>();

        if (bakingItem == null)
        {
            Debug.LogError("No BakingItem on dough!");

            isBaking = false;
            yield break;
        }

        Recipe recipe = bakingItem.recipe;

        Debug.Log("Baking " + recipe.recipeName);

        yield return new WaitForSeconds(recipe.bakeTime);

        Destroy(doughItem.gameObject);

        Instantiate(recipe.resultItem.prefab, transform.position, Quaternion.identity);

        Debug.Log(recipe.recipeName + " finished!");

        isBaking = false;
    }
}