using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log("Interact pressed!");
        TryInteract();
    }

    void TryInteract()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, interactRange);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
                continue;

            Debug.Log("Hit: " + hit.name);

            Interactable interactable = hit.GetComponentInParent<Interactable>();

            if (interactable != null)
            {
                interactable.Interact(); // ✅ FIXED
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