using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Auto-generated input event methods using Send Messages behavior
/// Demonstrates Unity's automatic method calling for Input Actions
/// </summary>
public class AutoInputEvents : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Vector2 currentMoveInput;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        // Apply movement in FixedUpdate for consistent physics
        rb.velocity = new Vector2(currentMoveInput.x * moveSpeed, rb.velocity.y);
    }

    // Unity automatically calls these methods based on action names
    public void OnMove(InputValue value)
    {
        Vector2 moveInput = value.Get<Vector2>();
        currentMoveInput = moveInput;
        Debug.Log($"Move input: {moveInput}");
    }

    public void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Jump();
        }
    }

    public void OnAttack()
    {
        // Called when attack action is performed
        Attack();
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump performed via OnJump()");
    }

    void Attack()
    {
        Debug.Log("Attack performed via OnAttack()");
        // Add attack logic here
    }
}