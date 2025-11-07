using UnityEngine;
using Cinemachine;

/// <summary>
/// Advanced camera controller with Cinemachine integration.
/// Demonstrates camera following, look-ahead, and screen shake effects.
/// </summary>
public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    [Tooltip("Cinemachine Virtual Camera reference")]
    public CinemachineVirtualCamera virtualCamera;
    
    [Tooltip("Camera follow speed")]
    public float followSpeed = 2f;
    
    [Tooltip("Look ahead distance")]
    public float lookAheadDistance = 3f;
    
    [Tooltip("Look ahead speed")]
    public float lookAheadSpeed = 2f;
    
    [Header("Screen Shake")]
    [Tooltip("Screen shake intensity")]
    public float shakeIntensity = 1f;
    
    [Tooltip("Screen shake duration")]
    public float shakeDuration = 0.5f;
    
    [Tooltip("Enable screen shake")]
    public bool enableScreenShake = true;
    
    [Header("Camera Zones")]
    [Tooltip("Camera zones for different areas")]
    public CameraZone[] cameraZones;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;
    private Vector3 lookAheadTarget;
    private Vector3 currentLookAhead;
    private Transform playerTransform;
    private CameraZone currentZone;
    
    [System.Serializable]
    public class CameraZone
    {
        public string name;
        public Bounds bounds;
        public float followSpeed = 2f;
        public float lookAheadDistance = 3f;
        public Color gizmoColor = Color.green;
    }
    
    void Start()
    {
        // Get virtual camera component
        if (virtualCamera == null)
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
        
        // Get noise component for screen shake
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
        
        // Find player
        playerTransform = FindObjectOfType<PlayerInputHandler>()?.transform;
        
        if (showDebugInfo)
            Debug.Log("CameraController: Camera system initialized");
    }
    
    void Update()
    {
        HandleScreenShake();
        HandleLookAhead();
        HandleCameraZones();
        HandleInput();
    }
    
    void HandleScreenShake()
    {
        if (!enableScreenShake) return;
        
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                StopShake();
            }
        }
    }
    
    void HandleLookAhead()
    {
        if (playerTransform == null) return;
        
        // Calculate look ahead based on player movement
        Vector2 playerVelocity = GetPlayerVelocity();
        lookAheadTarget = new Vector3(playerVelocity.x * lookAheadDistance, 0, 0);
        
        // Smooth look ahead movement
        currentLookAhead = Vector3.Lerp(currentLookAhead, lookAheadTarget, lookAheadSpeed * Time.deltaTime);
        
        // Apply look ahead to camera
        if (virtualCamera != null)
        {
            var transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer != null)
            {
                transposer.m_TrackedObjectOffset = currentLookAhead;
            }
        }
    }
    
    void HandleCameraZones()
    {
        if (playerTransform == null || cameraZones == null) return;
        
        // Check if player is in a camera zone
        for (int i = 0; i < cameraZones.Length; i++)
        {
            if (cameraZones[i].bounds.Contains(playerTransform.position))
            {
                if (currentZone != cameraZones[i])
                {
                    currentZone = cameraZones[i];
                    ApplyCameraZone(currentZone);
                    
                    if (showDebugInfo)
                        Debug.Log($"CameraController: Entered camera zone: {currentZone.name}");
                }
                return;
            }
        }
        
        // Player not in any zone, use default settings
        if (currentZone != null)
        {
            currentZone = null;
            ApplyDefaultSettings();
            
            if (showDebugInfo)
                Debug.Log("CameraController: Exited camera zone");
        }
    }
    
    void ApplyCameraZone(CameraZone zone)
    {
        followSpeed = zone.followSpeed;
        lookAheadDistance = zone.lookAheadDistance;
    }
    
    void ApplyDefaultSettings()
    {
        followSpeed = 2f;
        lookAheadDistance = 3f;
    }
    
    void HandleInput()
    {
        // Test screen shake
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartShake();
        }
        
        // Test camera zones
        if (Input.GetKeyDown(KeyCode.Z))
        {
            LogCameraZones();
        }
    }
    
    Vector2 GetPlayerVelocity()
    {
        if (playerTransform == null) return Vector2.zero;
        
        Rigidbody2D playerRb = playerTransform.GetComponent<Rigidbody2D>();
        return playerRb != null ? playerRb.velocity : Vector2.zero;
    }
    
    public void StartShake()
    {
        if (!enableScreenShake || noise == null) return;
        
        noise.m_AmplitudeGain = shakeIntensity;
        noise.m_FrequencyGain = 1f;
        shakeTimer = shakeDuration;
        
        if (showDebugInfo)
            Debug.Log("CameraController: Screen shake started");
    }
    
    public void StopShake()
    {
        if (noise == null) return;
        
        noise.m_AmplitudeGain = 0f;
        noise.m_FrequencyGain = 0f;
        
        if (showDebugInfo)
            Debug.Log("CameraController: Screen shake stopped");
    }
    
    public void SetShakeIntensity(float intensity)
    {
        shakeIntensity = intensity;
        if (showDebugInfo)
            Debug.Log($"CameraController: Shake intensity set to {intensity}");
    }
    
    public void SetShakeDuration(float duration)
    {
        shakeDuration = duration;
        if (showDebugInfo)
            Debug.Log($"CameraController: Shake duration set to {duration}");
    }
    
    void LogCameraZones()
    {
        if (cameraZones == null) return;
        
        Debug.Log("=== CAMERA ZONES ===");
        for (int i = 0; i < cameraZones.Length; i++)
        {
            Debug.Log($"Zone {i}: {cameraZones[i].name} - {cameraZones[i].bounds}");
        }
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display camera info
        GUI.Label(new Rect(10, 10, 300, 20), "Camera Controller:");
        GUI.Label(new Rect(10, 30, 300, 20), $"Follow Speed: {followSpeed}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Look Ahead: {lookAheadDistance}");
        GUI.Label(new Rect(10, 70, 300, 20), $"Shake Timer: {shakeTimer:F2}");
        GUI.Label(new Rect(10, 90, 300, 20), $"Current Zone: {(currentZone != null ? currentZone.name : "None")}");
        GUI.Label(new Rect(10, 110, 300, 20), "T - Test Shake, Z - Log Zones");
    }
    
    void OnDrawGizmos()
    {
        if (!showDebugInfo) return;
        
        // Draw camera zones
        if (cameraZones != null)
        {
            for (int i = 0; i < cameraZones.Length; i++)
            {
                Gizmos.color = cameraZones[i].gizmoColor;
                Gizmos.DrawWireCube(cameraZones[i].bounds.center, cameraZones[i].bounds.size);
            }
        }
        
        // Draw look ahead
        if (playerTransform != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(playerTransform.position, currentLookAhead);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (!showDebugInfo) return;
        
        // Draw additional gizmos when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 2f);
    }
}
