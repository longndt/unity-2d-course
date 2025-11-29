# Lesson 1 Reference - Unity Basics

## MonoBehaviour Lifecycle Order

```csharp
// Execution order (per GameObject):
Awake()     // Called before Start, even if disabled
OnEnable()  // Called when GameObject becomes active
Start()     // Called once before first Update
Update()    // Called every frame
LateUpdate() // Called after all Updates
OnDisable() // Called when GameObject becomes inactive
OnDestroy() // Called when GameObject is destroyed
```

## Common Unity APIs

### Transform
```csharp
transform.position = new Vector3(x, y, z);
transform.rotation = Quaternion.Euler(x, y, z);
transform.localScale = Vector3.one;
transform.Translate(Vector3.right * speed * Time.deltaTime);
```

### GameObject
```csharp
GameObject.Find("Name");
GameObject.FindWithTag("Player");
Instantiate(prefab, position, rotation);
Destroy(gameObject, delay);
SetActive(true/false);
```

### Scene Management
```csharp
using UnityEngine.SceneManagement;

SceneManager.LoadScene("SceneName");
SceneManager.LoadSceneAsync("SceneName");
SceneManager.GetActiveScene().name;
```

## Debug Tools

### Console Logging
```csharp
Debug.Log("Message");
Debug.LogWarning("Warning");
Debug.LogError("Error");
Debug.LogFormat("Player {0} has {1} health", name, health);
```

### Gizmos (Scene View)
```csharp
void OnDrawGizmos() {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, radius);
    Gizmos.DrawLine(start, end);
}
```

## Project Setup Checklist

- [ ] Unity 6.2 (6000.2.10f1) installed via Unity Hub
- [ ] 2D project template selected
- [ ] URP (Universal Render Pipeline) enabled if needed
- [ ] Input System package installed
- [ ] Git LFS configured for large assets
- [ ] IDE configured with Unity debugger

## Basic Input System (Legacy Input Manager)

### Keyboard Input
```csharp
// Check if key is currently pressed
if (Input.GetKey(KeyCode.W)) { }

// Check if key was just pressed this frame
if (Input.GetKeyDown(KeyCode.Space)) { }

// Check if key was just released
if (Input.GetKeyUp(KeyCode.Space)) { }

// Get input axes (configured in Project Settings)
float horizontal = Input.GetAxis("Horizontal");  // -1 to 1
float vertical = Input.GetAxis("Vertical");        // -1 to 1
```

### Common Key Codes
```csharp
KeyCode.Space, KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D
KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow
KeyCode.Return, KeyCode.Escape, KeyCode.Alpha0 to KeyCode.Alpha9
```

### Input Best Practices
- Use `GetKeyDown()` for actions that happen once (jump, attack)
- Use `GetKey()` for continuous actions (movement, holding)
- Use `GetAxis()` for smooth analog-like input
- Check input in `Update()`, not `FixedUpdate()`

## Simple Camera Follow

### Basic Camera Follow
```csharp
public class SimpleCameraFollow : MonoBehaviour
{
    public Transform target;
    public float followSpeed = 2f;
    public Vector3 offset = Vector3.zero;
    
    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 targetPos = target.position + offset;
            targetPos.z = transform.position.z;
            transform.position = Vector3.Lerp(
                transform.position,
                targetPos,
                followSpeed * Time.deltaTime
            );
        }
    }
}
```

### Camera Bounds
```csharp
Vector3 targetPos = target.position;
targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x, maxBounds.x);
targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y, maxBounds.y);
```

### Why LateUpdate()?
- Runs after all `Update()` calls
- Ensures camera follows after player moves
- Prevents camera jitter and lag

## Common Pitfalls

- **Null Reference**: Check if GameObject exists before accessing components
- **Execution Order**: Use `Awake()` for initialization, `Start()` for dependencies
- **Scene Loading**: Use `LoadSceneAsync()` for large scenes
- **Memory**: Destroy instantiated objects to prevent leaks
- **Input**: Use `GetKeyDown()` for actions, `GetKey()` for continuous movement
- **Camera**: Use `LateUpdate()` for smooth camera follow
