# Lab 1: Unity Basics

## 🎯 Learning Objectives

- Master Unity's GameObject and Component system
- Understand the MonoBehaviour lifecycle and execution order
- Learn scene management and prefab creation
- Practice with Unity's debug tools and console
- Build a simple scene management demo

## 🎮 Playtest Criteria

**Complete when you can:**
- [ ] Explain the GameObject/Component architecture
- [ ] Demonstrate MonoBehaviour lifecycle methods
- [ ] Create and manage scenes effectively
- [ ] Build and use prefabs in your project
- [ ] Use debug tools to troubleshoot issues

---

## 🚀 Quick Start

### Step 1: Create New 2D Project

1. **Open Unity Hub** → Click **"New project"**
2. **Select Template**: Choose **"2D (URP)"** template
3. **Project Name**: `Lesson1-UnityBasics`
4. **Location**: Choose your preferred folder
5. **Click "Create project"** and wait for Unity to load

### Step 2: Explore the Example

1. **Open the `example/` folder** in this lesson
2. **Import the provided scripts** into your project:
   - Copy scripts to `Assets/Scripts/` folder
   - Unity will automatically compile them
3. **Create a new scene**: `File → New Scene → 2D`
4. **Add the example scripts** to GameObjects and test them

---

## 🎯 Lab Tasks

### Task 1: GameObject and Component System

#### **1.1 Create Basic GameObjects**

**Create a Player GameObject:**
1. **Right-click in Hierarchy** → **2D Object** → **Sprite** → **Square**
2. **Rename** to "Player"
3. **In Inspector**, examine the components:
   - **Transform**: Position, Rotation, Scale
   - **Sprite Renderer**: Visual appearance
4. **Add Component** → **Rigidbody2D**
5. **Add Component** → **BoxCollider2D**

**Create an Enemy GameObject:**
1. **Right-click in Hierarchy** → **2D Object** → **Sprite** → **Circle**
2. **Rename** to "Enemy"
3. **In Inspector**, modify properties:
   - **Transform**: Position (5, 0, 0)
   - **Sprite Renderer**: Color (Red)
4. **Add Component** → **Rigidbody2D**
5. **Add Component** → **CircleCollider2D**

**Create a Platform GameObject:**
1. **Right-click in Hierarchy** → **2D Object** → **Sprite** → **Square**
2. **Rename** to "Platform"
3. **In Inspector**, modify properties:
   - **Transform**: Position (0, -3, 0), Scale (10, 1, 1)
   - **Sprite Renderer**: Color (Green)
4. **Add Component** → **BoxCollider2D**
5. **Set Rigidbody2D** to **Static** (no physics movement)

#### **1.2 Understand Component Relationships**

**Examine the Transform Component:**
- **Position**: Where the object is in 3D space
- **Rotation**: How the object is oriented
- **Scale**: How big the object is

**Examine the Sprite Renderer Component:**
- **Sprite**: The image displayed
- **Color**: Tint color applied
- **Material**: Shader used for rendering
- **Sorting Layer**: Rendering order
- **Order in Layer**: Priority within layer

**Examine the Rigidbody2D Component:**
- **Body Type**: Dynamic, Kinematic, or Static
- **Material**: Physics material properties
- **Mass**: How heavy the object is
- **Gravity Scale**: How gravity affects the object

#### **1.3 Practice Component Manipulation**

**Modify Transform Properties:**
1. **Select Player** in Hierarchy
2. **In Inspector**, change Position to (0, 2, 0)
3. **Change Scale** to (2, 2, 1) to make it bigger
4. **Rotate** by changing Rotation to (0, 0, 45)

**Modify Sprite Renderer Properties:**
1. **Select Player** in Hierarchy
2. **In Inspector**, change Color to Blue
3. **Change Sorting Layer** to "Default"
4. **Set Order in Layer** to 1

**Modify Rigidbody2D Properties:**
1. **Select Player** in Hierarchy
2. **In Inspector**, change Mass to 2
3. **Set Gravity Scale** to 1.5
4. **Enable Freeze Position** X to prevent horizontal movement

### Task 2: MonoBehaviour Lifecycle Demonstration

#### **2.1 Create Lifecycle Demo Script**

**Create the Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "LifecycleDemo"
3. **Double-click** to open in code editor
4. **Replace the content** with:

```csharp
using UnityEngine;

public class LifecycleDemo : MonoBehaviour
{
    [Header("Lifecycle Settings")]
    public string objectName = "LifecycleDemo";
    public bool showDebugInfo = true;
    
    void Awake()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: Awake() called");
    }
    
    void Start()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: Start() called");
    }
    
    void Update()
    {
        if (showDebugInfo && Input.GetKeyDown(KeyCode.Space))
            Debug.Log($"{objectName}: Update() - Space pressed");
    }
    
    void FixedUpdate()
    {
        if (showDebugInfo && Input.GetKeyDown(KeyCode.F))
            Debug.Log($"{objectName}: FixedUpdate() - F pressed");
    }
    
    void LateUpdate()
    {
        if (showDebugInfo && Input.GetKeyDown(KeyCode.L))
            Debug.Log($"{objectName}: LateUpdate() - L pressed");
    }
    
    void OnEnable()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: OnEnable() called");
    }
    
    void OnDisable()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: OnDisable() called");
    }
    
    void OnDestroy()
    {
        if (showDebugInfo)
            Debug.Log($"{objectName}: OnDestroy() called");
    }
}
```

#### **2.2 Test Lifecycle Methods**

**Attach Script to Player:**
1. **Select Player** in Hierarchy
2. **Drag LifecycleDemo script** from Project to Inspector
3. **Configure settings** in Inspector if needed

**Test Each Method:**
1. **Click Play** to start the game
2. **Check Console** for Awake() and Start() messages
3. **Press Space** to trigger Update() method
4. **Press F** to trigger FixedUpdate() method
5. **Press L** to trigger LateUpdate() method

**Test Enable/Disable:**
1. **In Inspector**, uncheck the **LifecycleDemo** component
2. **Check Console** for OnDisable() message
3. **Re-enable** the component
4. **Check Console** for OnEnable() message

**Test Destroy:**
1. **Select Player** in Hierarchy
2. **Press Delete** to destroy the object
3. **Check Console** for OnDestroy() message

#### **2.3 Understand Execution Order**

**Create Multiple Objects:**
1. **Duplicate Player** (Ctrl+D) to create Player (1)
2. **Attach LifecycleDemo** to Player (1) with different settings
3. **Test execution order** by playing the scene

**Observe the Pattern:**
- **Awake()**: Called when object is created
- **OnEnable()**: Called when object becomes active
- **Start()**: Called before first Update()
- **Update()**: Called every frame
- **FixedUpdate()**: Called at fixed intervals
- **LateUpdate()**: Called after all Updates()
- **OnDisable()**: Called when object becomes inactive
- **OnDestroy()**: Called when object is destroyed

### Task 3: Scene Management

#### **3.1 Create Multiple Scenes**

**Create Main Menu Scene:**
1. **File → New Scene** → **2D**
2. **File → Save Scene As** → Name it "MainMenu"
3. **Create UI elements**:
   - **Right-click in Hierarchy** → **UI** → **Text**
   - **Rename** to "TitleText"
   - **In Inspector**, set Text to "My Game"
   - **Position** in center of screen

**Create Game Scene:**
1. **File → New Scene** → **2D**
2. **File → Save Scene As** → Name it "Gameplay"
3. **Add Player and Platform** from previous tasks
4. **Position objects** appropriately

**Create Settings Scene:**
1. **File → New Scene** → **2D**
2. **File → Save Scene As** → Name it "Settings"
3. **Create UI elements**:
   - **Right-click in Hierarchy** → **UI** → **Text**
   - **Rename** to "SettingsText"
   - **In Inspector**, set Text to "Settings"
   - **Position** in center of screen

#### **3.2 Create Scene Manager Script**

**Create the Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "SceneManager"
3. **Double-click** to open in code editor
4. **Replace the content** with:

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    [Header("Scene Management")]
    public string mainMenuScene = "MainMenu";
    public string gameplayScene = "Gameplay";
    public string settingsScene = "Settings";
    
    void Update()
    {
        // Scene navigation with keyboard
        if (Input.GetKeyDown(KeyCode.Alpha1))
            LoadScene(mainMenuScene);
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
            LoadScene(gameplayScene);
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
            LoadScene(settingsScene);
        
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }
    
    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadMainMenu()
    {
        LoadScene(mainMenuScene);
    }
    
    public void LoadGameplay()
    {
        LoadScene(gameplayScene);
    }
    
    public void LoadSettings()
    {
        LoadScene(settingsScene);
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
        
        // For testing in editor
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
```

#### **3.3 Test Scene Management**

**Add Scene Manager to Each Scene:**
1. **Create Empty GameObject** in each scene
2. **Rename** to "SceneManager"
3. **Attach SceneManager script** to the object
4. **Configure scene names** in Inspector

**Test Scene Switching:**
1. **Click Play** to start the game
2. **Press 1** to load Main Menu
3. **Press 2** to load Gameplay
4. **Press 3** to load Settings
5. **Press Escape** to quit

### Task 4: Prefab Creation and Usage

#### **4.1 Create Player Prefab**

**Create the Prefab:**
1. **Select Player** in Hierarchy
2. **Drag Player** from Hierarchy to Project window
3. **Rename** the prefab to "PlayerPrefab"
4. **Delete the original Player** from Hierarchy

**Test the Prefab:**
1. **Drag PlayerPrefab** from Project to Hierarchy
2. **Position** it at (0, 0, 0)
3. **Test** that it works the same as before

#### **4.2 Create Enemy Prefab**

**Create the Prefab:**
1. **Select Enemy** in Hierarchy
2. **Drag Enemy** from Hierarchy to Project window
3. **Rename** the prefab to "EnemyPrefab"
4. **Delete the original Enemy** from Hierarchy

**Test the Prefab:**
1. **Drag EnemyPrefab** from Project to Hierarchy
2. **Position** it at (5, 0, 0)
3. **Test** that it works the same as before

#### **4.3 Create Prefab Spawner Script**

**Create the Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "PrefabSpawner"
3. **Double-click** to open in code editor
4. **Replace the content** with:

```csharp
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefab Spawning")]
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    
    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public int maxEnemies = 5;
    
    private float lastSpawnTime;
    private int enemyCount;
    
    void Start()
    {
        // Spawn player at start
        if (playerPrefab != null)
        {
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Player spawned!");
        }
    }
    
    void Update()
    {
        // Spawn enemies at intervals
        if (enemyPrefab != null && enemyCount < maxEnemies)
        {
            if (Time.time - lastSpawnTime >= spawnInterval)
            {
                SpawnEnemy();
                lastSpawnTime = Time.time;
            }
        }
    }
    
    void SpawnEnemy()
    {
        Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
        spawnPos += new Vector3(Random.Range(-5f, 5f), Random.Range(-2f, 2f), 0);
        
        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        enemyCount++;
        
        Debug.Log($"Enemy spawned! Count: {enemyCount}");
    }
    
    public void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            Vector3 spawnPos = spawnPoint != null ? spawnPoint.position : Vector3.zero;
            Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            Debug.Log("Player spawned manually!");
        }
    }
    
    public void SpawnEnemyManual()
    {
        if (enemyPrefab != null && enemyCount < maxEnemies)
        {
            SpawnEnemy();
        }
    }
}
```

#### **4.4 Test Prefab Spawning**

**Setup the Spawner:**
1. **Create Empty GameObject** in Hierarchy
2. **Rename** to "PrefabSpawner"
3. **Attach PrefabSpawner script** to the object
4. **In Inspector**, assign:
   - **Player Prefab**: Drag PlayerPrefab from Project
   - **Enemy Prefab**: Drag EnemyPrefab from Project
   - **Spawn Point**: Create empty GameObject as spawn point

**Test Spawning:**
1. **Click Play** to start the game
2. **Watch** as player spawns automatically
3. **Wait** for enemies to spawn at intervals
4. **Check Console** for spawn messages

### Task 5: Debug Tools and Console

#### **5.1 Use Debug.Log()**

**Add Debug Messages:**
1. **Open LifecycleDemo script**
2. **Add more debug messages** to different methods
3. **Test** by playing the scene
4. **Check Console** for all messages

**Example Debug Messages:**
```csharp
void Update()
{
    if (Input.GetKeyDown(KeyCode.Space))
    {
        Debug.Log("Space key pressed!");
        Debug.Log($"Current position: {transform.position}");
        Debug.Log($"Current rotation: {transform.rotation}");
    }
}
```

#### **5.2 Use Debug.DrawRay()**

**Add Visual Debugging:**
1. **Open LifecycleDemo script**
2. **Add visual debugging** to Update method
3. **Test** by playing the scene
4. **Look in Scene view** for debug lines

**Example Visual Debug:**
```csharp
void Update()
{
    // Draw a line from object to mouse position
    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    mousePos.z = 0;
    Debug.DrawRay(transform.position, mousePos - transform.position, Color.red);
}
```

#### **5.3 Use Inspector Debugging**

**Add Public Variables:**
1. **Open LifecycleDemo script**
2. **Add public variables** for debugging
3. **Test** by modifying values in Inspector
4. **Observe** changes in real-time

**Example Public Variables:**
```csharp
[Header("Debug Settings")]
public bool showDebugInfo = true;
public Color debugColor = Color.red;
public float debugScale = 1f;
```

#### **5.4 Use Gizmos for Debugging**

**Add Gizmo Drawing:**
1. **Open LifecycleDemo script**
2. **Add OnDrawGizmos method**
3. **Test** by playing the scene
4. **Look in Scene view** for gizmos

**Example Gizmo Drawing:**
```csharp
void OnDrawGizmos()
{
    if (showDebugInfo)
    {
        Gizmos.color = debugColor;
        Gizmos.DrawWireSphere(transform.position, debugScale);
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.up * 2f);
    }
}
```

---

## ✅ Completion Checklist

### **GameObject and Component System**
- [ ] **Created basic GameObjects** (Player, Enemy, Platform)
- [ ] **Understood component relationships** and properties
- [ ] **Modified component values** in Inspector
- [ ] **Added and removed components** as needed

### **MonoBehaviour Lifecycle**
- [ ] **Created LifecycleDemo script** with all lifecycle methods
- [ ] **Tested each lifecycle method** and observed execution order
- [ ] **Understood when each method** is called
- [ ] **Used debug messages** to track execution

### **Scene Management**
- [ ] **Created multiple scenes** (MainMenu, Gameplay, Settings)
- [ ] **Implemented SceneManager script** for scene switching
- [ ] **Tested scene navigation** with keyboard input
- [ ] **Understood scene loading** and unloading

### **Prefab System**
- [ ] **Created Player and Enemy prefabs** from GameObjects
- [ ] **Implemented PrefabSpawner script** for dynamic spawning
- [ ] **Tested prefab instantiation** and spawning
- [ ] **Understood prefab benefits** and usage

### **Debug Tools**
- [ ] **Used Debug.Log()** for console output
- [ ] **Used Debug.DrawRay()** for visual debugging
- [ ] **Used Inspector debugging** with public variables
- [ ] **Used Gizmos** for scene view debugging

### **Basic Input System**
- [ ] **Created BasicInput script** with movement and action inputs
- [ ] **Tested keyboard input** (WASD, Arrow Keys, Space, X, E)
- [ ] **Understood difference** between GetKey(), GetKeyDown(), and GetAxis()
- [ ] **Applied input best practices** for different action types

### **Simple Camera Follow**
- [ ] **Created SimpleCameraFollow script** with smooth following
- [ ] **Set up camera** to follow player target
- [ ] **Tested camera follow** with different speeds
- [ ] **Experimented with camera offset** and bounds
- [ ] **Understood LateUpdate()** for smooth camera movement

---

### Task 6: Basic Input System

#### **6.1 Create Simple Movement Script**

**Create the Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "BasicInput"
3. **Double-click** to open in code editor
4. **Use the example script** from `example/BasicInput.cs` or create your own

**Test Basic Movement:**
1. **Create a Player GameObject** (if not already created)
2. **Attach BasicInput script** to Player
3. **Configure settings** in Inspector:
   - **Move Speed**: 5
   - **Show Debug Info**: true
4. **Click Play** and test:
   - **WASD** or **Arrow Keys** to move
   - **Space** to jump (debug message)
   - **X** or **Mouse Button** to attack (debug message)
   - **E** to interact (debug message)

#### **6.2 Test Input Axes**

**Alternative Input Method:**
1. **Open BasicInput script**
2. **Uncomment** the Input.GetAxis lines:
   ```csharp
   float horizontal = Input.GetAxis("Horizontal");
   float vertical = Input.GetAxis("Vertical");
   ```
3. **Comment out** the manual key checking code
4. **Test** that movement works the same way

**Observe Differences:**
- **Input.GetAxis()** provides smooth analog-like input
- **Input.GetKey()** provides digital on/off input
- Both methods work, choose based on your needs

#### **6.3 Input Best Practices**

**Practice Different Input Methods:**
1. **GetKeyDown()**: For actions that happen once (jump, attack)
2. **GetKey()**: For continuous actions (movement, holding)
3. **GetKeyUp()**: For actions on release (release jump early)

**Test Each Method:**
- Create scripts that use each input method
- Observe the behavior differences
- Understand when to use each method

### Task 7: Simple Camera Follow

#### **7.1 Create Camera Follow Script**

**Create the Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "SimpleCameraFollow"
3. **Double-click** to open in code editor
4. **Use the example script** from `example/SimpleCameraFollow.cs` or create your own

**Setup Camera:**
1. **Select Main Camera** in Hierarchy
2. **Attach SimpleCameraFollow script** to Main Camera
3. **In Inspector**, assign:
   - **Target**: Drag Player GameObject from Hierarchy
   - **Follow Speed**: 2 (adjust as needed)
   - **Offset**: (0, 0, 0) or customize

#### **7.2 Test Camera Follow**

**Test Basic Following:**
1. **Click Play** to start the game
2. **Move Player** using WASD or Arrow Keys
3. **Observe** camera smoothly following the player
4. **Adjust Follow Speed** in Inspector (try values 1, 2, 5, 10)
5. **Observe** how different speeds feel

**Test Camera Offset:**
1. **In Inspector**, change Offset to (0, 2, 0)
2. **Test** that camera follows with offset above player
3. **Experiment** with different offset values

#### **7.3 Camera Bounds (Optional)**

**Enable Bounds:**
1. **In Inspector**, check **Use Bounds**
2. **Set Min Bounds**: (-10, -5)
3. **Set Max Bounds**: (10, 5)
4. **Test** that camera stops at bounds
5. **Observe** yellow wireframe in Scene view showing bounds

**Why Use Bounds:**
- **Prevent camera** from showing outside game world
- **Keep camera** within level boundaries
- **Professional games** use camera bounds

#### **7.4 Understand LateUpdate()**

**Why LateUpdate()?**
1. **Create a simple test**: Move player in Update()
2. **Camera follows** in LateUpdate()
3. **Observe** smooth following without jitter
4. **Try moving camera** in Update() instead
5. **Notice** potential jitter or lag

**Key Takeaway:**
- **Update()**: Player moves
- **LateUpdate()**: Camera follows after player moves
- **Prevents** camera lag and jitter

---

## 🚨 Troubleshooting

### **Common Issues and Solutions**

#### **Scripts not compiling**
**Possible causes:**
- Syntax errors in code
- Missing semicolons or brackets
- Incorrect method signatures

**Solutions:**
1. Check Console for compilation errors
2. Verify script syntax is correct
3. Ensure all methods are properly closed
4. Check for typos in method names

#### **Components not working**
**Possible causes:**
- Scripts not attached to GameObjects
- Missing required components
- Incorrect component configuration

**Solutions:**
1. Verify scripts are attached to GameObjects
2. Check that all required components are present
3. Verify component settings in Inspector
4. Test with simple debug messages

#### **Scenes not loading**
**Possible causes:**
- Scene names don't match
- Scenes not added to Build Settings
- Incorrect scene loading code

**Solutions:**
1. Check scene names in code match actual scene names
2. Add scenes to Build Settings (File → Build Settings)
3. Verify SceneManager.LoadScene() calls
4. Test with simple scene switching

#### **Prefabs not spawning**
**Possible causes:**
- Prefab references not assigned
- Spawn point not set
- Spawning code not executing

**Solutions:**
1. Verify prefab references are assigned in Inspector
2. Check spawn point is set and positioned correctly
3. Add debug messages to spawning methods
4. Test with simple instantiation

---

## 📝 Self-Assessment

After completing this lab, test your understanding with the interactive quiz:

**Take the Quiz**: [`quiz/quiz1.html`](../quiz/quiz1.html)

The quiz will help you:
- Verify your understanding of key concepts
- Identify areas that need review
- Prepare for the next lesson

---

## 📚 Next Steps

### **Immediate Next Steps**
1. **Complete all tasks** in this lab
2. **Test your scene management** thoroughly
3. **Experiment with different prefabs** and spawning
4. **Practice using debug tools** for troubleshooting

### **Prepare for Next Lesson**
1. **Review GameObject/Component** system understanding
2. **Understand MonoBehaviour lifecycle** and execution order
3. **Practice with scenes** and prefabs
4. **Read Lesson 2 materials** in advance

### **Further Learning**
1. **Unity Learn tutorials** for additional practice
2. **Unity documentation** for deeper understanding
3. **Community forums** for questions and help
4. **Practice projects** to reinforce learning

---

## 💡 Pro Tips

### **Development Workflow**
- **Save frequently**: Ctrl+S for scene, Ctrl+Shift+S for project
- **Test regularly**: Use Play mode to check functionality
- **Debug systematically**: Use Console and Inspector
- **Document your learning**: Keep notes on what you discover

### **Learning Strategy**
- **Don't rush**: Take time to understand each concept
- **Experiment freely**: Try different values and see what happens
- **Ask questions**: Don't hesitate to seek help
- **Practice regularly**: Consistent practice is key to mastery

### **Unity Best Practices**
- **Use meaningful names**: For GameObjects, scripts, and variables
- **Organize your project**: Keep assets in appropriate folders
- **Comment your code**: Explain complex logic for future reference
- **Test thoroughly**: Verify functionality before moving on

---

