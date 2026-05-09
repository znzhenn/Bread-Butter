using System.Collections;
using UnityEngine;

public class OvenStation : MonoBehaviour, Interactable
{
    public float interactRadius = 1.5f;

    private bool isBaking = false;

    public void Interact()
    {
        if (isBaking)
        {
            Debug.Log("Oven is already baking.");
            return;
        }

        Collider2D[] hits =
            Physics2D.OverlapCircleAll(
                transform.position,
                interactRadius
            );

        foreach (Collider2D hit in hits)
        {
            Item item = hit.GetComponent<Item>();

            if (item != null &&
                item.GetComponent<BakingItem>() != null)
            {
                Debug.Log("Found dough!");

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

        float quality = Random.Range(0.5f, 1f);

        Bread bread = new Bread(recipe, quality);

        BakingSystem bakingSystem =
            FindFirstObjectByType<BakingSystem>();

        if (bakingSystem != null)
        {
            bakingSystem.AddBread(bread);

            Debug.Log(
                recipe.recipeName +
                " added to bakery stock!"
            );
        }
        else
        {
            Debug.LogError("No BakingSystem found!");
        }

        Destroy(doughItem.gameObject);

        isBaking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(
            transform.position,
            interactRadius
        );
    }
}