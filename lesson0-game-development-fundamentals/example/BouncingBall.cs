using UnityEngine;

/// <summary>
/// Simple bouncing ball demonstrating basic physics and collision
/// Perfect first example for understanding game development
/// </summary>
public class BouncingBall : MonoBehaviour
{
    [Header("Bounce Settings")]
    [SerializeField] private float bounceForce = 10f;
    [SerializeField] private float bounceMultiplier = 0.8f;

    private Rigidbody2D rb;

    void Start()
    {
        // Get Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Ensure Rigidbody2D exists
        if (rb == null)
        {
            Debug.LogError("BouncingBall requires Rigidbody2D component!");
            enabled = false;
            return;
        }

        // Initial bounce to get started
        rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);

        Debug.Log("Bouncing Ball started! Watch it bounce!");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb == null) return;

        // When ball hits something, add upward force
        rb.AddForce(Vector2.up * bounceForce * bounceMultiplier, ForceMode2D.Impulse);

        Debug.Log($"Ball bounced off: {collision.gameObject.name}");
    }

    void OnDrawGizmosSelected()
    {
        // Visualize bounce force direction
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, Vector2.up * bounceForce * 0.1f);
    }
}
