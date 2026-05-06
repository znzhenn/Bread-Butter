using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, Interactable
{
    [Header("Dialogue")]
    public NPCDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMPro.TMP_Text dialogueText, nameText;
    public Image portraitImage;

    [Header("Customer Settings")]
    public bool isCustomer = false;
    public Recipe requestedRecipe;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    private BakingSystem bakingSystem;

    void Start()
    {
        bakingSystem = FindFirstObjectByType<BakingSystem>();
    }

    public bool CanInteract()
    {
        return !isDialogueActive;
    }

    public void Interact()
    {
        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
            return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            if (isCustomer)
            {
                StartCustomerDialogue();
            }
            else
            {
                StartDialogue();
            }
        }
    }

    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;
        dialoguePanel.SetActive(true);

        PauseController.SetDialoguePause(true);

        StartCoroutine(TypeLine());
    }

    void StartCustomerDialogue()
    {
        isDialogueActive = true;

        nameText.SetText(dialogueData.npcName);
        portraitImage.sprite = dialogueData.npcPortrait;
        dialoguePanel.SetActive(true);

        PauseController.SetDialoguePause(true);

        TrySellBread();
    }

    void TrySellBread()
    {
        if (requestedRecipe == null)
        {
            dialogueText.SetText("I don't know what I want yet.");
            return;
        }

        if (bakingSystem == null)
        {
            Debug.LogError("No BakingSystem found in scene.");
            dialogueText.SetText("Something went wrong with the bakery stock.");
            return;
        }

        Bread breadToSell = bakingSystem.breadsForSale.Find(
            bread => bread.recipe == requestedRecipe
        );

        if (breadToSell != null)
        {
            bakingSystem.breadsForSale.Remove(breadToSell);

            int saleAmount = Mathf.RoundToInt(breadToSell.breadValue);

            CurrencyManager currency = FindFirstObjectByType<CurrencyManager>();

            if (currency != null)
            {
                currency.AddMoney(saleAmount);
            }
            else
            {
                Debug.LogError("No CurrencyManager found in scene.");
            }

            dialogueText.SetText(
                "Thank you! This " + requestedRecipe.recipeName +
                " looks delicious. Here's " + saleAmount + " coins!"
            );

            Debug.Log("Sold " + requestedRecipe.recipeName + " for " + saleAmount + " coins.");
        }
        else
        {
            dialogueText.SetText(
                "Hi! I'd like a " + requestedRecipe.recipeName + ", please."
            );
        }
    }

    void NextLine()
    {
        if (isCustomer)
        {
            EndDialogue();
            return;
        }

        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
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

        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        if (dialogueData.autoProgressLines.Length > dialogueIndex &&
            dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
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