using UnityEngine;

/// <summary>
/// Simple AI Follow with player following and obstacle avoidance
/// </summary>
public class SimpleAIFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;
    public float followDistance = 5f;
    public float stopDistance = 1f;

    [Header("Movement Settings")]
    public float moveSpeed = 3f;
    public float rotationSpeed = 5f;

    [Header("Obstacle Avoidance")]
    public LayerMask obstacleLayer = 1;
    public float avoidanceDistance = 2f;
    public float avoidanceForce = 5f;

    [Header("State Management")]
    public bool isFollowing = true;
    public bool useSmoothMovement = true;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private Vector2 avoidanceDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }

        // Find player if no target assigned
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void Update()
    {
        if (target == null || !isFollowing) return;

        float distanceToTarget = Vector2.Distance(transform.position, target.position);

        if (distanceToTarget > followDistance)
        {
            // Start following
            FollowTarget();
        }
        else if (distanceToTarget < stopDistance)
        {
            // Stop moving
            StopMoving();
        }
        else
        {
            // Continue following
            FollowTarget();
        }
    }

    void FollowTarget()
    {
        Vector2 directionToTarget = (target.position - transform.position).normalized;
        Vector2 avoidance = CalculateAvoidance();
        Vector2 finalDirection = (directionToTarget + avoidance).normalized;

        if (useSmoothMovement)
        {
            // Smooth movement
            Vector2 targetVelocity = finalDirection * moveSpeed;
            currentVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime * 5f);
            rb.velocity = currentVelocity;
        }
        else
        {
            // Direct movement
            rb.velocity = finalDirection * moveSpeed;
        }

        // Rotate towards movement direction
        if (rb.velocity.magnitude > 0.1f)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    Vector2 CalculateAvoidance()
    {
        Vector2 avoidance = Vector2.zero;

        // Check for obstacles
        Collider2D[] obstacles = Physics2D.OverlapCircleAll(transform.position, avoidanceDistance, obstacleLayer);

        foreach (Collider2D obstacle in obstacles)
        {
            if (obstacle.gameObject == gameObject) continue;

            Vector2 directionAway = (transform.position - obstacle.transform.position).normalized;
            float distance = Vector2.Distance(transform.position, obstacle.transform.position);
            float avoidanceStrength = (avoidanceDistance - distance) / avoidanceDistance;

            avoidance += directionAway * avoidanceStrength * avoidanceForce;
        }

        return avoidance;
    }

    void StopMoving()
    {
        if (useSmoothMovement)
        {
            currentVelocity = Vector2.Lerp(currentVelocity, Vector2.zero, Time.deltaTime * 5f);
            rb.velocity = currentVelocity;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    public void SetFollowing(bool follow)
    {
        isFollowing = follow;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    void OnDrawGizmosSelected()
    {
        // Draw follow distance
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, followDistance);

        // Draw stop distance
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stopDistance);

        // Draw avoidance distance
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, avoidanceDistance);

        // Draw line to target
        if (target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
