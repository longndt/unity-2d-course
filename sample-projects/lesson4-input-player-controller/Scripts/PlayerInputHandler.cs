using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Advanced player input handler using Unity's New Input System.
/// Demonstrates input buffering, multi-device support, and responsive controls.
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    [Tooltip("Player movement speed")]
    public float moveSpeed = 5f;
    
    [Tooltip("Jump force applied to player")]
    public float jumpForce = 10f;
    
    [Tooltip("Coyote time duration")]
    public float coyoteTime = 0.1f;
    
    [Tooltip("Jump buffer time")]
    public float jumpBufferTime = 0.1f;
    
    [Header("Physics Settings")]
    [Tooltip("Ground check distance")]
    public float groundCheckDistance = 0.1f;
    
    [Tooltip("Layers considered as ground")]
    public LayerMask groundLayer = 1;
    
    [Header("Input System")]
    [Tooltip("Player Input component")]
    public PlayerInput playerInput;
    
    [Tooltip("Input action references")]
    public InputAction moveAction;
    public InputAction jumpAction;
    public InputAction attackAction;
    public InputAction interactAction;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool attackPressed;
    private bool interactPressed;
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private bool isFacingRight = true;
    private SpriteRenderer spriteRenderer;
    
    void Start()
    {
        // Get required components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Get Player Input component
        if (playerInput == null)
            playerInput = GetComponent<PlayerInput>();
        
        // Get input actions
        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            jumpAction = playerInput.actions["Jump"];
            attackAction = playerInput.actions["Attack"];
            interactAction = playerInput.actions["Interact"];
        }
        
        if (showDebugInfo)
            Debug.Log("PlayerInputHandler: Input system initialized");
    }
    
    void Update()
    {
        HandleCoyoteTime();
        HandleJumpBuffer();
        HandleJump();
        HandleDirection();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }
    
    // Input event methods (called by Player Input component)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        
        if (showDebugInfo)
            Debug.Log($"PlayerInputHandler: Move input: {moveInput}");
    }
    
    public void OnJump(InputValue value)
    {
        jumpPressed = value.isPressed;
        
        if (jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
        
        if (showDebugInfo)
            Debug.Log($"PlayerInputHandler: Jump input: {jumpPressed}");
    }
    
    public void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
        
        if (attackPressed)
        {
            Attack();
        }
        
        if (showDebugInfo)
            Debug.Log($"PlayerInputHandler: Attack input: {attackPressed}");
    }
    
    public void OnInteract(InputValue value)
    {
        interactPressed = value.isPressed;
        
        if (interactPressed)
        {
            Interact();
        }
        
        if (showDebugInfo)
            Debug.Log($"PlayerInputHandler: Interact input: {interactPressed}");
    }
    
    void HandleMovement()
    {
        if (moveInput.x != 0)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            // Apply deceleration when no input
            rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
        }
    }
    
    void HandleCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }
    
    void HandleJumpBuffer()
    {
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }
    
    void HandleJump()
    {
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
        }
    }
    
    void HandleDirection()
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            Flip();
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
        if (showDebugInfo)
            Debug.Log("PlayerInputHandler: Jump executed!");
    }
    
    void Attack()
    {
        if (showDebugInfo)
            Debug.Log("PlayerInputHandler: Attack executed!");
        
        // Add attack logic here
    }
    
    void Interact()
    {
        if (showDebugInfo)
            Debug.Log("PlayerInputHandler: Interact executed!");
        
        // Add interaction logic here
    }
    
    void Flip()
    {
        isFacingRight = !isFacingRight;
        
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !isFacingRight;
        }
        
        if (showDebugInfo)
            Debug.Log($"PlayerInputHandler: Flipped to face {(isFacingRight ? "right" : "left")}");
    }
    
    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        // Visual debug
        if (showDebugInfo)
        {
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
        }
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display input info
        GUI.Label(new Rect(10, 10, 300, 20), "Player Input Handler:");
        GUI.Label(new Rect(10, 30, 300, 20), $"Move Input: {moveInput}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Jump Pressed: {jumpPressed}");
        GUI.Label(new Rect(10, 70, 300, 20), $"Attack Pressed: {attackPressed}");
        GUI.Label(new Rect(10, 90, 300, 20), $"Interact Pressed: {interactPressed}");
        GUI.Label(new Rect(10, 110, 300, 20), $"Grounded: {isGrounded}");
        GUI.Label(new Rect(10, 130, 300, 20), $"Coyote Time: {coyoteTimeCounter:F2}");
        GUI.Label(new Rect(10, 150, 300, 20), $"Jump Buffer: {jumpBufferCounter:F2}");
        GUI.Label(new Rect(10, 170, 300, 20), $"Facing: {(isFacingRight ? "Right" : "Left")}");
    }
    
    void OnDrawGizmos()
    {
        if (!showDebugInfo) return;
        
        // Draw ground check ray
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
        
        // Draw movement direction
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.right * moveInput.x * 2f);
    }
}
