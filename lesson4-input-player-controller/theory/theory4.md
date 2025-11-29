# Theory: Input System & Player Controller

## 🎯 Learning Objectives

After completing this lesson, students will be able to:
- Understand and use Unity's New Input System
- Create responsive 2D character controllers
- Implement advanced jump mechanics (coyote time, jump buffering)
- Setup 2D camera follow system with Cinemachine
- Integrate animations with player movement
- Optimize input handling for better game feel

---

## 1. Unity Input System Overview

This lesson covers Unity's **New Input System** with device-agnostic input, rebinding, and event-driven architecture.

### 1.1 Old vs New Input System

#### **Legacy Input Manager** (from Lesson 1):
```csharp
if (Input.GetKeyDown(KeyCode.Space))
{
    Jump();
}
float horizontal = Input.GetAxis("Horizontal");
```

**Limitations:**
- Hard-coded inputs, limited device support
- Polling-based system, no rebinding

#### **New Input System**:
```csharp
public InputAction jumpAction;
public InputAction moveAction;

void OnEnable()
{
    jumpAction.performed += Jump;
    jumpAction.Enable();
}
```

**Advantages:**
- Event-driven, device agnostic
- Customizable controls, modern features (touch, gyroscope)

### 1.2 Input System Installation

#### **Package Manager Setup**:
1. **Window → Package Manager**
2. **Unity Registry → Input System**
3. **Install** package
4. **Restart Unity** when prompted

#### **Project Configuration**:
```
Edit → Project Settings → Player → Configuration
Active Input Handling: Input System Package (New)
```

---

## 2. Input Actions and Action Maps

### 2.1 Input Action Assets

#### **Creating Input Action Asset**:
```
Assets → Create → Input Actions
Name: "PlayerInputActions"
```

#### **Action Maps Structure**:
```
PlayerInputActions
├── Gameplay (Action Map)
│   ├── Move (Action)
│   ├── Jump (Action)
│   ├── Attack (Action)
│   └── Interact (Action)
├── UI (Action Map)
│   ├── Navigate (Action)
│   ├── Submit (Action)
│   └── Cancel (Action)
└── Menu (Action Map)
    ├── Pause (Action)
    └── Settings (Action)
```

### 2.2 Action Types

#### **Value Actions** (Continuous input):
- **Move**: Vector2 for movement direction
- **Look**: Vector2 for camera control
- **Throttle**: Float for analog input

#### **Button Actions** (Discrete input):
- **Jump**: Press/Release events
- **Attack**: Single press actions
- **Interact**: Context-sensitive actions

#### **Pass Through Actions** (Raw input):
- **Mouse Position**: Exact cursor coordinates
- **Touch**: Raw touch data

### 2.3 Input Bindings

#### **Keyboard Bindings**:
```
Move Action:
├── WASD (2D Vector Composite)
│   ├── Up: W
│   ├── Down: S
│   ├── Left: A
│   └── Right: D
└── Arrow Keys (2D Vector Composite)
    ├── Up: Up Arrow
    ├── Down: Down Arrow
    ├── Left: Left Arrow
    └── Right: Right Arrow
```

#### **Gamepad Bindings**:
```
Move Action: Left Stick
Jump Action: South Button (A/X)
Attack Action: East Button (B/Circle)
```

---

## 3. Player Input Component

### 3.1 Setup Player Input Component

```csharp
// Add Player Input component to player GameObject
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        // Get references to actions
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    void OnEnable()
    {
        // Subscribe to action events
        jumpAction.performed += OnJump;
        jumpAction.canceled += OnJumpCanceled;
    }

    void OnDisable()
    {
        // Unsubscribe from action events
        jumpAction.performed -= OnJump;
        jumpAction.canceled -= OnJumpCanceled;
    }
}
```

### 3.2 Input Event Methods

#### **Auto-Generated Methods**:
```csharp
public void OnMove(InputValue value)
{
    Vector2 moveInput = value.Get<Vector2>();
}

public void OnJump(InputValue value)
{
    if (value.isPressed)
    {
        Jump();
    }
}

public void OnAttack()
{
    Attack();
}
```

#### **Manual Event Subscription**:
```csharp
void OnEnable()
{
    moveAction.performed += OnMovePerformed;
    moveAction.canceled += OnMoveCanceled;

    jumpAction.started += OnJumpStarted;      // Key down
    jumpAction.performed += OnJumpPerformed;  // Key pressed
    jumpAction.canceled += OnJumpCanceled;    // Key up
}

void OnMovePerformed(InputAction.CallbackContext context)
{
    Vector2 input = context.ReadValue<Vector2>();
}

void OnJumpPerformed(InputAction.CallbackContext context)
{
    Jump();
}

void OnJumpCanceled(InputAction.CallbackContext context)
{
    ReleaseJump();
}
```

---

## 4. 2D Character Controller Implementation

Core flow of a 2D character controller:

1. Read input (Move / Jump)
2. Store in variables (`moveInput`, `jumpPressed`)
3. In `FixedUpdate`, apply forces/velocity to `Rigidbody2D`
4. Use state variables (ground check, coyote time, jump buffer)

### 4.1 Controller skeleton

Controller example:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class SimplePlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 8f;

    [Header("Jump")]
    public float jumpForce = 12f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayers;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        jumpPressed = value.isPressed;
    }

    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundCheckRadius, groundLayers);
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressed = false;
        }
    }
}
```


### 4.2 Coyote time & jump buffer

- **Coyote time**: Allow jumping shortly after leaving the ground (store `lastGroundedTime`)
- **Jump buffer**: Store jump input when pressed early; execute if player lands within the buffer window

Pseudocode:

```csharp
// Update
if (isGrounded)
    lastGroundedTime = Time.time;

if (jumpPressed)
    jumpBufferCounter = jumpBufferTime;

// FixedUpdate
bool canJumpByCoyote = Time.time - lastGroundedTime <= coyoteTime;

if (jumpBufferCounter > 0 && canJumpByCoyote)
{
    DoJump();
    jumpBufferCounter = 0;
}
```

Full implementation: `lesson4-input-player-controller/example/Player2DController.cs` and sample project.

---

## 5. Animation Integration

### 5.1 Animator Controller Setup

#### **Animator States**:
```
Player Animator Controller:
├── Idle (Default)
├── Run
├── Jump
├── Fall
└── Land
```

#### **Animator Parameters**:
```csharp
public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private Player2DController controller;

    private int speedParamID;
    private int groundedParamID;
    private int velocityYParamID;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<Player2DController>();

        speedParamID = Animator.StringToHash("Speed");
        groundedParamID = Animator.StringToHash("IsGrounded");
        velocityYParamID = Animator.StringToHash("VelocityY");
    }

    void Update()
    {
        UpdateAnimationParameters();
    }

    void UpdateAnimationParameters()
    {
        float speed = Mathf.Abs(controller.rb.velocity.x);
        animator.SetFloat(speedParamID, speed);
        animator.SetBool(groundedParamID, controller.isGrounded);
        animator.SetFloat(velocityYParamID, controller.rb.velocity.y);
    }
}
```

### 5.2 State Machine Transitions

#### **Idle ↔ Run Transition**:
```
Conditions:
- Idle → Run: Speed > 0.1
- Run → Idle: Speed < 0.1
- Has Exit Time: false
- Transition Duration: 0.1s
```

#### **Ground → Jump Transition**:
```
Conditions:
- Any State → Jump: Jump trigger
- Has Exit Time: false
- Transition Duration: 0s
```

#### **Jump → Fall Transition**:
```
Conditions:
- Jump → Fall: VelocityY < 0
- Has Exit Time: false
- Transition Duration: 0.1s
```

### 5.3 Animation Events

```csharp
public class PlayerAnimationEvents : MonoBehaviour
{
    private Player2DController controller;

    void Awake()
    {
        controller = GetComponent<Player2DController>();
    }

    // Called from animation event
    public void OnLandComplete()
    {
        // Land animation finished
        controller.OnLandAnimationComplete();
    }

    public void OnAttackHit()
    {
        // Attack hit frame
        controller.CheckAttackHits();
    }
}
```

---

## 6. Camera System with Cinemachine

Goal: get a **smooth follow camera**.

### 6.1 Install & create a Virtual Camera

1. Open **Window → Package Manager → Unity Registry**
2. Install the **“Cinemachine”** package
3. In the Hierarchy: `GameObject → Cinemachine → 2D Camera`
4. Select the new `CM vcam` → set **Follow = Player**.

With these 4 steps, most 2D games already have a good-enough follow camera.

### 6.2 Tuning camera feel

In the `CinemachineVirtualCamera`:

- **Lens → Orthographic Size**: Controls view size (e.g. 5–7)
- **Body → 2D Transposer**:
  - `Follow Offset`: Camera offset from player (0, 2, -10)
  - `Damping`: Smooth camera movement (1,1,0)
  - `Dead Zone`: Area where player can move without camera shifting


### 6.3 Advanced ideas

Advanced techniques:
- Camera zones (different framing per area)
- Look-ahead (camera looks ahead in movement direction)
- Advanced camera shake

Detailed examples:
- `extras/common-scripts-library/Camera2DFollow.cs`, `CameraShake.cs`
- sample projects: `lesson4-input-player-controller`, `lesson5-ui-complete-game`.

Full implementations are available in:

---

## 7. Input Feedback and Game Feel

**Game feel** makes controls feel responsive. Three types of feedback:

- **Visual**: trails, dust, flashes
- **Audio**: footstep, jump, land, attack sounds
- **Screen effects**: camera shake on impacts

Hook examples:
- `DoJump()` → play jump sound + spawn effect
- `OnLanded()` → play dust + land sound + camera shake
- `OnAttack()` → show hit effect + attack sound

Hook example:

```csharp
public class SimpleFeedback : MonoBehaviour
{
    public ParticleSystem landDust;
    public AudioSource audioSource;
    public AudioClip landClip;

    public void OnLanded()
    {
        if (landDust != null) landDust.Play();
        if (audioSource != null && landClip != null)
            audioSource.PlayOneShot(landClip);
    }
}
```

Advanced implementations: Lesson 4 sample project and Lesson 5.

---

## 8. Performance, Debugging & Advanced Patterns

The topics below are important for larger projects and polish phases:

- **Optimization**: cache `InputAction` references, cache components (`Rigidbody2D`, `Animator`, …), avoid `GetComponent` in `Update`.
- **Debug**: use `OnDrawGizmos` to visualize ground checks and velocity; simple UI text to log inputs if needed.
- **Unit testing**: Test movement/jump logic with PlayMode tests
- **Advanced input**: buffered inputs, multi-button combos, event pooling.

For detailed implementations, see:
  - `extras/performance-optimization.md`
  - `extras/common-scripts-library.md`
  - the Lesson 4 and Lesson 5 sample projects

---

## Chapter Summary

### Core Knowledge:
1. ✅ **New Input System**: Event-driven, device-agnostic input handling
2. ✅ **Input Actions**: Flexible action mapping and binding system
3. ✅ **2D Character Controller**: Physics-based movement with advanced mechanics
4. ✅ **Camera System**: Cinemachine integration for professional camera control
5. ✅ **Animation Integration**: Seamless animation state management
6. ✅ **Game Feel**: Visual and audio feedback for responsive controls

### Technical Skills Acquired:
- 🎮 **Modern Input Handling**: Unity's New Input System mastery
- 🏃 **Advanced Movement**: Coyote time, jump buffering, variable jump height
- 📷 **Professional Cameras**: Cinemachine virtual camera setup
- 🎨 **Polish Effects**: Screen shake, particles, and audio feedback
- ⚡ **Performance Optimization**: Efficient input and component management

### Preparation for Next Lesson:
- 🖼️ **UI Systems**: User interface design and implementation
- 🎵 **Audio Integration**: Sound effects and music management
- 🎮 **Game Management**: Scene transitions and game state handling
- 📦 **Build Process**: Preparing games for distribution

### Practice:
Complete **Lab 04** to create a fully functional 2D character with responsive controls, smooth camera following, and polished game feel effects.

---

## ✅ What's Next

Proceed to [Lesson 5: UI & Complete Game](../lesson5-ui-complete-game/) to learn about complete game development including UI, menus, and build pipeline.