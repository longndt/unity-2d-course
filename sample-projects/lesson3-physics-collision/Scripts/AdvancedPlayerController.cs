using UnityEngine;

/// <summary>
/// Advanced player controller with responsive jump mechanics.
/// Implements coyote time, jump buffering, and variable jump height.
/// </summary>
public class AdvancedPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Player movement speed")]
    public float moveSpeed = 10f;
    
    [Tooltip("Jump force applied to player")]
    public float jumpForce = 15f;
    
    [Tooltip("Maximum horizontal speed")]
    public float maxSpeed = 8f;
    
    [Tooltip("Acceleration rate")]
    public float acceleration = 20f;
    
    [Tooltip("Deceleration rate")]
    public float deceleration = 20f;
    
    [Header("Advanced Jump Settings")]
    [Tooltip("How long jump input is held")]
    public float jumpTime = 0.2f;
    
    [Tooltip("Coyote time duration")]
    public float coyoteTime = 0.1f;
    
    [Tooltip("Jump buffer time")]
    public float jumpBufferTime = 0.1f;
    
    [Tooltip("Jump cut multiplier for shorter jumps")]
    public float jumpCutMultiplier = 0.5f;
    
    [Header("Physics Settings")]
    [Tooltip("Ground check distance")]
    public float groundCheckDistance = 0.1f;
    
    [Tooltip("Layers considered as ground")]
    public LayerMask groundLayer = 1;
    
    [Tooltip("Layers considered as one-way platforms")]
    public LayerMask oneWayLayer = 2;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    [Tooltip("Show ground check rays")]
    public bool showGroundCheck = true;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool jumpInput;
    private bool jumpInputStop;
    private float horizontalInput;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (showDebugInfo)
            Debug.Log("AdvancedPlayerController: Advanced player controller initialized");
    }
    
    void Update()
    {
        HandleInput();
        CheckGrounded();
        HandleCoyoteTime();
        HandleJumpBuffer();
        HandleJump();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }
    
    void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKey(KeyCode.Space);
        jumpInputStop = Input.GetKeyUp(KeyCode.Space);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
    }
    
    void HandleMovement()
    {
        if (horizontalInput != 0)
        {
            // Apply horizontal movement with acceleration
            float targetSpeed = horizontalInput * moveSpeed;
            float currentSpeed = rb.velocity.x;
            float speedDifference = targetSpeed - currentSpeed;
            float accelerationRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : deceleration;
            
            rb.velocity = new Vector2(currentSpeed + speedDifference * accelerationRate * Time.fixedDeltaTime, rb.velocity.y);
        }
        else
        {
            // Apply deceleration when no input
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.fixedDeltaTime), rb.velocity.y);
        }
        
        // Limit horizontal speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }
    
    void CheckGrounded()
    {
        wasGrounded = isGrounded;
        
        // Check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        // Check for one-way platforms if not grounded
        if (!isGrounded)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, oneWayLayer);
            isGrounded = hit.collider != null;
        }
        
        // Visual debug
        if (showGroundCheck)
        {
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
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
        // Jump if conditions are met
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
        }
        
        // Variable jump height
        if (jumpInput && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        
        // Jump cut for shorter jumps
        if (jumpInputStop && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
            isJumping = false;
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpTimeCounter = jumpTime;
        isJumping = true;
        coyoteTimeCounter = 0f;
        
        if (showDebugInfo)
            Debug.Log("AdvancedPlayerController: Jump executed with coyote time!");
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display player info
        GUI.Label(new Rect(10, 10, 300, 20), "Advanced Player Controller:");
        GUI.Label(new Rect(10, 30, 300, 20), $"Speed: {rb.velocity.magnitude:F1}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Grounded: {isGrounded}");
        GUI.Label(new Rect(10, 70, 300, 20), $"Coyote Time: {coyoteTimeCounter:F2}");
        GUI.Label(new Rect(10, 90, 300, 20), $"Jump Buffer: {jumpBufferCounter:F2}");
        GUI.Label(new Rect(10, 110, 300, 20), $"Is Jumping: {isJumping}");
        GUI.Label(new Rect(10, 130, 300, 20), "Controls: WASD/Arrows - Move, Space - Jump");
    }
    
    void OnDrawGizmos()
    {
        if (!showGroundCheck) return;
        
        // Draw ground check ray
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
        
        // Draw movement direction
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.right * horizontalInput * 2f);
    }
}
