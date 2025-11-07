using UnityEngine;

/// <summary>
/// Simple enemy AI demonstrating basic AI behavior
/// Shows continuous checking pattern (game loop)
/// </summary>
public class SimpleEnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1f;

    [Header("Target")]
    [SerializeField] private Transform target;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Find player if no target assigned
        // Note: FindGameObjectWithTag is expensive - use sparingly, prefer Inspector assignment
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
                Debug.Log("SimpleEnemyAI: Found player using FindGameObjectWithTag. Consider assigning in Inspector for better performance.");
            }
            else
            {
                Debug.LogWarning("SimpleEnemyAI: Player not found. Enemy AI will not work.");
            }
        }
    }

    void Update()
    {
        // Continuously check distance to player (every frame!)
        if (target == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
    }

    void ChasePlayer()
    {
        // Move towards player
        Vector2 direction = (target.position - transform.position).normalized;

        if (rb != null)
        {
            rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);
        }
        else
        {
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
    }

    void Attack()
    {
        Debug.Log($"{gameObject.name} attacks player!");
        // Attack logic here
    }

    void OnDrawGizmosSelected()
    {
        // Visualize detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Visualize attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Draw line to target
        if (target != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, target.position);
        }
    }
}
