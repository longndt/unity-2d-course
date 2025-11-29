using UnityEngine;

/// <summary>
/// Moving platform that follows waypoints and can carry players.
/// Demonstrates platform movement, player carrying, and waypoint systems.
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Array of waypoints to follow")]
    public Transform[] waypoints;
    
    [Tooltip("Movement speed of the platform")]
    public float moveSpeed = 2f;
    
    [Tooltip("Wait time at each waypoint")]
    public float waitTime = 1f;
    
    [Tooltip("Loop through waypoints")]
    public bool loop = true;
    
    [Tooltip("Start moving automatically")]
    public bool autoStart = true;
    
    [Header("Platform Settings")]
    [Tooltip("Move players with the platform")]
    public bool movePlayer = true;
    
    [Tooltip("Layers that can be carried by the platform")]
    public LayerMask playerLayer = 1;
    
    [Tooltip("Platform movement type")]
    public MovementType movementType = MovementType.Linear;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    [Tooltip("Show waypoint gizmos")]
    public bool showGizmos = true;
    
    public enum MovementType
    {
        Linear,
        Smooth,
        PingPong
    }
    
    private int currentWaypoint = 0;
    private float waitCounter = 0f;
    private bool isWaiting = false;
    private bool isMoving = true;
    private Vector3 lastPosition;
    private Transform playerTransform;
    private Vector3[] waypointPositions;
    private bool movingForward = true;
    
    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("MovingPlatform: No waypoints assigned!");
            return;
        }
        
        // Store waypoint positions
        waypointPositions = new Vector3[waypoints.Length];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypointPositions[i] = waypoints[i].position;
        }
        
        lastPosition = transform.position;
        
        if (!autoStart)
        {
            isMoving = false;
        }
        
        if (showDebugInfo)
            Debug.Log("MovingPlatform: Moving platform initialized");
    }
    
    void Update()
    {
        if (waypoints.Length == 0) return;
        
        if (isMoving)
        {
            if (isWaiting)
            {
                HandleWaiting();
            }
            else
            {
                HandleMovement();
            }
        }
        
        HandlePlayerMovement();
    }
    
    void HandleWaiting()
    {
        waitCounter += Time.deltaTime;
        if (waitCounter >= waitTime)
        {
            isWaiting = false;
            waitCounter = 0f;
            MoveToNextWaypoint();
        }
    }
    
    void HandleMovement()
    {
        Vector3 targetPosition = waypointPositions[currentWaypoint];
        Vector3 direction = (targetPosition - transform.position).normalized;
        
        // Move towards current waypoint
        transform.Translate(direction * moveSpeed * Time.deltaTime);
        
        // Check if reached waypoint
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition;
            isWaiting = true;
        }
    }
    
    void MoveToNextWaypoint()
    {
        if (movementType == MovementType.PingPong)
        {
            if (movingForward)
            {
                currentWaypoint++;
                if (currentWaypoint >= waypoints.Length)
                {
                    currentWaypoint = waypoints.Length - 1;
                    movingForward = false;
                }
            }
            else
            {
                currentWaypoint--;
                if (currentWaypoint < 0)
                {
                    currentWaypoint = 0;
                    movingForward = true;
                }
            }
        }
        else
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
        
        if (showDebugInfo)
            Debug.Log($"MovingPlatform: Moving to waypoint {currentWaypoint}");
    }
    
    void HandlePlayerMovement()
    {
        if (!movePlayer || playerTransform == null) return;
        
        // Calculate platform movement delta
        Vector3 deltaPosition = transform.position - lastPosition;
        
        // Move player with platform
        playerTransform.position += deltaPosition;
        
        lastPosition = transform.position;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            playerTransform = other.transform;
            
            if (showDebugInfo)
                Debug.Log("MovingPlatform: Player stepped on platform");
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            playerTransform = null;
            
            if (showDebugInfo)
                Debug.Log("MovingPlatform: Player stepped off platform");
        }
    }
    
    bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
    
    // Public methods for external control
    public void StartMoving()
    {
        isMoving = true;
        if (showDebugInfo)
            Debug.Log("MovingPlatform: Started moving");
    }
    
    public void StopMoving()
    {
        isMoving = false;
        if (showDebugInfo)
            Debug.Log("MovingPlatform: Stopped moving");
    }
    
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        if (showDebugInfo)
            Debug.Log($"MovingPlatform: Speed set to {speed}");
    }
    
    public void SetWaitTime(float time)
    {
        waitTime = time;
        if (showDebugInfo)
            Debug.Log($"MovingPlatform: Wait time set to {time}");
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display platform info
        GUI.Label(new Rect(10, 10, 300, 20), "Moving Platform Info:");
        GUI.Label(new Rect(10, 30, 300, 20), $"Current Waypoint: {currentWaypoint}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Is Moving: {isMoving}");
        GUI.Label(new Rect(10, 70, 300, 20), $"Is Waiting: {isWaiting}");
        GUI.Label(new Rect(10, 90, 300, 20), $"Speed: {moveSpeed}");
        GUI.Label(new Rect(10, 110, 300, 20), $"Player On Platform: {playerTransform != null}");
    }
    
    void OnDrawGizmos()
    {
        if (!showGizmos || waypoints == null || waypoints.Length == 0) return;
        
        // Draw waypoint connections
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                // Draw waypoint
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(waypoints[i].position, 0.5f);
                
                // Draw connections
                Gizmos.color = Color.blue;
                if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
                else if (i == waypoints.Length - 1 && waypoints[0] != null && loop)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                }
            }
        }
        
        // Draw current waypoint
        if (currentWaypoint < waypoints.Length && waypoints[currentWaypoint] != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(waypoints[currentWaypoint].position, 0.7f);
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;
        
        // Draw additional gizmos when selected
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 1.2f);
    }
}
