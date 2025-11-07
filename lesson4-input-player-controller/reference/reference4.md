# Lesson 4 Reference - Input & Player Controller

## Input System Setup

### Input Actions Asset
1. **Create → Input Actions**
2. **Action Maps**: Gameplay, UI, Menu
3. **Actions**: Move, Jump, Attack, Pause
4. **Bindings**: Keyboard, Gamepad, Touch

### Generated C# Class
```csharp
// Auto-generated from Input Actions
public class PlayerInputActions : IInputActionCollection2 {
    public InputAction move;
    public InputAction jump;
    public InputAction attack;

    public void Enable() { /* ... */ }
    public void Disable() { /* ... */ }
}
```

## PlayerInput Component

### Setup
```csharp
public class PlayerController : MonoBehaviour {
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;

    void Awake() {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }
}
```

### Event-Based Input
```csharp
void OnEnable() {
    jumpAction.performed += OnJump;
    jumpAction.canceled += OnJumpRelease;
}

void OnDisable() {
    jumpAction.performed -= OnJump;
    jumpAction.canceled -= OnJumpRelease;
}

void OnJump(InputAction.CallbackContext context) {
    if (context.performed) {
        // Jump pressed
    } else if (context.canceled) {
        // Jump released
    }
}
```

### Polling Input
```csharp
void Update() {
    Vector2 moveInput = moveAction.ReadValue<Vector2>();
    bool jumpPressed = jumpAction.WasPressedThisFrame();
    bool jumpHeld = jumpAction.IsPressed();
}
```

## Movement Controller

### Basic Movement
```csharp
public class Player2DController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Update() {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
```

### Input System Movement
```csharp
public class PlayerInputController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D rb;
    private PlayerInputActions inputActions;
    private Vector2 moveInput;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        inputActions = new PlayerInputActions();
    }

    void Update() {
        moveInput = inputActions.Player.Move.ReadValue<Vector2>();
        rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
    }
}
```

## Input Buffering

### Jump Buffer
```csharp
float jumpBufferTime = 0.2f;
float lastJumpInputTime;

void Update() {
    if (jumpAction.WasPressedThisFrame()) {
        lastJumpInputTime = Time.time;
    }

    if (IsGrounded() && Time.time - lastJumpInputTime < jumpBufferTime) {
        Jump();
        lastJumpInputTime = 0f; // Consume the input
    }
}
```

### Input Queuing
```csharp
Queue<InputAction> inputQueue = new Queue<InputAction>();

void Update() {
    if (jumpAction.WasPressedThisFrame()) {
        inputQueue.Enqueue(jumpAction);
    }

    if (CanProcessInput() && inputQueue.Count > 0) {
        ProcessInput(inputQueue.Dequeue());
    }
}
```

## Camera Follow

### Basic Follow
```csharp
public class CameraFollow : MonoBehaviour {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);
    [SerializeField] private float smoothSpeed = 0.125f;

    void LateUpdate() {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
```

### Cinemachine 2D
```csharp
// Virtual Camera setup
public class CameraController : MonoBehaviour {
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform player;

    void Start() {
        virtualCamera.Follow = player;
    }
}
```

## Input Rebinding

### Runtime Rebinding
```csharp
public void RebindJump() {
    jumpAction.Disable();
    var rebindOperation = jumpAction.PerformInteractiveRebinding()
        .WithControlsExcluding("Mouse")
        .OnMatchWaitForAnother(0.1f)
        .Start();
}
```

### Save/Load Bindings
```csharp
void SaveBindings() {
    string bindings = inputActions.SaveBindingOverridesAsJson();
    PlayerPrefs.SetString("InputBindings", bindings);
}

void LoadBindings() {
    string bindings = PlayerPrefs.GetString("InputBindings");
    if (!string.IsNullOrEmpty(bindings)) {
        inputActions.LoadBindingOverridesFromJson(bindings);
    }
}
```

## UI Input Handling

### Pause Menu
```csharp
public class MenuManager : MonoBehaviour {
    [SerializeField] private GameObject pauseMenu;
    private PlayerInputActions inputActions;

    void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.UI.Pause.performed += TogglePause;
    }

    void TogglePause(InputAction.CallbackContext context) {
        if (Time.timeScale == 0) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }
}
```

## Common Input Patterns

### Dash Input
```csharp
float dashCooldown = 1f;
float lastDashTime;

void Update() {
    if (dashAction.WasPressedThisFrame() && Time.time - lastDashTime > dashCooldown) {
        Dash();
        lastDashTime = Time.time;
    }
}
```

### Combo Input
```csharp
float comboWindow = 0.5f;
float lastAttackTime;
int comboCount;

void Update() {
    if (attackAction.WasPressedThisFrame()) {
        if (Time.time - lastAttackTime < comboWindow) {
            comboCount++;
        } else {
            comboCount = 1;
        }
        lastAttackTime = Time.time;
        PerformAttack(comboCount);
    }
}
```

## Performance Tips

- Use **InputAction.WasPressedThisFrame()** for single-press events
- Use **InputAction.IsPressed()** for held inputs
- Disable input actions when not needed
- Use **InputAction.Enable()/Disable()** for performance
- Cache input values to avoid repeated calls
