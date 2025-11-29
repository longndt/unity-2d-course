# Lesson 1: Unity Basics - Sample Project

## 🎯 Project Overview

This sample project demonstrates Unity's fundamental concepts including GameObjects, Components, MonoBehaviour lifecycle, scene management, and prefab usage. It's designed to help students understand the core Unity architecture.

## 🎮 Project Description

### **Objective**
Create a simple scene management system with interactive objects that demonstrate Unity's basic concepts.

### **Features**
- GameObject and Component system demonstration
- MonoBehaviour lifecycle visualization
- Scene switching between different areas
- Prefab creation and instantiation
- Debug tools and console output

### **Controls**
- **1-3 Keys**: Switch between scenes
- **Space**: Trigger MonoBehaviour events
- **Mouse Click**: Select and interact with objects
- **F Key**: Focus on selected object

## 🏗️ Project Structure

```
Assets/
├── Scenes/
│   ├── MainMenu.unity          # Main menu scene
│   ├── Gameplay.unity          # Gameplay scene
│   └── Settings.unity          # Settings scene
├── Scripts/
│   ├── LifecycleDemo.cs        # MonoBehaviour lifecycle demonstration
│   ├── SceneManager.cs         # Scene management system
│   ├── PrefabSpawner.cs        # Prefab instantiation
│   └── DebugHelper.cs          # Debug utilities
├── Prefabs/
│   ├── Player.prefab           # Player prefab
│   ├── Enemy.prefab            # Enemy prefab
│   └── Collectible.prefab      # Collectible prefab
├── Materials/
│   ├── BouncyMaterial.physicsMaterial2D
│   └── StickyMaterial.physicsMaterial2D
└── Sprites/
    ├── Player.png
    ├── Enemy.png
    └── Collectible.png
```

## 🎯 Learning Objectives

After studying this project, you will understand:

### **Unity Basics**
- GameObject and Component architecture
- Transform component usage
- Component lifecycle and execution order
- Scene management and switching

### **MonoBehaviour Lifecycle**
- Awake(), Start(), Update(), FixedUpdate(), LateUpdate()
- OnEnable(), OnDisable(), OnDestroy()
- Execution order and timing

### **Prefab System**
- Prefab creation and modification
- Instantiation and destruction
- Prefab variants and inheritance
- Dynamic prefab spawning

### **Scene Management**
- Scene loading and unloading
- Scene transitions
- Scene-specific objects
- Cross-scene communication

## 🔧 Setup Instructions

### **1. Open Project**
1. Launch Unity Hub
2. Click "Add" and select this project folder
3. Open the project in Unity Editor
4. Wait for assets to import

### **2. Explore the Scenes**
1. **MainMenu Scene**: Demonstrates UI and scene switching
2. **Gameplay Scene**: Shows GameObject interactions
3. **Settings Scene**: Displays settings and configuration

### **3. Test the Features**
1. **Press 1-3** to switch between scenes
2. **Press Space** to trigger lifecycle events
3. **Click objects** to select and interact
4. **Check Console** for debug messages

## 📝 Code Walkthrough

### **LifecycleDemo.cs**
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
    
    // ... other lifecycle methods
}
```

### **SceneManager.cs**
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
    }
    
    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }
}
```

### **PrefabSpawner.cs**
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
}
```

## 🎨 Visual Elements

### **GameObjects**
- **Player**: Blue square with movement capabilities
- **Enemy**: Red circle with basic AI
- **Collectible**: Yellow diamond with collection logic
- **Platforms**: Green rectangles for collision

### **Materials**
- **BouncyMaterial**: High bounciness for dynamic objects
- **StickyMaterial**: High friction for static objects

### **UI Elements**
- **Scene buttons**: Navigate between scenes
- **Debug panel**: Show current scene and object count
- **Instructions**: Display controls and information

## 🎮 Gameplay Features

### **Scene Management**
- Smooth transitions between scenes
- Scene-specific object management
- Cross-scene data persistence

### **Prefab System**
- Dynamic object spawning
- Prefab modification and inheritance
- Object pooling for performance

### **Debug Tools**
- Console output for all events
- Visual debugging with Gizmos
- Inspector debugging with public variables

## 🔧 Customization Options

### **Easy Modifications**
1. **Change spawn rates**: Modify `spawnInterval` in PrefabSpawner
2. **Add new scenes**: Create new scenes and add to SceneManager
3. **Modify prefabs**: Edit prefab properties in Inspector
4. **Add new objects**: Create new GameObjects and prefabs

### **Advanced Features**
1. **Object pooling**: Implement object reuse for better performance
2. **Scene transitions**: Add fade effects between scenes
3. **Save system**: Persist data across scenes
4. **Event system**: Implement custom events for communication

## 🐛 Common Issues

### **Scene not loading**
- **Cause**: Scene not added to Build Settings
- **Solution**: Add scenes to Build Settings (File → Build Settings)

### **Prefab not spawning**
- **Cause**: Prefab reference not assigned
- **Solution**: Assign prefab in Inspector

### **Lifecycle events not working**
- **Cause**: Script not attached to GameObject
- **Solution**: Attach script to GameObject

### **Console not showing messages**
- **Cause**: Console window not open
- **Solution**: Open Console window (Window → General → Console)

## 📚 Learning Exercises

### **Beginner Exercises**
1. **Create new GameObjects** with different components
2. **Modify prefab properties** and see changes
3. **Add new scenes** and test scene switching
4. **Experiment with lifecycle methods** and timing

### **Intermediate Exercises**
1. **Create custom prefabs** with unique behaviors
2. **Implement object pooling** for better performance
3. **Add scene transition effects** with fade in/out
4. **Create a simple inventory system** using prefabs

### **Advanced Exercises**
1. **Build a complete level editor** using prefabs
2. **Implement a save/load system** for scene data
3. **Create a modular component system** for objects
4. **Add networking support** for multiplayer scenes

## 🎯 Next Steps

After completing this project:

1. **Study the code**: Understand how each component works
2. **Experiment**: Try modifying values and adding features
3. **Move to Lesson 2**: Learn about sprites and animation
4. **Build your own**: Create a similar project with your own twist

## 💡 Pro Tips

### **Development Tips**
- **Use prefabs** for reusable objects
- **Test frequently** during development
- **Use debug tools** to understand behavior
- **Document your learning** with notes

### **Learning Tips**
- **Read the code** carefully and understand each line
- **Experiment freely** with different values
- **Ask questions** when something is unclear
- **Practice regularly** to reinforce learning

---

**Happy Learning!** This project provides a solid foundation for understanding Unity's core concepts. Take your time to explore and experiment with the code!
