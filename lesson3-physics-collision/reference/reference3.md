# Lesson 3 Reference - Physics & Collision

## Rigidbody2D Settings

### Body Type
- **Dynamic**: Affected by forces and collisions
- **Kinematic**: Moved by code, not affected by forces
- **Static**: Immovable, optimized for static objects

### Physics Properties
```csharp
Rigidbody2D rb = GetComponent<Rigidbody2D>();

rb.gravityScale = 1f;        // 0 = no gravity, 1 = normal
rb.drag = 0f;                // Air resistance
rb.angularDrag = 0.05f;      // Rotation resistance
rb.mass = 1f;                // Affects collision response
rb.freezeRotation = true;    // Prevent rotation
```

## Collider Types

### 2D Colliders
- **BoxCollider2D**: Rectangular shapes
- **CircleCollider2D**: Circular shapes
- **PolygonCollider2D**: Custom shapes
- **EdgeCollider2D**: One-sided walls

### Collider Settings
```csharp
Collider2D col = GetComponent<Collider2D>();

col.isTrigger = true;        // Trigger vs Collision
col.offset = Vector2.zero;   // Offset from center
col.size = Vector2.one;      // Size (BoxCollider2D)
col.radius = 0.5f;          // Radius (CircleCollider2D)
```

## Physics Materials

### Physics Material 2D
- **Friction**: 0-1 (0 = no friction, 1 = max friction)
- **Bounciness**: 0-1 (0 = no bounce, 1 = perfect bounce)
- **Friction Combine**: Average, Minimum, Maximum, Multiply
- **Bounce Combine**: Average, Minimum, Maximum, Multiply

## Collision Detection

### FixedUpdate vs Update
```csharp
void FixedUpdate() {
    // Physics calculations
    // Rigidbody2D movement
    // Collision detection
}

void Update() {
    // Input handling
    // UI updates
    // Non-physics logic
}
```

### Collision Callbacks
```csharp
void OnCollisionEnter2D(Collision2D collision) {
    // First contact
}

void OnCollisionStay2D(Collision2D collision) {
    // Continuous contact
}

void OnCollisionExit2D(Collision2D collision) {
    // Contact ended
}

void OnTriggerEnter2D(Collider2D other) {
    // Trigger entered
}

void OnTriggerStay2D(Collider2D other) {
    // Inside trigger
}

void OnTriggerExit2D(Collider2D other) {
    // Left trigger
}
```

## Ground Detection

### Raycast Method
```csharp
bool IsGrounded() {
    Vector2 rayOrigin = transform.position;
    float rayDistance = 0.1f;
    LayerMask groundLayer = LayerMask.GetMask("Ground");

    RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, rayDistance, groundLayer);
    return hit.collider != null;
}
```

### OverlapCircle Method
```csharp
bool IsGrounded() {
    Vector2 circleCenter = transform.position;
    float radius = 0.5f;
    LayerMask groundLayer = LayerMask.GetMask("Ground");

    return Physics2D.OverlapCircle(circleCenter, radius, groundLayer);
}
```

## Jump Mechanics

### Basic Jump
```csharp
private Rigidbody2D rb;

void Start() {
    rb = GetComponent<Rigidbody2D>();
}

void Jump() {
    if (IsGrounded()) {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
```

### Variable Jump Height
```csharp
void Update() {
    if (Input.GetButton("Jump") && rb.velocity.y > 0) {
        rb.velocity += Vector2.up * jumpForce * Time.deltaTime;
    }
}
```

### Coyote Time
```csharp
float coyoteTime = 0.2f;
float lastGroundedTime;

void Update() {
    if (IsGrounded()) {
        lastGroundedTime = Time.time;
    }

    if (Input.GetButtonDown("Jump") && Time.time - lastGroundedTime < coyoteTime) {
        Jump();
    }
}
```

## Layer Collision Matrix

### Setup
1. **Edit → Project Settings → Physics 2D**
2. **Layer Collision Matrix**: Configure which layers collide
3. **Layer Names**: Ground, Player, Enemy, Collectible, Trigger

### Common Layer Setup
- **Ground**: Collides with Player, Enemy
- **Player**: Collides with Ground, Enemy
- **Enemy**: Collides with Ground, Player
- **Collectible**: Trigger only (no collision)
- **Trigger**: Trigger only (no collision)

## Performance Tips

- Use **FixedUpdate** for physics
- Set **Fixed Timestep** to 0.02 (50 FPS) for stable physics
- Use **Layer Collision Matrix** to prevent unnecessary checks
- Prefer **CircleCollider2D** over **PolygonCollider2D** when possible
- Use **Rigidbody2D.Sleep()** for inactive objects
