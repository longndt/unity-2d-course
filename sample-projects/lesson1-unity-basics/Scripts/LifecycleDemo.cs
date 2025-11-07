using UnityEngine;

/// <summary>
/// Demonstrates MonoBehaviour lifecycle methods and their execution order.
/// Perfect for understanding when each method is called during object lifetime.
/// </summary>
public class LifecycleDemo : MonoBehaviour
{
    [Header("Lifecycle Settings")]
    [Tooltip("Name identifier for this object")]
    public string objectName = "LifecycleDemo";
    
    [Tooltip("Show debug information in console")]
    public bool showDebugInfo = true;
    
    [Tooltip("Enable visual debugging with Gizmos")]
    public bool showGizmos = true;
    
    [Header("Demo Settings")]
    [Tooltip("Speed of rotation for visual feedback")]
    public float rotationSpeed = 30f;
    
    [Tooltip("Scale factor for pulsing effect")]
    public float pulseScale = 1.2f;
    
    [Tooltip("Pulse speed for visual feedback")]
    public float pulseSpeed = 2f;
    
    private Vector3 originalScale;
    private float pulseTimer;
    private bool isInitialized = false;
    
    void Awake()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: Awake() called - Object is being created");
        
        // Store original scale for pulsing effect
        originalScale = transform.localScale;
        
        // This is where you typically initialize components that other scripts might need
        // before Start() is called
    }
    
    void Start()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: Start() called - Object is fully initialized");
        
        isInitialized = true;
        
        // This is where you typically set up initial state
        // All objects in the scene have been created at this point
    }
    
    void Update()
    {
        if (!isInitialized) return;
        
        // Handle input for testing
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (showDebugInfo)
                Debug.Log($"{objectName}: Update() - Space pressed");
        }
        
        // Visual feedback - rotation
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        
        // Visual feedback - pulsing
        HandlePulsing();
    }
    
    void FixedUpdate()
    {
        if (!isInitialized) return;
        
        // FixedUpdate is called at fixed intervals (typically 50Hz)
        // Use this for physics-related updates
        if (showDebugInfo && Time.frameCount % 50 == 0) // Log every 50 frames
        {
            Debug.Log($"{objectName}: FixedUpdate() - Frame {Time.frameCount}");
        }
    }
    
    void LateUpdate()
    {
        if (!isInitialized) return;
        
        // LateUpdate is called after all Update() methods
        // Use this for camera following, UI updates, etc.
        if (showDebugInfo && Time.frameCount % 100 == 0) // Log every 100 frames
        {
            Debug.Log($"{objectName}: LateUpdate() - Frame {Time.frameCount}");
        }
    }
    
    void OnEnable()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: OnEnable() called - Object became active");
    }
    
    void OnDisable()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: OnDisable() called - Object became inactive");
    }
    
    void OnDestroy()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: OnDestroy() called - Object is being destroyed");
    }
    
    void HandlePulsing()
    {
        pulseTimer += Time.deltaTime * pulseSpeed;
        float pulseValue = Mathf.Sin(pulseTimer) * 0.1f + 1f;
        transform.localScale = originalScale * pulseValue * pulseScale;
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display lifecycle information on screen
        GUI.Label(new Rect(10, 10, 300, 20), $"{objectName} - Lifecycle Demo");
        GUI.Label(new Rect(10, 30, 300, 20), $"Frame: {Time.frameCount}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Time: {Time.time:F2}s");
        GUI.Label(new Rect(10, 70, 300, 20), "Press SPACE to test Update()");
    }
    
    void OnDrawGizmos()
    {
        if (!showGizmos) return;
        
        // Draw a circle around the object
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 1f);
        
        // Draw direction indicator
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.right * 2f);
    }
    
    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;
        
        // Draw additional gizmos when object is selected
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 1.5f);
    }
}
