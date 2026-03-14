using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BreadInventoryUI : MonoBehaviour
{
    public BakingSystem bakingSystem;

    public Transform breadListParent;
    public GameObject breadItemPrefab;

    void Update()
    {
        RefreshUI();
    }

    void RefreshUI()
    {
        foreach (Transform child in breadListParent)
        {
            Destroy(child.gameObject);
        }

        Dictionary<string, int> breadCounts = new Dictionary<string, int>();

        foreach (Bread bread in bakingSystem.breadsForSale)
        {
            string name = bread.recipe.recipeName;

            if (!breadCounts.ContainsKey(name))
                breadCounts[name] = 0;

            breadCounts[name]++;
        }

        foreach (var entry in breadCounts)
        {
            GameObject item = Instantiate(breadItemPrefab, breadListParent);

            TextMeshProUGUI text = item.GetComponent<TextMeshProUGUI>();
            text.text = entry.Key + " x" + entry.Value;
        }
    }
}