using UnityEngine;

/// <summary>
/// Rigidbody2D configuration and manipulation examples
/// Demonstrates basic physics properties and constraints
/// </summary>
public class RigidbodyControl : MonoBehaviour
{
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("RigidbodyControl requires Rigidbody2D component!");
            enabled = false;
            return;
        }

        // Mass configuration
        rb.mass = 2.0f; // Set custom mass

        // Gravity modifications
        rb.gravityScale = 1.5f; // 50% stronger gravity
        rb.drag = 0.5f;         // Light air resistance
        rb.angularDrag = 0.05f; // Rotational damping

        // Lock Z rotation for 2D character
        rb.freezeRotation = true;
        // Or specifically:
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void Update()
    {
        if (rb == null) return;

        // Example of dynamic physics modifications
        if (Input.GetKeyDown(KeyCode.G))
        {
            rb.gravityScale = rb.gravityScale == 1f ? 0f : 1f; // Toggle gravity
        }
    }
}