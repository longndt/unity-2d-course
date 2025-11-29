using UnityEngine;

/// <summary>
/// Manages sprite animations and sprite-based visual effects.
/// Provides manual sprite animation control as an alternative to Animator.
/// </summary>
public class SpriteManager : MonoBehaviour
{
    [Header("Sprite Settings")]
    [Tooltip("Sprite renderer component")]
    public SpriteRenderer spriteRenderer;
    
    [Tooltip("Array of idle animation sprites")]
    public Sprite[] idleSprites;
    
    [Tooltip("Array of walk animation sprites")]
    public Sprite[] walkSprites;
    
    [Tooltip("Array of jump animation sprites")]
    public Sprite[] jumpSprites;
    
    [Tooltip("Array of attack animation sprites")]
    public Sprite[] attackSprites;
    
    [Header("Animation Settings")]
    [Tooltip("Animation speed (frames per second)")]
    public float animationSpeed = 10f;
    
    [Tooltip("Enable sprite animation")]
    public bool enableAnimation = true;
    
    [Tooltip("Loop animations")]
    public bool loopAnimations = true;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    [Tooltip("Show animation info on screen")]
    public bool showOnScreen = true;
    
    private int currentFrame = 0;
    private float timer = 0f;
    private string currentAnimation = "idle";
    private bool isAnimating = false;
    private Sprite[] currentSpriteArray;
    
    void Start()
    {
        // Get sprite renderer if not assigned
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Initialize with idle animation
        SetAnimation("idle");
        
        if (showDebugInfo)
            Debug.Log("SpriteManager: Sprite animation system initialized");
    }
    
    void Update()
    {
        if (enableAnimation && isAnimating)
        {
            HandleSpriteAnimation();
        }
        
        HandleInput();
    }
    
    void HandleInput()
    {
        // Test different animations
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetAnimation("idle");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetAnimation("walk");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetAnimation("jump");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetAnimation("attack");
        }
    }
    
    void HandleSpriteAnimation()
    {
        if (currentSpriteArray == null || currentSpriteArray.Length == 0)
            return;
        
        timer += Time.deltaTime;
        
        if (timer >= 1f / animationSpeed)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % currentSpriteArray.Length;
            
            // Update sprite
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = currentSpriteArray[currentFrame];
            }
            
            // Check if animation should loop
            if (currentFrame == 0 && !loopAnimations)
            {
                isAnimating = false;
                if (showDebugInfo)
                    Debug.Log($"SpriteManager: Animation '{currentAnimation}' completed");
            }
        }
    }
    
    public void SetAnimation(string animationName)
    {
        if (currentAnimation == animationName && isAnimating)
            return;
        
        currentAnimation = animationName;
        currentFrame = 0;
        timer = 0f;
        isAnimating = true;
        
        // Get appropriate sprite array
        currentSpriteArray = GetSpriteArray(animationName);
        
        if (currentSpriteArray == null || currentSpriteArray.Length == 0)
        {
            if (showDebugInfo)
                Debug.LogWarning($"SpriteManager: No sprites found for animation '{animationName}'");
            isAnimating = false;
            return;
        }
        
        // Set first frame
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = currentSpriteArray[0];
        }
        
        if (showDebugInfo)
            Debug.Log($"SpriteManager: Started animation '{animationName}' with {currentSpriteArray.Length} frames");
    }
    
    Sprite[] GetSpriteArray(string animationName)
    {
        switch (animationName.ToLower())
        {
            case "idle":
                return idleSprites;
            case "walk":
                return walkSprites;
            case "jump":
                return jumpSprites;
            case "attack":
                return attackSprites;
            default:
                return idleSprites;
        }
    }
    
    public void StopAnimation()
    {
        isAnimating = false;
        if (showDebugInfo)
            Debug.Log("SpriteManager: Animation stopped");
    }
    
    public void PauseAnimation()
    {
        isAnimating = false;
        if (showDebugInfo)
            Debug.Log("SpriteManager: Animation paused");
    }
    
    public void ResumeAnimation()
    {
        isAnimating = true;
        if (showDebugInfo)
            Debug.Log("SpriteManager: Animation resumed");
    }
    
    public void SetAnimationSpeed(float speed)
    {
        animationSpeed = Mathf.Max(0.1f, speed);
        if (showDebugInfo)
            Debug.Log($"SpriteManager: Animation speed set to {animationSpeed}");
    }
    
    public string GetCurrentAnimation()
    {
        return currentAnimation;
    }
    
    public int GetCurrentFrame()
    {
        return currentFrame;
    }
    
    public bool IsAnimating()
    {
        return isAnimating;
    }
    
    void OnGUI()
    {
        if (!showOnScreen) return;
        
        // Display animation info
        GUI.Label(new Rect(10, 10, 300, 20), "Sprite Manager Controls:");
        GUI.Label(new Rect(10, 30, 300, 20), "1 - Idle Animation");
        GUI.Label(new Rect(10, 50, 300, 20), "2 - Walk Animation");
        GUI.Label(new Rect(10, 70, 300, 20), "3 - Jump Animation");
        GUI.Label(new Rect(10, 90, 300, 20), "4 - Attack Animation");
        
        GUI.Label(new Rect(10, 120, 300, 20), $"Current Animation: {currentAnimation}");
        GUI.Label(new Rect(10, 140, 300, 20), $"Current Frame: {currentFrame}");
        GUI.Label(new Rect(10, 160, 300, 20), $"Animation Speed: {animationSpeed}");
        GUI.Label(new Rect(10, 180, 300, 20), $"Is Animating: {isAnimating}");
    }
}
