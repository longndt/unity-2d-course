using UnityEngine;

/// <summary>
/// Physics Material 2D assignment examples
/// Demonstrates how to create and assign physics materials
/// </summary>
public class PhysicsMaterialSetup : MonoBehaviour
{
    public PhysicsMaterial2D iceMaterial;
    public PhysicsMaterial2D bouncyMaterial;

    void Start()
    {
        AssignMaterials();
    }

    void AssignMaterials()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        BoxCollider2D collider = GetComponent<BoxCollider2D>();

        // Assign to Rigidbody2D
        if (rb != null && iceMaterial != null)
        {
            rb.sharedMaterial = iceMaterial;
        }

        // Assign to Collider2D
        if (collider != null && bouncyMaterial != null)
        {
            collider.sharedMaterial = bouncyMaterial;
        }
    }

    // Example material configurations in comments:
    /*
    // Ice Material
    Friction: 0.01
    Bounciness: 0.1

    // Bouncy Material
    Friction: 0.4
    Bounciness: 0.9

    // Sticky Material
    Friction: 1.0
    Bounciness: 0.0

    // Player Character
    Friction: 0.4 (good ground control)
    Bounciness: 0.0 (no unwanted bouncing)
    */
}