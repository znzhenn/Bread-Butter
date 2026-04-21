using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    private PlayerInputActions inputActions;
    private Animator animator;

    void Start()
    {
      rb = GetComponent<Rigidbody2D>();  
      animator = GetComponent<Animator>();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Update()
    {
        rb.linearVelocity = movement * moveSpeed;
        movement = inputActions.Player.Move.ReadValue<Vector2>();
        /*if (movement != Vector2.zero)
        {
            Debug.Log(movement);
        }*/
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("InputX", movement.x);
            animator.SetFloat("InputY", movement.y);
            
            return;
        }
        movement = context.ReadValue<Vector2>();
        animator.SetFloat("InputX", movement.x);
        animator.SetFloat("InputY", movement.y);
    }

}