using UnityEngine;

/// <summary>
/// Simple 2D Player Controller with left/right movement and jump physics
/// </summary>
public class Simple2DPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayerMask;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;
    private bool facingRight = true;

    // Animation parameter names
    private const string SPEED_PARAM = "Speed";
    private const string IS_GROUNDED_PARAM = "IsGrounded";
    private const string JUMP_TRIGGER = "Jump";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Simple2DPlayerController requires Rigidbody2D component!");
            enabled = false;
            return;
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("Simple2DPlayerController: Animator component not found. Animations will not work.");
        }

        // Create ground check if not assigned
        if (groundCheck == null)
        {
            GameObject groundCheckObj = new GameObject("GroundCheck");
            groundCheckObj.transform.SetParent(transform);
            groundCheckObj.transform.localPosition = new Vector3(0, -0.5f, 0);
            groundCheck = groundCheckObj.transform;
            Debug.LogWarning("Simple2DPlayerController: GroundCheck not assigned, created default.");
        }
    }

    void Update()
    {
        if (rb == null) return;

        HandleMovement();
        HandleJump();
        UpdateAnimations();
    }

    void HandleMovement()
    {
        if (rb == null) return;

        float horizontalInput = Input.GetAxis("Horizontal");

        // Move player
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Flip sprite based on movement direction
        if (horizontalInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (horizontalInput < 0 && facingRight)
        {
            Flip();
        }
    }

    void HandleJump()
    {
        if (rb == null || groundCheck == null) return;

        // Check if grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            if (animator != null)
            {
                animator.SetTrigger(JUMP_TRIGGER);
            }
        }
    }

    void UpdateAnimations()
    {
        if (animator == null || rb == null) return;

        animator.SetFloat(SPEED_PARAM, Mathf.Abs(rb.velocity.x));
        animator.SetBool(IS_GROUNDED_PARAM, isGrounded);
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
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
