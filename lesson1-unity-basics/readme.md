# Lesson 1: Unity Basics

## Overview

**Difficulty**: Beginner
**Prerequisites**: Lesson 0 completed, Basic C# programming, Unity Hub + Unity 6.2 (6000.2.10f1) installed

This lesson introduces Unity's technical fundamentals: Editor, scenes, prefabs, and MonoBehaviour lifecycle. You'll also learn basic Input System and simple camera follow to create an interactive scene from the start. This approach aligns with Unity Learn best practices by introducing interactivity early for better engagement.

---

## 🎯 Learning Objectives

- Set up a 2D project and navigate the Unity Editor efficiently
- Understand GameObject/Component model and MonoBehaviour lifecycle (Awake/Start/Update)
- Create and use prefabs; manage scenes and scene loading
- Implement basic Input System for keyboard input (simple movement)
- Set up simple camera follow system for player tracking
- Use simple debug tools (Gizmos, Logs) to inspect behavior

---

## 🚀 Quick Start

### Step 1: Create New 2D Project
1. Open Unity Hub → New Project → **2D (URP)** template
2. Project name: `Lesson1-UnityBasics`
3. Create project and wait for Unity to load

### Step 2: Create Test Scene
1. Create a scene named `Lesson1-Scene`
2. Save scene: `File → Save Scene As → Lesson1-Scene`

### Step 3: Add Example Scripts
1. Open the `example/` folder in this lesson
2. Import scripts into your project's Scripts folder
3. Add empty GameObjects and attach scripts:
   - `TransformBasics.cs`, `BasicInput.cs`, `SimpleCameraFollow.cs`, `SceneManagement.cs`, `DebugTools.cs`

### Step 4: Test Your Setup
1. **Press Play** and observe logs for lifecycle order
2. **Check Console** for debug messages
3. **Try switching scenes** using the SceneManagement script
4. **Test basic input**: Create a player GameObject, attach `BasicInput.cs`, move with WASD/Arrow keys
5. **Test camera follow**: Attach `SimpleCameraFollow.cs` to Main Camera, assign player as target

---

## 📚 Learning Path

- Reference → `reference/` (APIs, checklists)
- Example → `example/` (run and playtest first)
- Theory → `theory/theory1.md` (read targeted sections)
- Lab → `lab/lab1-instructions.md` (complete playtest tasks)
- Quiz → [`quiz/quiz1.html`](quiz/quiz1.html) (test your understanding)

---

## ✅ What's Next

Proceed to [Lesson 2: Sprites & Animation](../lesson2-sprites-animation/) to learn about visual game elements and animation systems.

**Next Steps:**
- Study examples in `example/` folder
- Complete exercises in `lab/` folder
- Take the quiz: [`quiz/quiz1.html`](quiz/quiz1.html)
- Explore sample project: `sample-projects/lesson1-unity-basics/`

---

## Resources & References

- Unity Manual: Getting started
- Execution Order of Event Functions
- Scenes and SceneManager API
- Prefabs workflow


