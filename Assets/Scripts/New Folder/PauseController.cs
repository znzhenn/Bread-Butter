using UnityEngine;

public class PauseController : MonoBehaviour
{
    private static bool menuPaused = false;
    private static bool dialoguePaused = false;

    public static bool IsGamePaused => menuPaused || dialoguePaused;

    public static void SetMenuPause(bool pause)
    {
        menuPaused = pause;
        UpdateTimeScale();
    }

    public static void SetDialoguePause(bool pause)
    {
        dialoguePaused = pause;
        UpdateTimeScale();
    }

    private static void UpdateTimeScale()
    {
        Time.timeScale = IsGamePaused ? 0f : 1f;
    }
}