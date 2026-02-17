using UnityEngine;
using UnityEngine.InputSystem;

public class BakingSystem : MonoBehaviour
{
    private DisplaySystem displaySystem;
    private PlayerInputActions inputActions;
    private void Awake()
    {
        displaySystem = DisplaySystem.Instance;
        inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Bake.performed += OnBake;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Bake.performed -= OnBake;
        inputActions.Gameplay.Disable();
    }

    private void OnBake(InputAction.CallbackContext context)
    {
        BakeBread();
    }

    public void BakeBread()
    {
        if (displaySystem == null)
        {
            Debug.LogError("Display System is not assigned!");
            return;
        }
        
        GameManager.GameState current = GameManager.Instance.CurrentState;
    if (current != GameManager.GameState.StartDay && current != GameManager.GameState.OpenShop)
    {
        Debug.Log("Cannot bake right now! Wait until StartDay or OpenShop.");
        return;
    }

    if (displaySystem.breadsOnDisplay.Count >= displaySystem.MaxSlots)
    {
        Debug.Log("Display is Already Full!");
        return;
    }

        int quality = Random.Range(50, 101);
        int value = quality/10;

        Bread bread = new Bread("White Loaf", quality, value);
        displaySystem.AddBread(bread);
        //prints what bread is added
            
    }

}
