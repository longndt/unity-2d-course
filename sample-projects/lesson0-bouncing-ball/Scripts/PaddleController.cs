using UnityEngine;

/// <summary>
/// Controls the paddle movement and input handling in the bouncing ball game.
/// Handles keyboard input, boundary checking, and collision with the ball.
/// </summary>
public class PaddleController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Speed of paddle movement")]
    public float moveSpeed = 10f;
    
    [Tooltip("Left and right boundaries for paddle movement")]
    public float boundary = 8f;
    
    [Tooltip("Smoothing factor for movement (0 = instant, 1 = very smooth)")]
    [Range(0f, 1f)]
    public float smoothing = 0.1f;
    
    [Header("Input Settings")]
    [Tooltip("Input axis name for horizontal movement")]
    public string horizontalAxis = "Horizontal";
    
    [Tooltip("Alternative input keys (A/D)")]
    public KeyCode leftKey = KeyCode.A;
    public KeyCode rightKey = KeyCode.D;
    
    [Header("Visual Settings")]
    [Tooltip("Paddle trail effect")]
    public TrailRenderer trailRenderer;
    
    [Tooltip("Paddle glow effect")]
    public GameObject glowEffect;
    
    [Header("Audio")]
    [Tooltip("Sound played when paddle hits ball")]
    public AudioClip hitSound;
    
    [Tooltip("Audio source for paddle sounds")]
    public AudioSource audioSource;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = false;
    
    // Private variables
    private float horizontalInput;
    private Vector2 targetPosition;
    private Vector2 currentVelocity;
    private float lastInputTime;
    private bool isMoving;
    
    // Events
    public System.Action OnPaddleMove;     // Called when paddle starts moving
    public System.Action OnPaddleStop;     // Called when paddle stops moving
    public System.Action OnBallHit;        // Called when paddle hits ball
    
    void Start()
    {
        // Initialize target position
        targetPosition = transform.position;
        
        // Get audio source if not assigned
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
        
        // Setup trail renderer
        if (trailRenderer != null)
        {
            trailRenderer.enabled = false;
        }
        
        if (showDebugInfo)
        {
            Debug.Log("PaddleController: Paddle initialized");
        }
    }
    
    void Update()
    {
        HandleInput();
        MovePaddle();
        UpdateEffects();
    }
    
    /// <summary>
    /// Handles input from keyboard and gamepad
    /// </summary>
    void HandleInput()
    {
        // Get input from axis (supports both keyboard and gamepad)
        float axisInput = Input.GetAxis(horizontalAxis);
        
        // Get input from direct key presses (for more responsive control)
        float keyInput = 0f;
        if (Input.GetKey(leftKey))
            keyInput -= 1f;
        if (Input.GetKey(rightKey))
            keyInput += 1f;
        
        // Use the input with greater magnitude (more responsive)
        horizontalInput = Mathf.Abs(axisInput) > Mathf.Abs(keyInput) ? axisInput : keyInput;
        
        // Record input time for effects
        if (horizontalInput != 0)
        {
            lastInputTime = Time.time;
        }
    }
    
    /// <summary>
    /// Moves the paddle based on input
    /// </summary>
    void MovePaddle()
    {
        // Calculate target position
        float newX = transform.position.x + horizontalInput * moveSpeed * Time.deltaTime;
        
        // Apply boundary constraints
        newX = Mathf.Clamp(newX, -boundary, boundary);
        
        // Set target position
        targetPosition = new Vector2(newX, transform.position.y);
        
        // Smooth movement towards target position
        if (smoothing > 0)
        {
            transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothing);
        }
        else
        {
            transform.position = targetPosition;
        }
        
        // Check if paddle is moving
        bool wasMoving = isMoving;
        isMoving = Mathf.Abs(horizontalInput) > 0.1f;
        
        // Trigger events
        if (isMoving && !wasMoving)
        {
            OnPaddleMove?.Invoke();
        }
        else if (!isMoving && wasMoving)
        {
            OnPaddleStop?.Invoke();
        }
    }
    
    /// <summary>
    /// Updates visual effects based on movement
    /// </summary>
    void UpdateEffects()
    {
        // Update trail renderer
        if (trailRenderer != null)
        {
            trailRenderer.enabled = isMoving;
        }
        
        // Update glow effect
        if (glowEffect != null)
        {
            glowEffect.SetActive(isMoving);
        }
    }
    
    /// <summary>
    /// Handles collision with the ball
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            // Play hit sound
            if (hitSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(hitSound);
            }
            
            // Trigger hit event
            OnBallHit?.Invoke();
            
            // Add slight upward force to ball for better gameplay
            Rigidbody2D ballRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (ballRb != null)
            {
                // Calculate hit angle based on where ball hit paddle
                float hitPoint = collision.contacts[0].point.x - transform.position.x;
                float normalizedHitPoint = hitPoint / (GetComponent<Collider2D>().bounds.size.x / 2f);
                
                // Add upward force and slight horizontal influence
                Vector2 hitForce = new Vector2(normalizedHitPoint * 2f, 1f) * 2f;
                ballRb.AddForce(hitForce, ForceMode2D.Impulse);
            }
            
            if (showDebugInfo)
            {
                Debug.Log("PaddleController: Ball hit paddle");
            }
        }
    }
    
    /// <summary>
    /// Moves paddle to specific position instantly
    /// </summary>
    public void SetPosition(float x)
    {
        x = Mathf.Clamp(x, -boundary, boundary);
        transform.position = new Vector2(x, transform.position.y);
        targetPosition = transform.position;
    }
    
    /// <summary>
    /// Gets current paddle position
    /// </summary>
    public Vector2 GetPosition()
    {
        return transform.position;
    }
    
    /// <summary>
    /// Gets paddle boundaries
    /// </summary>
    public float GetBoundary()
    {
        return boundary;
    }
    
    /// <summary>
    /// Sets paddle boundaries
    /// </summary>
    public void SetBoundary(float newBoundary)
    {
        boundary = newBoundary;
    }
    
    /// <summary>
    /// Sets paddle movement speed
    /// </summary>
    public void SetMoveSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
    
    /// <summary>
    /// Checks if paddle is currently moving
    /// </summary>
    public bool IsMoving()
    {
        return isMoving;
    }
    
    /// <summary>
    /// Gets time since last input
    /// </summary>
    public float GetTimeSinceLastInput()
    {
        return Time.time - lastInputTime;
    }
    
    /// <summary>
    /// Visual debugging in Scene view
    /// </summary>
    void OnDrawGizmos()
    {
        if (showDebugInfo)
        {
            // Draw boundaries
            Gizmos.color = Color.red;
            Gizmos.DrawLine(new Vector3(-boundary, transform.position.y - 1f, 0), 
                           new Vector3(-boundary, transform.position.y + 1f, 0));
            Gizmos.DrawLine(new Vector3(boundary, transform.position.y - 1f, 0), 
                           new Vector3(boundary, transform.position.y + 1f, 0));
            
            // Draw movement direction
            if (isMoving)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawRay(transform.position, Vector2.right * horizontalInput * 2f);
            }
        }
    }
}
