# Lesson 0 Example: Game Development Fundamentals

This folder contains working examples to help you understand game development fundamentals.

---

## 📁 Example Files

### **1. BouncingBall.cs**
Simple physics-based bouncing ball demonstrating:
- Rigidbody2D physics
- Collision detection
- Basic movement

**How to Use:**
1. Create a GameObject (sphere/circle sprite)
2. Add Rigidbody2D component
3. Add CircleCollider2D component
4. Attach BouncingBall script
5. Press Play and watch it bounce!

---

### **2. SimplePlayerController.cs**
Basic player movement demonstrating:
- Input handling
- Transform manipulation
- Ground checking

**How to Use:**
1. Create a GameObject with SpriteRenderer
2. Add Rigidbody2D component
3. Add Collider2D component
4. Attach SimplePlayerController script
5. Use arrow keys/WASD to move, Space to jump

---

### **3. GameManager.cs**
Simple game manager demonstrating:
- Singleton pattern
- Global game state
- Score management

**How to Use:**
1. Create empty GameObject named "GameManager"
2. Attach GameManager script
3. Access from other scripts: `GameManager.Instance.AddScore(10)`

---

### **4. SimpleEnemyAI.cs**
Basic enemy AI demonstrating:
- Target following
- Simple AI behavior
- Distance calculations

**How to Use:**
1. Create enemy GameObject
2. Add Rigidbody2D component
3. Attach SimpleEnemyAI script
4. Set target in Inspector (player GameObject)

---

### **5. CameraShake.cs**
Camera shake effect demonstrating:
- Visual feedback
- Coroutine usage
- Animation effects

**How to Use:**
1. Attach to Main Camera GameObject
2. Call `StartShake()` from any script
3. Customize intensity and duration in Inspector

---

## 🚀 Quick Start Guide

### **Step 1: Create New Scene**
1. File → New Scene → 2D
2. Save as "Lesson0-Example"

### **Step 2: Import Scripts**
1. Copy scripts from this folder
2. Paste into Assets/Scripts folder in Unity

### **Step 3: Create GameObjects**
1. Create Player: GameObject → 2D Object → Sprite
2. Create Ground: GameObject → 2D Object → Sprite (scale to make platform)
3. Create GameManager: GameObject → Create Empty

### **Step 4: Add Components**
1. Player: Add Rigidbody2D, Collider2D, SimplePlayerController
2. Ground: Add Collider2D (set as Static)
3. GameManager: Add GameManager script

### **Step 5: Test**
1. Press Play (Ctrl/Cmd + P)
2. Use arrow keys to move player
3. Observe console messages

---

## 💡 Learning Tips

1. **Read the code** - Understand what each line does
2. **Modify values** - Change speed, jump force, etc.
3. **Break it** - Remove components, see what happens
4. **Experiment** - Add your own features
5. **Use Console** - Check Debug.Log messages

---

## 🎯 Example Project: Simple Bouncing Ball

### **Complete Setup:**
1. Create ball GameObject with circle sprite
2. Add BouncingBall script
3. Add Rigidbody2D (set gravity scale: 1)
4. Add CircleCollider2D
5. Create ground with BoxCollider2D
6. Press Play - ball should bounce!

---

**Next Steps**: 
1. Try modifying these examples, then create your own variations in the Lab exercises!
2. After completing the lab, test your knowledge with the [quiz](../quiz/quiz0.html).
