using UnityEngine;

/// <summary>
/// Patrol AI with waypoint patrol and player detection
/// </summary>
public class PatrolAI : MonoBehaviour
{
    [Header("Patrol Settings")]
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float waitTime = 1f;
    public bool loop = true;

    [Header("Player Detection")]
    public float detectionRange = 5f;
    public LayerMask playerLayer = 1;
    public string playerTag = "Player";

    [Header("Alert States")]
    public float alertDuration = 3f;
    public float chaseSpeed = 4f;

    private int currentWaypoint = 0;
    private float waitTimer = 0f;
    private bool isWaiting = false;
    private bool movingForward = true;

    private Transform player;
    private bool isAlert = false;
    private float alertTimer = 0f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Find player
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        if (waypoints.Length == 0)
        {
            Debug.LogWarning("No waypoints assigned to PatrolAI!");
            return;
        }

        // Set initial position
        transform.position = waypoints[0].position;
    }

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Check for player detection
        if (DetectPlayer())
        {
            SetAlert(true);
        }
        else if (isAlert)
        {
            alertTimer -= Time.deltaTime;
            if (alertTimer <= 0)
            {
                SetAlert(false);
            }
        }

        if (isAlert)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    bool DetectPlayer()
    {
        if (player == null) return false;

        float distance = Vector2.Distance(transform.position, player.position);
        return distance <= detectionRange;
    }

    void SetAlert(bool alert)
    {
        isAlert = alert;
        if (alert)
        {
            alertTimer = alertDuration;
        }
    }

    void ChasePlayer()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * chaseSpeed;

        // Rotate towards player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Patrol()
    {
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
        Vector2 direction = (targetPosition - transform.position).normalized;

        // Move towards target
        rb.velocity = direction * moveSpeed;

        // Rotate towards movement direction
        if (rb.velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        // Check if reached target
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
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

    public void SetWaypoints(Transform[] newWaypoints)
    {
        waypoints = newWaypoints;
        currentWaypoint = 0;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void SetDetectionRange(float range)
    {
        detectionRange = range;
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

        // Draw detection range
        Gizmos.color = isAlert ? Color.red : Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw current target
        if (waypoints.Length > 0 && waypoints[currentWaypoint] != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(waypoints[currentWaypoint].position, 0.7f);
        }
    }
}
