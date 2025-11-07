using UnityEngine;

/// <summary>
/// Moving Platform with waypoint movement and player attachment
/// </summary>
public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float waitTime = 1f;
    public bool loop = true;

    [Header("Player Attachment")]
    public bool attachPlayer = true;
    public LayerMask playerLayer = 1;

    private int currentWaypoint = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private bool movingForward = true;

    private Transform player;
    private Vector3 lastPlatformPosition;

    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned to MovingPlatform!");
            return;
        }

        // Set initial position
        transform.position = waypoints[0].position;
        lastPlatformPosition = transform.position;
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        if (isWaiting)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                isWaiting = false;
            }
        }
        else
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        Vector3 targetPosition = waypoints[currentWaypoint].position;
        Vector3 direction = (targetPosition - transform.position).normalized;

        // Move towards target
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Check if reached target
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            transform.position = targetPosition;
            isWaiting = true;
            waitTimer = waitTime;

            // Move to next waypoint
            if (loop)
            {
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            }
            else
            {
                if (movingForward)
                {
                    currentWaypoint++;
                    if (currentWaypoint >= waypoints.Length)
                    {
                        currentWaypoint = waypoints.Length - 2;
                        movingForward = false;
                    }
                }
                else
                {
                    currentWaypoint--;
                    if (currentWaypoint < 0)
                    {
                        currentWaypoint = 1;
                        movingForward = true;
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (attachPlayer && IsInLayerMask(other.gameObject, playerLayer))
        {
            player = other.transform;
            player.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (attachPlayer && other.transform == player)
        {
            player.SetParent(null);
            player = null;
        }
    }

    bool IsInLayerMask(GameObject obj, LayerMask layerMask)
    {
        return (layerMask.value & (1 << obj.layer)) > 0;
    }

    void OnDrawGizmosSelected()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        // Draw waypoints
        Gizmos.color = Color.yellow;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (waypoints[i] != null)
            {
                Gizmos.DrawWireSphere(waypoints[i].position, 0.5f);

                // Draw line to next waypoint
                if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
                else if (loop && waypoints[0] != null)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                }
            }
        }

        // Draw current target
        if (waypoints.Length > 0 && waypoints[currentWaypoint] != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(waypoints[currentWaypoint].position, 0.7f);
        }
    }
}
