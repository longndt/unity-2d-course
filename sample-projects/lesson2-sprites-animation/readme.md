# Lesson 2: Sprites & Animation - Sample Project

## 🎯 Project Overview

This sample project demonstrates Unity's 2D sprite system, animation workflow, and Animator Controller. It features a complete character animation system with idle, walk, jump, and attack animations.

### **📚 Relationship to Lesson 2 Examples**
- **Basic Examples**: `lesson2-sprites-animation/example/` - Individual scripts for learning specific concepts
- **This Sample Project**: Complete integrated system with all concepts working together
- **Learning Path**: Study individual examples first, then explore this complete implementation

## 🎮 Project Description

### **Objective**
Create an animated 2D character with smooth animations and responsive controls.

### **Features**
- Character sprite import and configuration
- Complete animation system (idle, walk, jump, attack)
- Animator Controller with state machine
- Animation events and curves
- Sprite sorting and layering

### **Controls**
- **A/D Keys**: Move character left/right
- **Space**: Jump
- **Left Mouse**: Attack
- **E**: Interact with objects

## 🏗️ Project Structure

```
Assets/
├── Scenes/
│   └── AnimationDemo.unity     # Main demo scene
├── Scripts/
│   ├── CharacterController.cs  # Character movement and animation
│   ├── AnimationEventHandler.cs # Animation event handling
│   └── SpriteManager.cs        # Sprite management utilities
├── Sprites/
│   ├── Character/
│   │   ├── idle_01.png         # Idle animation frames
│   │   ├── idle_02.png
│   │   ├── walk_01.png         # Walk animation frames
│   │   ├── walk_02.png
│   │   ├── jump_01.png         # Jump animation frames
│   │   └── attack_01.png       # Attack animation frames
│   └── Environment/
│       ├── platform.png        # Platform sprites
│       └── background.png      # Background elements
├── Animations/
│   ├── CharacterIdle.anim      # Idle animation clip
│   ├── CharacterWalk.anim      # Walk animation clip
│   ├── CharacterJump.anim      # Jump animation clip
│   └── CharacterAttack.anim    # Attack animation clip
├── Animators/
│   └── CharacterAnimator.controller # Animator Controller
└── Materials/
    └── CharacterMaterial.mat   # Character material
```

## 🎯 Learning Objectives

After studying this project, you will understand:

### **Sprite Management**
- Sprite import settings and configuration
- Sprite slicing and atlas creation
- Sorting layers and order in layer
- Sprite renderer properties

### **Animation System**
- Animation clip creation and editing
- Keyframe animation and curves
- Animation events and callbacks
- Animation blending and transitions

### **Animator Controller**
- State machine design and implementation
- Parameters and conditions
- Transition setup and timing
- Sub-state machines and layers

### **Character Animation**
- Movement-based animation triggers
- Direction-based sprite flipping
- Animation state management
- Performance optimization

## 🔧 Setup Instructions

### **1. Open Project**
1. Launch Unity Hub
2. Click "Add" and select this project folder
3. Open the project in Unity Editor
4. Wait for assets to import

### **2. Explore the Scene**
1. **Open AnimationDemo scene**
2. **Select Character** in Hierarchy
3. **Examine Animator Controller** in Project window
4. **Test animations** in Play mode

### **3. Test the Features**
1. **Use A/D keys** to move character
2. **Press Space** to jump
3. **Click Left Mouse** to attack
4. **Watch animations** change based on input

## 📝 Code Walkthrough

### **CharacterController.cs**
```csharp
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer = 1;
    
    [Header("Animation Settings")]
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    private bool isFacingRight = true;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        HandleInput();
        CheckGrounded();
        UpdateAnimations();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }
    
    void HandleInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
    
    void HandleMovement()
    {
        if (horizontalInput != 0)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            
            // Flip sprite based on direction
            if (horizontalInput > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (horizontalInput < 0 && isFacingRight)
            {
                Flip();
            }
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetTrigger("Jump");
    }
    
    void Attack()
    {
        animator.SetTrigger("Attack");
    }
    
    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        animator.SetBool("IsGrounded", isGrounded);
    }
    
    void UpdateAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(horizontalInput));
        animator.SetFloat("VerticalSpeed", rb.velocity.y);
    }
    
    void Flip()
    {
        isFacingRight = !isFacingRight;
        spriteRenderer.flipX = !isFacingRight;
    }
}
```

### **AnimationEventHandler.cs**
```csharp
using UnityEngine;

public class AnimationEventHandler : MonoBehaviour
{
    [Header("Animation Events")]
    public AudioSource audioSource;
    public ParticleSystem dustEffect;
    public ParticleSystem attackEffect;
    
    [Header("Audio Clips")]
    public AudioClip footstepSound;
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip landSound;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    // Called by animation events
    public void OnFootstep()
    {
        if (footstepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(footstepSound);
        }
        
        if (dustEffect != null)
        {
            dustEffect.Play();
        }
        
        Debug.Log("Footstep event triggered");
    }
    
    public void OnJumpStart()
    {
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        
        Debug.Log("Jump start event triggered");
    }
    
    public void OnJumpLand()
    {
        if (landSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(landSound);
        }
        
        if (dustEffect != null)
        {
            dustEffect.Play();
        }
        
        Debug.Log("Jump land event triggered");
    }
    
    public void OnAttackStart()
    {
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
        
        if (attackEffect != null)
        {
            attackEffect.Play();
        }
        
        Debug.Log("Attack start event triggered");
    }
    
    public void OnAttackEnd()
    {
        Debug.Log("Attack end event triggered");
    }
}
```

### **SpriteManager.cs**
```csharp
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [Header("Sprite Settings")]
    public SpriteRenderer spriteRenderer;
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;
    public Sprite[] jumpSprites;
    public Sprite[] attackSprites;
    
    [Header("Animation Settings")]
    public float animationSpeed = 0.1f;
    
    private int currentFrame = 0;
    private float timer = 0f;
    private string currentAnimation = "idle";
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        HandleSpriteAnimation();
    }
    
    void HandleSpriteAnimation()
    {
        timer += Time.deltaTime;
        
        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % GetCurrentSpriteArray().Length;
            spriteRenderer.sprite = GetCurrentSpriteArray()[currentFrame];
        }
    }
    
    public void SetAnimation(string animationName)
    {
        if (currentAnimation != animationName)
        {
            currentAnimation = animationName;
            currentFrame = 0;
            timer = 0f;
        }
    }
    
    Sprite[] GetCurrentSpriteArray()
    {
        switch (currentAnimation)
        {
            case "idle": return idleSprites;
            case "walk": return walkSprites;
            case "jump": return jumpSprites;
            case "attack": return attackSprites;
            default: return idleSprites;
        }
    }
}
```

## 🎨 Visual Elements

### **Character Sprites**
- **Idle Animation**: 4 frames of idle pose
- **Walk Animation**: 6 frames of walking cycle
- **Jump Animation**: 3 frames of jump sequence
- **Attack Animation**: 4 frames of attack motion

### **Environment Sprites**
- **Platforms**: Various sized platform sprites
- **Background**: Parallax background elements
- **Collectibles**: Animated collectible items

### **Particle Effects**
- **Dust Particles**: Footstep and landing effects
- **Attack Effects**: Visual feedback for attacks
- **Jump Effects**: Landing impact particles

## 🎮 Gameplay Features

### **Character Movement**
- Smooth horizontal movement
- Responsive jump mechanics
- Direction-based sprite flipping
- Ground detection and validation

### **Animation System**
- State-based animation switching
- Smooth transitions between states
- Animation event integration
- Performance-optimized sprite management

### **Visual Feedback**
- Particle effects for actions
- Audio feedback for interactions
- Smooth animation blending
- Responsive input handling

## 🔧 Customization Options

### **Easy Modifications**
1. **Change animation speed**: Modify `animationSpeed` in SpriteManager
2. **Add new animations**: Create new animation clips and states
3. **Modify movement**: Adjust `moveSpeed` and `jumpForce` values
4. **Add new effects**: Create new particle systems and audio clips

### **Advanced Features**
1. **Animation blending**: Implement smooth transitions between animations
2. **Sub-state machines**: Create complex animation hierarchies
3. **Animation layers**: Add overlay animations for effects
4. **Performance optimization**: Implement sprite atlasing and batching

## 🐛 Common Issues

### **Animations not playing**
- **Cause**: Animator Controller not assigned
- **Solution**: Assign Animator Controller to Animator component

### **Sprites not displaying**
- **Cause**: Sprite not assigned to Sprite Renderer
- **Solution**: Assign sprite to Sprite Renderer component

### **Animation events not working**
- **Cause**: Animation event functions not found
- **Solution**: Ensure function names match exactly

### **Performance issues**
- **Cause**: Too many draw calls or large sprites
- **Solution**: Use sprite atlasing and optimize sprite sizes

## 📚 Learning Exercises

### **Beginner Exercises**
1. **Create new animations** for different character actions
2. **Modify animation timing** and transitions
3. **Add new sprite frames** to existing animations
4. **Experiment with animation curves** and easing

### **Intermediate Exercises**
1. **Implement animation blending** between states
2. **Create sub-state machines** for complex behaviors
3. **Add animation layers** for overlay effects
4. **Implement animation events** for sound and effects

### **Advanced Exercises**
1. **Build a complete animation system** with multiple characters
2. **Create procedural animations** using code
3. **Implement animation state persistence** across scenes
4. **Add animation compression** and optimization

## 🎯 Next Steps

After completing this project:

1. **Study the animation system**: Understand how states and transitions work
2. **Experiment with different animations**: Try creating your own
3. **Move to Lesson 3**: Learn about physics and collisions
4. **Build your own character**: Create a unique animated character

## 💡 Pro Tips

### **Animation Tips**
- **Plan your animations** before creating them
- **Use consistent frame rates** across all animations
- **Test animations** in context with gameplay
- **Optimize for performance** from the start

### **Sprite Management**
- **Organize sprites** in logical folders
- **Use consistent naming** conventions
- **Optimize sprite sizes** for your target platform
- **Use sprite atlasing** for better performance

---

**Happy Learning!** This project provides a comprehensive understanding of Unity's 2D animation system. Take your time to explore and experiment with the animations!
