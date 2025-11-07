using UnityEngine;

/// <summary>
/// Controls the ball's physics behavior, movement, and collision detection
/// in the bouncing ball game. Handles speed progression and game mechanics.
/// </summary>
public class BallController : MonoBehaviour
{
    [Header("Ball Settings")]
    [Tooltip("Initial speed when ball is launched")]
    public float initialSpeed = 5f;

    [Tooltip("How much speed increases per second")]
    public float speedIncrease = 0.1f;

    [Tooltip("Maximum speed the ball can reach")]
    public float maxSpeed = 15f;

    [Tooltip("Minimum speed before ball is relaunched")]
    public float minSpeed = 1f;

    [Header("Audio & Effects")]
    [Tooltip("Sound played when ball bounces")]
    public AudioClip bounceSound;

    [Tooltip("Particle effect played on bounce")]
    public GameObject bounceEffect;

    [Header("Debug")]
    [Tooltip("Show debug information in console")]
    public bool showDebugInfo = false;

    // Private variables
    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private float gameStartTime;
    private int bounceCount;

    // Events
    public System.Action<int> OnBounce; // Called when ball bounces
    public System.Action OnBallLost;   // Called when ball is lost

    void Start()
    {
        // Get required components
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("BallController requires Rigidbody2D component!");
            enabled = false;
            return;
        }

        // Record game start time
        gameStartTime = Time.time;

        // Launch ball initially
        LaunchBall();

        if (showDebugInfo)
        {
            Debug.Log("BallController: Ball initialized and launched");
        }
    }

    void Update()
    {
        if (rb == null) return;

        // Store current velocity for collision calculations
        lastVelocity = rb.velocity;

        // Increase speed over time (progressive difficulty)
        if (rb.velocity.magnitude < maxSpeed)
        {
            float currentSpeed = rb.velocity.magnitude;
            float newSpeed = currentSpeed + speedIncrease * Time.deltaTime;
            rb.velocity = rb.velocity.normalized * newSpeed;
        }

        // Check if ball is moving too slowly (stuck)
        if (rb.velocity.magnitude < minSpeed)
        {
            if (showDebugInfo)
            {
                Debug.Log("BallController: Ball speed too low, relaunching");
            }
            LaunchBall();
        }

        // Check if ball fell off the bottom
        if (transform.position.y < -10f)
        {
            OnBallLost?.Invoke();
            LaunchBall();
        }
    }

    /// <summary>
    /// Launches the ball in a random direction with initial speed
    /// </summary>
    public void LaunchBall()
    {
        if (rb == null) return;

        // Generate random direction (avoid straight up/down)
        Vector2 randomDirection;
        do
        {
            randomDirection = Random.insideUnitCircle.normalized;
        } while (Mathf.Abs(randomDirection.y) > 0.8f); // Avoid too vertical angles

        // Apply initial velocity
        rb.velocity = randomDirection * initialSpeed;

        if (showDebugInfo)
        {
            Debug.Log($"BallController: Ball launched with velocity {rb.velocity}");
        }
    }

    /// <summary>
    /// Handles collision detection and bounce effects
    /// </summary>
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb == null) return;
        if (collision.contacts.Length == 0) return;

        // Increment bounce count
        bounceCount++;

        // Trigger bounce event
        OnBounce?.Invoke(bounceCount);

        // Play bounce sound effect
        if (bounceSound != null)
        {
            AudioSource.PlayClipAtPoint(bounceSound, transform.position);
        }

        // Create particle effect
        if (bounceEffect != null)
        {
            Instantiate(bounceEffect, transform.position, Quaternion.identity);
        }

        // Calculate reflection vector for more realistic bouncing
        Vector2 reflection = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = reflection * rb.velocity.magnitude;

        if (showDebugInfo)
        {
            Debug.Log($"BallController: Ball bounced off {collision.gameObject.name}. Bounce count: {bounceCount}");
        }
    }

    /// <summary>
    /// Handles trigger collisions (for special areas)
    /// </summary>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Paddle"))
        {
            // Special handling for paddle collision
            if (showDebugInfo)
            {
                Debug.Log("BallController: Ball hit paddle trigger");
            }
        }
    }

    /// <summary>
    /// Resets the ball to initial state
    /// </summary>
    public void ResetBall()
    {
        if (rb == null) return;

        // Stop current movement
        rb.velocity = Vector2.zero;

        // Reset position to center
        transform.position = Vector3.zero;

        // Reset bounce count
        bounceCount = 0;

        // Launch ball
        LaunchBall();

        if (showDebugInfo)
        {
            Debug.Log("BallController: Ball reset to initial state");
        }
    }

    /// <summary>
    /// Gets current ball speed
    /// </summary>
    public float GetCurrentSpeed()
    {
        if (rb == null) return 0f;
        return rb.velocity.magnitude;
    }

    /// <summary>
    /// Gets total bounce count
    /// </summary>
    public int GetBounceCount()
    {
        return bounceCount;
    }

    /// <summary>
    /// Sets ball speed (useful for power-ups)
    /// </summary>
    public void SetSpeed(float newSpeed)
    {
        if (rb == null) return;

        if (rb.velocity.magnitude > 0)
        {
            rb.velocity = rb.velocity.normalized * newSpeed;
        }
        else
        {
            rb.velocity = Vector2.right * newSpeed;
        }
    }

    /// <summary>
    /// Adds force to the ball (useful for power-ups)
    /// </summary>
    public void AddForce(Vector2 force)
    {
        if (rb == null) return;
        rb.AddForce(force, ForceMode2D.Impulse);
    }

    /// <summary>
    /// Visual debugging in Scene view
    /// </summary>
    void OnDrawGizmos()
    {
        if (showDebugInfo && rb != null)
        {
            // Draw velocity vector
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, rb.velocity.normalized * 2f);

            // Draw speed indicator
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rb.velocity.magnitude * 0.1f);
        }
    }
}
