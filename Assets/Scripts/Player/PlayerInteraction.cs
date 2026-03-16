using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;

    void OnInteract()
    {
        Debug.Log("Interact pressed!");
        TryInteract();
    }

    void TryInteract()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue; // skip the player

            Debug.Log("Hit: " + hit.name);

            Interactable interactable = hit.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact();
                return;
            }
        }

        Debug.Log("Nothing interactable found");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}