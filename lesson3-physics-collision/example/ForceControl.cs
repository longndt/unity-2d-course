using UnityEngine;

/// <summary>
/// Force application and velocity control
/// Demonstrates different ways to apply forces and control movement
/// </summary>
public class ForceControl : MonoBehaviour
{
    [Header("Force Settings")]
    public float jumpForce = 400f;
    public float pushForce = 10f;
    public float moveSpeed = 5f;
    public float maxSpeed = 10f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleForces();
        HandleVelocity();
    }

    void HandleForces()
    {
        // Force: Continuous force affected by mass
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
        }

        // Impulse: Instant force affected by mass
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
        }
    }

    void HandleVelocity()
    {
        // Direct Velocity Assignment
        float input = Input.GetAxis("Horizontal");

        // Set horizontal movement, preserve vertical
        Vector2 newVelocity = new Vector2(moveSpeed * input, rb.velocity.y);
        rb.velocity = newVelocity;

        // Velocity Clamping - Limit maximum speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
}