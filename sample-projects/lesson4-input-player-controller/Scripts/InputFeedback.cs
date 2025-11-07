using UnityEngine;

/// <summary>
/// Provides visual and audio feedback for player inputs.
/// Demonstrates input feedback systems and game feel enhancement.
/// </summary>
public class InputFeedback : MonoBehaviour
{
    [Header("Visual Feedback")]
    [Tooltip("Dust effect for footstep")]
    public GameObject dustEffect;
    
    [Tooltip("Jump effect for jumping")]
    public GameObject jumpEffect;
    
    [Tooltip("Attack effect for attacking")]
    public GameObject attackEffect;
    
    [Tooltip("Impact effect for collisions")]
    public GameObject impactEffect;
    
    [Header("Audio Feedback")]
    [Tooltip("Audio source for feedback sounds")]
    public AudioSource audioSource;
    
    [Tooltip("Jump sound effect")]
    public AudioClip jumpSound;
    
    [Tooltip("Attack sound effect")]
    public AudioClip attackSound;
    
    [Tooltip("Footstep sound effect")]
    public AudioClip footstepSound;
    
    [Tooltip("Impact sound effect")]
    public AudioClip impactSound;
    
    [Header("Screen Effects")]
    [Tooltip("Camera controller for screen shake")]
    public CameraController cameraController;
    
    [Tooltip("Screen shake intensity")]
    public float shakeIntensity = 0.5f;
    
    [Tooltip("Screen shake duration")]
    public float shakeDuration = 0.2f;
    
    [Header("Particle Settings")]
    [Tooltip("Particle system for dust")]
    public ParticleSystem dustParticles;
    
    [Tooltip("Particle system for jump")]
    public ParticleSystem jumpParticles;
    
    [Tooltip("Particle system for attack")]
    public ParticleSystem attackParticles;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private float lastFootstepTime;
    private float footstepInterval = 0.3f;
    
    void Start()
    {
        // Get audio source if not assigned
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        // Get camera controller if not assigned
        if (cameraController == null)
            cameraController = FindObjectOfType<CameraController>();
        
        if (showDebugInfo)
            Debug.Log("InputFeedback: Input feedback system initialized");
    }
    
    void Update()
    {
        HandleInput();
    }
    
    void HandleInput()
    {
        // Test feedback
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnJump();
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            OnAttack();
        }
        
        if (Input.GetKeyDown(KeyCode.F3))
        {
            OnFootstep();
        }
        
        if (Input.GetKeyDown(KeyCode.F4))
        {
            OnImpact();
        }
    }
    
    public void OnJump()
    {
        if (showDebugInfo)
            Debug.Log("InputFeedback: Jump feedback triggered");
        
        // Visual feedback
        if (jumpEffect != null)
        {
            Instantiate(jumpEffect, transform.position, Quaternion.identity);
        }
        
        if (jumpParticles != null)
        {
            jumpParticles.Play();
        }
        
        // Audio feedback
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        
        // Screen shake
        if (cameraController != null)
        {
            cameraController.StartShake();
        }
    }
    
    public void OnAttack()
    {
        if (showDebugInfo)
            Debug.Log("InputFeedback: Attack feedback triggered");
        
        // Visual feedback
        if (attackEffect != null)
        {
            Instantiate(attackEffect, transform.position, Quaternion.identity);
        }
        
        if (attackParticles != null)
        {
            attackParticles.Play();
        }
        
        // Audio feedback
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
        
        // Screen shake
        if (cameraController != null)
        {
            cameraController.StartShake();
        }
    }
    
    public void OnFootstep()
    {
        if (showDebugInfo)
            Debug.Log("InputFeedback: Footstep feedback triggered");
        
        // Visual feedback
        if (dustEffect != null)
        {
            Instantiate(dustEffect, transform.position, Quaternion.identity);
        }
        
        if (dustParticles != null)
        {
            dustParticles.Play();
        }
        
        // Audio feedback
        if (audioSource != null && footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
    }
    
    public void OnImpact()
    {
        if (showDebugInfo)
            Debug.Log("InputFeedback: Impact feedback triggered");
        
        // Visual feedback
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
        // Audio feedback
        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }
        
        // Screen shake
        if (cameraController != null)
        {
            cameraController.StartShake();
        }
    }
    
    public void OnLand()
    {
        if (showDebugInfo)
            Debug.Log("InputFeedback: Land feedback triggered");
        
        // Visual feedback
        if (dustEffect != null)
        {
            Instantiate(dustEffect, transform.position, Quaternion.identity);
        }
        
        if (dustParticles != null)
        {
            dustParticles.Play();
        }
        
        // Audio feedback
        if (audioSource != null && footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
        
        // Screen shake
        if (cameraController != null)
        {
            cameraController.StartShake();
        }
    }
    
    public void OnCollect()
    {
        if (showDebugInfo)
            Debug.Log("InputFeedback: Collect feedback triggered");
        
        // Visual feedback
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
        // Audio feedback
        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }
    }
    
    public void OnDamage()
    {
        if (showDebugInfo)
            Debug.Log("InputFeedback: Damage feedback triggered");
        
        // Visual feedback
        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }
        
        // Audio feedback
        if (audioSource != null && impactSound != null)
        {
            audioSource.PlayOneShot(impactSound);
        }
        
        // Screen shake
        if (cameraController != null)
        {
            cameraController.StartShake();
        }
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display feedback controls
        GUI.Label(new Rect(10, 10, 300, 20), "Input Feedback Controls:");
        GUI.Label(new Rect(10, 30, 300, 20), "F1 - Jump Feedback");
        GUI.Label(new Rect(10, 50, 300, 20), "F2 - Attack Feedback");
        GUI.Label(new Rect(10, 70, 300, 20), "F3 - Footstep Feedback");
        GUI.Label(new Rect(10, 90, 300, 20), "F4 - Impact Feedback");
    }
}
