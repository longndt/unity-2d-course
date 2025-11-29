# Lab 3: Physics & Collision

## 🎯 Learning Objectives

- Master Unity's 2D Physics System and Box2D integration
- Understand Rigidbody2D properties and physics materials
- Learn collision detection vs trigger events
- Implement advanced jump mechanics with coyote time and jump buffering
- Optimize physics performance for 2D games

## 🎮 Playtest Criteria

**Complete when you can:**
- [ ] Configure physics settings for optimal 2D gameplay
- [ ] Create responsive character movement with physics
- [ ] Implement advanced jump mechanics
- [ ] Use collision and trigger events effectively
- [ ] Optimize physics performance

---

## 🚀 Quick Start

### Step 1: Prepare Your Project

1. **Open your Unity project** from previous lessons
2. **Create new scene**: `File → New Scene → 2D`
3. **Save scene** as "PhysicsLab"
4. **Create folder structure**:
   - `Assets/Physics/`
   - `Assets/Materials/`
   - `Assets/Scripts/Physics/`

### Step 2: Configure Physics 2D Settings

1. **Edit → Project Settings → Physics 2D**
2. **Configure settings**:
   - **Gravity**: X = 0, Y = -9.81
   - **Default Material**: None
   - **Velocity Iterations**: 8
   - **Position Iterations**: 3
   - **Velocity Threshold**: 1
   - **Max Linear Speed**: 1000
   - **Max Angular Speed**: 1000

---

## 🎯 Lab Tasks

### Task 1: Physics 2D System Setup

#### **1.1 Configure Physics Settings**

**Open Physics 2D Settings:**
1. **Edit → Project Settings → Physics 2D**
2. **Examine all settings** and understand their purpose
3. **Configure for 2D platformer**:
   - **Gravity**: X = 0, Y = -9.81
   - **Default Material**: None (for now)
   - **Velocity Iterations**: 8
   - **Position Iterations**: 3
   - **Velocity Threshold**: 1
   - **Max Linear Speed**: 1000
   - **Max Angular Speed**: 1000

**Create Physics Materials:**
1. **Right-click in Project** → **Create** → **Physics Material 2D**
2. **Name it** "BouncyMaterial"
3. **Configure properties**:
   - **Friction**: 0.1
   - **Bounciness**: 0.8
4. **Create more materials**:
   - **IceMaterial**: Friction = 0.05, Bounciness = 0.1
   - **StickyMaterial**: Friction = 0.9, Bounciness = 0.1

#### **1.2 Create Physics Objects**

**Create Player with Physics:**
1. **Right-click in Hierarchy** → **2D Object** → **Sprite** → **Square**
2. **Rename** to "Player"
3. **Add components**:
   - **Rigidbody2D**: Body Type = Dynamic
   - **BoxCollider2D**: Size = (1, 1, 1)
   - **Sprite Renderer**: Color = Blue
4. **Configure Rigidbody2D**:
   - **Mass**: 1
   - **Linear Drag**: 0
   - **Angular Drag**: 0.05
   - **Gravity Scale**: 1
   - **Freeze Rotation**: Z = true

**Create Ground Platform:**
1. **Right-click in Hierarchy** → **2D Object** → **Sprite** → **Square**
2. **Rename** to "Ground"
3. **Add components**:
   - **BoxCollider2D**: Size = (20, 1, 1)
   - **Sprite Renderer**: Color = Green
4. **Configure Transform**:
   - **Position**: (0, -4, 0)
   - **Scale**: (20, 1, 1)

**Create Moving Platform:**
1. **Right-click in Hierarchy** → **2D Object** → **Sprite** → **Square**
2. **Rename** to "MovingPlatform"
3. **Add components**:
   - **Rigidbody2D**: Body Type = Kinematic
   - **BoxCollider2D**: Size = (5, 1, 1)
   - **Sprite Renderer**: Color = Yellow
4. **Configure Transform**:
   - **Position**: (0, 0, 0)
   - **Scale**: (5, 1, 1)

### Task 2: Basic Physics Movement

#### **2.1 Create Physics-Based Player Controller**

**Create Player Controller Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "PhysicsPlayerController"
3. **Replace content** with:

```csharp
using UnityEngine;

public class PhysicsPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float jumpForce = 15f;
    public float maxSpeed = 8f;
    
    [Header("Physics Settings")]
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer = 1;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private float horizontalInput;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxis("Horizontal");
        
        // Jump input
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
        
        // Check if grounded
        CheckGrounded();
    }
    
    void FixedUpdate()
    {
        // Apply horizontal movement
        if (horizontalInput != 0)
        {
            rb.AddForce(Vector2.right * horizontalInput * moveSpeed);
        }
        
        // Limit horizontal speed
        if (Mathf.Abs(rb.velocity.x) > maxSpeed)
        {
            rb.velocity = new Vector2(Mathf.Sign(rb.velocity.x) * maxSpeed, rb.velocity.y);
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Debug.Log("Jump!");
    }
    
    void CheckGrounded()
    {
        // Raycast to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        // Visual debug
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}
```

**Test Basic Movement:**
1. **Attach script** to Player GameObject
2. **Click Play** and test movement
3. **Use A/D keys** for horizontal movement
4. **Use Space** for jumping
5. **Check Console** for debug messages

#### **2.2 Improve Movement with Physics Materials**

**Apply Materials to Objects:**
1. **Select Player** in Hierarchy
2. **In BoxCollider2D**, assign **BouncyMaterial**
3. **Test movement** and observe bouncy behavior
4. **Change to IceMaterial** and test sliding
5. **Change to StickyMaterial** and test friction

**Create Material Tester Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "MaterialTester"
3. **Replace content** with:

```csharp
using UnityEngine;

public class MaterialTester : MonoBehaviour
{
    [Header("Material Testing")]
    public PhysicsMaterial2D bouncyMaterial;
    public PhysicsMaterial2D iceMaterial;
    public PhysicsMaterial2D stickyMaterial;
    
    private BoxCollider2D boxCollider;
    
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    void Update()
    {
        // Switch materials with number keys
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            boxCollider.sharedMaterial = bouncyMaterial;
            Debug.Log("Switched to Bouncy Material");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            boxCollider.sharedMaterial = iceMaterial;
            Debug.Log("Switched to Ice Material");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            boxCollider.sharedMaterial = stickyMaterial;
            Debug.Log("Switched to Sticky Material");
        }
    }
}
```

**Test Material Effects:**
1. **Attach MaterialTester script** to Player
2. **Assign materials** in Inspector
3. **Test with number keys** (1, 2, 3)
4. **Observe different** physics behaviors

### Task 3: Advanced Jump Mechanics

#### **3.1 Implement Coyote Time**

**Create Advanced Jump Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "AdvancedJumpController"
3. **Replace content** with:

```csharp
using UnityEngine;

public class AdvancedJumpController : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 15f;
    public float jumpTime = 0.2f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;
    
    [Header("Physics Settings")]
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer = 1;
    
    private Rigidbody2D rb;
    private bool isGrounded;
    private bool wasGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    private float jumpTimeCounter;
    private bool isJumping;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        // Check if grounded
        CheckGrounded();
        
        // Handle coyote time
        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        
        // Handle jump buffer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }
        
        // Jump if conditions are met
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
        }
        
        // Variable jump height
        if (Input.GetKey(KeyCode.Space) && isJumping)
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
        
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        jumpTimeCounter = jumpTime;
        isJumping = true;
        coyoteTimeCounter = 0f;
        
        Debug.Log("Jump with coyote time!");
    }
    
    void CheckGrounded()
    {
        wasGrounded = isGrounded;
        
        // Raycast to check for ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        // Visual debug
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}
```

**Test Coyote Time:**
1. **Attach script** to Player
2. **Configure settings** in Inspector
3. **Test jumping** at platform edges
4. **Observe** coyote time behavior

#### **3.2 Implement Jump Buffering**

**Test Jump Buffering:**
1. **Press Space** before landing on platform
2. **Observe** jump happens automatically when landing
3. **Adjust jumpBufferTime** for different feel
4. **Test with different** timing scenarios

#### **3.3 Implement Variable Jump Height**

**Test Variable Jump:**
1. **Hold Space** for maximum jump height
2. **Release Space** early for shorter jump
3. **Observe** different jump heights
4. **Adjust jumpTime** for different feel

### Task 4: Collision Detection vs Triggers

#### **4.1 Create Collision Detection System**

**Create Collision Handler Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "CollisionHandler"
3. **Replace content** with:

```csharp
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [Header("Collision Settings")]
    public LayerMask enemyLayer = 2;
    public LayerMask collectibleLayer = 4;
    public LayerMask platformLayer = 1;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log($"Collision with: {collision.gameObject.name}");
        
        // Check if colliding with enemy
        if (IsInLayerMask(collision.gameObject.layer, enemyLayer))
        {
            Debug.Log("Hit enemy!");
            // Handle enemy collision
        }
        
        // Check if colliding with platform
        if (IsInLayerMask(collision.gameObject.layer, platformLayer))
        {
            Debug.Log("Landed on platform!");
            // Handle platform collision
        }
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        // Called every frame while colliding
        if (IsInLayerMask(collision.gameObject.layer, platformLayer))
        {
            // Handle continuous platform contact
        }
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log($"Stopped colliding with: {collision.gameObject.name}");
        
        if (IsInLayerMask(collision.gameObject.layer, platformLayer))
        {
            Debug.Log("Left platform!");
        }
    }
    
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
```

#### **4.2 Create Trigger Detection System**

**Create Trigger Handler Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "TriggerHandler"
3. **Replace content** with:

```csharp
using UnityEngine;

public class TriggerHandler : MonoBehaviour
{
    [Header("Trigger Settings")]
    public LayerMask collectibleLayer = 4;
    public LayerMask checkpointLayer = 8;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"Trigger entered: {other.gameObject.name}");
        
        // Check if triggered collectible
        if (IsInLayerMask(other.gameObject.layer, collectibleLayer))
        {
            Debug.Log("Collected item!");
            // Handle collectible
            Destroy(other.gameObject);
        }
        
        // Check if triggered checkpoint
        if (IsInLayerMask(other.gameObject.layer, checkpointLayer))
        {
            Debug.Log("Checkpoint reached!");
            // Handle checkpoint
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        // Called every frame while in trigger
        if (IsInLayerMask(other.gameObject.layer, collectibleLayer))
        {
            // Handle continuous trigger contact
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"Trigger exited: {other.gameObject.name}");
    }
    
    private bool IsInLayerMask(int layer, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << layer));
    }
}
```

#### **4.3 Test Collision vs Trigger**

**Create Test Objects:**
1. **Create Enemy** with Collider2D (not trigger)
2. **Create Collectible** with Collider2D (is trigger)
3. **Create Checkpoint** with Collider2D (is trigger)
4. **Attach scripts** to Player
5. **Test collision** and trigger events

### Task 5: Physics Performance Optimization

#### **5.1 Configure Collision Matrix**

**Set Up Layers:**
1. **Edit → Project Settings → Tags and Layers**
2. **Create layers**:
   - **Default**: 0
   - **Ground**: 1
   - **Enemy**: 2
   - **Player**: 3
   - **Collectible**: 4
   - **Checkpoint**: 8

**Configure Collision Matrix:**
1. **Edit → Project Settings → Physics 2D**
2. **Click "Layer Collision Matrix"**
3. **Configure collisions**:
   - **Player vs Ground**: ✓
   - **Player vs Enemy**: ✓
   - **Player vs Collectible**: ✓
   - **Enemy vs Ground**: ✓
   - **Enemy vs Enemy**: ✗
   - **Collectible vs Ground**: ✗

#### **5.2 Optimize Physics Settings**

**Configure Physics 2D Settings:**
1. **Edit → Project Settings → Physics 2D**
2. **Optimize settings**:
   - **Velocity Iterations**: 6 (reduced from 8)
   - **Position Iterations**: 2 (reduced from 3)
   - **Velocity Threshold**: 1
   - **Max Linear Speed**: 1000
   - **Max Angular Speed**: 1000

**Create Performance Monitor Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "PhysicsPerformanceMonitor"
3. **Replace content** with:

```csharp
using UnityEngine;

public class PhysicsPerformanceMonitor : MonoBehaviour
{
    [Header("Performance Monitoring")]
    public bool showFPS = true;
    public bool showPhysicsInfo = true;
    
    private float deltaTime = 0.0f;
    private int frameCount = 0;
    private float fps;
    
    void Update()
    {
        // Calculate FPS
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        fps = 1.0f / deltaTime;
        
        frameCount++;
        
        // Display FPS
        if (showFPS && frameCount % 60 == 0)
        {
            Debug.Log($"FPS: {fps:F1}");
        }
        
        // Display physics info
        if (showPhysicsInfo && frameCount % 60 == 0)
        {
            int rigidbodyCount = FindObjectsOfType<Rigidbody2D>().Length;
            int colliderCount = FindObjectsOfType<Collider2D>().Length;
            
            Debug.Log($"Rigidbodies: {rigidbodyCount}, Colliders: {colliderCount}");
        }
    }
    
    void OnGUI()
    {
        if (showFPS)
        {
            GUI.Label(new Rect(10, 10, 100, 20), $"FPS: {fps:F1}");
        }
    }
}
```

#### **5.3 Test Performance**

**Monitor Performance:**
1. **Attach PhysicsPerformanceMonitor** to any GameObject
2. **Run game** and observe FPS
3. **Check Console** for physics info
4. **Adjust settings** if performance is poor

### Task 6: Advanced Physics Features

#### **6.1 Create Moving Platform**

**Create Moving Platform Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "MovingPlatform"
3. **Replace content** with:

```csharp
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform[] waypoints;
    public float moveSpeed = 2f;
    public float waitTime = 1f;
    
    private int currentWaypoint = 0;
    private float waitCounter = 0f;
    private bool isWaiting = false;
    
    void Start()
    {
        if (waypoints.Length == 0)
        {
            Debug.LogError("No waypoints assigned to MovingPlatform!");
        }
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
                    else if (i == waypoints.Length - 1 && waypoints[0] != null)
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

**Setup Moving Platform:**
1. **Create waypoints** (empty GameObjects)
2. **Position waypoints** at different locations
3. **Attach MovingPlatform script** to platform
4. **Assign waypoints** in Inspector
5. **Test platform movement**

#### **6.2 Create One-Way Platform**

**Create One-Way Platform Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "OneWayPlatform"
3. **Replace content** with:

```csharp
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [Header("One-Way Platform Settings")]
    public float disableTime = 0.5f;
    
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
        if (collision.gameObject.CompareTag("Player"))
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
}
```

**Test One-Way Platform:**
1. **Create platform** with OneWayPlatform script
2. **Test jumping** onto platform from above
3. **Test jumping** through platform from below
4. **Observe** one-way behavior

---

## ✅ Completion Checklist

### **Physics 2D System Setup**
- [ ] **Configured Physics 2D settings** for optimal 2D gameplay
- [ ] **Created physics materials** with different properties
- [ ] **Set up physics objects** with proper components
- [ ] **Tested basic physics** behavior

### **Physics-Based Movement**
- [ ] **Implemented physics-based** player controller
- [ ] **Applied physics materials** to objects
- [ ] **Tested material effects** on movement
- [ ] **Optimized movement** for responsiveness

### **Advanced Jump Mechanics**
- [ ] **Implemented coyote time** for edge jumping
- [ ] **Added jump buffering** for responsive jumping
- [ ] **Created variable jump height** system
- [ ] **Tested advanced jump** mechanics

### **Collision Detection**
- [ ] **Set up collision detection** system
- [ ] **Implemented trigger detection** for collectibles
- [ ] **Configured collision matrix** for performance
- [ ] **Tested collision vs trigger** events

### **Performance Optimization**
- [ ] **Optimized physics settings** for performance
- [ ] **Configured collision matrix** to reduce unnecessary collisions
- [ ] **Monitored performance** with profiling tools
- [ ] **Tested performance** under load

### **Advanced Physics Features**
- [ ] **Created moving platform** system
- [ ] **Implemented one-way platform** behavior
- [ ] **Tested advanced physics** features
- [ ] **Optimized physics** for gameplay

---

## 🚨 Troubleshooting

### **Common Issues and Solutions**

#### **Physics not working correctly**
**Possible causes:**
- Incorrect physics settings
- Missing Rigidbody2D components
- Wrong collider setup

**Solutions:**
1. Check Physics 2D settings in Project Settings
2. Verify all objects have required components
3. Ensure colliders are properly configured
4. Test with simple physics objects first

#### **Jump mechanics feel unresponsive**
**Possible causes:**
- Input timing issues
- Physics settings too slow
- Jump force too low

**Solutions:**
1. Adjust jump force and timing
2. Implement coyote time and jump buffering
3. Test with different physics settings
4. Use FixedUpdate for physics calculations

#### **Performance issues**
**Possible causes:**
- Too many physics objects
- Inefficient collision matrix
- High physics iterations

**Solutions:**
1. Reduce physics iterations
2. Optimize collision matrix
3. Use static colliders where possible
4. Monitor performance with profiler

#### **Collision detection not working**
**Possible causes:**
- Wrong layer setup
- Collision matrix misconfigured
- Missing collision events

**Solutions:**
1. Check layer assignments
2. Verify collision matrix settings
3. Ensure collision events are implemented
4. Test with simple collision scenarios

---

## 📝 Self-Assessment

After completing this lab, test your understanding with the interactive quiz:

**Take the Quiz**: [`quiz/quiz3.html`](../quiz/quiz3.html)

The quiz will help you:
- Verify your understanding of key concepts
- Identify areas that need review
- Prepare for the next lesson

---

## 📚 Next Steps

### **Immediate Next Steps**
1. **Complete all tasks** in this lab
2. **Test your physics system** thoroughly
3. **Experiment with different** physics materials
4. **Practice with advanced** jump mechanics

### **Prepare for Next Lesson**
1. **Review physics concepts** and implementation
2. **Understand collision detection** vs triggers
3. **Practice with physics** optimization
4. **Read Lesson 4 materials** in advance

### **Further Learning**
1. **Unity Learn tutorials** for additional physics practice
2. **Unity documentation** for deeper understanding
3. **Community forums** for questions and help
4. **Practice projects** to reinforce learning

---

## 💡 Pro Tips

### **Physics Best Practices**
- **Use appropriate physics materials** for different surfaces
- **Implement coyote time** for better jump feel
- **Optimize collision matrix** for performance
- **Test physics** with different frame rates

### **Performance Tips**
- **Use static colliders** for immovable objects
- **Limit physics iterations** for better performance
- **Monitor performance** regularly with profiler
- **Optimize collision matrix** to reduce unnecessary collisions

### **Gameplay Tips**
- **Tune physics values** for responsive gameplay
- **Test with different** input devices
- **Implement feedback** for physics interactions
- **Balance realism** with fun gameplay

---

