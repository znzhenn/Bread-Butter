using UnityEngine;
using TMPro;

public class BakeryUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI breadCountText;
    [SerializeField] private BakingSystem bakingSystem;

    private void Update()
    {
        if (bakingSystem != null && breadCountText != null)
        {
            breadCountText.text = "Bread In Stock: " + bakingSystem.BreadCount;
        }
    }
}