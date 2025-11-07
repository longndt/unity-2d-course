using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Complete 2D Player Controller with advanced features
/// Demonstrates professional movement mechanics with Input System
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player2DController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float airControl = 0.5f;

    [Header("Jump Settings")]
    public float jumpHeight = 4f;
    public float jumpTimeToApex = 0.5f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayerMask = 1;

    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private InputAction moveAction;
    private InputAction jumpAction;

    private Vector2 moveInput;
    private bool isGrounded;
    private float lastGroundedTime;
    private float jumpBufferCounter;
    private float jumpForce;
    private float gravity;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("Player2DController requires PlayerInput component!");
            enabled = false;
            return;
        }

        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Player2DController requires Rigidbody2D component!");
            enabled = false;
            return;
        }

        if (playerInput.actions == null)
        {
            Debug.LogError("PlayerInput has no Input Actions assigned!");
            enabled = false;
            return;
        }

        moveAction = playerInput.actions.FindAction("Move", false);
        jumpAction = playerInput.actions.FindAction("Jump", false);

        if (moveAction == null || jumpAction == null)
        {
            Debug.LogError("Move or Jump action not found in Input Actions!");
        }

        CalculateJumpPhysics();
    }

    void OnEnable()
    {
        if (moveAction != null)
        {
            moveAction.performed += OnMove;
            moveAction.canceled += OnMove;
            moveAction.Enable();
        }

        if (jumpAction != null)
        {
            jumpAction.performed += OnJump;
            jumpAction.Enable();
        }
    }

    void OnDisable()
    {
        if (moveAction != null)
        {
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnMove;
            moveAction.Disable();
        }

        if (jumpAction != null)
        {
            jumpAction.performed -= OnJump;
            jumpAction.Disable();
        }
    }

    void Update()
    {
        if (rb == null) return;

        CheckGrounded();
        HandleCoyoteTime();
        HandleJumpBuffer();
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        HandleMovement();
    }

    void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnJump(InputAction.CallbackContext context)
    {
        jumpBufferCounter = jumpBufferTime;
    }

    void HandleMovement()
    {
        if (rb == null) return;

        float targetSpeed = moveInput.x * moveSpeed;
        float currentSpeed = rb.velocity.x;

        float speedDifference = targetSpeed - currentSpeed;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;

        if (!isGrounded)
            accelRate *= airControl;

        float movement = speedDifference * accelRate;
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);
    }

    void CheckGrounded()
    {
        if (groundCheck == null)
        {
            Debug.LogWarning("Player2DController: GroundCheck not assigned!");
            return;
        }

        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

        if (wasGrounded && !isGrounded)
        {
            lastGroundedTime = Time.time;
        }
    }

    void HandleCoyoteTime()
    {
        // Coyote time allows jumping shortly after leaving ground
        // This makes platforming feel more forgiving
    }

    void HandleJumpBuffer()
    {
        if (jumpBufferCounter > 0f)
        {
            jumpBufferCounter -= Time.deltaTime;

            bool canCoyoteJump = Time.time - lastGroundedTime <= coyoteTime;
            if (isGrounded || canCoyoteJump)
            {
                Jump();
                jumpBufferCounter = 0f;
            }
        }
    }

    void Jump()
    {
        if (rb == null) return;

        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    void CalculateJumpPhysics()
    {
        if (rb == null) return;

        // Calculate jump force based on desired height and time to apex
        gravity = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        jumpForce = Mathf.Abs(gravity) * jumpTimeToApex;

        rb.gravityScale = gravity / Physics2D.gravity.y;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}