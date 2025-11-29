# Theory: Unity Basics

## 🎯 Learning Objectives

After completing this lesson, students will be able to:
- Set up a 2D project and navigate the Unity Editor efficiently
- Understand GameObject/Component model and MonoBehaviour lifecycle
- Create and use prefabs; manage scenes and scene loading
- Implement basic input handling for player interaction (keyboard input)
- Set up simple camera follow system for player tracking
- Use simple debug tools (Gizmos, Logs) to inspect behavior

---

## 🚀 Quick Start

### Unity's Component-Based Architecture

#### **GameObject/Component Model**
```csharp
// Unity: Real-time simulation, continuous updates
public class GameComponent : MonoBehaviour
{
    void Start() {
        // Initialize once
    }

    void Update() {
        // Update every frame (60+ FPS)
    }
}
```

### Unity Editor Overview

**Key Windows:**
- **Scene View**: 3D/2D world editor
- **Game View**: What the player sees
- **Hierarchy**: GameObject tree structure
- **Inspector**: Component properties
- **Project**: Asset files
- **Console**: Debug messages and errors

### 2D Project Setup
1. **Unity Hub** → New Project → **2D (URP)** template
2. **Scene View** → Enable 2D mode (top toolbar)
3. **Camera** → Set to Orthographic projection
4. **Gizmos** → Show only 2D elements

---

## 🧩 GameObject & Component System

### GameObject
- **Container** for Components
- Has **Transform** component by default
- Can be **parented** to other GameObjects
- **Active/Inactive** state affects visibility

### Components
- **Modular behaviors** attached to GameObjects
- **Transform**: Position, rotation, scale
- **SpriteRenderer**: Displays 2D images
- **Rigidbody2D**: Physics simulation
- **Scripts**: Custom C# behaviors

### Common 2D Components
```csharp
// Transform - Always present
transform.position = new Vector3(x, y, 0);
transform.rotation = Quaternion.Euler(0, 0, angle);
transform.localScale = Vector3.one;

// SpriteRenderer - For 2D graphics
SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
spriteRenderer.sprite = newSprite;
spriteRenderer.color = Color.white;
spriteRenderer.sortingOrder = 1;
```

---

## 🔄 MonoBehaviour Lifecycle

### Execution Order
```
Awake() → OnEnable() → Start() → Update() → LateUpdate() → OnDisable() → OnDestroy()
```

### When to Use Each
- **Awake()**: Component references, initialization
- **Start()**: Dependencies on other objects
- **Update()**: Input handling, game logic
- **FixedUpdate()**: Physics calculations
- **LateUpdate()**: Camera follow, UI positioning

### Example Lifecycle
```csharp
public class LifecycleDemo : MonoBehaviour
{
    void Awake()
    {
        Debug.Log("Awake: Component initialized");
    }

    void Start()
    {
        Debug.Log("Start: Ready to play");
    }

    void Update()
    {
        Debug.Log("Update: Running every frame");
    }
}
```

---

## 🎬 Scene Management

### Scenes
- **Containers** for GameObjects
- **Levels, menus, cutscenes**
- **Build Settings** must include scenes

### SceneManager API
```csharp
using UnityEngine.SceneManagement;

// Load scene by name
SceneManager.LoadScene("Level1");

// Load scene asynchronously
SceneManager.LoadSceneAsync("Level1");

// Get current scene
string currentScene = SceneManager.GetActiveScene().name;
```

### Scene Workflow
1. **Create Scene**: File → New Scene → 2D
2. **Save Scene**: Ctrl+S, name it appropriately
3. **Add to Build**: File → Build Settings → Add Open Scenes
4. **Load Scene**: Use SceneManager in scripts

---

## 🎭 Prefabs

### What are Prefabs?
- **Reusable GameObject templates**
- **Changes propagate** to all instances
- **Efficient** for repeated objects

### Creating Prefabs
1. **Design GameObject** in scene
2. **Drag to Project window**
3. **Prefab created** (blue cube icon)
4. **Modify prefab** affects all instances

### Using Prefabs in Code
```csharp
public class PrefabSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
```

---

## 🎮 Basic Input System

### Introduction to Input

Unity provides multiple ways to handle input. For beginners, we'll start with the **Legacy Input Manager** (Input class) which is simple and straightforward. In Lesson 4, you'll learn the modern **New Input System** for advanced scenarios.

### Basic Keyboard Input

#### **Reading Input**
```csharp
// Check if key is currently pressed
if (Input.GetKey(KeyCode.W))
{
    // Move forward
}

// Check if key was just pressed this frame
if (Input.GetKeyDown(KeyCode.Space))
{
    // Jump once
}

// Check if key was just released this frame
if (Input.GetKeyUp(KeyCode.Space))
{
    // Stop jumping
}
```

#### **Movement Input Example**
```csharp
public class SimpleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Read horizontal input (A/D or Left/Right arrows)
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontal = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontal = 1f;

        // Read vertical input (W/S or Up/Down arrows)
        float vertical = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            vertical = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            vertical = -1f;

        // Apply movement
        Vector3 movement = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }
}
```

#### **Input Axes (Alternative)**
```csharp
// Unity provides default axes (configured in Edit → Project Settings → Input Manager)
float horizontal = Input.GetAxis("Horizontal");  // Returns -1 to 1
float vertical = Input.GetAxis("Vertical");        // Returns -1 to 1

// Use for smooth movement
Vector3 movement = new Vector3(horizontal, vertical, 0) * moveSpeed * Time.deltaTime;
transform.position += movement;
```

### Common Key Codes

```csharp
KeyCode.Space      // Spacebar
KeyCode.Return     // Enter
KeyCode.Escape     // ESC
KeyCode.W, A, S, D // WASD keys
KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow
KeyCode.Alpha0 to KeyCode.Alpha9  // Number keys 0-9
```

### Input Best Practices
- **Use GetKeyDown()** for actions that should happen once (jump, attack)
- **Use GetKey()** for continuous actions (movement, holding)
- **Use GetAxis()** for smooth analog-like input
- **Check input in Update()**, not FixedUpdate() for responsiveness

---

## 📷 Simple Camera Follow

### Basic Camera Following

For 2D games, a common pattern is making the camera follow the player. We'll implement a simple version here; advanced camera systems with Cinemachine will be covered in Lesson 4.

#### **Simple Transform Follow**
```csharp
public class SimpleCameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;           // Player to follow
    public float followSpeed = 2f;    // How fast camera follows
    public Vector3 offset = Vector3.zero;  // Offset from target

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate target position
            Vector3 targetPosition = target.position + offset;
            targetPosition.z = transform.position.z; // Keep camera's Z position

            // Smoothly move camera towards target
            transform.position = Vector3.Lerp(
                transform.position,
                targetPosition,
                followSpeed * Time.deltaTime
            );
        }
    }
}
```

#### **Camera Bounds (Optional)**
```csharp
public class CameraFollowWithBounds : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;
    public Vector2 minBounds = new Vector2(-10, -5);
    public Vector2 maxBounds = new Vector2(10, 5);

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPos = target.position;

            // Clamp camera position within bounds
            targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x, maxBounds.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y, maxBounds.y);
            targetPos.z = transform.position.z;

            // Smooth follow
            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                followSpeed * Time.deltaTime
            );
        }
    }
}
```

### Camera Setup Steps
1. **Select Main Camera** in Hierarchy
2. **Attach SimpleCameraFollow script**
3. **Assign Player** as the target in Inspector
4. **Adjust follow speed** and offset as needed
5. **Test in Play mode**

### Why LateUpdate()?
- **LateUpdate()** runs after all Update() calls
- Ensures camera follows player after player has moved
- Prevents camera jitter and lag

---

## 🐛 Debug Tools

### Console Logging
```csharp
Debug.Log("Info message");
Debug.LogWarning("Warning message");
Debug.LogError("Error message");
Debug.LogFormat("Player {0} has {1} health", name, health);
```

### Gizmos (Scene View)
```csharp
void OnDrawGizmos()
{
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, 1f);
    Gizmos.DrawLine(transform.position, target.position);
}
```

### Inspector Debugging
- **Public fields** show in Inspector
- **SerializeField** for private fields
- **Range** attribute for sliders
- **Header** for organization

---

## ⚡ Performance Tips

### Best Practices
- **Use FixedUpdate()** for physics
- **Use Update()** for input and logic
- **Use LateUpdate()** for camera follow
- **Avoid expensive operations** in Update()
- **Use object pooling** for frequent instantiation

### Common Pitfalls
- **Null Reference**: Check if objects exist
- **Memory Leaks**: Destroy instantiated objects
- **Performance**: Profile with Unity Profiler
- **Build Errors**: Check Build Settings

---

## 🔧 Troubleshooting

### Common Issues
- **Scripts not compiling**: Check syntax and file names
- **Objects not visible**: Check camera position and object Z values
- **Scene not loading**: Add scenes to Build Settings
- **Prefabs not working**: Check prefab references

### Debug Steps
1. **Check Console** for errors
2. **Verify component assignments**
3. **Test in Play mode**
4. **Use Debug.Log()** to track execution

---

## 📚 Reference

- **MonoBehaviour Lifecycle**: See `reference/reference1.md`
- **Unity Manual**: GameObject and Component system
- **API Documentation**: Transform, SpriteRenderer, SceneManager
- **Best Practices**: Unity's official guidelines

---

## ✅ What's Next

Proceed to [Lesson 2: Sprites & Animation](../lesson2-sprites-animation/) to learn about visual game elements and animation systems.

---
