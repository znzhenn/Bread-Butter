using UnityEngine;
using UnityEngine.InputSystem;

public class BakingSystem : MonoBehaviour
{
     private PlayerInputActions inputActions;
    public DisplaySystem displaySystem;
   
    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        inputActions.Gameplay.Bake.performed += ctx => BakeBread();
        inputActions.Gameplay.Enable();
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Bake.performed -= ctx => BakeBread();
        inputActions.Gameplay.Disable();
    }

    private void OnBake(InputAction.CallbackContext context)
    {
        if (GameManager.Instance == null || displaySystem == null) return;
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
