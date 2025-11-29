# Lesson 1 Example: Unity Basics

## 🎯 What This Example Teaches

This example demonstrates Unity's core concepts:
- **GameObject/Component System**: How Unity organizes game objects
- **MonoBehaviour Lifecycle**: Awake, Start, Update execution order
- **Basic Input System**: Keyboard input handling (WASD, Arrow keys, Space, etc.)
- **Simple Camera Follow**: Basic camera following system
- **Scene Management**: Loading and switching between scenes
- **Debug Tools**: Using Console, Gizmos, and Debug.Log
- **Transform Operations**: Moving, rotating, and scaling objects

## 🚀 How to Use This Example

### Step 1: Setup the Scene
1. Create a new 2D scene in Unity
2. Create several empty GameObjects with different names
3. Add the example scripts to different GameObjects

### Step 2: Add the Scripts
1. **TransformBasics.cs** → Add to a GameObject to test movement and transform operations
2. **BasicInput.cs** → Add to a player GameObject for keyboard input (WASD, Arrow keys)
3. **SimpleCameraFollow.cs** → Add to Main Camera for basic camera following
4. **SceneManagement.cs** → Add to an empty GameObject for scene switching
5. **DebugTools.cs** → Add to any GameObject for debug utilities
6. **NamingConventions.cs** → Reference for proper naming

### Step 3: Test Each Component
1. **TransformBasics**: Press W/E/R to move, rotate, and scale the object
2. **BasicInput**: Use WASD or Arrow keys to move, Space to jump, X to attack
3. **SimpleCameraFollow**: Assign a player GameObject as target, camera will follow smoothly
4. **SceneManagement**: Press number keys (1, 2, 3) to switch scenes
5. **DebugTools**: Check Console for debug messages and use debug features

### Step 4: Observe the Lifecycle
1. Press Play and watch the Console
2. Notice the order: Awake → Start → Update
3. Try enabling/disabling GameObjects to see OnEnable/OnDisable

## 🔧 Troubleshooting

**Scripts don't work**: Make sure all required components are attached
**Console errors**: Check that scenes exist for SceneManagement
**No movement**: Verify Input System is enabled
**Lifecycle issues**: Check Console for execution order

## 💡 Learning Points

- **GameObject**: Container for components
- **Component**: Modular functionality (Transform, Script, etc.)
- **MonoBehaviour**: Base class for Unity scripts
- **Lifecycle**: Awake → Start → Update → LateUpdate
- **Basic Input**: Using Input.GetKey(), Input.GetKeyDown(), Input.GetAxis()
- **Camera Follow**: Using LateUpdate() for smooth camera following
- **Scene Management**: Loading and switching scenes
- **Debug Tools**: Console, Gizmos, Debug.Log

**Note**: Basic input and camera follow from this lesson are foundational. Advanced Input System and professional camera systems (Cinemachine) will be covered in Lesson 4.

---

**Next Steps**: 
1. Complete the lab exercises to practice these concepts
2. After completing the lab, test your knowledge with the [quiz](../quiz/quiz1.html).