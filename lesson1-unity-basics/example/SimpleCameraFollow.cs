using UnityEngine;

/// <summary>
/// Simple camera follow system for 2D games
/// Demonstrates basic camera following with smooth interpolation
/// Advanced camera systems with Cinemachine will be covered in Lesson 4
/// </summary>
public class SimpleCameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;           // Player to follow (assign in Inspector)
    public float followSpeed = 2f;    // How fast camera follows (higher = faster)
    public Vector3 offset = Vector3.zero;  // Offset from target position
    
    [Header("Camera Bounds (Optional)")]
    public bool useBounds = false;
    public Vector2 minBounds = new Vector2(-10, -5);
    public Vector2 maxBounds = new Vector2(10, 5);
    
    [Header("Camera Settings")]
    public float defaultOrthographicSize = 8f;
    
    private Vector3 initialPosition;
    
    void Start()
    {
        // Store initial camera position
        initialPosition = transform.position;
        
        // Set camera orthographic size
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.orthographicSize = defaultOrthographicSize;
        }
        else
        {
            Debug.LogWarning("SimpleCameraFollow: Main camera not found!");
        }
        
        // Warn if target not assigned
        if (target == null)
        {
            Debug.LogWarning("SimpleCameraFollow: Target not assigned! Please assign a player transform in Inspector.");
        }
    }
    
    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate target position
            Vector3 targetPosition = target.position + offset;
            targetPosition.z = transform.position.z; // Keep camera's Z position
            
            // Apply bounds if enabled
            if (useBounds)
            {
                targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
                targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);
            }
            
            // Smoothly move camera towards target using Lerp
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );
        }
    }
    
    /// <summary>
    /// Reset camera to initial position
    /// </summary>
    public void ResetCamera()
    {
        transform.position = initialPosition;
    }
    
    /// <summary>
    /// Set new target to follow
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
    
    void OnDrawGizmos()
    {
        // Draw bounds in Scene view
        if (useBounds)
        {
            Gizmos.color = Color.yellow;
            Vector3 center = new Vector3(
                (minBounds.x + maxBounds.x) / 2f,
                (minBounds.y + maxBounds.y) / 2f,
                transform.position.z
            );
            Vector3 size = new Vector3(
                maxBounds.x - minBounds.x,
                maxBounds.y - minBounds.y,
                0
            );
            Gizmos.DrawWireCube(center, size);
        }
    }
}

