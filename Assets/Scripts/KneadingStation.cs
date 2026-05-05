using System.Collections;
using UnityEngine;

public class KneadingStation : MonoBehaviour
{
    public Recipe recipe;

    public void StartKneading()
    {
        StartCoroutine(KneadProcess());
    }

    private IEnumerator KneadProcess()
    {
        Debug.Log("Kneading...");
        yield return new WaitForSeconds(5f);

        Debug.Log("Proofing...");
        yield return new WaitForSeconds(5f);

        if (recipe.doughItem != null && recipe.doughItem.prefab != null)
        {
            Instantiate(recipe.doughItem.prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Dough item or prefab missing!");
        }
    }
}