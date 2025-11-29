# Input Flow

## Overview

This diagram illustrates how input flows from devices through Unity's Input System to gameplay and UI systems.

## Input Flow Diagram

```mermaid
flowchart TD
    Devices[Input Devices<br/>Keyboard: WASD, Arrow Keys<br/>Mouse: Movement, Clicks<br/>Gamepad: Sticks, Buttons<br/>Touch: Mobile Devices] --> InputSystem[Input System]
    
    InputSystem --> Action1[1. Input Actions<br/>Define input mappings]
    Action1 --> Action2[2. Action Maps<br/>Group related actions]
    Action2 --> Action3[3. Bindings<br/>Device-specific input mappings]
    Action3 --> Action4[4. PlayerInput Component<br/>Handle input events]
    
    Action4 --> Processing[Input Processing]
    Processing --> EventBased[Event-based<br/>OnActionPerformed]
    Processing --> Polling[Polling<br/>Read current input state]
    Processing --> Buffering[Buffering<br/>Queue input for later]
    Processing --> Rebinding[Rebinding<br/>Customize controls]
    
    EventBased --> Gameplay[Gameplay Systems]
    Polling --> Gameplay
    Buffering --> Gameplay
    Rebinding --> Gameplay
    
    Gameplay --> Movement[Player Movement<br/>Character controller]
    Gameplay --> Camera[Camera Control<br/>Mouse look, zoom]
    Gameplay --> Actions[Game Actions<br/>Jump, attack, interact]
    Gameplay --> UINav[UI Navigation<br/>Menu controls]
    
    style Devices fill:#e1f5ff
    style InputSystem fill:#fff4e1
    style Gameplay fill:#e1ffe1
```

## Input System Components

### Input Actions Asset
- **Purpose**: Define all input mappings
- **Location**: Project window
- **Usage**: Generate C# class for type-safe input

### Action Maps
- **Player**: Gameplay controls
- **UI**: Menu navigation
- **Debug**: Developer tools

### Bindings
- **Keyboard**: WASD, Space, Shift
- **Mouse**: Movement, clicks
- **Gamepad**: Sticks, face buttons

## Input Patterns

### Event-Based Input
```csharp
public void OnMove(InputAction.CallbackContext context)
{
    Vector2 input = context.ReadValue<Vector2>();
    // Handle movement
}
```

### Polling Input
```csharp
void Update()
{
    Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
    // Handle movement
}
```

### Input Buffering
```csharp
private float jumpBufferTime = 0.2f;
private float jumpBufferCounter;

void Update()
{
    if (Input.GetButtonDown("Jump"))
    {
        jumpBufferCounter = jumpBufferTime;
    }

    if (jumpBufferCounter > 0 && isGrounded)
    {
        Jump();
        jumpBufferCounter = 0;
    }

    jumpBufferCounter -= Time.deltaTime;
}
```

## Best Practices

### Input Handling
- Use **event-based** for discrete actions (jump, attack)
- Use **polling** for continuous actions (movement)
- Implement **input buffering** for responsive controls

### UI Integration
- Disable gameplay input when UI is active
- Use separate action maps for UI and gameplay
- Handle input focus properly

### Mobile Considerations
- Support touch input
- Provide on-screen controls
- Handle different screen sizes

---

**Next**: Learn about [UI Navigation Flow](./ui_navigation_flow.md) for menu systems
