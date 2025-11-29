# Lab 4: Input & Player Controller

## 🎯 Learning Objectives

- Master Unity's New Input System and Input Actions
- Create responsive character controllers with advanced input handling
- Implement camera systems with Cinemachine
- Learn input feedback and game feel techniques
- Optimize input performance and debugging

## 🎮 Playtest Criteria

**Complete when you can:**
- [ ] Set up Input System with proper configuration
- [ ] Create responsive character movement with input
- [ ] Implement advanced jump mechanics with input
- [ ] Set up camera following system
- [ ] Add input feedback for better game feel

---

## 🚀 Quick Start

### Step 1: Install Input System Package

1. **Open Package Manager**: `Window → Package Manager`
2. **Select "Unity Registry"** in dropdown
3. **Search for "Input System"**
4. **Click "Install"** and wait for installation
5. **Restart Unity** when prompted

### Step 2: Configure Project Settings

1. **Edit → Project Settings → Player**
2. **Configuration → Active Input Handling**
3. **Select "Input System Package (New)"**
4. **Click "Apply"** to save settings

---

## 🎯 Lab Tasks

### Task 1: Input System Setup

#### **1.1 Create Input Actions Asset**

**Create Input Actions:**
1. **Right-click in Project** → **Create** → **Input Actions**
2. **Name it** "PlayerInputActions"
3. **Save in** `Assets/Input/` folder
4. **Double-click** to open Input Actions Editor

**Configure Action Maps:**
1. **In Input Actions Editor**, click **"+"** next to Action Maps
2. **Name it** "Gameplay"
3. **Click "+"** next to Action Maps again
4. **Name it** "UI"

#### **1.2 Create Input Actions**

**Add Movement Action:**
1. **Select "Gameplay"** action map
2. **Click "+"** next to Actions
3. **Name it** "Move"
4. **Set Action Type** to "Value"
5. **Set Control Type** to "Vector2"

**Add Jump Action:**
1. **Click "+"** next to Actions
2. **Name it** "Jump"
3. **Set Action Type** to "Button"
4. **Set Control Type** to "Button"

**Add Attack Action:**
1. **Click "+"** next to Actions
2. **Name it** "Attack"
3. **Set Action Type** to "Button"
4. **Set Control Type** to "Button"

**Add Interact Action:**
1. **Click "+"** next to Actions
2. **Name it** "Interact"
3. **Set Action Type** to "Button"
4. **Set Control Type** to "Button"

#### **1.3 Configure Input Bindings**

**Configure Move Action:**
1. **Select "Move"** action
2. **Click "+"** next to Bindings
3. **Select "2D Vector Composite"**
4. **Configure sub-bindings**:
   - **Up**: W key
   - **Down**: S key
   - **Left**: A key
   - **Right**: D key

**Configure Jump Action:**
1. **Select "Jump"** action
2. **Click "+"** next to Bindings
3. **Select "Add Up/Down/Left/Right Composite"**
4. **Set Up** to **Space** key
5. **Delete** Down, Left, Right bindings

**Configure Attack Action:**
1. **Select "Attack"** action
2. **Click "+"** next to Bindings
3. **Select "Add Up/Down/Left/Right Composite"**
4. **Set Up** to **Left Mouse Button**
5. **Delete** Down, Left, Right bindings

**Configure Interact Action:**
1. **Select "Interact"** action
2. **Click "+"** next to Bindings
3. **Select "Add Up/Down/Left/Right Composite"**
4. **Set Up** to **E** key
5. **Delete** Down, Left, Right bindings

#### **1.4 Generate C# Class**

**Generate C# Class:**
1. **In Input Actions Editor**, click **"Generate C# Class"**
2. **Choose save location**: `Assets/Scripts/Input/`
3. **Name it** "PlayerInputActions"
4. **Click "Save"**
5. **Wait for Unity** to compile the generated code

### Task 2: Player Input Component Setup

#### **2.1 Create Player GameObject**

**Create Player:**
1. **Right-click in Hierarchy** → **2D Object** → **Sprite** → **Square**
2. **Rename** to "Player"
3. **Add components**:
   - **Rigidbody2D**: Body Type = Dynamic
   - **BoxCollider2D**: Size = (1, 1, 1)
   - **Sprite Renderer**: Color = Blue
4. **Configure Transform**:
   - **Position**: (0, 0, 0)
   - **Scale**: (1, 1, 1)

**Add Player Input Component:**
1. **Select Player** in Hierarchy
2. **Add Component** → **Player Input**
3. **Configure Player Input**:
   - **Actions**: Drag PlayerInputActions asset
   - **Default Map**: Set to "Gameplay"
   - **Behavior**: Set to "Send Messages"

#### **2.2 Create Input Handler Script**

**Create Input Handler:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "PlayerInputHandler"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float coyoteTime = 0.1f;
    public float jumpBufferTime = 0.1f;
    
    [Header("Physics Settings")]
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer = 1;
    
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private bool jumpPressed;
    private bool attackPressed;
    private bool interactPressed;
    
    private bool isGrounded;
    private float coyoteTimeCounter;
    private float jumpBufferCounter;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
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
        if (jumpPressed)
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
        
        // Check if grounded
        CheckGrounded();
    }
    
    void FixedUpdate()
    {
        // Apply horizontal movement
        if (moveInput.x != 0)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }
    }
    
    // Input event methods (called by Player Input component)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log($"Move input: {moveInput}");
    }
    
    public void OnJump(InputValue value)
    {
        jumpPressed = value.isPressed;
        Debug.Log($"Jump pressed: {jumpPressed}");
    }
    
    public void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
        if (attackPressed)
        {
            Debug.Log("Attack!");
            // Add attack logic here
        }
    }
    
    public void OnInteract(InputValue value)
    {
        interactPressed = value.isPressed;
        if (interactPressed)
        {
            Debug.Log("Interact!");
            // Add interaction logic here
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Debug.Log("Jump executed!");
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

**Test Input System:**
1. **Attach PlayerInputHandler script** to Player
2. **Click Play** and test input
3. **Use A/D keys** for movement
4. **Use Space** for jumping
5. **Use E** for interaction
6. **Use Left Mouse** for attack

### Task 3: Advanced Input Features

#### **3.1 Implement Input Buffering**

**Create Input Buffer Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "InputBuffer"
3. **Replace content** with:

```csharp
using UnityEngine;
using System.Collections.Generic;

public class InputBuffer : MonoBehaviour
{
    [Header("Buffer Settings")]
    public float bufferTime = 0.1f;
    
    private Dictionary<string, float> inputBuffer = new Dictionary<string, float>();
    
    void Update()
    {
        // Decrease buffer time for all inputs
        var keys = new List<string>(inputBuffer.Keys);
        foreach (string key in keys)
        {
            inputBuffer[key] -= Time.deltaTime;
            if (inputBuffer[key] <= 0f)
            {
                inputBuffer.Remove(key);
            }
        }
    }
    
    public void BufferInput(string inputName)
    {
        inputBuffer[inputName] = bufferTime;
        Debug.Log($"Buffered input: {inputName}");
    }
    
    public bool ConsumeInput(string inputName)
    {
        if (inputBuffer.ContainsKey(inputName))
        {
            inputBuffer.Remove(inputName);
            Debug.Log($"Consumed input: {inputName}");
            return true;
        }
        return false;
    }
    
    public bool HasInput(string inputName)
    {
        return inputBuffer.ContainsKey(inputName);
    }
    
    public void ClearBuffer()
    {
        inputBuffer.Clear();
        Debug.Log("Input buffer cleared");
    }
}
```

#### **3.2 Implement Combo System**

**Create Combo System Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "ComboSystem"
3. **Replace content** with:

```csharp
using UnityEngine;
using System.Collections.Generic;

public class ComboSystem : MonoBehaviour
{
    [Header("Combo Settings")]
    public float comboWindow = 0.5f;
    public int maxComboLength = 3;
    
    private List<string> currentCombo = new List<string>();
    private float lastInputTime;
    
    void Update()
    {
        // Reset combo if too much time has passed
        if (Time.time - lastInputTime > comboWindow)
        {
            ResetCombo();
        }
    }
    
    public void AddInput(string inputName)
    {
        // Add input to combo
        currentCombo.Add(inputName);
        lastInputTime = Time.time;
        
        // Limit combo length
        if (currentCombo.Count > maxComboLength)
        {
            currentCombo.RemoveAt(0);
        }
        
        Debug.Log($"Combo: {string.Join(" -> ", currentCombo)}");
        
        // Check for combo completion
        CheckCombo();
    }
    
    void CheckCombo()
    {
        string comboString = string.Join("", currentCombo);
        
        // Define combo patterns
        if (comboString == "AA")
        {
            Debug.Log("Double Attack Combo!");
            ExecuteCombo("DoubleAttack");
        }
        else if (comboString == "AAA")
        {
            Debug.Log("Triple Attack Combo!");
            ExecuteCombo("TripleAttack");
        }
        else if (comboString == "AS")
        {
            Debug.Log("Attack + Special Combo!");
            ExecuteCombo("AttackSpecial");
        }
    }
    
    void ExecuteCombo(string comboName)
    {
        Debug.Log($"Executing combo: {comboName}");
        // Add combo execution logic here
        ResetCombo();
    }
    
    void ResetCombo()
    {
        currentCombo.Clear();
        Debug.Log("Combo reset");
    }
}
```

#### **3.3 Test Advanced Input Features**

**Test Input Buffering:**
1. **Attach InputBuffer script** to Player
2. **Modify PlayerInputHandler** to use buffering
3. **Test buffered inputs** with timing
4. **Observe buffer behavior** in Console

**Test Combo System:**
1. **Attach ComboSystem script** to Player
2. **Modify input handling** to use combo system
3. **Test combo sequences** (A-A, A-A-A, A-S)
4. **Observe combo detection** in Console

### Task 4: Camera System with Cinemachine

#### **4.1 Install Cinemachine Package**

**Install Cinemachine:**
1. **Window → Package Manager**
2. **Select "Unity Registry"**
3. **Search for "Cinemachine"**
4. **Click "Install"**
5. **Wait for installation** to complete

#### **4.2 Set Up Virtual Camera**

**Create Virtual Camera:**
1. **GameObject → Cinemachine → 2D Camera**
2. **Rename** to "PlayerFollowCamera"
3. **Configure Virtual Camera**:
   - **Follow**: Drag Player GameObject
   - **Look At**: Drag Player GameObject
   - **Body**: 3rd Person Follow
   - **Aim**: Do Nothing

**Configure Camera Settings:**
1. **In Virtual Camera Inspector**:
   - **Damping**: X = 1, Y = 1
   - **Screen X**: 0.5
   - **Screen Y**: 0.5
   - **Dead Zone Width**: 0.1
   - **Dead Zone Height**: 0.1

#### **4.3 Create Camera Zones**

**Create Camera Zone Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "CameraZone"
3. **Replace content** with:

```csharp
using UnityEngine;
using Cinemachine;

public class CameraZone : MonoBehaviour
{
    [Header("Camera Zone Settings")]
    public CinemachineVirtualCamera virtualCamera;
    public float priority = 10f;
    public bool followPlayer = true;
    public bool lookAtPlayer = true;
    
    private CinemachineVirtualCamera playerCamera;
    private float originalPriority;
    
    void Start()
    {
        // Find player camera
        playerCamera = FindObjectOfType<CinemachineVirtualCamera>();
        if (playerCamera != null)
        {
            originalPriority = playerCamera.Priority;
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Switch to zone camera
            if (virtualCamera != null)
            {
                virtualCamera.Priority = priority;
                if (playerCamera != null)
                {
                    playerCamera.Priority = 0;
                }
                
                Debug.Log("Switched to zone camera");
            }
        }
    }
    
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Switch back to player camera
            if (virtualCamera != null)
            {
                virtualCamera.Priority = 0;
            }
            if (playerCamera != null)
            {
                playerCamera.Priority = originalPriority;
            }
            
            Debug.Log("Switched back to player camera");
        }
    }
}
```

**Test Camera Zones:**
1. **Create trigger collider** for camera zone
2. **Attach CameraZone script** to zone
3. **Assign virtual camera** to script
4. **Test camera switching** when entering/exiting zone

### Task 5: Input Feedback and Game Feel

#### **5.1 Create Screen Shake System**

**Create Screen Shake Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "ScreenShake"
3. **Replace content** with:

```csharp
using UnityEngine;
using Cinemachine;

public class ScreenShake : MonoBehaviour
{
    [Header("Screen Shake Settings")]
    public float shakeIntensity = 1f;
    public float shakeDuration = 0.5f;
    
    private CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;
    
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
    
    void Update()
    {
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0)
            {
                StopShake();
            }
        }
    }
    
    public void StartShake()
    {
        if (noise != null)
        {
            noise.m_AmplitudeGain = shakeIntensity;
            noise.m_FrequencyGain = 1f;
            shakeTimer = shakeDuration;
            
            Debug.Log("Screen shake started");
        }
    }
    
    public void StopShake()
    {
        if (noise != null)
        {
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
            
            Debug.Log("Screen shake stopped");
        }
    }
}
```

#### **5.2 Create Input Feedback System**

**Create Input Feedback Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "InputFeedback"
3. **Replace content** with:

```csharp
using UnityEngine;

public class InputFeedback : MonoBehaviour
{
    [Header("Visual Feedback")]
    public GameObject dustEffect;
    public GameObject jumpEffect;
    public GameObject attackEffect;
    
    [Header("Audio Feedback")]
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip landSound;
    
    private AudioSource audioSource;
    private ScreenShake screenShake;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        screenShake = FindObjectOfType<ScreenShake>();
    }
    
    public void OnJump()
    {
        // Visual feedback
        if (jumpEffect != null)
        {
            Instantiate(jumpEffect, transform.position, Quaternion.identity);
        }
        
        // Audio feedback
        if (jumpSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(jumpSound);
        }
        
        // Screen shake
        if (screenShake != null)
        {
            screenShake.StartShake();
        }
        
        Debug.Log("Jump feedback triggered");
    }
    
    public void OnAttack()
    {
        // Visual feedback
        if (attackEffect != null)
        {
            Instantiate(attackEffect, transform.position, Quaternion.identity);
        }
        
        // Audio feedback
        if (attackSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(attackSound);
        }
        
        // Screen shake
        if (screenShake != null)
        {
            screenShake.StartShake();
        }
        
        Debug.Log("Attack feedback triggered");
    }
    
    public void OnLand()
    {
        // Visual feedback
        if (dustEffect != null)
        {
            Instantiate(dustEffect, transform.position, Quaternion.identity);
        }
        
        // Audio feedback
        if (landSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(landSound);
        }
        
        Debug.Log("Land feedback triggered");
    }
}
```

#### **5.3 Test Input Feedback**

**Setup Feedback System:**
1. **Attach InputFeedback script** to Player
2. **Create particle effects** for visual feedback
3. **Add audio clips** for sound feedback
4. **Test feedback** with different inputs

### Task 6: Input Performance and Debugging

#### **6.1 Create Input Debugger**

**Create Input Debug Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "InputDebugger"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class InputDebugger : MonoBehaviour
{
    [Header("Debug Settings")]
    public bool showInputValues = true;
    public bool showInputTiming = true;
    public bool showInputHistory = true;
    
    private string inputHistory = "";
    private float lastInputTime;
    
    void Update()
    {
        if (showInputValues)
        {
            // Display current input values
            Vector2 moveInput = Vector2.zero;
            if (Keyboard.current != null)
            {
                if (Keyboard.current.aKey.isPressed) moveInput.x -= 1;
                if (Keyboard.current.dKey.isPressed) moveInput.x += 1;
                if (Keyboard.current.wKey.isPressed) moveInput.y += 1;
                if (Keyboard.current.sKey.isPressed) moveInput.y -= 1;
            }
            
            if (moveInput != Vector2.zero)
            {
                Debug.Log($"Current input: {moveInput}");
            }
        }
        
        if (showInputTiming)
        {
            // Track input timing
            if (Input.anyKeyDown)
            {
                float timeSinceLastInput = Time.time - lastInputTime;
                Debug.Log($"Time since last input: {timeSinceLastInput:F3}s");
                lastInputTime = Time.time;
            }
        }
    }
    
    public void LogInput(string inputName, bool pressed)
    {
        if (showInputHistory)
        {
            string timestamp = System.DateTime.Now.ToString("HH:mm:ss.fff");
            inputHistory += $"[{timestamp}] {inputName}: {pressed}\n";
            
            // Keep only last 10 inputs
            string[] lines = inputHistory.Split('\n');
            if (lines.Length > 10)
            {
                inputHistory = string.Join("\n", lines, lines.Length - 10, 10);
            }
            
            Debug.Log($"Input History:\n{inputHistory}");
        }
    }
}
```

#### **6.2 Optimize Input Performance**

**Create Input Optimizer Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "InputOptimizer"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class InputOptimizer : MonoBehaviour
{
    [Header("Optimization Settings")]
    public bool useInputCaching = true;
    public bool useInputPooling = true;
    public int maxInputEvents = 100;
    
    private InputAction[] cachedActions;
    private Queue<InputEvent> inputEventPool;
    
    void Start()
    {
        if (useInputCaching)
        {
            CacheInputActions();
        }
        
        if (useInputPooling)
        {
            InitializeInputPool();
        }
    }
    
    void CacheInputActions()
    {
        // Cache frequently used input actions
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput != null)
        {
            cachedActions = new InputAction[]
            {
                playerInput.actions["Move"],
                playerInput.actions["Jump"],
                playerInput.actions["Attack"],
                playerInput.actions["Interact"]
            };
        }
        
        Debug.Log("Input actions cached for performance");
    }
    
    void InitializeInputPool()
    {
        inputEventPool = new Queue<InputEvent>();
        for (int i = 0; i < maxInputEvents; i++)
        {
            inputEventPool.Enqueue(new InputEvent());
        }
        
        Debug.Log("Input event pool initialized");
    }
    
    public InputEvent GetInputEvent()
    {
        if (useInputPooling && inputEventPool.Count > 0)
        {
            return inputEventPool.Dequeue();
        }
        return new InputEvent();
    }
    
    public void ReturnInputEvent(InputEvent inputEvent)
    {
        if (useInputPooling && inputEventPool.Count < maxInputEvents)
        {
            inputEvent.Reset();
            inputEventPool.Enqueue(inputEvent);
        }
    }
}

[System.Serializable]
public class InputEvent
{
    public string actionName;
    public bool pressed;
    public float timestamp;
    
    public void Reset()
    {
        actionName = "";
        pressed = false;
        timestamp = 0f;
    }
}
```

#### **6.3 Test Input Performance**

**Monitor Input Performance:**
1. **Attach InputDebugger** to Player
2. **Attach InputOptimizer** to Player
3. **Use Unity Profiler** to monitor input performance
4. **Test with different** input scenarios
5. **Optimize settings** based on performance

---

## ✅ Completion Checklist

### **Input System Setup**
- [ ] **Installed Input System package** and configured project
- [ ] **Created Input Actions asset** with proper action maps
- [ ] **Configured input bindings** for keyboard and gamepad
- [ ] **Generated C# class** for input actions

### **Player Input Component**
- [ ] **Set up Player Input component** with proper configuration
- [ ] **Created input handler script** with event methods
- [ ] **Tested input system** with different inputs
- [ ] **Verified input events** are working correctly

### **Advanced Input Features**
- [ ] **Implemented input buffering** for responsive input
- [ ] **Created combo system** for input sequences
- [ ] **Tested advanced input** features
- [ ] **Optimized input performance** for gameplay

### **Camera System**
- [ ] **Installed Cinemachine package** and set up virtual camera
- [ ] **Configured camera following** and look-at settings
- [ ] **Created camera zones** for different areas
- [ ] **Tested camera switching** between zones

### **Input Feedback**
- [ ] **Implemented screen shake** system for impact feedback
- [ ] **Created visual feedback** for input actions
- [ ] **Added audio feedback** for input responses
- [ ] **Tested feedback system** with different inputs

### **Performance and Debugging**
- [ ] **Created input debugger** for troubleshooting
- [ ] **Optimized input performance** with caching and pooling
- [ ] **Monitored input performance** with profiler
- [ ] **Tested input system** under load

---

## 🚨 Troubleshooting

### **Common Issues and Solutions**

#### **Input not working**
**Possible causes:**
- Input System not installed
- Input Actions not configured
- Player Input component not set up

**Solutions:**
1. Verify Input System package is installed
2. Check Input Actions asset is configured correctly
3. Ensure Player Input component is attached and configured
4. Test with simple input actions first

#### **Input feels unresponsive**
**Possible causes:**
- Input buffering not implemented
- Input timing issues
- Physics settings too slow

**Solutions:**
1. Implement input buffering for responsive input
2. Adjust input timing and buffer windows
3. Optimize physics settings for responsiveness
4. Test with different input scenarios

#### **Camera not following**
**Possible causes:**
- Cinemachine not installed
- Virtual camera not configured
- Follow target not assigned

**Solutions:**
1. Install Cinemachine package
2. Configure virtual camera settings
3. Assign follow and look-at targets
4. Test camera following in different scenarios

#### **Performance issues**
**Possible causes:**
- Too many input events
- Inefficient input handling
- Camera updates too frequent

**Solutions:**
1. Implement input caching and pooling
2. Optimize input handling code
3. Adjust camera update frequency
4. Use Unity Profiler to identify bottlenecks

---

## 📝 Self-Assessment

After completing this lab, test your understanding with the interactive quiz:

**Take the Quiz**: [`quiz/quiz4.html`](../quiz/quiz4.html)

The quiz will help you:
- Verify your understanding of key concepts
- Identify areas that need review
- Prepare for the next lesson

---

## 📚 Next Steps

### **Immediate Next Steps**
1. **Complete all tasks** in this lab
2. **Test your input system** thoroughly
3. **Experiment with different** input configurations
4. **Practice with camera** systems and feedback

### **Prepare for Next Lesson**
1. **Review input concepts** and implementation
2. **Understand camera systems** and Cinemachine
3. **Practice with input feedback** and game feel
4. **Read Lesson 5 materials** in advance

### **Further Learning**
1. **Unity Learn tutorials** for additional input practice
2. **Unity documentation** for deeper understanding
3. **Community forums** for questions and help
4. **Practice projects** to reinforce learning

---

## 💡 Pro Tips

### **Input System Best Practices**
- **Use Input Actions** for consistent input handling
- **Implement input buffering** for responsive gameplay
- **Test with different** input devices
- **Optimize input performance** for smooth gameplay

### **Camera System Tips**
- **Use Cinemachine** for professional camera work
- **Configure camera zones** for different areas
- **Implement screen shake** for impact feedback
- **Test camera following** in different scenarios

### **Game Feel Tips**
- **Add visual feedback** for all input actions
- **Use audio feedback** for input responses
- **Implement screen shake** for impact
- **Test game feel** with different players

---

