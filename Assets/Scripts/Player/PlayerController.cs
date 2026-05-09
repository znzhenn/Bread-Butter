using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private PlayerInputActions inputActions;
    private Animator animator;
    public Vector2 facingDirection = Vector2.down;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        if (inputActions == null)
            inputActions = new PlayerInputActions();

        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        if (inputActions != null)
            inputActions.Player.Disable();
    }

    void Update()
    {
        if (PauseController.IsGamePaused)
        {
            rb.linearVelocity = Vector2.zero;
            movement = Vector2.zero;
            animator.SetBool("isWalking",false);
            return;
        } 
            rb.linearVelocity = movement * moveSpeed;
            animator.SetBool("isWalking",rb.linearVelocity.magnitude > 0.1f);
        
    }

    // void FixedUpdate()
    // {
    //     if (PauseController.IsGamePaused)
    //     {
    //         return;
    //     }

    //     rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    // }

    public void Move(InputAction.CallbackContext context)
    {
        
        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);
        }

        movement = context.ReadValue<Vector2>();

        if(movement != Vector2.zero)
        {
            facingDirection = movement.normalized;
        }
        
        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);
    }
}