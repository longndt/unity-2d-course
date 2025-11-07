using UnityEngine;

/// <summary>
/// Collider configuration examples
/// Demonstrates different 2D collider types and their properties
/// </summary>
public class ColliderSetup : MonoBehaviour
{
    void Start()
    {
        SetupColliders();
    }

    void SetupColliders()
    {
        // Box Collider 2D setup
        BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
        if (boxCol != null)
        {
            boxCol.size = new Vector2(2f, 1f);      // 2×1 collision box
            boxCol.offset = new Vector2(0f, 0.5f);   // Offset up by 0.5 units
        }

        // Circle Collider 2D setup
        CircleCollider2D circleCol = GetComponent<CircleCollider2D>();
        if (circleCol != null)
        {
            circleCol.radius = 1.5f;                 // 1.5 unit radius
            circleCol.offset = new Vector2(0f, 1f);  // Offset center up
        }

        // Edge Collider 2D setup
        EdgeCollider2D edgeCol = GetComponent<EdgeCollider2D>();
        if (edgeCol != null)
        {
            Vector2[] points = { new Vector2(-2, 0), new Vector2(0, 1), new Vector2(2, 0) };
            edgeCol.points = points; // Define edge shape
        }
    }
}