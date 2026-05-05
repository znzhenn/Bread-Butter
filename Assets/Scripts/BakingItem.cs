using UnityEngine;

public class BakingItem : MonoBehaviour
{
    public Recipe recipe;
    public float quality = 1f;

    public enum State
    {
        Raw,
        Kneaded,
        Proofed,
        Baked
    }

    public State currentState;
}