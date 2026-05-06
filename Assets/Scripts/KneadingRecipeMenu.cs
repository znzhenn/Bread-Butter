using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KneadingRecipeUI : MonoBehaviour
{
    public static KneadingRecipeUI Instance { get; private set; }

    [Header("UI")]
    public GameObject menuPanel;
    public Transform recipeButtonContainer;
    public GameObject recipeButtonPrefab;

    private KneadingStation currentStation;

    private void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    public void Open(KneadingStation station, List<Recipe> recipes)
    {
        currentStation = station;

        gameObject.SetActive(true);

        ClearButtons();

        foreach (Recipe recipe in recipes)
        {
            GameObject buttonObj = Instantiate(recipeButtonPrefab, recipeButtonContainer);

            TMP_Text buttonText = buttonObj.GetComponentInChildren<TMP_Text>();
            buttonText.text = recipe.recipeName;

            Button button = buttonObj.GetComponent<Button>();
            button.onClick.AddListener(() =>
            {
                currentStation.CraftRecipe(recipe);
                Close();
            });
        }

        PauseController.SetMenuPause(true);
    }

    public void Close()
    {
        ClearButtons();

        currentStation = null;

        gameObject.SetActive(false);

        PauseController.SetMenuPause(false);
    }

    private void ClearButtons()
    {
        foreach (Transform child in recipeButtonContainer)
        {
            Destroy(child.gameObject);
        }
    }
}