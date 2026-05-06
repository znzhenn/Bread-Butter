using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, Interactable
{
    [Header("Dialogue")]
    public NPCDialogue dialogueData;

    public GameObject dialoguePanel;
    public TMPro.TMP_Text dialogueText;
    public TMPro.TMP_Text nameText;

    public Image portraitImage;

    [Header("Customer")]
    public bool isCustomer;
    public Recipe requestedBread;

    private int dialogueIndex;

    private bool isTyping;
    private bool isDialogueActive;

    private bool hasOrdered = false;
    private bool hasBeenServed = false;

    private string[] currentLines;

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (isCustomer)
        {
            HandleCustomerInteraction();
            return;
        }

        currentLines = dialogueData.orderLines;

        HandleDialogue();
    }

    void HandleCustomerInteraction()
    {
        if (isDialogueActive)
        {
            NextLine();
            return;
        }

        // First interaction
        if (!hasOrdered)
        {
            hasOrdered = true;

            currentLines =
                dialogueData.orderLines;

            StartDialogue();

            return;
        }

        // Try buying bread
        if (!hasBeenServed)
        {
            TryPurchaseBread();

            return;
        }

        // Already served
        currentLines =
            dialogueData.thankYouLines;

        StartDialogue();
    }

    void TryPurchaseBread()
    {
        BakingSystem bakingSystem =
            FindFirstObjectByType<BakingSystem>();

        if (bakingSystem == null)
        {
            Debug.LogError("No BakingSystem found!");
            return;
        }

        Bread breadToSell =
            bakingSystem.breadsForSale.Find(
                bread => bread.recipe == requestedBread
            );

        // Customer found requested bread
        if (breadToSell != null)
        {
            bakingSystem.breadsForSale.Remove(
                breadToSell
            );

            CurrencyManager currency =
                FindFirstObjectByType<CurrencyManager>();

            if (currency != null)
            {
                currency.AddMoney(
                    Mathf.RoundToInt(
                        breadToSell.breadValue
                    )
                );
            }

            hasBeenServed = true;

            currentLines =
                dialogueData.thankYouLines;

            Debug.Log(
                "Customer bought " +
                breadToSell.recipe.recipeName
            );
        }
        else
        {
            currentLines =
                dialogueData.waitingLines;

            Debug.Log(
                "Customer is still waiting for bread."
            );
        }

        StartDialogue();
    }

    void HandleDialogue()
    {
        if (dialogueData == null)
        {
            Debug.LogError(
                name + " has no dialogue data!"
            );

            return;
        }

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue()
    {
        if (dialogueData == null)
        {
            Debug.LogError(
                name + " missing dialogueData!"
            );

            return;
        }

        if (currentLines == null ||
            currentLines.Length == 0)
        {
            Debug.LogError(
                name + " has no dialogue lines assigned!"
            );

            return;
        }

        isDialogueActive = true;

        dialogueIndex = 0;

        nameText.SetText(
            dialogueData.npcName
        );

        portraitImage.sprite =
            dialogueData.npcPortrait;

        dialoguePanel.SetActive(true);

        PauseController.SetDialoguePause(true);

        StartCoroutine(TypeLine());
    }

    void NextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();

            dialogueText.SetText(
                currentLines[dialogueIndex]
            );

            isTyping = false;
        }
        else if (++dialogueIndex <
                 currentLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine()
    {
        isTyping = true;

        dialogueText.SetText("");

        foreach (char letter in
                 currentLines[dialogueIndex])
        {
            dialogueText.text += letter;

            yield return new WaitForSeconds(
                dialogueData.typingSpeed
            );
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length >
            dialogueIndex &&
            dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(
                dialogueData.autoProgressDelay
            );

            NextLine();
        }

        if (dialogueData.voiceSound != null)
        {
            AudioSource.PlayClipAtPoint(
                dialogueData.voiceSound,
                Camera.main.transform.position,
                0.5f
            );
        }
    }

    public void EndDialogue()
    {
        StopAllCoroutines();

        isDialogueActive = false;

        dialoguePanel.SetActive(false);

        PauseController.SetDialoguePause(false);
    }
}