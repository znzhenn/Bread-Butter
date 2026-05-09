using UnityEngine;
using UnityEngine.InputSystem;

public class MenuController : MonoBehaviour
{
    public GameObject menuCanvas;

    void Start()
    {
        menuCanvas.SetActive(false);
    }

    void Update()
    {
        if (KneadingRecipeMenu.ClosedThisFrame)
            return;

        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            bool isOpening = !menuCanvas.activeSelf;

            menuCanvas.SetActive(isOpening);

            PauseController.SetMenuPause(isOpening);
        }
    }
}