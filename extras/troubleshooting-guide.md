# Unity Course Troubleshooting Guide

## 🎯 Overview

This guide helps you solve common issues encountered during the Unity course. Most problems have simple solutions once you know what to look for.

---

## 🚨 Common Issues & Solutions

### **Project Setup Issues**

#### Issue: Unity Hub won't open or crashes
**Symptoms**: Unity Hub doesn't launch, shows error messages
**Solutions**:
- Restart your computer
- Update Unity Hub to latest version
- Check system requirements (Windows 10+, macOS 10.15+)
- Run as administrator (Windows)

#### Issue: Can't create new project
**Symptoms**: "Create project" button grayed out, project creation fails
**Solutions**:
- Check available disk space (need 25GB+)
- Verify Unity LTS version is installed
- Try different project location
- Restart Unity Hub

#### Issue: Project opens but shows errors
**Symptoms**: Red error messages in Console, missing components
**Solutions**:
- Check Unity version compatibility
- Reimport all assets: `Assets → Reimport All`
- Delete Library folder and reopen project
- Verify all required packages are installed

---

### **Script Compilation Issues**

#### Issue: Scripts won't compile
**Symptoms**: Red error messages, scripts show syntax errors
**Solutions**:
- Check C# syntax (missing semicolons, brackets)
- Verify script names match class names
- Check for typos in variable names
- Ensure scripts are in correct folder structure

#### Issue: "Script not found" errors
**Symptoms**: Missing script components, broken references
**Solutions**:
- Reattach scripts to GameObjects
- Check script file names match class names
- Verify scripts are in Assets folder
- Reimport scripts: Right-click → Reimport

#### Issue: Input System errors
**Symptoms**: "Input System not found" or binding errors
**Solutions**:
- Install Input System package: `Window → Package Manager → Input System`
- Regenerate Input Actions: Right-click → Regenerate C# Class
- Check Input Action asset is assigned to PlayerInput component

---

### **Scene and Build Issues**

#### Issue: Objects not visible in Game view
**Symptoms**: Scene view shows objects but Game view is empty
**Solutions**:
- Check camera position (Z should be negative for 2D)
- Verify camera is set to Orthographic
- Check object positions (Z should be 0 for 2D sprites)
- Ensure objects are within camera view

#### Issue: Scene switching doesn't work
**Symptoms**: SceneManager.LoadScene() fails, scenes don't load
**Solutions**:
- Add scenes to Build Settings: `File → Build Settings → Add Open Scenes`
- Check scene names match exactly (case-sensitive)
- Verify scenes are saved before building
- Use SceneManager.LoadSceneAsync() for large scenes

#### Issue: Build fails or won't run
**Symptoms**: Build process fails, executable won't start
**Solutions**:
- Check Build Settings configuration
- Verify target platform is correct
- Check for compilation errors before building
- Try building to different location

---

### **Physics and Animation Issues**

#### Issue: Physics not working correctly
**Symptoms**: Objects don't fall, collisions don't detect
**Solutions**:
- Check Rigidbody2D is attached
- Verify colliders are not triggers
- Check physics layers and collision matrix
- Ensure FixedUpdate() is used for physics code

#### Issue: Animations not playing
**Symptoms**: Animator shows states but no animation
**Solutions**:
- Check Animator Controller is assigned
- Verify animation clips are assigned to states
- Check transition conditions are met
- Ensure Animator component is enabled

#### Issue: Animation events not firing
**Symptoms**: Animation plays but events don't trigger
**Solutions**:
- Check event methods are public
- Verify event names match exactly
- Ensure events are placed on animation timeline
- Check method signatures match event parameters

---

### **UI and Input Issues**

#### Issue: UI not responding to input
**Symptoms**: Buttons don't click, UI elements not interactive
**Solutions**:
- Check EventSystem is in scene
- Verify UI elements have GraphicRaycaster
- Check Canvas is set to Screen Space - Overlay
- Ensure UI elements are not behind other objects

#### Issue: Input not working
**Symptoms**: Keyboard/gamepad input not detected
**Solutions**:
- Check Input Actions are properly configured
- Verify PlayerInput component is attached
- Check input bindings are correct
- Ensure Input System package is installed

#### Issue: Camera follow is jittery
**Symptoms**: Camera stutters when following player
**Solutions**:
- Use LateUpdate() for camera movement
- Add smoothing with Vector3.Lerp()
- Check if player movement is in FixedUpdate()
- Use Cinemachine for professional camera control

---

### **Performance Issues**

#### Issue: Game runs slowly
**Symptoms**: Low FPS, stuttering, lag
**Solutions**:
- Check Unity Profiler for bottlenecks
- Reduce draw calls by using Sprite Atlas
- Optimize physics timestep
- Use object pooling for frequently created objects

#### Issue: Memory usage is high
**Symptoms**: Game uses too much RAM
**Solutions**:
- Check for memory leaks in scripts
- Use Resources.UnloadUnusedAssets()
- Avoid creating objects in Update()
- Use object pooling instead of Instantiate/Destroy

---

## 🔧 Debugging Tools

### **Console Window**
- **Window → General → Console**
- Shows errors, warnings, and Debug.Log messages
- Click on error messages to jump to problematic code
- Clear console: Right-click → Clear

### **Unity Profiler**
- **Window → Analysis → Profiler**
- Shows performance metrics in real-time
- Identifies CPU, GPU, and memory bottlenecks
- Essential for optimization

### **Scene View Debugging**
- **Gizmos**: Visualize colliders, triggers, and custom shapes
- **Wireframe Mode**: See object wireframes
- **Overdraw Mode**: Check for overdraw issues

### **Debug.Log() Usage**
```csharp
// Basic logging
Debug.Log("Player position: " + transform.position);

// Conditional logging
Debug.LogWarning("Health is low: " + health);

// Error logging
Debug.LogError("Failed to load scene: " + sceneName);

// Format strings
Debug.LogFormat("Player {0} has {1} health", playerName, health);
```

---

## 📋 Pre-Flight Checklist

Before asking for help, check these common issues:

### **Project Setup**
- [ ] Unity 6.2 (6000.2.10f1) installed and updated
- [ ] Project created with 2D template
- [ ] All required packages installed
- [ ] No compilation errors in Console

### **Script Issues**
- [ ] Scripts compile without errors
- [ ] Script names match class names
- [ ] Scripts are attached to GameObjects
- [ ] All public variables are assigned in Inspector

### **Scene Issues**
- [ ] Objects are visible in Scene view
- [ ] Camera is positioned correctly
- [ ] All scenes are added to Build Settings
- [ ] No missing references in Inspector

### **Build Issues**
- [ ] No compilation errors
- [ ] Target platform is correct
- [ ] Build Settings are configured
- [ ] All required scenes are included

---

## 🆘 Getting Help

### **When to Ask for Help**
- You've tried all solutions in this guide
- Error messages are unclear
- Issue persists after restarting Unity
- Problem affects multiple projects

### **Information to Provide**
- Unity version and platform
- Exact error messages
- Steps to reproduce the issue
- What you've already tried
- Screenshots of the problem

### **Additional Resources**
- [Unity Manual](https://docs.unity3d.com/Manual/)
- [Unity Forums](https://forum.unity.com/)
- [Unity Learn](https://learn.unity.com/)
- [Stack Overflow](https://stackoverflow.com/questions/tagged/unity3d)

---

## 🎯 Prevention Tips

### **Best Practices**
- Save your project frequently (Ctrl+S)
- Use version control (Git) for important projects
- Test builds regularly during development
- Keep Unity and packages updated
- Follow naming conventions consistently

### **Project Organization**
- Keep scripts in organized folders
- Use meaningful names for GameObjects
- Document complex code with comments
- Create prefabs for reusable objects
- Use tags and layers appropriately

---