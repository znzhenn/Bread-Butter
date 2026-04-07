using UnityEngine;

public class OpenShopTrigger : Interactable
{
    public override void Interact()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("GameManager not found!");
            return;
        }

        if (GameManager.Instance.CurrentState == GameManager.GameState.StartDay)
        {
            GameManager.Instance.OpenShop();
            Debug.Log("Shop opened via interaction!");
        }
        else
        {
            Debug.Log("Shop is already open or cannot be opened right now.");
        }
    }
}