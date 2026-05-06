using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/NPC Dialogue")]
public class NPCDialogue : ScriptableObject
{
    [Header("NPC")]
    public string npcName;

    public Sprite npcPortrait;

    [Header("Order Dialogue")]
    [TextArea]
    public string[] orderLines;

    [Header("Waiting Dialogue")]
    [TextArea]
    public string[] waitingLines;

    [Header("Thank You Dialogue")]
    [TextArea]
    public string[] thankYouLines;

    [Header("Dialogue Settings")]
    public float typingSpeed = 0.03f;

    public float autoProgressDelay = 1f;

    public bool[] autoProgressLines;

    public AudioClip voiceSound;
}