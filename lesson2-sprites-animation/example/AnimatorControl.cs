using UnityEngine;

/// <summary>
/// Animator parameter control examples
/// Demonstrates how to set different types of animator parameters
/// </summary>
public class AnimatorControl : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("AnimatorControl requires Animator component!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (animator == null) return;

        // Bool parameter
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        // Int parameter
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            animator.SetInteger("AttackType", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            animator.SetInteger("AttackType", 2);
        }

        // Float parameter
        float speed = Mathf.Abs(Input.GetAxis("Horizontal")) * 5.5f;
        animator.SetFloat("Speed", speed);

        // Trigger parameter
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jump");
        }
    }
}