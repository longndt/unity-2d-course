using UnityEngine;

/// <summary>
/// Advanced character controller with animation integration.
/// Demonstrates sprite management, animation states, and character movement.
/// </summary>
public class CharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Character movement speed")]
    public float moveSpeed = 5f;
    
    [Tooltip("Jump force applied to character")]
    public float jumpForce = 10f;
    
    [Tooltip("Maximum horizontal speed")]
    public float maxSpeed = 8f;
    
    [Tooltip("Ground check distance")]
    public float groundCheckDistance = 0.1f;
    
    [Tooltip("Layers considered as ground")]
    public LayerMask groundLayer = 1;
    
    [Header("Animation Settings")]
    [Tooltip("Animator component reference")]
    public Animator animator;
    
    [Tooltip("Sprite renderer component")]
    public SpriteRenderer spriteRenderer;
    
    [Tooltip("Animation parameter names")]
    public string speedParameter = "Speed";
    public string jumpParameter = "Jump";
    public string attackParameter = "Attack";
    public string groundedParameter = "IsGrounded";
    
    [Header("Audio Settings")]
    [Tooltip("Audio source for character sounds")]
    public AudioSource audioSource;
    
    [Tooltip("Jump sound effect")]
    public AudioClip jumpSound;
    
    [Tooltip("Footstep sound effect")]
    public AudioClip footstepSound;
    
    [Tooltip("Attack sound effect")]
    public AudioClip attackSound;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private bool isFacingRight = true;
    private float lastFootstepTime;
    private float footstepInterval = 0.3f;
    
    void Start()
    {
        // Get required components
        rb = GetComponent<Rigidbody2D>();
        
        if (animator == null)
            animator = GetComponent<Animator>();
        
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        if (showDebugInfo)
            Debug.Log("CharacterController: Character controller initialized");
    }
    
    void Update()
    {
        HandleInput();
        CheckGrounded();
        UpdateAnimations();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }
    
    void HandleInput()
    {
        // Get horizontal input
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        // Attack input
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        
        // Handle direction change
        if (horizontalInput > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && isFacingRight)
        {
            Flip();
        }
    }
    
    void HandleMovement()
    {
        if (horizontalInput != 0)
        {
            // Apply horizontal movement
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            
            // Play footstep sound
            PlayFootstepSound();
        }
        else
        {
            // Apply friction when not moving
            rb.velocity = new Vector2(rb.velocity.x * 0.9f, rb.velocity.y);
        }
        
        // Limit horizontal speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }
    
    void Jump()
    {
        if (!isGrounded) return;
        
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        
        // Trigger jump animation
        if (animator != null)
        {
            animator.SetTrigger(jumpParameter);
        }
        
        // Play jump sound
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        
        if (showDebugInfo)
            Debug.Log("CharacterController: Jump executed");
    }
    
    void Attack()
    {
        // Trigger attack animation
        if (animator != null)
        {
            animator.SetTrigger(attackParameter);
        }
        
        // Play attack sound
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
        
        if (showDebugInfo)
            Debug.Log("CharacterController: Attack executed");
    }
    
    void CheckGrounded()
    {
        // Check for ground using raycast
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        // Visual debug
        if (showDebugInfo)
        {
            Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
        }
    }
    
    void UpdateAnimations()
    {
        if (animator == null) return;
        
        // Update animation parameters
        animator.SetFloat(speedParameter, Mathf.Abs(horizontalInput));
        animator.SetBool(groundedParameter, isGrounded);
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }
    
    void Flip()
    {
        isFacingRight = !isFacingRight;
        
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !isFacingRight;
        }
        
        if (showDebugInfo)
            Debug.Log($"CharacterController: Flipped to face {(isFacingRight ? "right" : "left")}");
    }
    
    void PlayFootstepSound()
    {
        if (audioSource == null || footstepSound == null) return;
        if (!isGrounded) return;
        
        // Play footstep sound at intervals
        if (Time.time - lastFootstepTime >= footstepInterval)
        {
            audioSource.PlayOneShot(footstepSound);
            lastFootstepTime = Time.time;
        }
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display character info
        GUI.Label(new Rect(10, 10, 300, 20), "Character Controller Info:");
        GUI.Label(new Rect(10, 30, 300, 20), $"Speed: {rb.velocity.magnitude:F1}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Grounded: {isGrounded}");
        GUI.Label(new Rect(10, 70, 300, 20), $"Facing: {(isFacingRight ? "Right" : "Left")}");
        GUI.Label(new Rect(10, 90, 300, 20), "Controls: WASD/Arrows - Move, Space - Jump, Mouse - Attack");
    }
    
    void OnDrawGizmos()
    {
        if (!showDebugInfo) return;
        
        // Draw ground check ray
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * groundCheckDistance);
        
        // Draw movement direction
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, Vector2.right * horizontalInput * 2f);
    }
}
