using UnityEngine;

/// <summary>
/// Animation debugging tools
/// Demonstrates how to debug animator states and parameters
/// </summary>
public class AnimationDebugger : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        DebugAnimatorInfo();
        DebugAnimatorParameters();
    }

    void DebugAnimatorInfo()
    {
        // Check animator state info
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Current State Hash: " + stateInfo.fullPathHash);
            Debug.Log("Normalized Time: " + stateInfo.normalizedTime);
        }

        // Debug current animation state
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk"))
        {
            Debug.Log("Currently playing Walk animation");
        }
    }

    void DebugAnimatorParameters()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Debug animator parameters
            Debug.Log("IsWalking: " + animator.GetBool("IsWalking"));
            Debug.Log("Speed: " + animator.GetFloat("Speed"));
        }
    }
}