# Input System Setup - Complete Visual Guide

## 🎯 Learning Objectives

After following this guide, you will be able to:
- Install and configure Unity's New Input System
- Create Input Actions assets with proper bindings
- Set up Player Input component for character control
- Configure input for both keyboard and gamepad
- Debug input issues effectively

---

## 📦 Step 1: Installing Input System Package

### 1.1 Open Package Manager
1. In Unity Editor, go to **Window → Package Manager**
2. Wait for packages to load
3. Ensure **"Unity Registry"** is selected in the dropdown

<img src="../images/unity-6.2/package-manager-unity-registry.png" alt="Package Manager - Unity Registry" width="900" />

### 1.2 Install Input System
1. In the search bar, type **"Input System"**
2. Select **"Input System"** from the list
3. Click **"Install"** button
4. Wait for installation to complete

<img src="../images/unity-6.2/package-manager-input-system.png" alt="Install Input System Package" width="900" />

### 1.3 Restart Unity
1. Unity will prompt you to restart
2. Click **"Yes"** to restart Unity
3. Wait for Unity to reload with Input System

---

## ⚙️ Step 2: Configure Project Settings

### 2.1 Open Project Settings
1. Go to **Edit → Project Settings**
2. Navigate to **XR Plug-in Management → Player**

### 2.2 Set Active Input Handling
1. Find **"Configuration"** section
2. Set **"Active Input Handling"** to **"Input System Package (New)"**
3. **Alternative**: "Both" for backward compatibility
4. **Don't use**: "Input Manager (Old)" for new projects

<img src="../images/unity-6.2/project-settings-active-input-handling.png" alt="Project Settings - Active Input Handling" width="900" />

### 2.3 Verify Installation
1. Check **Console** for any errors
2. Look for Input System related messages
3. If errors appear, restart Unity again

---

## 🎮 Step 3: Creating Input Actions Asset

### 3.1 Create Input Actions
1. In **Project** window, right-click
2. Go to **Create → Input Actions**
3. Name it **"PlayerInputActions"**
4. Save in **Settings** folder (create if needed)

### 3.2 Open Input Actions Editor
1. **Double-click** the PlayerInputActions asset
2. This opens the **Input Actions Editor** window
3. You'll see a clean interface ready for configuration

<img src="../images/unity-6.2/input-actions-editor.png" alt="Input Actions Editor" width="900" />

---

## 🎯 Step 4: Setting Up Action Maps

### 4.1 Create Gameplay Action Map
1. In Input Actions Editor, click **"+"** next to Action Maps
2. Name it **"Gameplay"**
3. This will contain all in-game input actions

### 4.2 Create UI Action Map
1. Click **"+"** next to Action Maps again
2. Name it **"UI"**
3. This will contain menu navigation actions

### 4.3 Action Map Structure
Your structure should look like:
```
PlayerInputActions
├── Gameplay (Action Map)
└── UI (Action Map)
```

---

## 🎮 Step 5: Creating Input Actions

### 5.1 Add Movement Action
1. **Select "Gameplay"** action map
2. Click **"+"** next to Actions
3. Name it **"Move"**
4. Set **Action Type** to **"Value"**
5. Set **Control Type** to **"Vector2"**

### 5.2 Add Jump Action
1. Click **"+"** next to Actions again
2. Name it **"Jump"**
3. Set **Action Type** to **"Button"**
4. Set **Control Type** to **"Button"**

### 5.3 Add Attack Action
1. Click **"+"** next to Actions again
2. Name it **"Attack"**
3. Set **Action Type** to **"Button"**
4. Set **Control Type** to **"Button"**

### 5.4 Add Interact Action
1. Click **"+"** next to Actions again
2. Name it **"Interact"**
3. Set **Action Type** to **"Button"**
4. Set **Control Type** to **"Button"**

---

## ⌨️ Step 6: Setting Up Keyboard Bindings

### 6.1 Configure Move Action
1. **Select "Move"** action
2. Click **"+"** next to Bindings
3. Select **"2D Vector Composite"**
4. This creates four sub-bindings:
   - **Up**: W key
   - **Down**: S key
   - **Left**: A key
   - **Right**: D key

### 6.2 Configure Jump Action
1. **Select "Jump"** action
2. Click **"+"** next to Bindings
3. Select **"Add Up/Down/Left/Right Composite"**
4. Set **Up** to **Space** key
5. **Delete** Down, Left, Right bindings (right-click → Delete)

### 6.3 Configure Attack Action
1. **Select "Attack"** action
2. Click **"+"** next to Bindings
3. Select **"Add Up/Down/Left/Right Composite"**
4. Set **Up** to **Left Mouse Button**
5. **Delete** Down, Left, Right bindings

### 6.4 Configure Interact Action
1. **Select "Interact"** action
2. Click **"+"** next to Bindings
3. Select **"Add Up/Down/Left/Right Composite"**
4. Set **Up** to **E** key
5. **Delete** Down, Left, Right bindings

---

## 🎮 Step 7: Setting Up Gamepad Bindings

### 7.1 Add Gamepad to Move Action
1. **Select "Move"** action
2. Click **"+"** next to existing bindings
3. Select **"Add Up/Down/Left/Right Composite"**
4. Set **Up** to **Gamepad/Left Stick/Up**
5. Set **Down** to **Gamepad/Left Stick/Down**
6. Set **Left** to **Gamepad/Left Stick/Left**
7. Set **Right** to **Gamepad/Left Stick/Right**

### 7.2 Add Gamepad to Jump Action
1. **Select "Jump"** action
2. Click **"+"** next to existing bindings
3. Select **"Add Up/Down/Left/Right Composite"**
4. Set **Up** to **Gamepad/Button South** (A button)
5. **Delete** Down, Left, Right bindings

### 7.3 Add Gamepad to Attack Action
1. **Select "Attack"** action
2. Click **"+"** next to existing bindings
3. Select **"Add Up/Down/Left/Right Composite"**
4. Set **Up** to **Gamepad/Button West** (X button)
5. **Delete** Down, Left, Right bindings

### 7.4 Add Gamepad to Interact Action
1. **Select "Interact"** action
2. Click **"+"** next to existing bindings
3. Select **"Add Up/Down/Left/Right Composite"**
4. Set **Up** to **Gamepad/Button East** (B button)
5. **Delete** Down, Left, Right bindings

---

## 🎯 Step 8: Setting Up UI Actions

### 8.1 Switch to UI Action Map
1. **Select "UI"** action map
2. This will show empty actions list

### 8.2 Add Navigate Action
1. Click **"+"** next to Actions
2. Name it **"Navigate"**
3. Set **Action Type** to **"Value"**
4. Set **Control Type** to **"Vector2"**

### 8.3 Add Submit Action
1. Click **"+"** next to Actions
2. Name it **"Submit"**
3. Set **Action Type** to **"Button"**
4. Set **Control Type** to **"Button"**

### 8.4 Add Cancel Action
1. Click **"+"** next to Actions
2. Name it **"Cancel"**
3. Set **Action Type** to **"Button"**
4. Set **Control Type** to **"Button"**

### 8.5 Configure UI Bindings
1. **Navigate**: Arrow keys + WASD + Left Stick
2. **Submit**: Enter + Space + A button
3. **Cancel**: Escape + B button

---

## 💾 Step 9: Save and Generate C# Class

### 9.1 Save Input Actions
1. **Ctrl + S** to save the asset
2. Check **Console** for any errors
3. Fix any binding conflicts if shown

### 9.2 Generate C# Class
1. In Input Actions Editor, click **"Generate C# Class"**
2. Choose **"Save As"** location (Scripts folder)
3. Name it **"PlayerInputActions"**
4. Click **"Save"**
5. Wait for Unity to compile the generated code

### 9.3 Verify Generation
1. Check **Console** for compilation errors
2. Look for **PlayerInputActions.cs** in Scripts folder
3. The file should contain generated C# code

---

## 🎮 Step 10: Setting Up Player Input Component

### 10.1 Create Player GameObject
1. **Right-click** in Hierarchy
2. **Create Empty**
3. Name it **"Player"**
4. Add **Sprite Renderer** component
5. Add **Rigidbody2D** component
6. Add **BoxCollider2D** component

### 10.2 Add Player Input Component
1. **Select Player** GameObject
2. Click **"Add Component"**
3. Search for **"Player Input"**
4. Add **Player Input** component

### 10.3 Configure Player Input
1. **Actions**: Drag PlayerInputActions asset
2. **Default Map**: Set to "Gameplay"
3. **Behavior**: Set to "Send Messages"
4. **UI Input Module**: Leave empty for now

<img src="../images/unity-6.2/player-input-component.png" alt="Player Input Component Setup" width="900" />

---

## 📝 Step 11: Creating Input Handler Script

### 11.1 Create Input Script
1. **Right-click** in Scripts folder
2. **Create → C# Script**
3. Name it **"PlayerInputHandler"**

### 11.2 Basic Input Handler Code
```csharp
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Input Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Vector2 moveInput;
    private bool jumpPressed;
    private bool attackPressed;
    private bool interactPressed;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("PlayerInputHandler requires Rigidbody2D component!");
            enabled = false;
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("PlayerInputHandler: SpriteRenderer component not found.");
        }
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        HandleAttack();
        HandleInteract();
    }

    // Input event methods (called by Player Input component)
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        jumpPressed = value.isPressed;
    }

    public void OnAttack(InputValue value)
    {
        attackPressed = value.isPressed;
    }

    public void OnInteract(InputValue value)
    {
        interactPressed = value.isPressed;
    }

    void HandleMovement()
    {
        if (rb == null) return;

        if (moveInput.x != 0)
        {
            rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);

            // Flip sprite based on direction
            if (spriteRenderer != null)
            {
                spriteRenderer.flipX = moveInput.x < 0;
            }
        }
    }

    void HandleJump()
    {
        if (rb == null) return;

        if (jumpPressed && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleAttack()
    {
        if (attackPressed)
        {
            Debug.Log("Attack!");
            // Add attack logic here
        }
    }

    void HandleInteract()
    {
        if (interactPressed)
        {
            Debug.Log("Interact!");
            // Add interaction logic here
        }
    }

    bool IsGrounded()
    {
        // Simple ground check - you can improve this
        return Physics2D.Raycast(transform.position, Vector2.down, 1f);
    }
}
```

### 11.3 Attach Script to Player
1. **Drag** PlayerInputHandler script to Player GameObject
2. **Configure** public variables in Inspector
3. **Test** in Play mode

---

## 🐛 Step 12: Debugging Input Issues

### 12.1 Common Issues and Solutions

#### **Problem**: Input not working
**Solutions**:
1. Check **Console** for errors
2. Verify **Player Input** component is attached
3. Ensure **Actions** asset is assigned
4. Check **Default Map** is set correctly

#### **Problem**: Script methods not called
**Solutions**:
1. Verify method names match action names exactly
2. Check **Behavior** is set to "Send Messages"
3. Ensure methods are **public**
4. Check **Console** for error messages

#### **Problem**: Gamepad not detected
**Solutions**:
1. Check if gamepad is connected
2. Verify gamepad bindings are correct
3. Test with different gamepad
4. Check Windows gamepad settings

### 12.2 Debug Input Values
Add this to your script for debugging:
```csharp
void Update()
{
    // Debug input values
    Debug.Log($"Move: {moveInput}, Jump: {jumpPressed}, Attack: {attackPressed}");
}
```

---

## ✅ Step 13: Verification Checklist

### Input System Installation
- [ ] Input System package installed
- [ ] Project settings configured
- [ ] No compilation errors

### Input Actions Asset
- [ ] PlayerInputActions asset created
- [ ] Action maps configured (Gameplay, UI)
- [ ] Actions created (Move, Jump, Attack, Interact)
- [ ] Keyboard bindings set up
- [ ] Gamepad bindings set up
- [ ] C# class generated

### Player Setup
- [ ] Player GameObject created
- [ ] Player Input component attached
- [ ] Input handler script attached
- [ ] Script methods working
- [ ] Input values detected

---

## 🎯 Next Steps

1. **Test Input**: Verify all inputs work correctly
2. **Add More Actions**: Create additional input actions as needed
3. **Implement UI Input**: Set up UI navigation
4. **Read Next Guide**: [Project Setup Guide](../project-setup/2d-project-setup.md)
5. **Start Lesson 4**: Begin with Input System lesson

---

## 💡 Pro Tips

### Input System Best Practices
- **Use Events**: Prefer event-driven input over polling
- **Separate Concerns**: Keep input handling separate from game logic
- **Test All Devices**: Verify keyboard, mouse, and gamepad work
- **Handle Edge Cases**: Consider input buffering and timing

### Performance Tips
- **Cache References**: Store component references in Start()
- **Use Input Actions**: More efficient than Input Manager
- **Minimize Updates**: Only process input when needed
- **Profile Input**: Use Unity Profiler to check input performance

---

