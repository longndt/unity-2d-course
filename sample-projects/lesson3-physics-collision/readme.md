# Lesson 3: Physics & Collision - Sample Project

## 🎯 Project Overview

This sample project demonstrates Unity's 2D Physics System with advanced mechanics including coyote time, jump buffering, variable jump height, and physics-based interactions. It features a complete 2D platformer with realistic physics.

### **📚 Relationship to Lesson 3 Examples**
- **Basic Examples**: `lesson3-physics-collision/example/` - Individual physics concepts and scripts
- **This Sample Project**: Complete platformer with all physics concepts integrated
- **Learning Path**: Study individual examples first, then explore this complete game

## 🎮 Project Description

### **Objective**
Create a responsive 2D platformer with advanced physics mechanics and smooth character movement.

### **Features**
- Advanced jump mechanics (coyote time, jump buffering, variable height)
- Physics-based character movement
- Collision detection and trigger systems
- Moving platforms and one-way platforms
- Physics materials and optimization

### **Controls**
- **A/D Keys**: Move left/right
- **Space**: Jump (hold for higher jump)
- **S Key**: Drop through one-way platforms
- **Mouse**: Interact with objects

## 🏗️ Project Structure

```
Assets/
├── Scenes/
│   └── PhysicsDemo.unity       # Main physics demo scene
├── Scripts/
│   ├── AdvancedPlayerController.cs    # Advanced player controller
│   ├── MovingPlatform.cs              # Moving platform system
│   ├── OneWayPlatform.cs              # One-way platform behavior
│   ├── PhysicsMaterialTester.cs       # Physics material testing
│   └── CollisionHandler.cs            # Collision detection system
├── Prefabs/
│   ├── Player.prefab           # Player with advanced controller
│   ├── MovingPlatform.prefab   # Moving platform prefab
│   ├── OneWayPlatform.prefab   # One-way platform prefab
│   └── Collectible.prefab      # Physics-based collectible
├── Materials/
│   ├── BouncyMaterial.physicsMaterial2D
│   ├── IceMaterial.physicsMaterial2D
│   ├── StickyMaterial.physicsMaterial2D
│   └── FrictionlessMaterial.physicsMaterial2D
└── Sprites/
    ├── Player/
    │   ├── idle.png
    │   ├── walk.png
    │   └── jump.png
    ├── Platforms/
    │   ├── platform.png
    │   ├── moving_platform.png
    │   └── one_way_platform.png
    └── Collectibles/
        ├── coin.png
        └── powerup.png
```

## 🎯 Learning Objectives

After studying this project, you will understand:

### **Advanced Jump Mechanics**
- Coyote time for edge jumping
- Jump buffering for responsive input
- Variable jump height based on input duration
- Ground detection and validation

### **Physics System**
- Rigidbody2D properties and constraints
- Physics materials and their effects
- Collision detection vs trigger events
- Physics optimization techniques

### **Platform Systems**
- Moving platforms with waypoints
- One-way platforms with drop-through
- Physics-based interactions
- Collision matrix configuration

### **Performance Optimization**
- Physics 2D settings optimization
- Collision matrix setup
- Object pooling for physics objects
- Profiling and debugging

## 🔧 Setup Instructions

### **1. Open Project**
1. Launch Unity Hub
2. Click "Add" and select this project folder
3. Open the project in Unity Editor
4. Wait for assets to import

### **2. Configure Physics Settings**
1. **Edit → Project Settings → Physics 2D**
2. **Set Gravity**: X = 0, Y = -9.81
3. **Configure Collision Matrix** for optimal performance
4. **Set Velocity Iterations**: 8, Position Iterations: 3

### **3. Test the Features**
1. **Use A/D keys** to move character
2. **Press Space** to jump (hold for higher jump)
3. **Press S** to drop through one-way platforms
4. **Test different physics materials** with number keys

## 📝 Code Walkthrough

### **AdvancedPlayerController.cs**
```csharp
using UnityEngine;

public class AdvancedPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 15f;
    public float maxSpeed = 8f;
    public float acceleration = 20f;
    public float deceleration = 20f;
    
    [Header("Jump Settings")]
    public float jumpTime = 0.2f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;
    public float jumpCutMultiplier = 0.5f;
    
    [Header("Physics Settings")]
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer = 1;
    public LayerMask oneWayLayer = 2;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float jumpTimeCounter;
    private bool isJumping;
    private bool jumpInput;
    private bool jumpInputStop;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        HandleInput();
        CheckGrounded();
        HandleCoyoteTime();
        HandleJumpBuffer();
        HandleJump();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }
    
    void HandleInput()
    {
        jumpInput = Input.GetKey(KeyCode.Space);
        jumpInputStop = Input.GetKeyUp(KeyCode.Space);
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
    }
    
    void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        if (horizontalInput != 0)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
        else
        {
            // Apply deceleration when no input
            rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, deceleration * Time.fixedDeltaTime), rb.velocity.y);
        }
        
        // Limit horizontal speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }
    
    void CheckGrounded()
    {
        wasGrounded = isGrounded;
        
        // Check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        // Check for one-way platforms
        if (!isGrounded)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, oneWayLayer);
            isGrounded = hit.collider != null;
        }
        
        // Visual debug
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
    
    void HandleCoyoteTime()
    {
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }
    
    void HandleJumpBuffer()
    {
        if (jumpBufferCounter > 0)
        {
            jumpBufferCounter -= Time.deltaTime;
        }
    }
    
    void HandleJump()
    {
        // Jump if conditions are met
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
        }
        
        // Variable jump height
        if (jumpInput && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        
        // Jump cut for shorter jumps
        if (jumpInputStop && isJumping)
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
            isJumping = false;
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpTimeCounter = jumpTime;
        isJumping = true;
        coyoteTimeCounter = 0f;
        
        Debug.Log("Jump executed with coyote time!");
    }
}
```

### **MovingPlatform.cs**
```csharp
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float waitTime = 1f;
    public bool loop = true;
    
    [Header("Platform Settings")]
    public bool movePlayer = true;
    public LayerMask playerLayer = 1;
    
    private int currentWaypoint = 0;
    private float waitCounter = 0f;
    private bool isWaiting = false;
    private Vector3 lastPosition;
    private Transform playerTransform;
    
    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to MovingPlatform!");
        }
        
        lastPosition = transform.position;
    }
    
    void Update()
    {
        if (waypoints.Length == 0) return;
        
        if (isWaiting)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter >= waitTime)
            {
                isWaiting = false;
                waitCounter = 0f;
                currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            }
        }
        else
        {
            // Move towards current waypoint
            Vector2 direction = (waypoints[currentWaypoint].position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            
            // Check if reached waypoint
            if (Vector2.Distance(transform.position, waypoints[currentWaypoint].position) < 0.1f)
            {
                isWaiting = true;
            }
        }
        
        // Move player with platform
        if (movePlayer && playerTransform != null)
        {
            Vector3 deltaPosition = transform.position - lastPosition;
            playerTransform.position += deltaPosition;
        }
        
        lastPosition = transform.position;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            playerTransform = other.transform;
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            playerTransform = null;
        }
    }
    
    bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
    
    void OnDrawGizmos()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            // Draw waypoint connections
            for (int i = 0; i < waypoints.Length; i++)
            {
                if (waypoints[i] != null)
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere(waypoints[i].position, 0.5f);
                    
                    if (i < waypoints.Length - 1 && waypoints[i + 1] != null)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                    }
                    else if (i == waypoints.Length - 1 && waypoints[0] != null && loop)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(waypoints[i].position, waypoints[0].position);
                    }
                }
            }
        }
    }
}
```

### **OneWayPlatform.cs**
```csharp
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [Header("One-Way Platform Settings")]
    public float disableTime = 0.5f;
    public LayerMask playerLayer = 1;
    
    private Collider2D platformCollider;
    private float disableTimer = 0f;
    
    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
    }
    
    void Update()
    {
        // Re-enable collider after disable time
        if (disableTimer > 0f)
        {
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0f)
            {
                platformCollider.enabled = true;
            }
        }
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if player is above platform
        if (IsInLayerMask(collision.gameObject.layer, playerLayer))
        {
            float playerBottom = collision.gameObject.transform.position.y - 0.5f;
            float platformTop = transform.position.y + 0.5f;
            
            if (playerBottom > platformTop)
            {
                // Player is above platform, allow collision
                return;
            }
            else
            {
                // Player is below platform, disable collision temporarily
                platformCollider.enabled = false;
                disableTimer = disableTime;
            }
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Handle drop-through input
        if (IsInLayerMask(other.gameObject.layer, playerLayer))
        {
            if (Input.GetKey(KeyCode.S))
            {
                platformCollider.enabled = false;
                disableTimer = disableTime;
            }
        }
    }
    
    bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
```

## 🎨 Visual Elements

### **Character Sprites**
- **Idle**: Static character pose
- **Walk**: Walking animation cycle
- **Jump**: Jump pose with motion blur

### **Platform Sprites**
- **Static Platform**: Standard platform sprite
- **Moving Platform**: Animated platform with indicators
- **One-Way Platform**: Platform with visual drop-through indicator

### **Physics Materials**
- **Bouncy**: High bounciness for dynamic objects
- **Ice**: Low friction for sliding surfaces
- **Sticky**: High friction for grip surfaces
- **Frictionless**: No friction for smooth movement

## 🎮 Gameplay Features

### **Advanced Jump System**
- **Coyote Time**: Jump for a short time after leaving ground
- **Jump Buffering**: Input buffering for responsive jumping
- **Variable Height**: Hold jump for higher jumps
- **Jump Cut**: Release jump early for shorter jumps

### **Platform Mechanics**
- **Moving Platforms**: Follow waypoint paths
- **One-Way Platforms**: Drop through with S key
- **Physics Materials**: Different surface properties
- **Collision Matrix**: Optimized collision detection

### **Physics Optimization**
- **Efficient Collision Detection**: Layer-based collision matrix
- **Performance Monitoring**: Built-in performance tracking
- **Object Pooling**: Reusable physics objects
- **Profiling Tools**: Unity Profiler integration

## 🔧 Customization Options

### **Easy Modifications**
1. **Adjust jump mechanics**: Modify coyote time and jump buffer
2. **Change platform behavior**: Modify speed and wait times
3. **Add new physics materials**: Create custom material properties
4. **Modify collision layers**: Update collision matrix settings

### **Advanced Features**
1. **Wall jumping**: Add wall jump mechanics
2. **Dash mechanics**: Implement air dash system
3. **Physics puzzles**: Create physics-based challenges
4. **Multiplayer physics**: Add networked physics

## 🐛 Common Issues

### **Jump feels unresponsive**
- **Cause**: Coyote time or jump buffer too short
- **Solution**: Increase coyote time and jump buffer values

### **Platforms not moving**
- **Cause**: Waypoints not assigned or platform not enabled
- **Solution**: Assign waypoints and check platform settings

### **One-way platforms not working**
- **Cause**: Collision layers not configured correctly
- **Solution**: Check layer assignments and collision matrix

### **Performance issues**
- **Cause**: Too many physics objects or inefficient collision
- **Solution**: Optimize collision matrix and use object pooling

## 📚 Learning Exercises

### **Beginner Exercises**
1. **Modify jump parameters** to change feel
2. **Create new physics materials** with different properties
3. **Add new waypoints** to moving platforms
4. **Test different collision settings**

### **Intermediate Exercises**
1. **Implement wall jumping** mechanics
2. **Create physics puzzles** with moving objects
3. **Add dash mechanics** for air movement
4. **Implement physics-based collectibles**

### **Advanced Exercises**
1. **Build a complete physics engine** with custom behaviors
2. **Create multiplayer physics** with networking
3. **Implement advanced platform mechanics** like conveyor belts
4. **Add physics-based particle systems**

## 🎯 Next Steps

After completing this project:

1. **Study the physics system**: Understand how advanced mechanics work
2. **Experiment with parameters**: Try different values and combinations
3. **Move to Lesson 4**: Learn about input systems and player controllers
4. **Build your own platformer**: Create a unique physics-based game

## 💡 Pro Tips

### **Physics Tips**
- **Tune parameters carefully** for responsive gameplay
- **Use coyote time** for better jump feel
- **Optimize collision matrix** for performance
- **Test on different devices** for consistency

### **Platform Design**
- **Plan platform layouts** before implementing
- **Use visual indicators** for one-way platforms
- **Test platform timing** for smooth gameplay
- **Consider player skill level** in difficulty

---

**Happy Learning!** This project provides a deep understanding of Unity's 2D physics system. Take your time to experiment with the advanced mechanics!
