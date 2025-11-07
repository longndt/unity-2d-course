# Lesson 4: Input & Player Controller - Sample Project

## 🎯 Project Overview

This sample project demonstrates Unity's New Input System with advanced player controller features including responsive input handling, camera following, and game feel enhancements. It features a complete input system setup with keyboard, mouse, and gamepad support.

### **📚 Relationship to Lesson 4 Examples**
- **Basic Examples**: `lesson4-input-player-controller/example/` - Individual input concepts and scripts
- **This Sample Project**: Complete controller system with all input features integrated
- **Learning Path**: Study individual examples first, then explore this complete implementation

## 🎮 Project Description

### **Objective**
Create a responsive player controller using Unity's New Input System with advanced features and smooth camera following.

### **Features**
- Unity New Input System implementation
- Responsive player movement and jumping
- Camera following with Cinemachine
- Input feedback and game feel
- Multi-device input support (keyboard, mouse, gamepad)

### **Controls**
- **A/D Keys**: Move left/right
- **Space**: Jump
- **Left Mouse**: Attack
- **Right Mouse**: Interact
- **E Key**: Alternative interact
- **Gamepad**: Full gamepad support

## 🏗️ Project Structure

```
Assets/
├── Scenes/
│   └── InputDemo.unity         # Main input demo scene
├── Scripts/
│   ├── PlayerInputHandler.cs   # Input system handler
│   ├── CameraController.cs     # Camera following system
│   ├── InputFeedback.cs        # Visual and audio feedback
│   ├── InputBuffer.cs          # Input buffering system
│   └── ComboSystem.cs          # Input combo detection
├── Input/
│   └── PlayerInputActions.inputactions # Input Actions asset
├── Prefabs/
│   ├── Player.prefab           # Player with input system
│   ├── Camera.prefab           # Camera with Cinemachine
│   └── UI.prefab               # Input feedback UI
└── Sprites/
    ├── Player/
    │   ├── idle.png
    │   ├── walk.png
    │   └── jump.png
    └── Effects/
        ├── dust.png
        └── impact.png
```

## 🎯 Learning Objectives

After studying this project, you will understand:

### **Input System Setup**
- Input Actions asset creation and configuration
- Action Maps and Action Types
- Input Bindings for multiple devices
- Player Input component usage

### **Advanced Input Handling**
- Input buffering for responsive gameplay
- Combo system for input sequences
- Input feedback and game feel
- Multi-device input management

### **Camera Systems**
- Cinemachine Virtual Camera setup
- Camera following and look-ahead
- Camera zones and transitions
- Screen shake and effects

### **Game Feel Enhancement**
- Visual feedback for inputs
- Audio feedback system
- Screen effects and particles
- Responsive control tuning

## 🔧 Setup Instructions

### **1. Open Project**
1. Launch Unity Hub
2. Click "Add" and select this project folder
3. Open the project in Unity Editor
4. Wait for assets to import

### **2. Configure Input System**
1. **Edit → Project Settings → Player**
2. **Set Active Input Handling** to "Input System Package (New)"
3. **Apply** settings and restart Unity if prompted

### **3. Test the Features**
1. **Use A/D keys** to move character
2. **Press Space** to jump
3. **Click Left Mouse** to attack
4. **Test with gamepad** if available

## 📝 Code Walkthrough

### **PlayerInputHandler.cs**
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
    
    // Input System references
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction attackAction;
    private InputAction interactAction;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
        
        // Get input actions
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        attackAction = playerInput.actions["Attack"];
        interactAction = playerInput.actions["Interact"];
    }
    
    void Update()
    {
        HandleCoyoteTime();
        HandleJumpBuffer();
        HandleJump();
    }
    
    void FixedUpdate()
    {
        HandleMovement();
    }
    
    // Input event methods (called by Player Input component)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    
    public void OnJump(InputValue value)
    {
        jumpPressed = value.isPressed;
        
        if (jumpPressed)
        {
            jumpBufferCounter = jumpBufferTime;
        }
    }
    
    public void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
        
        if (attackPressed)
        {
            Attack();
        }
    }
    
    public void OnInteract(InputValue value)
    {
        interactPressed = value.isPressed;
        
        if (interactPressed)
        {
            Interact();
        }
    }
    
    void HandleMovement()
    {
        if (moveInput.x != 0)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        }
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
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
            jumpBufferCounter = 0f;
        }
    }
    
    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        Debug.Log("Jump executed!");
    }
    
    void Attack()
    {
        Debug.Log("Attack!");
        // Add attack logic here
    }
    
    void Interact()
    {
        Debug.Log("Interact!");
        // Add interaction logic here
    }
    
    void CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        isGrounded = hit.collider != null;
        
        Debug.DrawRay(transform.position, Vector2.down * groundCheckDistance, isGrounded ? Color.green : Color.red);
    }
}
```

### **CameraController.cs**
```csharp
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    [Header("Camera Settings")]
    public CinemachineVirtualCamera virtualCamera;
    public float followSpeed = 2f;
    public float lookAheadDistance = 3f;
    public float lookAheadSpeed = 2f;
    
    [Header("Screen Shake")]
    public float shakeIntensity = 1f;
    public float shakeDuration = 0.5f;
    
    private CinemachineBasicMultiChannelPerlin noise;
    private float shakeTimer;
    private Vector3 lookAheadTarget;
    private Vector3 currentLookAhead;
    
    void Start()
    {
        if (virtualCamera != null)
        {
            noise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        }
    }
    
    void Update()
    {
        HandleScreenShake();
        HandleLookAhead();
    }
    
    void HandleScreenShake()
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
    
    void HandleLookAhead()
    {
        // Calculate look ahead based on player movement
        Vector2 playerVelocity = GetPlayerVelocity();
        lookAheadTarget = new Vector3(playerVelocity.x * lookAheadDistance, 0, 0);
        
        // Smooth look ahead movement
        currentLookAhead = Vector3.Lerp(currentLookAhead, lookAheadTarget, lookAheadSpeed * Time.deltaTime);
        
        // Apply look ahead to camera
        if (virtualCamera != null)
        {
            virtualCamera.GetCinemachineComponent<CinemachineTransposer>().m_TrackedObjectOffset = currentLookAhead;
        }
    }
    
    Vector2 GetPlayerVelocity()
    {
        // Get player velocity for look ahead calculation
        Rigidbody2D playerRb = FindObjectOfType<PlayerInputHandler>()?.GetComponent<Rigidbody2D>();
        return playerRb != null ? playerRb.velocity : Vector2.zero;
    }
    
    public void StartShake()
    {
        if (noise != null)
        {
            noise.m_AmplitudeGain = shakeIntensity;
            noise.m_FrequencyGain = 1f;
            shakeTimer = shakeDuration;
        }
    }
    
    public void StopShake()
    {
        if (noise != null)
        {
            noise.m_AmplitudeGain = 0f;
            noise.m_FrequencyGain = 0f;
        }
    }
}
```

### **InputFeedback.cs**
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
    
    [Header("Screen Effects")]
    public CameraController cameraController;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        cameraController = FindObjectOfType<CameraController>();
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
        if (cameraController != null)
        {
            cameraController.StartShake();
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
        if (cameraController != null)
        {
            cameraController.StartShake();
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

### **InputBuffer.cs**
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

## 🎨 Visual Elements

### **Character Sprites**
- **Idle**: Static character pose
- **Walk**: Walking animation cycle
- **Jump**: Jump pose with motion blur

### **Effects**
- **Dust Particles**: Footstep and landing effects
- **Jump Effects**: Visual feedback for jumping
- **Attack Effects**: Impact effects for attacks

### **UI Elements**
- **Input Display**: Show current input values
- **Feedback Indicators**: Visual input feedback
- **Control Instructions**: Display available controls

## 🎮 Gameplay Features

### **Responsive Input System**
- **Input Buffering**: Responsive input handling
- **Multi-Device Support**: Keyboard, mouse, and gamepad
- **Input Feedback**: Visual and audio feedback
- **Combo System**: Input sequence detection

### **Camera System**
- **Smooth Following**: Cinemachine-based camera
- **Look Ahead**: Camera anticipates movement
- **Screen Shake**: Impact feedback
- **Camera Zones**: Different areas with different camera behavior

### **Game Feel Enhancement**
- **Visual Feedback**: Particles and effects
- **Audio Feedback**: Sound effects for actions
- **Screen Effects**: Shake and visual impact
- **Responsive Controls**: Tuned for optimal feel

## 🔧 Customization Options

### **Easy Modifications**
1. **Adjust input sensitivity**: Modify input values
2. **Change camera behavior**: Modify camera settings
3. **Add new input actions**: Create new input bindings
4. **Modify feedback effects**: Change visual and audio feedback

### **Advanced Features**
1. **Input rebinding**: Allow players to customize controls
2. **Advanced combos**: Complex input sequences
3. **Haptic feedback**: Controller vibration
4. **Accessibility options**: Customizable input settings

## 🐛 Common Issues

### **Input not working**
- **Cause**: Input System not installed or configured
- **Solution**: Install Input System package and configure settings

### **Camera not following**
- **Cause**: Cinemachine not installed or configured
- **Solution**: Install Cinemachine package and configure virtual camera

### **Input feels unresponsive**
- **Cause**: Input buffering not implemented
- **Solution**: Implement input buffering system

### **Feedback not working**
- **Cause**: Audio sources or effects not assigned
- **Solution**: Assign audio sources and particle effects

## 📚 Learning Exercises

### **Beginner Exercises**
1. **Modify input bindings** for different devices
2. **Adjust camera settings** for different feel
3. **Add new input actions** for different abilities
4. **Test with different input devices**

### **Intermediate Exercises**
1. **Implement input rebinding** system
2. **Create advanced combos** with multiple inputs
3. **Add haptic feedback** for controllers
4. **Implement accessibility options**

### **Advanced Exercises**
1. **Build a complete input system** with all features
2. **Create input recording** and playback system
3. **Implement networked input** for multiplayer
4. **Add advanced camera behaviors** and transitions

## 🎯 Next Steps

After completing this project:

1. **Study the input system**: Understand how Input Actions work
2. **Experiment with different inputs**: Try various input devices
3. **Move to Lesson 5**: Learn about UI and complete game development
4. **Build your own controller**: Create a unique input system

## 💡 Pro Tips

### **Input System Tips**
- **Use Input Actions** for consistent input handling
- **Implement input buffering** for responsive gameplay
- **Test with different devices** for compatibility
- **Provide input feedback** for better game feel

### **Camera Tips**
- **Use Cinemachine** for professional camera work
- **Implement look ahead** for smooth following
- **Add screen shake** for impact feedback
- **Test camera behavior** in different scenarios

---

**Happy Learning!** This project provides a comprehensive understanding of Unity's Input System and camera control. Take your time to experiment with the different input features!
