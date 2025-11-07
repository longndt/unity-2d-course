using UnityEngine;

/// <summary>
/// Debug tools and Gizmos for Unity development
/// Demonstrates debugging techniques and visual debugging
/// </summary>
public class DebugTools : MonoBehaviour
{
    [Header("Debug Settings")]
    public bool showPositionLog = false;

    void Update()
    {
        // Console logging examples (only when enabled to avoid spam)
        if (showPositionLog)
        {
            Debug.Log("Player position: " + transform.position);
        }

        // One-time logs on key press (better practice)
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.LogWarning("Health is low!");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.LogError("Player not found!");
        }
    }

    void OnDrawGizmos()
    {
        // Gizmos for visual debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 1f);
    }
}