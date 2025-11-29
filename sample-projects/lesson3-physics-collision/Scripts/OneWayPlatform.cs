using UnityEngine;

/// <summary>
/// One-way platform that allows players to drop through with specific input.
/// Demonstrates one-way collision detection and input-based platform behavior.
/// </summary>
public class OneWayPlatform : MonoBehaviour
{
    [Header("One-Way Platform Settings")]
    [Tooltip("Time to disable collider when player drops through")]
    public float disableTime = 0.5f;
    
    [Tooltip("Layers that can interact with this platform")]
    public LayerMask playerLayer = 1;
    
    [Tooltip("Key to drop through platform")]
    public KeyCode dropKey = KeyCode.S;
    
    [Tooltip("Allow drop through from above")]
    public bool allowDropFromAbove = true;
    
    [Tooltip("Allow drop through from below")]
    public bool allowDropFromBelow = false;
    
    [Header("Visual Settings")]
    [Tooltip("Show visual indicator for drop-through")]
    public bool showDropIndicator = true;
    
    [Tooltip("Color of drop indicator")]
    public Color indicatorColor = Color.cyan;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private Collider2D platformCollider;
    private float disableTimer = 0f;
    private bool isDisabled = false;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    
    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
        
        if (showDebugInfo)
            Debug.Log("OneWayPlatform: One-way platform initialized");
    }
    
    void Update()
    {
        HandleDisableTimer();
        HandleInput();
    }
    
    void HandleDisableTimer()
    {
        // Re-enable collider after disable time
        if (isDisabled && disableTimer > 0f)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0f)
            {
                EnableCollider();
            }
        }
    }
    
    void HandleInput()
    {
        // Check for drop input
        if (Input.GetKeyDown(dropKey))
        {
            if (showDebugInfo)
                Debug.Log("OneWayPlatform: Drop input detected");
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is above platform
        if (IsInLayerMask(collision.gameObject.layer, playerLayer))
        {
            float playerBottom = collision.gameObject.transform.position.y - 0.5f;
            float platformTop = transform.position.y + 0.5f;
            
            if (playerBottom > platformTop)
            {
                // Player is above platform, allow collision
                if (showDebugInfo)
                    Debug.Log("OneWayPlatform: Player above platform - collision allowed");
                return;
            }
            else
            {
                // Player is below platform, disable collision temporarily
                if (allowDropFromBelow)
                {
                    DisableCollider();
                }
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Handle drop-through input
        if (IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            if (Input.GetKey(dropKey))
            {
                DisableCollider();
                
                if (showDebugInfo)
                    Debug.Log("OneWayPlatform: Player dropped through platform");
            }
        }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        // Handle drop-through input while on platform
        if (IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            if (Input.GetKeyDown(dropKey))
            {
                DisableCollider();
                
                if (showDebugInfo)
                    Debug.Log("OneWayPlatform: Player dropped through platform");
            }
        }
    }
    
    void DisableCollider()
    {
        if (platformCollider != null)
        {
            platformCollider.enabled = false;
            isDisabled = true;
            disableTimer = disableTime;
            
            // Visual feedback
            if (spriteRenderer != null && showDropIndicator)
            {
                spriteRenderer.color = indicatorColor;
            }
            
            if (showDebugInfo)
                Debug.Log("OneWayPlatform: Collider disabled");
        }
    }
    
    void EnableCollider()
    {
        if (platformCollider != null)
        {
            platformCollider.enabled = true;
            isDisabled = false;
            
            // Restore visual
            if (spriteRenderer != null && showDropIndicator)
            {
                spriteRenderer.color = originalColor;
            }
            
            if (showDebugInfo)
                Debug.Log("OneWayPlatform: Collider enabled");
        }
    }
    
    bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
    
    // Public methods for external control
    public void ForceDisable()
    {
        DisableCollider();
    }
    
    public void ForceEnable()
    {
        EnableCollider();
    }
    
    public void SetDisableTime(float time)
    {
        disableTime = time;
        if (showDebugInfo)
            Debug.Log($"OneWayPlatform: Disable time set to {time}");
    }
    
    public void SetDropKey(KeyCode key)
    {
        dropKey = key;
        if (showDebugInfo)
            Debug.Log($"OneWayPlatform: Drop key set to {key}");
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display platform info
        GUI.Label(new Rect(10, 10, 300, 20), "One-Way Platform Info:");
        GUI.Label(new Rect(10, 30, 300, 20), $"Drop Key: {dropKey}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Is Disabled: {isDisabled}");
        GUI.Label(new Rect(10, 70, 300, 20), $"Disable Timer: {disableTimer:F2}");
        GUI.Label(new Rect(10, 90, 300, 20), $"Allow Drop From Above: {allowDropFromAbove}");
        GUI.Label(new Rect(10, 110, 300, 20), $"Allow Drop From Below: {allowDropFromBelow}");
    }
    
    void OnDrawGizmos()
    {
        if (!showDebugInfo) return;
        
        // Draw platform bounds
        Gizmos.color = isDisabled ? Color.red : Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one);
        
        // Draw drop indicator
        if (showDropIndicator)
        {
            Gizmos.color = indicatorColor;
            Gizmos.DrawWireSphere(transform.position, 0.3f);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (!showDebugInfo) return;
        
        // Draw additional gizmos when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 1.2f);
    }
}
