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

### 4.1 Complete Player Controller Structure

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player2DController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    public float airControl = 0.5f;

    [Header("Jump Settings")]
    public float jumpHeight = 4f;
    public float jumpTimeToApex = 0.5f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;

    [Header("Ground Detection")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayers;

    // Private variables
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector2 moveInput;
    private float gravity;
    private float jumpVelocity;

    private bool isGrounded;
    private bool wasGroundedLastFrame;
    private float lastGroundedTime;
    private float jumpBufferCounter;

    // Input references
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    void Awake()
    {
        // Get components
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();

        // Get input actions
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];

        // Calculate physics values
        CalculateJumpPhysics();
    }

    void CalculateJumpPhysics()
    {
        // Calculate gravity and jump velocity from designer values
        gravity = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        jumpVelocity = Mathf.Abs(gravity) * jumpTimeToApex;
    }

    void OnEnable()
    {
        // Subscribe to input events
        moveAction.performed += OnMoveInput;
        moveAction.canceled += OnMoveInput;
        jumpAction.performed += OnJumpInput;
        jumpAction.canceled += OnJumpCanceled;
    }

    void OnDisable()
    {
        // Unsubscribe from input events
        moveAction.performed -= OnMoveInput;
        moveAction.canceled -= OnMoveInput;
        jumpAction.performed -= OnJumpInput;
        jumpAction.canceled -= OnJumpCanceled;
    }

    void Update()
    {
        CheckGroundStatus();
        HandleCoyoteTime();
        HandleJumpBuffer();
        UpdateAnimations();
    }

    void FixedUpdate()
    {
        ApplyMovement();
        ApplyGravity();
    }

    void CheckGroundStatus()
    {
        wasGroundedLastFrame = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);

        if (!wasGroundedLastFrame && isGrounded)
        {
            // Just landed
            OnLanded();
        }

        if (wasGroundedLastFrame && !isGrounded)
        {
            // Just left ground
            lastGroundedTime = Time.time;
        }
    }

    void ApplyMovement()
    {
        float currentAcceleration = isGrounded ? acceleration : acceleration * airControl;

        if (moveInput.x != 0)
        {
            // Accelerate towards target velocity
            rb.velocity = Vector2.MoveTowards(rb.velocity,
                new Vector2(moveInput.x * moveSpeed, rb.velocity.y),
                currentAcceleration * Time.fixedDeltaTime);
        }
        else
        {
            // Decelerate when no input
            rb.velocity = Vector2.MoveTowards(rb.velocity,
                new Vector2(0, rb.velocity.y),
                deceleration * Time.fixedDeltaTime);
        }

        // Handle sprite flipping
        if (moveInput.x > 0)
            spriteRenderer.flipX = false;
        else if (moveInput.x < 0)
            spriteRenderer.flipX = true;
    }

    void ApplyGravity()
    {
        if (!isGrounded)
        {
            rb.velocity += Vector2.up * gravity * Time.fixedDeltaTime;
        }
    }
}
```

### 4.2 Advanced Jump System

#### **Coyote Time Implementation**:
```csharp
void HandleCoyoteTime()
{
    // Allow jumping for a short time after leaving ground
    if (isGrounded)
    {
        lastGroundedTime = Time.time;
    }
}

bool CanJump()
{
    return isGrounded || (Time.time - lastGroundedTime <= coyoteTime);
}
```

#### **Jump Buffering System**:
```csharp
void HandleJumpBuffer()
{
    if (jumpBufferCounter > 0)
    {
        jumpBufferCounter -= Time.deltaTime;

        if (CanJump())
        {
            Jump();
            jumpBufferCounter = 0;
        }
    }
}

void OnJumpInput(InputAction.CallbackContext context)
{
    jumpBufferCounter = jumpBufferTime;

    if (CanJump())
    {
        Jump();
        jumpBufferCounter = 0;
    }
}
```

#### **Variable Jump Height**:
```csharp
void Jump()
{
    rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
    isGrounded = false;

    // Play jump animation/sound
    animator.SetTrigger("Jump");
}

void OnJumpCanceled(InputAction.CallbackContext context)
{
    // Short hop when jump button released early
    if (rb.velocity.y > 0)
    {
        rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
    }
}
```

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

## 6. Camera System with Cinemachine

### 6.1 Cinemachine Installation

#### **Package Manager Setup**:
```
Window → Package Manager → Unity Registry
Search: "Cinemachine"
Install: Cinemachine package
```

### 6.2 Basic Camera Follow

#### **Virtual Camera Setup**:
```csharp
// Create Cinemachine Virtual Camera
GameObject → Cinemachine → 2D Camera

// Configure Follow target
public class CameraSetup : MonoBehaviour
{
    void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        var player = FindObjectOfType<Player2DController>();

        vcam.Follow = player.transform;
    }
}
```

#### **Camera Settings**:
```
Cinemachine Virtual Camera:
├── Follow: Player Transform
├── Lens: Orthographic Size = 5
├── Body: 2D Transposer
│   ├── Follow Offset: (0, 2, -10)
│   ├── Damping: (1, 1, 0)
│   └── Dead Zone Width/Height: 0.1
└── Aim: Do Nothing
```

### 6.3 Advanced Camera Features

#### **Camera Zones**:
```csharp
public class CameraZone : MonoBehaviour
{
    [Header("Camera Settings")]
    public float orthographicSize = 5f;
    public Vector3 followOffset = Vector3.zero;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.Instance.SetCameraZone(this);
        }
    }
}

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineTransposer transposer;

    void Awake()
    {
        Instance = this;
        transposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
    }

    public void SetCameraZone(CameraZone zone)
    {
        // Smoothly transition camera settings
        StartCoroutine(TransitionCamera(zone));
    }

    IEnumerator TransitionCamera(CameraZone zone)
    {
        float duration = 2f;
        float elapsed = 0f;

        float startSize = virtualCamera.m_Lens.OrthographicSize;
        Vector3 startOffset = transposer.m_FollowOffset;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Smoothly lerp camera properties
            virtualCamera.m_Lens.OrthographicSize = Mathf.Lerp(startSize, zone.orthographicSize, t);
            transposer.m_FollowOffset = Vector3.Lerp(startOffset, zone.followOffset, t);

            yield return null;
        }
    }
}
```

#### **Look Ahead System**:
```csharp
public class CameraLookAhead : MonoBehaviour
{
    [Header("Look Ahead")]
    public float lookAheadDistance = 3f;
    public float lookAheadSpeed = 2f;

    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer transposer;
    private Player2DController player;
    private Vector3 baseOffset;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
        player = FindObjectOfType<Player2DController>();
        baseOffset = transposer.m_FollowOffset;
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate look ahead offset based on movement direction
            float moveDirection = Mathf.Sign(player.rb.velocity.x);
            Vector3 targetOffset = baseOffset + Vector3.right * (moveDirection * lookAheadDistance);

            // Smoothly move camera offset
            transposer.m_FollowOffset = Vector3.Lerp(
                transposer.m_FollowOffset,
                targetOffset,
                lookAheadSpeed * Time.deltaTime
            );
        }
    }
}
```

---

## 7. Input Feedback and Game Feel

### 7.1 Visual Feedback

#### **Movement Trails**:
```csharp
public class MovementTrail : MonoBehaviour
{
    [Header("Trail Settings")]
    public TrailRenderer trailRenderer;
    public float minSpeedForTrail = 5f;

    private Player2DController controller;

    void Start()
    {
        controller = GetComponent<Player2DController>();
        trailRenderer.emitting = false;
    }

    void Update()
    {
        // Enable trail when moving fast
        bool shouldEmit = Mathf.Abs(controller.rb.velocity.x) > minSpeedForTrail;
        trailRenderer.emitting = shouldEmit;
    }
}
```

#### **Dust Particles**:
```csharp
public class DustEffects : MonoBehaviour
{
    [Header("Dust Particles")]
    public ParticleSystem landingDust;
    public ParticleSystem runningDust;

    private Player2DController controller;

    void Start()
    {
        controller = GetComponent<Player2DController>();
        controller.OnLanded += PlayLandingDust;
    }

    void Update()
    {
        // Play running dust when moving on ground
        bool isRunning = controller.isGrounded && Mathf.Abs(controller.rb.velocity.x) > 0.1f;

        if (isRunning && !runningDust.isPlaying)
            runningDust.Play();
        else if (!isRunning && runningDust.isPlaying)
            runningDust.Stop();
    }

    void PlayLandingDust()
    {
        landingDust.Play();
    }
}
```

### 7.2 Audio Feedback

#### **Footstep System**:
```csharp
public class FootstepAudio : MonoBehaviour
{
    [Header("Footstep Audio")]
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;

    private Player2DController controller;
    private float stepTimer;

    void Start()
    {
        controller = GetComponent<Player2DController>();
    }

    void Update()
    {
        if (controller.isGrounded && Mathf.Abs(controller.rb.velocity.x) > 0.1f)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                PlayFootstep();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length > 0)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
```

### 7.3 Screen Effects

#### **Camera Shake**:
```csharp
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private CinemachineBasicMultiChannelPerlin noise;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        noise = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float intensity, float duration)
    {
        StartCoroutine(ShakeRoutine(intensity, duration));
    }

    IEnumerator ShakeRoutine(float intensity, float duration)
    {
        noise.m_AmplitudeGain = intensity;

        yield return new WaitForSeconds(duration);

        noise.m_AmplitudeGain = 0f;
    }
}

// Usage example
public class PlayerEvents : MonoBehaviour
{
    private CameraShake cameraShake;

    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
    }

    public void OnLanded()
    {
        // Shake camera when landing
        cameraShake.ShakeCamera(0.5f, 0.1f);
    }
}
```

---

## 8. Performance Optimization

### 8.1 Input System Optimization

#### **Action Caching**:
```csharp
public class OptimizedPlayerController : MonoBehaviour
{
    // Cache input actions for performance
    private InputAction moveAction;
    private InputAction jumpAction;

    // Cache input values
    private Vector2 cachedMoveInput;
    private bool jumpPressed;

    void Awake()
    {
        var playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    void OnEnable()
    {
        // Use performed instead of reading every frame
        moveAction.performed += OnMovePerformed;
        moveAction.canceled += OnMoveCanceled;
        jumpAction.performed += OnJumpPerformed;
    }

    void OnMovePerformed(InputAction.CallbackContext context)
    {
        cachedMoveInput = context.ReadValue<Vector2>();
    }

    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        cachedMoveInput = Vector2.zero;
    }

    void FixedUpdate()
    {
        // Use cached input instead of reading every frame
        ApplyMovement(cachedMoveInput);
    }
}
```

#### **Input Pooling**:
```csharp
public class InputEventPool : MonoBehaviour
{
    private Queue<InputEvent> eventPool = new Queue<InputEvent>();

    public InputEvent GetEvent()
    {
        if (eventPool.Count > 0)
            return eventPool.Dequeue();

        return new InputEvent();
    }

    public void ReturnEvent(InputEvent inputEvent)
    {
        inputEvent.Reset();
        eventPool.Enqueue(inputEvent);
    }
}
```

### 8.2 Component Optimization

#### **Component Caching**:
```csharp
public class CachedComponents : MonoBehaviour
{
    // Cache frequently used components
    [System.NonSerialized] public Rigidbody2D rb;
    [System.NonSerialized] public SpriteRenderer spriteRenderer;
    [System.NonSerialized] public Animator animator;
    [System.NonSerialized] public AudioSource audioSource;

    protected virtual void Awake()
    {
        // Cache all components once
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
}

public class Player2DController : CachedComponents
{
    // Inherit cached components
    void Update()
    {
        // Use cached references (no GetComponent calls)
        if (rb.velocity.magnitude > 0.1f)
        {
            animator.SetBool("IsMoving", true);
        }
    }
}
```

---

## 9. Debugging and Testing

### 9.1 Input Debugging

#### **Input Debug UI**:
```csharp
using UnityEngine;
using UnityEngine.UI;

public class InputDebugUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Text moveInputText;
    public Text jumpInputText;
    public Text groundedText;

    private Player2DController controller;

    void Start()
    {
        controller = FindObjectOfType<Player2DController>();
    }

    void Update()
    {
        if (controller != null)
        {
            moveInputText.text = $"Move: {controller.moveInput}";
            jumpInputText.text = $"Jump Buffer: {controller.jumpBufferCounter:F2}";
            groundedText.text = $"Grounded: {controller.isGrounded}";
        }
    }
}
```

#### **Visual Debug Helpers**:
```csharp
void OnDrawGizmos()
{
    // Draw ground check area
    if (groundCheck != null)
    {
        Gizmos.color = isGrounded ? Color.green : Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    // Draw movement direction
    Gizmos.color = Color.blue;
    Gizmos.DrawRay(transform.position, rb.velocity.normalized * 2f);

    // Draw coyote time window
    if (!isGrounded && Time.time - lastGroundedTime <= coyoteTime)
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, Vector3.one * 0.5f);
    }
}
```

### 9.2 Unit Testing

#### **Movement Test Scripts**:
```csharp
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class PlayerControllerTests
{
    private GameObject playerObject;
    private Player2DController controller;

    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject("TestPlayer");
        playerObject.AddComponent<Rigidbody2D>();
        playerObject.AddComponent<BoxCollider2D>();
        controller = playerObject.AddComponent<Player2DController>();
    }

    [Test]
    public void PlayerMovement_WithInput_ChangesVelocity()
    {
        // Arrange
        Vector2 testInput = Vector2.right;

        // Act
        controller.SetMoveInput(testInput);
        controller.ApplyMovement();

        // Assert
        Assert.Greater(controller.rb.velocity.x, 0);
    }

    [UnityTest]
    public IEnumerator Jump_WhenGrounded_IncreasesYVelocity()
    {
        // Arrange
        controller.SetGrounded(true);
        float initialY = controller.rb.velocity.y;

        // Act
        controller.Jump();
        yield return new WaitForFixedUpdate();

        // Assert
        Assert.Greater(controller.rb.velocity.y, initialY);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerObject);
    }
}
```

---

## 10. Advanced Input Techniques

### 10.1 Input Buffering System

#### **Multi-Action Buffer**:
```csharp
public class InputBuffer : MonoBehaviour
{
    [System.Serializable]
    public class BufferedInput
    {
        public string actionName;
        public float timePressed;
        public bool consumed;
    }

    public float bufferTime = 0.2f;
    private List<BufferedInput> bufferedInputs = new List<BufferedInput>();

    public void BufferInput(string actionName)
    {
        bufferedInputs.Add(new BufferedInput
        {
            actionName = actionName,
            timePressed = Time.time,
            consumed = false
        });
    }

    public bool ConsumeBufferedInput(string actionName)
    {
        for (int i = bufferedInputs.Count - 1; i >= 0; i--)
        {
            var input = bufferedInputs[i];

            if (Time.time - input.timePressed > bufferTime || input.consumed)
            {
                bufferedInputs.RemoveAt(i);
                continue;
            }

            if (input.actionName == actionName && !input.consumed)
            {
                input.consumed = true;
                return true;
            }
        }

        return false;
    }
}
```

### 10.2 Combo System

#### **Input Sequence Detection**:
```csharp
public class ComboSystem : MonoBehaviour
{
    [System.Serializable]
    public class ComboSequence
    {
        public string name;
        public string[] inputSequence;
        public float maxTimeBetweenInputs = 1f;
    }

    public ComboSequence[] combos;
    private List<string> currentSequence = new List<string>();
    private float lastInputTime;

    public void RegisterInput(string inputName)
    {
        // Clear sequence if too much time passed
        if (Time.time - lastInputTime > GetMaxComboTime())
        {
            currentSequence.Clear();
        }

        currentSequence.Add(inputName);
        lastInputTime = Time.time;

        CheckForCombos();
    }

    void CheckForCombos()
    {
        foreach (var combo in combos)
        {
            if (IsSequenceMatch(combo))
            {
                ExecuteCombo(combo);
                currentSequence.Clear();
                break;
            }
        }
    }

    bool IsSequenceMatch(ComboSequence combo)
    {
        if (currentSequence.Count < combo.inputSequence.Length)
            return false;

        int startIndex = currentSequence.Count - combo.inputSequence.Length;

        for (int i = 0; i < combo.inputSequence.Length; i++)
        {
            if (currentSequence[startIndex + i] != combo.inputSequence[i])
                return false;
        }

        return true;
    }

    void ExecuteCombo(ComboSequence combo)
    {
        Debug.Log($"Combo executed: {combo.name}");
        // Execute combo logic
    }

    float GetMaxComboTime()
    {
        float maxTime = 0f;
        foreach (var combo in combos)
        {
            if (combo.maxTimeBetweenInputs > maxTime)
                maxTime = combo.maxTimeBetweenInputs;
        }
        return maxTime;
    }
}
```

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

Proceed to [Lesson 5: UI, Gameplay Loop & Build](../lesson5-ui-complete-game/) to learn about complete game development including UI, menus, and build pipeline.