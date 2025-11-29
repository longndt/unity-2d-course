using UnityEngine;

/// <summary>
/// Provides debugging utilities and visual debugging tools.
/// Helps with development and testing by providing useful debug information.
/// </summary>
public class DebugHelper : MonoBehaviour
{
    [Header("Debug Settings")]
    [Tooltip("Enable debug information display")]
    public bool showDebugInfo = true;

    [Tooltip("Enable visual debugging with Gizmos")]
    public bool showGizmos = true;

    [Tooltip("Enable console logging")]
    public bool enableLogging = true;

    [Header("Performance Monitoring")]
    [Tooltip("Show FPS counter")]
    public bool showFPS = true;

    [Tooltip("Show memory usage")]
    public bool showMemory = true;

    [Tooltip("Update interval for performance stats")]
    public float updateInterval = 0.5f;

    [Header("Visual Debug")]
    [Tooltip("Show object bounds")]
    public bool showBounds = true;

    [Tooltip("Show object names")]
    public bool showNames = true;

    [Tooltip("Show object positions")]
    public bool showPositions = true;

    private float fps;
    private float lastUpdateTime;
    private int frameCount;
    private float memoryUsage;

    void Start()
    {
        if (showDebugInfo)
            Debug.Log("DebugHelper: Debug system initialized");

        lastUpdateTime = Time.time;
    }

    void Update()
    {
        if (showFPS || showMemory)
        {
            UpdatePerformanceStats();
        }

        HandleInput();
    }

    void UpdatePerformanceStats()
    {
        frameCount++;

        if (Time.time - lastUpdateTime >= updateInterval)
        {
            fps = frameCount / (Time.time - lastUpdateTime);
            frameCount = 0;
            lastUpdateTime = Time.time;

            if (showMemory)
            {
                memoryUsage = System.GC.GetTotalMemory(false) / (1024f * 1024f); // MB
            }
        }
    }

    void HandleInput()
    {
        // Toggle debug info
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ToggleDebugInfo();
        }

        // Toggle gizmos
        if (Input.GetKeyDown(KeyCode.F2))
        {
            ToggleGizmos();
        }

        // Log current scene info
        if (Input.GetKeyDown(KeyCode.F3))
        {
            LogSceneInfo();
        }

        // Log performance info
        if (Input.GetKeyDown(KeyCode.F4))
        {
            LogPerformanceInfo();
        }
    }

    void ToggleDebugInfo()
    {
        showDebugInfo = !showDebugInfo;
        Debug.Log($"DebugHelper: Debug info {(showDebugInfo ? "enabled" : "disabled")}");
    }

    void ToggleGizmos()
    {
        showGizmos = !showGizmos;
        Debug.Log($"DebugHelper: Gizmos {(showGizmos ? "enabled" : "disabled")}");
    }

    void LogSceneInfo()
    {
        if (!enableLogging) return;

        // Note: FindObjectsOfType is expensive - only use in debug methods
        Debug.Log("=== SCENE INFO ===");
        Debug.Log($"Scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        Debug.Log($"GameObjects: {allObjects.Length}");
        Debug.Log($"Components: {FindObjectsOfType<Component>().Length}");
        Debug.Log($"Active Objects: {allObjects.Length}");
    }

    void LogPerformanceInfo()
    {
        if (!enableLogging) return;

        Debug.Log("=== PERFORMANCE INFO ===");
        Debug.Log($"FPS: {fps:F1}");
        Debug.Log($"Memory: {memoryUsage:F1} MB");
        Debug.Log($"Time Scale: {Time.timeScale}");
        Debug.Log($"Delta Time: {Time.deltaTime:F4}");
    }

    void OnGUI()
    {
        if (!showDebugInfo) return;

        int yOffset = 10;

        // Debug controls
        GUI.Label(new Rect(10, yOffset, 300, 20), "Debug Helper Controls:");
        yOffset += 20;
        GUI.Label(new Rect(10, yOffset, 300, 20), "F1 - Toggle Debug Info");
        yOffset += 20;
        GUI.Label(new Rect(10, yOffset, 300, 20), "F2 - Toggle Gizmos");
        yOffset += 20;
        GUI.Label(new Rect(10, yOffset, 300, 20), "F3 - Log Scene Info");
        yOffset += 20;
        GUI.Label(new Rect(10, yOffset, 300, 20), "F4 - Log Performance");
        yOffset += 40;

        // Performance stats
        if (showFPS)
        {
            GUI.Label(new Rect(10, yOffset, 300, 20), $"FPS: {fps:F1}");
            yOffset += 20;
        }

        if (showMemory)
        {
            GUI.Label(new Rect(10, yOffset, 300, 20), $"Memory: {memoryUsage:F1} MB");
            yOffset += 20;
        }

        // Scene info
        GUI.Label(new Rect(10, yOffset, 300, 20), $"Scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");
        yOffset += 20;
        GUI.Label(new Rect(10, yOffset, 300, 20), $"Objects: {FindObjectsOfType<GameObject>().Length}");
        yOffset += 20;
        GUI.Label(new Rect(10, yOffset, 300, 20), $"Time: {Time.time:F1}s");
    }

    void OnDrawGizmos()
    {
        if (!showGizmos) return;

        // Note: FindObjectsOfType is expensive - avoid using in production code
        // This is acceptable here because it's a debug tool only
        // Draw debug information for all objects in scene
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            if (obj == gameObject) continue; // Skip self

            // Draw object bounds
            if (showBounds)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Gizmos.color = Color.cyan;
                    Gizmos.DrawWireCube(renderer.bounds.center, renderer.bounds.size);
                }
            }

            // Draw object position
            if (showPositions)
            {
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(obj.transform.position, 0.2f);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        // Draw additional gizmos when this object is selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 2f);
    }

    // Public methods for external use
    public void LogMessage(string message)
    {
        if (enableLogging)
        {
            Debug.Log($"DebugHelper: {message}");
        }
    }

    public void LogWarning(string message)
    {
        if (enableLogging)
        {
            Debug.LogWarning($"DebugHelper: {message}");
        }
    }

    public void LogError(string message)
    {
        if (enableLogging)
        {
            Debug.LogError($"DebugHelper: {message}");
        }
    }
}
