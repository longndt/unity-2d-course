# Theory: 2D Physics & Collision Detection

## 🎯 Learning Objectives

After completing this lesson, students will be able to:
- Understand Unity 2D Physics System and how it works
- Use Rigidbody2D to create realistic physics behavior
- Configure various types of Collider2D for different game objects
- Create and apply Physics Materials 2D
- Distinguish between Collision and Trigger events
- Implement platformer physics with jump mechanics

---

## 1. Unity 2D Physics System Overview

### 1.1 Physics Engine Foundation

Unity 2D uses **Box2D physics engine** - one of the most popular physics engines for 2D games:

#### Key Features:
- ✅ **Realistic physics simulation**: Gravity, friction, collision response
- ✅ **High performance**: Optimized for real-time games
- ✅ **Stable simulation**: Consistent behavior across different framerates
- ✅ **Flexible**: Support many types of physics objects

#### Physics Simulation Loop:
```
1. Input Processing → 2. Physics Update → 3. Collision Detection → 4. Collision Response → 5. Render
```

### 1.2 Physics vs Kinematic vs Static

#### **Dynamic Physics Objects** (Rigidbody2D):
- 📦 **Affected by gravity** and forces
- 💥 **Realistic collision response**
- 🎯 **Use for**: Player characters, enemies, projectiles, physics props

#### **Kinematic Objects** (Rigidbody2D.isKinematic = true):
- 🎮 **Controlled by scripts**, not physics
- 💪 **Can push other objects** but not pushed back
- 🎯 **Use for**: Moving platforms, doors, scripted movers

#### **Static Objects** (No Rigidbody2D, only Collider):
- 🏢 **Completely stationary**
- ⚡ **Most performant** collision detection
- 🎯 **Use for**: Ground, walls, static environment

---

## 2. Rigidbody2D Component Deep Dive

### 2.1 Core Properties

#### **Body Type**:
- **Dynamic**: Full physics simulation
- **Kinematic**: Script-controlled movement
- **Static**: No movement (automatically set when no Rigidbody2D)

#### **Material**: Physics Material 2D reference

#### **Simulated**: Enable/disable physics simulation

#### **Use Auto Mass**: Automatically calculate mass from collider size

### 2.2 Mass and Density

#### Mass Calculation:
```csharp
// Auto Mass enabled:
Mass = Density × Collider Area

// Example:
Density = 1.0
Box Collider Area = 2×2 = 4 units²
Calculated Mass = 1.0 × 4 = 4 kg
```

#### Mass Effects:
- **Higher mass**: Harder to move, more momentum
- **Lower mass**: Easier to move, less momentum
- **Mass ratio**: Affects collision outcomes

```csharp
// Code example
Rigidbody2D rb = GetComponent<Rigidbody2D>();
rb.mass = 2.0f; // Set custom mass
```

### 2.3 Gravity Settings

#### **Gravity Scale**:
- `1.0`: Normal gravity
- `0.0`: No gravity (floating)
- `2.0`: Double gravity (faster falling)
- `-1.0`: Reverse gravity (floating up)

#### **Linear Drag**: Air resistance for movement
#### **Angular Drag**: Rotational resistance

```csharp
// Gravity modifications
rb.gravityScale = 1.5f; // 50% stronger gravity
rb.drag = 0.5f;         // Light air resistance
rb.angularDrag = 0.05f; // Rotational damping
```

### 2.4 Constraints

#### **Freeze Position**: Lock movement on specific axes
- **X**: Prevent horizontal movement
- **Y**: Prevent vertical movement

#### **Freeze Rotation**: Lock rotation
- **Z**: Prevent spinning (common for 2D characters)

```csharp
// Lock Z rotation for 2D character
rb.freezeRotation = true;
// Or specifically:
rb.constraints = RigidbodyConstraints2D.FreezeRotation;
```

---

## 3. 2D Collider Types and Usage

### 3.1 Box Collider 2D

#### **Best Use Cases**:
- 📦 **Rectangular objects**: Platforms, walls, boxes
- 🏃 **Character collision**: Simple character bounds
- 🎯 **UI elements**: Buttons, clickable areas

#### **Properties**:
- **Size**: Width and height of collision box
- **Offset**: Position relative to GameObject center
- **Is Trigger**: Toggle collision/trigger mode

```csharp
BoxCollider2D boxCol = GetComponent<BoxCollider2D>();
boxCol.size = new Vector2(2f, 1f);      // 2×1 collision box
boxCol.offset = new Vector2(0f, 0.5f);   // Offset up by 0.5 units
```

### 3.2 Circle Collider 2D

#### **Best Use Cases**:
- ⚪ **Circular objects**: Balls, coins, wheels
- 🎯 **Projectiles**: Bullets, magic orbs
- 📍 **Range detection**: Attack ranges, sensor areas

#### **Properties**:
- **Radius**: Size of collision circle
- **Offset**: Center position relative to GameObject

```csharp
CircleCollider2D circleCol = GetComponent<CircleCollider2D>();
circleCol.radius = 1.5f;                 // 1.5 unit radius
circleCol.offset = new Vector2(0f, 1f);  // Offset center up
```

### 3.3 Polygon Collider 2D

#### **Best Use Cases**:
- 🎨 **Complex shapes**: Irregular terrain, detailed character bounds
- 🗺️ **Sprite-based collision**: Auto-generated from sprite outline
- 🎯 **Precise collision**: When exact shape matching is needed

#### **Properties**:
- **Points**: Array of vertices defining shape
- **Auto-generation**: Can create from sprite transparency

```csharp
PolygonCollider2D polyCol = GetComponent<PolygonCollider2D>();
// Points are automatically generated from sprite
// Can be manually edited for optimization
```

### 3.4 Edge Collider 2D

#### **Best Use Cases**:
- 📏 **One-way platforms**: Jump-through platforms
- 🌊 **Terrain edges**: Hills, cliffs, irregular ground
- 🎯 **Boundaries**: Invisible walls, level bounds

```csharp
EdgeCollider2D edgeCol = GetComponent<EdgeCollider2D>();
Vector2[] points = { new Vector2(-2, 0), new Vector2(0, 1), new Vector2(2, 0) };
edgeCol.points = points; // Define edge shape
```

### 3.5 Composite Collider 2D

#### **Purpose**: Combine multiple colliders into single efficient collider

#### **Benefits**:
- ✅ **Performance**: Reduced collision checks
- ✅ **Complex shapes**: Multiple primitives combined
- ✅ **Optimization**: Better for complex terrain

---

## 4. Physics Materials 2D

### 4.1 Material Properties

#### **Friction** (0.0 - 1.0):
- `0.0`: No friction (ice-like, sliding)
- `0.4`: Normal friction (wood, stone)
- `1.0`: High friction (rubber, sandpaper)

#### **Bounciness** (0.0 - 1.0):
- `0.0`: No bounce (soft landing)
- `0.5`: Medium bounce (basketball)
- `1.0`: Perfect bounce (super ball)

### 4.2 Creating Physics Materials

```csharp
// Create Physics Material 2D asset
// Assets → Create → 2D → Physics Material 2D

// Example configurations:

// Ice Material
Friction: 0.01
Bounciness: 0.1

// Bouncy Material
Friction: 0.4
Bounciness: 0.9

// Sticky Material
Friction: 1.0
Bounciness: 0.0
```

### 4.3 Material Assignment

```csharp
// Assign to Rigidbody2D
Rigidbody2D rb = GetComponent<Rigidbody2D>();
rb.sharedMaterial = iceMaterial;

// Assign to Collider2D
BoxCollider2D collider = GetComponent<BoxCollider2D>();
collider.sharedMaterial = bouncyMaterial;
```

### 4.4 Common Material Presets

#### **Player Character**:
```
Friction: 0.4 (good ground control)
Bounciness: 0.0 (no unwanted bouncing)
```

#### **Moving Platform**:
```
Friction: 0.6 (players stick to platform)
Bounciness: 0.0 (stable platform)
```

#### **Bouncy Enemy**:
```
Friction: 0.2 (slides a bit)
Bounciness: 0.8 (bounces on hit)
```

---

## 5. Collision Detection vs Triggers

### 5.1 Collision (Solid Objects)

#### **Characteristics**:
- 💥 **Physical response**: Objects stop/bounce
- ⚡ **Momentum transfer**: Mass affects outcome
- 🎯 **Realistic behavior**: Objects can't pass through

#### **Event Methods**:
```csharp
void OnCollisionEnter2D(Collision2D collision)
{
    // Called when collision starts
    Debug.Log("Hit: " + collision.gameObject.name);

    // Access collision details
    ContactPoint2D contact = collision.contacts[0];
    Vector2 hitPoint = contact.point;
    Vector2 hitNormal = contact.normal;
}

void OnCollisionStay2D(Collision2D collision)
{
    // Called every frame while colliding
}

void OnCollisionExit2D(Collision2D collision)
{
    // Called when collision ends
}
```

#### **Use Cases**:
- 🏢 **Ground/Walls**: Physical barriers
- 📦 **Pushable objects**: Boxes, boulders
- 💥 **Physics interactions**: Realistic responses

### 5.2 Triggers (Ghost Objects)

#### **Characteristics**:
- 👻 **No physical response**: Objects pass through
- 📡 **Detection only**: Events fire but no collision
- 🎯 **Logic-based**: Custom scripted responses

#### **Event Methods**:
```csharp
void OnTriggerEnter2D(Collider2D other)
{
    // Called when object enters trigger
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player entered area");
    }
}

void OnTriggerStay2D(Collider2D other)
{
    // Called every frame while inside trigger
}

void OnTriggerExit2D(Collider2D other)
{
    // Called when object exits trigger
}
```

#### **Use Cases**:
- 🪙 **Collectibles**: Coins, power-ups, items
- 🚪 **Area triggers**: Level transitions, cutscenes
- 💀 **Damage zones**: Spikes, lava, poison areas
- 📍 **Sensors**: AI detection, checkpoint systems

---

## 6. Forces and Movement

### 6.1 AddForce Methods

#### **ForceMode2D Options**:

**Force**: Continuous force affected by mass
```csharp
rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
// Realistic physics, mass matters
```

**Impulse**: Instant force affected by mass
```csharp
rb.AddForce(Vector2.right * pushForce, ForceMode2D.Impulse);
// One-time push, like explosion
```

### 6.2 Velocity Control

#### **Direct Velocity Assignment**:
```csharp
// Set horizontal movement, preserve vertical
Vector2 newVelocity = new Vector2(moveSpeed * input, rb.velocity.y);
rb.velocity = newVelocity;
```

#### **Velocity Clamping**:
```csharp
// Limit maximum speed
if (rb.velocity.magnitude > maxSpeed)
{
    rb.velocity = rb.velocity.normalized * maxSpeed;
}
```

### 6.3 Jump Mechanics Implementation

#### **Basic Jump**:
```csharp
public class PlayerJump : MonoBehaviour
{
    [Header("Jump Settings")]
    public float jumpForce = 400f;
    public LayerMask groundLayer = 1;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        CheckGrounded();

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        // Reset vertical velocity before jump
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        // Apply jump force
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void CheckGrounded()
    {
        // Raycast down to check ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.6f, groundLayer);
        isGrounded = hit.collider != null;
    }
}
```

---

## 7. Advanced Physics Concepts

### 7.1 Variable Jump Height

```csharp
public class VariableJump : MonoBehaviour
{
    public float jumpForce = 400f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;

    void Update()
    {
        // Apply variable gravity for better jump feel
        if (rb.velocity.y < 0)
        {
            // Falling: increase gravity
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            // Released jump early: cut jump short
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
```

### 7.2 Coyote Time Implementation

```csharp
public class CoyoteTime : MonoBehaviour
{
    public float coyoteTimeThreshold = 0.1f;

    private bool isGrounded;
    private float lastGroundedTime;

    void Update()
    {
        // Update grounded status
        bool wasGrounded = isGrounded;
        CheckGrounded();

        // Start coyote time when leaving ground
        if (wasGrounded && !isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        // Allow jump during coyote time
        bool canJump = isGrounded || (Time.time - lastGroundedTime < coyoteTimeThreshold);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }
    }
}
```

### 7.3 Jump Buffering

```csharp
public class JumpBuffer : MonoBehaviour
{
    public float jumpBufferTime = 0.1f;

    private float jumpBufferCounter;
    private bool isGrounded;

    void Update()
    {
        // Handle jump input buffering
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // Execute jump if conditions met
        if (jumpBufferCounter > 0f && isGrounded)
        {
            Jump();
            jumpBufferCounter = 0f; // Reset buffer
        }
    }
}
```

---

## 8. Physics Performance Optimization

### 8.1 Physics Settings

#### **Physics 2D Settings** (Edit → Project Settings → Physics 2D):

**Gravity**: Default (-9.81, 0) - adjust for game feel
**Velocity Iterations**: 8 (collision accuracy)
**Position Iterations**: 3 (overlap resolution)
**Velocity Threshold**: 1 (sleep threshold)
**Max Linear Correction**: 0.2 (position correction)
**Max Angular Correction**: 8 (rotation correction)

### 8.2 Collision Matrix

#### **Layer Collision Matrix**:
```
Configure which layers can collide:
- Player vs Ground: ✅ Enabled
- Player vs Player: ❌ Disabled
- Enemy vs Enemy: ❌ Disabled
- Projectile vs Ground: ✅ Enabled
```

### 8.3 Performance Best Practices

#### **Collider Optimization**:
- ✅ **Use simple shapes**: Box/Circle over Polygon when possible
- ✅ **Composite Colliders**: Combine multiple static colliders
- ✅ **Appropriate triggers**: Use triggers for detection, not physics
- ✅ **Collision layers**: Limit collision checks with layer matrix

#### **Rigidbody Optimization**:
- ✅ **Sleep inactive objects**: Let physics system sleep idle objects
- ✅ **Kinematic when appropriate**: Use kinematic for scripted movement
- ✅ **Appropriate mass**: Avoid extreme mass differences
- ✅ **Continuous collision**: Only when necessary for fast objects

---

## 9. Common Physics Patterns

### 9.1 One-Way Platforms

```csharp
public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D platformEffector;

    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        // Allow dropping through platform
        if (Input.GetKey(KeyCode.S))
        {
            platformEffector.rotationalOffset = 180f;
        }
        else
        {
            platformEffector.rotationalOffset = 0f;
        }
    }
}
```

### 9.2 Moving Platforms

```csharp
public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;

    private int currentWaypoint = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // Kinematic for scripted movement
    }

    void FixedUpdate()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        Vector2 targetPosition = waypoints[currentWaypoint].position;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Switch waypoints when reached
        if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    // Carry player with platform
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
```

### 9.3 Physics-Based Projectiles

```csharp
public class Projectile : MonoBehaviour
{
    public float lifetime = 5f;
    public float damage = 10f;

    void Start()
    {
        // Auto-destroy after lifetime
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Handle projectile hit
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Deal damage
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            enemy?.TakeDamage(damage);
        }

        // Destroy projectile on hit
        Destroy(gameObject);
    }
}
```

---

## 10. Physics Debugging Tools

### 10.1 Visual Debugging

#### **Physics Debugger** (Window → Analysis → Physics Debugger):
- 🔍 **Collision visualization**: See collision shapes
- 📊 **Performance stats**: Physics performance metrics
- 🎯 **Contact points**: Visualize collision contacts

#### **Gizmos trong Scene View**:
```csharp
void OnDrawGizmos()
{
    // Visualize ground check
    Gizmos.color = isGrounded ? Color.green : Color.red;
    Gizmos.DrawWireSphere(transform.position, 0.5f);

    // Draw raycast for ground detection
    Gizmos.color = Color.blue;
    Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 0.6f);
}
```

### 10.2 Console Debugging

```csharp
void OnCollisionEnter2D(Collision2D collision)
{
    Debug.Log($"Collision with {collision.gameObject.name}");
    Debug.Log($"Collision force: {collision.relativeVelocity.magnitude}");
    Debug.Log($"Contact point: {collision.contacts[0].point}");
}
```

### 10.3 Physics Profiling

#### **Unity Profiler** (Window → Analysis → Profiler):
- ⚡ **Physics.Processing**: Time spent in physics simulation
- 💥 **Physics.Contacts**: Number of collision contacts
- 🔄 **Physics.Queries**: Raycast and overlap queries

---

## Chapter Summary

### Core Knowledge:
1. ✅ **Physics System**: Unity 2D physics foundation and Box2D engine
2. ✅ **Rigidbody2D**: Mass, gravity, constraints, and physics properties
3. ✅ **Collider Types**: Box, Circle, Polygon, Edge colliders and use cases
4. ✅ **Physics Materials**: Friction, bounciness, and material interactions
5. ✅ **Collision vs Triggers**: Physical response vs detection events
6. ✅ **Forces and Movement**: AddForce, velocity control, jump mechanics

### Advanced Techniques:
- 🎮 **Variable Jump Height**: Enhanced jump feel with gravity modulation
- ⏰ **Coyote Time**: Grace period for jump input after leaving ground
- 🔄 **Jump Buffering**: Input buffering for responsive controls
- 🏗️ **Moving Platforms**: Kinematic movement with player interaction

### Performance Optimization:
- ⚡ **Physics Settings**: Optimal configuration for 2D games
- 🎯 **Collision Matrix**: Efficient layer-based collision filtering
- 🔧 **Best Practices**: Collider optimization and appropriate physics usage

### Preparing for Next Lesson:
- 🎮 **Advanced Input System**: Unity's New Input System (you already know basic input from Lesson 1)
- 🎬 **Advanced Movement**: Character controllers with state management
- 📹 **Professional Camera Systems**: Cinemachine integration (you already know basic camera follow from Lesson 1)
- 🎯 **Game Feel**: Polish and juice for better player experience

### Practice:
Complete **Lab 03** to build 2D platformer with realistic physics, jump mechanics, and collision detection system.

---

## ✅ What's Next

Proceed to [Lesson 4: Input & Player Controller](../lesson4-input-player-controller/) to learn about the New Input System and build a professional 2D player controller with responsive camera follow. You already know basic input and camera follow from Lesson 1, so this lesson will build on that foundation.