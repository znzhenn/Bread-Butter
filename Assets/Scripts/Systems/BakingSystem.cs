using UnityEngine;
using UnityEngine.InputSystem;

public class BakingSystem : MonoBehaviour
{
    public DisplaySystem displaySystem;
    private PlayerInputActions inputActions;

    private void Awake()
    {
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
        if (GameManager.Instance.CurrentState == GameManager.GameState.EndDay)
        {
            return; // no baking at the end of the day
        }
        int quality = Random.Range(50, 101);
        int value = quality/10;

        Bread bread = new Bread("White Loaf", quality, value);
        displaySystem.AddBread(bread);
        //prints what bread is added
            
    }

}
