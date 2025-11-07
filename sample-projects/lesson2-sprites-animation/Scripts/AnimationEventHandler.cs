using UnityEngine;

/// <summary>
/// Handles animation events and provides audio/visual feedback.
/// Integrates with Animator Controller to trigger effects during animations.
/// </summary>
public class AnimationEventHandler : MonoBehaviour
{
    [Header("Animation Events")]
    [Tooltip("Audio source for animation sounds")]
    public AudioSource audioSource;
    
    [Tooltip("Particle system for dust effects")]
    public ParticleSystem dustEffect;
    
    [Tooltip("Particle system for attack effects")]
    public ParticleSystem attackEffect;
    
    [Tooltip("Particle system for jump effects")]
    public ParticleSystem jumpEffect;
    
    [Header("Audio Clips")]
    [Tooltip("Footstep sound effect")]
    public AudioClip footstepSound;
    
    [Tooltip("Jump sound effect")]
    public AudioClip jumpSound;
    
    [Tooltip("Land sound effect")]
    public AudioClip landSound;
    
    [Tooltip("Attack sound effect")]
    public AudioClip attackSound;
    
    [Tooltip("Hit sound effect")]
    public AudioClip hitSound;
    
    [Header("Visual Effects")]
    [Tooltip("Screen shake intensity")]
    public float shakeIntensity = 0.5f;
    
    [Tooltip("Screen shake duration")]
    public float shakeDuration = 0.2f;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private Camera mainCamera;
    private Vector3 originalCameraPosition;
    
    void Start()
    {
        // Get audio source if not assigned
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
        
        // Get main camera for screen shake
        mainCamera = Camera.main;
        if (mainCamera != null)
        {
            originalCameraPosition = mainCamera.transform.position;
        }
        
        if (showDebugInfo)
            Debug.Log("AnimationEventHandler: Animation event system initialized");
    }
    
    // Animation event methods (called by Animator)
    public void OnFootstep()
    {
        if (showDebugInfo)
            Debug.Log("AnimationEventHandler: Footstep event triggered");
        
        // Play footstep sound
        if (audioSource != null && footstepSound != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
        
        // Play dust effect
        if (dustEffect != null)
        {
            dustEffect.Play();
        }
        
        // Trigger screen shake
        StartCoroutine(ScreenShake(0.1f, 0.05f));
    }
    
    public void OnJumpStart()
    {
        if (showDebugInfo)
            Debug.Log("AnimationEventHandler: Jump start event triggered");
        
        // Play jump sound
        if (audioSource != null && jumpSound != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        
        // Play jump effect
        if (jumpEffect != null)
        {
            jumpEffect.Play();
        }
        
        // Trigger screen shake
        StartCoroutine(ScreenShake(0.2f, 0.1f));
    }
    
    public void OnJumpLand()
    {
        if (showDebugInfo)
            Debug.Log("AnimationEventHandler: Jump land event triggered");
        
        // Play land sound
        if (audioSource != null && landSound != null)
        {
            audioSource.PlayOneShot(landSound);
        }
        
        // Play dust effect
        if (dustEffect != null)
        {
            dustEffect.Play();
        }
        
        // Trigger screen shake
        StartCoroutine(ScreenShake(0.3f, 0.15f));
    }
    
    public void OnAttackStart()
    {
        if (showDebugInfo)
            Debug.Log("AnimationEventHandler: Attack start event triggered");
        
        // Play attack sound
        if (audioSource != null && attackSound != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
        
        // Play attack effect
        if (attackEffect != null)
        {
            attackEffect.Play();
        }
        
        // Trigger screen shake
        StartCoroutine(ScreenShake(shakeIntensity, shakeDuration));
    }
    
    public void OnAttackHit()
    {
        if (showDebugInfo)
            Debug.Log("AnimationEventHandler: Attack hit event triggered");
        
        // Play hit sound
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }
        
        // Trigger stronger screen shake
        StartCoroutine(ScreenShake(shakeIntensity * 1.5f, shakeDuration * 0.5f));
    }
    
    public void OnAttackEnd()
    {
        if (showDebugInfo)
            Debug.Log("AnimationEventHandler: Attack end event triggered");
        
        // Stop attack effects
        if (attackEffect != null)
        {
            attackEffect.Stop();
        }
    }
    
    public void OnAnimationComplete()
    {
        if (showDebugInfo)
            Debug.Log("AnimationEventHandler: Animation complete event triggered");
        
        // Reset camera position
        if (mainCamera != null)
        {
            mainCamera.transform.position = originalCameraPosition;
        }
    }
    
    // Screen shake coroutine
    System.Collections.IEnumerator ScreenShake(float intensity, float duration)
    {
        if (mainCamera == null) yield break;
        
        Vector3 startPosition = mainCamera.transform.position;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;
            
            mainCamera.transform.position = startPosition + new Vector3(x, y, 0);
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        mainCamera.transform.position = startPosition;
    }
    
    // Public methods for external triggering
    public void TriggerFootstep()
    {
        OnFootstep();
    }
    
    public void TriggerJump()
    {
        OnJumpStart();
    }
    
    public void TriggerLand()
    {
        OnJumpLand();
    }
    
    public void TriggerAttack()
    {
        OnAttackStart();
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display animation event info
        GUI.Label(new Rect(10, 10, 300, 20), "Animation Event Handler:");
        GUI.Label(new Rect(10, 30, 300, 20), "Events are triggered by Animator");
        GUI.Label(new Rect(10, 50, 300, 20), "Check Console for event logs");
    }
}
