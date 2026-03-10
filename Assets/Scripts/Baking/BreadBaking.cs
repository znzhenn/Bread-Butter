using UnityEngine;

public class BreadBaking : MonoBehaviour
{
    public GameObject breadPrefab;
    public BakingSystem bakingSystem; 

    public void BakeBread(Recipe recipe, float quality = 1f)
    {
        GameObject breadObj = Instantiate(breadPrefab);

        Bread breadScript = breadObj.GetComponent<Bread>();
        breadScript.Initialize(recipe, quality);
        bakingSystem.breadsForSale.Add(breadScript);

        Debug.Log($"Baked {recipe.recipeName} | Quality: {quality} | Value: {breadScript.breadValue}");
    }
}