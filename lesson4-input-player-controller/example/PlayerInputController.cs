using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Modern Input System implementation using Player Input component
/// Demonstrates the new Input System approach with Input Actions
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerInputController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private Rigidbody2D rb;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInputController requires PlayerInput component!");
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("PlayerInputController requires Rigidbody2D component!");
            enabled = false;
            return;
        }

        // Get references to actions
        if (playerInput.actions == null)
        {
            Debug.LogError("PlayerInput has no Input Actions assigned!");
            enabled = false;
            return;
        }

        moveAction = playerInput.actions.FindAction("Move", false);
        jumpAction = playerInput.actions.FindAction("Jump", false);

        if (moveAction == null)
        {
            Debug.LogError("Move action not found in Input Actions!");
        }

        if (jumpAction == null)
        {
            Debug.LogError("Jump action not found in Input Actions!");
        }
    }

    void OnEnable()
    {
        // NEW WAY - Event-driven input (recommended)
        if (moveAction != null)
        {
            moveAction.performed += OnMovePerformed;
            moveAction.canceled += OnMoveCanceled;
            moveAction.Enable();
        }

        if (jumpAction != null)
        {
            jumpAction.started += OnJumpStarted;      // Key down
            jumpAction.performed += OnJumpPerformed;  // Key pressed
            jumpAction.canceled += OnJumpCanceled;    // Key up
            jumpAction.Enable();
        }
    }

    void OnDisable()
    {
        // Unsubscribe from events
        if (moveAction != null)
        {
            moveAction.performed -= OnMovePerformed;
            moveAction.canceled -= OnMoveCanceled;
            moveAction.Disable();
        }

        if (jumpAction != null)
        {
            jumpAction.started -= OnJumpStarted;
            jumpAction.performed -= OnJumpPerformed;
            jumpAction.canceled -= OnJumpCanceled;
            jumpAction.Disable();
        }
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        if (rb == null) return;
        Vector2 input = context.ReadValue<Vector2>();
        rb.velocity = new Vector2(input.x * moveSpeed, rb.velocity.y);
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        if (rb == null) return;
        rb.velocity = new Vector2(0f, rb.velocity.y);
    }

    void OnJumpStarted(InputAction.CallbackContext context)
    {
        Debug.Log("Jump started");
    }

    void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (rb == null) return;
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump performed (New Input System)");
    }

    void OnJumpCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Jump released");
    }
}