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

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (isCustomer)
        {
            if (isDialogueActive)
            {
                NextLine();
            }
            else
            {
                TryPurchaseBread();
            }

            return;
        }

        if (dialogueData == null ||
            (PauseController.IsGamePaused &&
             !isDialogueActive))
        {
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

        if (breadToSell != null)
        {
            bakingSystem.breadsForSale.Remove(breadToSell);

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

            dialogueData.dialogueLines =
                new string[]
                {
                    "Thank you for the bread!"
                };

            Debug.Log(
                "Customer bought " +
                breadToSell.recipe.recipeName
            );
        }
        else
        {
            dialogueData.dialogueLines =
                new string[]
                {
                    "I'm still waiting for my order..."
                };

            Debug.Log(
                "Customer could not find requested bread."
            );
        }

        StartDialogue();
    }

    void StartDialogue()
    {
        isDialogueActive = true;

        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);

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
                dialogueData.dialogueLines[dialogueIndex]
            );

            isTyping = false;
        }
        else if (++dialogueIndex <
                 dialogueData.dialogueLines.Length)
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
                 dialogueData.dialogueLines[dialogueIndex])
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