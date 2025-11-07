using UnityEngine;

/// <summary>
/// Simple Enemy AI with chase behavior, attack patterns, and health system
/// </summary>
public class SimpleEnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public float moveSpeed = 2f;
    public float attackCooldown = 2f;

    [Header("Health Settings")]
    public int maxHealth = 100;
    public int currentHealth;

    [Header("Attack Settings")]
    public int attackDamage = 20;
    public float attackForce = 5f;

    [Header("References")]
    public Transform player;
    public LayerMask playerLayer;

    private enum AIState
    {
        Idle,
        Chasing,
        Attacking,
        Dead
    }

    private AIState currentState = AIState.Idle;
    private float lastAttackTime;
    private Rigidbody2D rb;
    private Animator animator;

    // Animation parameters
    private const string SPEED_PARAM = "Speed";
    private const string ATTACK_TRIGGER = "Attack";
    private const string DEATH_TRIGGER = "Death";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;

        // Find player if not assigned
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
        }
    }

    void Update()
    {
        if (currentState == AIState.Dead) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        switch (currentState)
        {
            case AIState.Idle:
                if (distanceToPlayer <= detectionRange)
                {
                    currentState = AIState.Chasing;
                }
                break;

            case AIState.Chasing:
                if (distanceToPlayer > detectionRange)
                {
                    currentState = AIState.Idle;
                }
                else if (distanceToPlayer <= attackRange)
                {
                    currentState = AIState.Attacking;
                }
                else
                {
                    ChasePlayer();
                }
                break;

            case AIState.Attacking:
                if (distanceToPlayer > attackRange)
                {
                    currentState = AIState.Chasing;
                }
                else
                {
                    AttackPlayer();
                }
                break;
        }

        UpdateAnimations();
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // Flip sprite based on direction
        if (direction.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (direction.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    void AttackPlayer()
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // Stop moving while attacking
            rb.velocity = new Vector2(0, rb.velocity.y);

            // Trigger attack animation
            if (animator != null)
            {
                animator.SetTrigger(ATTACK_TRIGGER);
            }

            // Deal damage to player
            if (player != null)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(attackDamage);
                }
            }

            lastAttackTime = Time.time;
        }
    }

    void UpdateAnimations()
    {
        if (animator != null)
        {
            animator.SetFloat(SPEED_PARAM, Mathf.Abs(rb.velocity.x));
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        currentState = AIState.Dead;

        // Stop movement
        rb.velocity = Vector2.zero;

        // Trigger death animation
        if (animator != null)
        {
            animator.SetTrigger(DEATH_TRIGGER);
        }

        // Disable collider
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Destroy after delay
        Destroy(gameObject, 2f);
    }

    void OnDrawGizmosSelected()
    {
        // Draw detection range
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);

        // Draw attack range
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
