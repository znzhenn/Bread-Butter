using System.Collections;
using UnityEngine;

public class OvenStation : MonoBehaviour
{
    public Recipe recipe;

    public void StartBaking()
    {
        StartCoroutine(BakeProcess());
    }

    private IEnumerator BakeProcess()
    {
        Debug.Log("Baking...");
        yield return new WaitForSeconds(20f);

        if (recipe.resultItem != null && recipe.resultItem.prefab != null)
        {
            Instantiate(recipe.resultItem.prefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Result item or prefab missing!");
        }
    }
}