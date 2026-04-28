using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movement;

    private PlayerInputActions inputActions;
    private Animator animator;

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
        movement = inputActions.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = movement * moveSpeed;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (animator == null) return;

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            return;
        }

        movement = context.ReadValue<Vector2>();

        animator.SetBool("isWalking", true);
        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);
    }
}