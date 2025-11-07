# Lesson 0 Reference - Game Development Fundamentals

## 🎯 Key Concepts Checklist

### **Mindset Shift**
- [ ] Understand loop-driven vs event-driven thinking
- [ ] Grasp continuous simulation concept
- [ ] Recognize performance requirements (60 FPS)
- [ ] Appreciate visual feedback importance

### **Unity Editor Basics**
- [ ] Navigate Scene view
- [ ] Use Game view for testing
- [ ] Understand Hierarchy window
- [ ] Use Inspector to modify properties
- [ ] Access Project window
- [ ] Check Console for errors

### **Game Development Concepts**
- [ ] Game loop execution
- [ ] Frame rate and delta time
- [ ] GameObject and Component model
- [ ] Scene management basics

---

## 🔄 Web vs Game Development Patterns

### **State Management**

#### **Web (React-like):**
```javascript
// Component state
const [health, setHealth] = useState(100);
setHealth(health - 10);
```

#### **Game (Unity):**
```csharp
// MonoBehaviour class
public class Player : MonoBehaviour {
    public int health = 100;

    void Update() {
        if (hit) {
            health -= 10;
        }
    }
}
```

---

### **Event Handling**

#### **Web:**
```javascript
button.addEventListener('click', handleClick);
```

#### **Game:**
```csharp
void Update() {
    if (Input.GetButtonDown("Jump")) {
        Jump();
    }
}
```

---

### **Rendering**

#### **Web:**
```javascript
// Re-render on state change
function render() {
    element.textContent = score;
}
```

#### **Game:**
```csharp
// Render every frame
void Update() {
    scoreText.text = score.ToString();
}
```

---

## 🎮 Unity Basics - Quick Reference

### **GameObject Operations**
```csharp
// Create GameObject
GameObject obj = new GameObject("MyObject");

// Find GameObject
GameObject player = GameObject.Find("Player");
GameObject player = GameObject.FindWithTag("Player");

// Destroy GameObject
Destroy(gameObject);
Destroy(gameObject, 2f); // Destroy after 2 seconds

// Instantiate (clone)
GameObject clone = Instantiate(prefab, position, rotation);
```

---

### **Component Operations**
```csharp
// Get component
Rigidbody2D rb = GetComponent<Rigidbody2D>();
SpriteRenderer sprite = GetComponent<SpriteRenderer>();

// Add component
gameObject.AddComponent<Rigidbody2D>();

// Check if component exists
if (GetComponent<Collider2D>() != null) {
    // Component exists
}
```

---

### **Transform Operations**
```csharp
// Position
transform.position = new Vector3(5, 2, 0);
transform.position = Vector3.zero;

// Movement
transform.Translate(Vector3.right * speed * Time.deltaTime);
transform.position += Vector3.up * jumpForce * Time.deltaTime;

// Rotation
transform.rotation = Quaternion.identity;
transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);

// Scale
transform.localScale = Vector3.one * 2f;
transform.localScale = new Vector3(2, 2, 1);
```

---

### **Time Operations**
```csharp
// Delta time (time since last frame)
float delta = Time.deltaTime;

// Smooth movement (frame-rate independent)
transform.position += Vector3.right * speed * Time.deltaTime;

// Time since game started
float elapsed = Time.time;

// Time scale (0 = paused, 1 = normal, 2 = 2x speed)
Time.timeScale = 1f;
```

---

### **Input Operations**
```csharp
// Keyboard input
if (Input.GetKeyDown(KeyCode.Space)) { }
if (Input.GetKey(KeyCode.W)) { }

// Mouse input
Vector3 mousePos = Input.mousePosition;
if (Input.GetMouseButtonDown(0)) { } // Left click

// Axis input (-1 to 1)
float horizontal = Input.GetAxis("Horizontal");
float vertical = Input.GetAxis("Vertical");
```

---

## 📋 Unity Editor Shortcuts

### **Navigation**
- **F**: Focus on selected GameObject in Scene view
- **Q**: Pan tool
- **W**: Move tool
- **E**: Rotate tool
- **R**: Scale tool
- **T**: Rect tool

### **Play Mode**
- **Ctrl/Cmd + P**: Play/Pause
- **Ctrl/Cmd + Shift + P**: Step frame

### **Scene Management**
- **Ctrl/Cmd + S**: Save scene
- **Ctrl/Cmd + Shift + S**: Save all

### **GameObject Operations**
- **Ctrl/Cmd + D**: Duplicate
- **Delete**: Remove GameObject
- **F2**: Rename

---

## 🎯 Common Game Development Patterns

### **1. GameManager Singleton**
```csharp
public class GameManager : MonoBehaviour {
    public static GameManager Instance;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
}
```

### **2. Component Access Pattern**
```csharp
private Rigidbody2D rb;

void Start() {
    rb = GetComponent<Rigidbody2D>();
    if (rb == null) {
        Debug.LogError("Rigidbody2D missing!");
    }
}
```

### **3. Movement Pattern**
```csharp
void Update() {
    float horizontal = Input.GetAxis("Horizontal");
    transform.position += Vector3.right * horizontal * speed * Time.deltaTime;
}
```

---

## 📊 Unity Execution Order

```
GameObject Created
    │
    ├── Awake()         // First, even if disabled
    │
    ├── OnEnable()      // When GameObject becomes active
    │
    ├── Start()         // Before first Update
    │
    └── Update Loop:
        ├── Update()    // Every frame
        ├── LateUpdate() // After all Updates
        └── FixedUpdate() // Fixed timestep (physics)
```

---

## 🐛 Debugging Basics

### **Debug.Log**
```csharp
Debug.Log("Message here");
Debug.LogWarning("Warning message");
Debug.LogError("Error message");

// With variables
Debug.Log($"Player position: {transform.position}");
Debug.Log("Score: " + score);
```

### **Visual Debugging**
```csharp
// Draw line in Scene view
Debug.DrawLine(startPos, endPos, Color.red);

// Draw ray
Debug.DrawRay(startPos, direction * distance, Color.green);
```

### **Inspector Debugging**
```csharp
// Show in Inspector
[SerializeField] private float debugValue;

// Header for organization
[Header("Movement Settings")]
public float speed = 5f;
```

---

## 🎮 First Game Checklist

### **Simple Bouncing Ball Game**
- [ ] Create new 2D project
- [ ] Create scene
- [ ] Add ball GameObject
- [ ] Add SpriteRenderer component
- [ ] Add Rigidbody2D component
- [ ] Add Collider2D component
- [ ] Create ground GameObject
- [ ] Add Collider2D to ground
- [ ] Press Play and test
- [ ] Add script for custom behavior

---

## 💡 Key Takeaways

1. **Game development is loop-driven**, not event-driven
2. **Performance matters**: Target 60 FPS (16ms per frame)
3. **Visual feedback is critical** for good gameplay
4. **GameObject = container, Components = behavior**
5. **Time.deltaTime** ensures frame-rate independent movement
6. **Playtest constantly** - code working ≠ game feeling good

---

**Next**: Move to Lesson 1 to learn Unity technical fundamentals!
