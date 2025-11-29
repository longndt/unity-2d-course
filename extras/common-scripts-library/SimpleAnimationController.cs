using UnityEngine;

/// <summary>
/// Simple Animation Controller with parameter management and state transitions
/// </summary>
public class SimpleAnimationController : MonoBehaviour
{
    [Header("Animation Settings")]
    public Animator animator;
    public string speedParameter = "Speed";
    public string jumpParameter = "Jump";
    public string groundedParameter = "IsGrounded";
    public string attackParameter = "Attack";

    [Header("Movement Settings")]
    public float moveThreshold = 0.1f;
    public bool useRootMotion = false;

    private Vector3 lastPosition;
    private float currentSpeed;

    void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        lastPosition = transform.position;
    }

    void Update()
    {
        CalculateSpeed();
        UpdateAnimatorParameters();
    }

    void CalculateSpeed()
    {
        Vector3 currentPosition = transform.position;
        currentSpeed = Vector3.Distance(currentPosition, lastPosition) / Time.deltaTime;
        lastPosition = currentPosition;
    }

    void UpdateAnimatorParameters()
    {
        if (animator == null) return;

        // Update speed parameter
        if (!string.IsNullOrEmpty(speedParameter))
        {
            animator.SetFloat(speedParameter, currentSpeed);
        }

        // Update grounded parameter
        if (!string.IsNullOrEmpty(groundedParameter))
        {
            bool isGrounded = IsGrounded();
            animator.SetBool(groundedParameter, isGrounded);
        }
    }

    public void SetSpeed(float speed)
    {
        if (animator != null && !string.IsNullOrEmpty(speedParameter))
        {
            animator.SetFloat(speedParameter, speed);
        }
    }

    public void TriggerJump()
    {
        if (animator != null && !string.IsNullOrEmpty(jumpParameter))
        {
            animator.SetTrigger(jumpParameter);
        }
    }

    public void TriggerAttack()
    {
        if (animator != null && !string.IsNullOrEmpty(attackParameter))
        {
            animator.SetTrigger(attackParameter);
        }
    }

    public void SetGrounded(bool grounded)
    {
        if (animator != null && !string.IsNullOrEmpty(groundedParameter))
        {
            animator.SetBool(groundedParameter, grounded);
        }
    }

    public void SetBool(string parameterName, bool value)
    {
        if (animator != null)
        {
            animator.SetBool(parameterName, value);
        }
    }

    public void SetFloat(string parameterName, float value)
    {
        if (animator != null)
        {
            animator.SetFloat(parameterName, value);
        }
    }

    public void SetTrigger(string parameterName)
    {
        if (animator != null)
        {
            animator.SetTrigger(parameterName);
        }
    }

    bool IsGrounded()
    {
        // Simple ground check - can be customized
        return Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
    }

    // Animation Events
    public void OnAnimationEvent(string eventName)
    {
        Debug.Log($"Animation Event: {eventName}");

        // Handle specific animation events
        switch (eventName)
        {
            case "Footstep":
                PlayFootstepSound();
                break;
            case "AttackHit":
                DealDamage();
                break;
            case "JumpEnd":
                OnJumpEnd();
                break;
        }
    }

    void PlayFootstepSound()
    {
        // Play footstep sound
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    void DealDamage()
    {
        // Deal damage to nearby enemies
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Deal damage to enemy
                Debug.Log("Dealing damage to enemy!");
            }
        }
    }

    void OnJumpEnd()
    {
        // Handle jump end
        Debug.Log("Jump ended!");
    }
}
