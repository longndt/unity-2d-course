# Theory: Input System & 2D Player Controller

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

> **Note**: In Lesson 1, you learned basic input using Unity's Legacy Input Manager (Input.GetKey, Input.GetAxis). This lesson covers the **New Input System** which provides advanced features like device-agnostic input, rebinding, and event-driven architecture. Both systems are valid, but the New Input System is recommended for modern games.

### 1.1 Old vs New Input System

#### **Legacy Input Manager** (Input.GetKey) - Covered in Lesson 1:
```csharp
// Basic input from Lesson 1 - good for simple games
if (Input.GetKeyDown(KeyCode.Space))
{
    Jump();
}

float horizontal = Input.GetAxis("Horizontal");
```

**What You Learned in Lesson 1:**
- ✅ **Simple and straightforward**: Easy to understand and implement
- ✅ **Quick prototyping**: Fast to set up for basic movement
- ✅ **Good for beginners**: No complex setup required

**Limitations (Why New Input System is Better):**
- ❌ **Hard-coded inputs**: Difficult to customize controls
- ❌ **Limited device support**: Mainly keyboard/mouse
- ❌ **No input events**: Polling-based system
- ❌ **Performance issues**: Not optimized for modern games
- ❌ **No rebinding**: Players can't customize controls

#### **New Input System** (Input Actions):
```csharp
// New way - recommended
public InputAction jumpAction;
public InputAction moveAction;

void OnEnable()
{
    jumpAction.performed += Jump;
    jumpAction.Enable();
}
```

**Advantages:**
- ✅ **Event-driven**: Efficient performance
- ✅ **Device agnostic**: Automatic controller/keyboard support
- ✅ **Customizable**: Players can rebind controls
- ✅ **Modern features**: Touch, gyroscope, multiple devices

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

#### **Auto-Generated Methods** (when using Send Messages):
```csharp
// Unity automatically calls these methods based on action names
public void OnMove(InputValue value)
{
    Vector2 moveInput = value.Get<Vector2>();
    // Handle movement
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
    // Called when attack action is performed
    Attack();
}
```

#### **Manual Event Subscription** (Recommended):
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
    // Handle continuous movement
}

void OnJumpPerformed(InputAction.CallbackContext context)
{
    Jump();
}

void OnJumpCanceled(InputAction.CallbackContext context)
{
    // Handle jump release (for variable jump height)
    ReleaseJump();
}
```

---

## 4. 2D Character Controller Implementation

The goal of this section is **not** to read a huge class, but to understand the **core flow** of a 2D character that uses the New Input System:

1. Read input (Move / Jump)  
2. Store it in variables (`moveInput`, `jumpPressed`)  
3. In `FixedUpdate`, apply forces/velocity changes to the `Rigidbody2D`  
4. Use a few simple state variables for better feel (ground check, coyote time, jump buffer).

### 4.1 Minimal controller skeleton

Here is a minimal controller that is easy to teach and extend:

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

    // Called from PlayerInput (Send Messages)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        // only care about press down
        jumpPressed = value.isPressed;
    }

    void Update()
    {
        // Simple ground check
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position, groundCheckRadius, groundLayers);
    }

    void FixedUpdate()
    {
        // 1. Horizontal movement
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

        // 2. Jump
        if (jumpPressed && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpPressed = false; // reset
        }
    }
}
```

**How to use this when teaching:**
- Start with this version so students can get a moving/jumping character in 10–15 minutes.  
- Then **add features step by step**: coyote time, jump buffer, animation, camera, etc. (see lab and examples).

### 4.2 Coyote time & jump buffer (concept only)

Instead of full code, this section focuses on the **idea**:

- **Coyote time**: store `lastGroundedTime`. Allow jumping if the player just left the ground within X seconds.  
- **Jump buffer**: when the player presses jump slightly too early, store a “pending jump” in `jumpBufferCounter`; if the player lands within that window, perform the jump.

Minimal pseudocode:

```csharp
// Update
    if (isGrounded)
        lastGroundedTime = Time.time;

if (jumpPressed)
    jumpBufferCounter = jumpBufferTime;

// FixedUpdate
bool canJumpByCoyote =
    Time.time - lastGroundedTime <= coyoteTime;

if (jumpBufferCounter > 0 && canJumpByCoyote)
{
    DoJump();
            jumpBufferCounter = 0;
}
```

The full, production-style implementation (with more state, animation hooks, events, etc.) lives in:
- `lesson4-input-player-controller/example/Player2DController.cs`  
- the `lesson4-input-player-controller` sample project in `sample-projects/`.

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

    // Animation parameter IDs
    private int speedParamID;
    private int groundedParamID;
    private int velocityYParamID;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<Player2DController>();

        // Cache parameter IDs for performance
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
        // Update movement speed
        float speed = Mathf.Abs(controller.rb.velocity.x);
        animator.SetFloat(speedParamID, speed);

        // Update grounded state
        animator.SetBool(groundedParamID, controller.isGrounded);

        // Update vertical velocity
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

## 6. Camera System with Cinemachine (simplified)

Goal: get a **smooth follow camera** with minimal code.

### 6.1 Install & create a Virtual Camera

1. Open **Window → Package Manager → Unity Registry**  
2. Install the **“Cinemachine”** package  
3. In the Hierarchy: `GameObject → Cinemachine → 2D Camera`  
4. Select the new `CM vcam` → set **Follow = Player**.

With these 4 steps, most 2D games already have a good-enough follow camera for teaching.

### 6.2 Tuning camera feel

In the `CinemachineVirtualCamera`:

- **Lens → Orthographic Size**: change how much of the world you see (e.g. 5–7).  
- **Body → 2D Transposer**:
  - `Follow Offset`: move the camera slightly above the player (0, 2, -10).  
  - `Damping`: increase a bit (1,1,0) so the camera eases into movement.  
  - `Dead Zone`: small dead zone so the player can move a bit without the camera constantly shifting.

Teaching tip: let students **drag these sliders** and see how the camera feel changes – much more memorable than reading code.

### 6.3 Advanced ideas (for later)

Techniques like:
- Camera zones (different framing per area)  
- Look-ahead (camera looks ahead in movement direction)  
- More advanced camera shake  

already have detailed examples in:
- `extras/common-scripts-library/Camera2DFollow.cs`, `CameraShake.cs`  
- sample projects: `lesson4-input-player-controller`, `lesson5-ui-complete-game`.

In this theory chapter, you only need to **explain the ideas and show a quick demo**; students can read the full code in those examples later.

---

## 7. Input Feedback and Game Feel (concepts, light code)

**Game feel** is why the same “jump” button feels tight in one game and floaty in another.  
For this lesson, focus on 3 types of feedback:

- **Visual**: trails when running fast, dust when landing, flashes on attack.  
- **Audio**: footstep, jump, land, attack sounds.  
- **Screen effects**: light camera shake on strong impacts/landings.

Instead of full systems here, think in terms of simple **hooks**:

- When `DoJump()` is called → play jump sound + spawn jump effect.  
- When landing (`OnLanded`) → play dust + land sound + small camera shake.  
- When attacking (`OnAttack`) → show hit effect + attack sound.

Minimal hook example:

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

Full trail systems, complex hit effects, and advanced camera shake can live in:
- the Lesson 4 sample project  
- Lesson 5 (UI & polish)  
so this theory chapter stays light and easy to follow.

---

## 8. Performance, Debugging & Advanced Patterns (high-level only)

The topics below are **more important for larger projects** or polish phases, not for the very first Input System lesson:

- **Optimization**: cache `InputAction` references, cache components (`Rigidbody2D`, `Animator`, …), avoid `GetComponent` in `Update`.  
- **Debug**: use `OnDrawGizmos` to visualize ground checks and velocity; simple UI text to log inputs if needed.  
- **Unit testing**: you can test movement/jump logic with PlayMode tests, but this is advanced.  
- **Advanced input**: buffered inputs, multi-button combos, event pooling – better kept for an advanced session or homework for strong students.

Instead of full code here, you can:

- Briefly **mention the concepts** in class  
- Point students to:
  - `extras/performance-optimization.md`  
  - `extras/common-scripts-library.md`  
  - the Lesson 4 and Lesson 5 sample projects

for self-study after class.

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