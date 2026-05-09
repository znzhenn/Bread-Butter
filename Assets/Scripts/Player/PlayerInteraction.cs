using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public float interactRange = 2f;
    // public int interactionPriority;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        Debug.Log("Interact pressed!");
        TryInteract();
    }

 void TryInteract()
    {
        Collider2D[] hits =
            Physics2D.OverlapCircleAll(transform.position, interactRange);

        PlayerController playerController =
            GetComponent<PlayerController>();

        Vector2 facing = playerController.facingDirection;

        Interactable bestInteractable = null;
        float bestScore = -Mathf.Infinity;

        foreach(Collider2D hit in hits)
        {
            if(hit.gameObject == gameObject)
                continue;

            Interactable interactable =
                hit.GetComponentInParent<Interactable>();

            if(interactable == null)
                continue;

            Vector2 toTarget = (hit.transform.position - transform.position).normalized;

            float directionScore = Vector2.Dot(facing, toTarget);

            float distance = Vector2.Distance(transform.position, hit.transform.position);

            // closer objects get slightly better score
            float distanceScore = 1f / (distance + 0.1f);

            float score = directionScore + distanceScore;

            Debug.Log(hit.name + " score: " + score);

            if(score > bestScore)
            {
                bestScore = score;
                bestInteractable = interactable;
            }
        }

        if(bestInteractable != null)
        {
            bestInteractable.Interact();
        }
        else
        {
            Debug.Log("Nothing interactable found");
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, interactRange);
    }
}