using UnityEngine;

/// <summary>
/// Comparison between old and new input systems
/// Demonstrates legacy vs modern input handling approaches
/// </summary>
public class InputSystemComparison : MonoBehaviour
{
    [Header("Legacy Input Example")]
    public float jumpForce = 10f;
    public float moveSpeed = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // OLD WAY - Legacy Input Manager (not recommended)
        LegacyInputExample();
    }

    void LegacyInputExample()
    {
        // Old way - not recommended
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        float horizontal = Input.GetAxis("Horizontal");
        Move(horizontal);
    }

    void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        Debug.Log("Jump performed (Legacy Input)");
    }

    void Move(float horizontal)
    {
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
    }

    // Note: For New Input System example, see PlayerInputController.cs
}