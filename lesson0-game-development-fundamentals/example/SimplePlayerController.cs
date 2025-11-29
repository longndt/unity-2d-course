using UnityEngine;

/// <summary>
/// Simple player controller demonstrating basic movement and input
/// Shows the fundamental game loop pattern in action
/// </summary>
public class SimplePlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.2f;
    [SerializeField] private LayerMask groundLayer = 1;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("SimplePlayerController requires Rigidbody2D component!");
            enabled = false;
            return;
        }

        // Create ground check if not assigned
        if (groundCheck == null)
        {
            GameObject groundCheckObj = new GameObject("GroundCheck");
            groundCheckObj.transform.SetParent(transform);
            groundCheckObj.transform.localPosition = new Vector3(0, -0.5f, 0);
            groundCheck = groundCheckObj.transform;
        }

        Debug.Log("Simple Player Controller started!");
        Debug.Log("Use Arrow Keys/WASD to move, Space to jump");
    }

    void Update()
    {
        // Check if grounded (runs every frame - part of game loop!)
        CheckGrounded();

        // Handle input (runs every frame - continuous polling!)
        HandleInput();
    }

    void CheckGrounded()
    {
        // Check if player is touching ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // Visual debugging
        if (isGrounded)
        {
            Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckRadius, Color.green);
        }
        else
        {
            Debug.DrawRay(groundCheck.position, Vector2.down * groundCheckRadius, Color.red);
        }
    }

    void HandleInput()
    {
        // Horizontal movement (continuous input checking)
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        // Jump (one-time input)
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        // Debug info
        if (horizontal != 0)
        {
            Debug.Log($"Moving: {horizontal}, Speed: {rb.velocity.x}");
        }
    }

    void Jump()
    {
        // Apply jump force
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Debug.Log("Jumped!");
    }

    void OnDrawGizmosSelected()
    {
        // Draw ground check circle in Scene view
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
