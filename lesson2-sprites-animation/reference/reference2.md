# Lesson 2 Reference - Sprites & Animation

## Sprite Import Settings

### Texture Type
- **Sprite (2D and UI)** for character sprites
- **Sprite (2D and UI) - Multiple** for sprite sheets
- **Pixels Per Unit**: 16-100 (higher = smaller sprites)

### Sprite Editor
- **Slice Type**: Automatic, Grid By Cell Size, or Manual
- **Pivot**: Center, Bottom, Custom
- **Mesh Type**: Tight (better performance) or Full Rect

## SpriteRenderer Component

```csharp
SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

spriteRenderer.sprite = newSprite;
spriteRenderer.color = Color.white;
spriteRenderer.flipX = true;  // Flip horizontally
spriteRenderer.flipY = false; // Flip vertically
spriteRenderer.sortingOrder = 1;
spriteRenderer.sortingLayerName = "Foreground";
```

## Sorting Layers Setup

1. **Edit → Project Settings → Tags and Layers**
2. **Sorting Layers**: Add layers (Background, Default, Foreground, UI)
3. **Order in Layer**: Higher numbers render on top

## Animator Controller

### Parameters
```csharp
// Float parameters
animator.SetFloat("Speed", velocity.magnitude);
animator.SetFloat("Horizontal", input.x);

// Bool parameters
animator.SetBool("IsGrounded", isGrounded);
animator.SetBool("IsJumping", isJumping);

// Trigger parameters
animator.SetTrigger("Attack");
animator.SetTrigger("Die");
```

### State Machine
- **States**: Idle, Walk, Run, Jump, Attack
- **Transitions**: Conditions based on parameters
- **Has Exit Time**: Uncheck for immediate transitions
- **Transition Duration**: 0.1-0.25 seconds

## Animation Events

```csharp
// Called from Animation window
public void OnFootstep() {
    // Play footstep sound
}

public void OnAttackHit() {
    // Deal damage to enemies
}

public void OnAnimationComplete() {
    // Return to idle state
}
```

## Common Animation Patterns

### Direction Flipping
```csharp
void Update() {
    if (horizontalInput > 0) {
        spriteRenderer.flipX = false;
    } else if (horizontalInput < 0) {
        spriteRenderer.flipX = true;
    }
}
```

### Speed-Based Animation
```csharp
void Update() {
    float speed = Mathf.Abs(rigidbody.velocity.x);
    animator.SetFloat("Speed", speed);
}
```

## Performance Tips

- Use **Sprite Atlas** for multiple sprites
- Keep **Pixels Per Unit** consistent across sprites
- Use **Tight** mesh type for better batching
- Limit **Sorting Layers** to essential ones only
