# Lab 0: Game Development Fundamentals & Mindset

## 🎯 Learning Objectives

- Understand the key differences between web and game development
- Learn basic game development concepts and terminology
- Navigate Unity Editor for the first time
- Create a simple bouncing ball game
- Understand the game development workflow

## 🎮 Playtest Criteria

**Complete when you can:**
- [ ] Explain the key differences between web and game development
- [ ] Navigate Unity Editor confidently for game development
- [ ] Create a simple bouncing ball game
- [ ] Understand the game development pipeline
- [ ] Identify core gameplay loop in a game

---

## 🚀 Quick Start

### Step 1: Create New 2D Project
1. Open Unity Hub → New Project → **2D (URP)** template
2. Project name: `Lesson0-GameFundamentals`
3. Create project and wait for Unity to load

### Step 2: Explore the Example
1. Open the `example/` folder in this lesson
2. Import scripts into your project: `Assets/Scripts/`
3. Create a new scene: `File → New Scene → 2D`
4. Save scene: `File → Save Scene As → Lesson0-Lab`

---

## 📋 Lab Exercises

### **Exercise 1: Unity Editor Navigation** ⏱️ 15 minutes

**Goal**: Get comfortable with Unity Editor interface

**Tasks:**
1. **Explore Scene View**
   - Pan around: Middle mouse button drag
   - Zoom: Mouse wheel
   - Rotate: Alt + Left click drag
   - Focus on object: Select GameObject, press F

2. **Create GameObjects**
   - Right-click in Hierarchy → Create Empty
   - Right-click → 2D Object → Sprite
   - Rename objects: Select → F2 or right-click → Rename

3. **Use Inspector**
   - Select any GameObject
   - Modify Transform values (Position, Rotation, Scale)
   - Observe changes in Scene view

4. **Test Play Mode**
   - Press Play button (top center)
   - Press again to stop
   - Notice: Changes in Play mode are NOT saved

**Checkpoint**: Can you navigate Scene view and create GameObjects? ✅

---

### **Exercise 2: Simple Bouncing Ball** ⏱️ 30 minutes

**Goal**: Create your first physics-based game object

**Setup:**
1. **Create Ball**
   - GameObject → 2D Object → Sprite (circle)
   - Rename to "Ball"
   - Add Component → Rigidbody2D
   - Add Component → Circle Collider 2D

2. **Create Ground**
   - GameObject → 2D Object → Sprite (square/rectangle)
   - Rename to "Ground"
   - Scale X to 10, Y to 1 (make it wide and flat)
   - Position Y to -3 (move below ball)
   - Add Component → Box Collider 2D

3. **Configure Physics**
   - Select Ball
   - In Rigidbody2D: Gravity Scale = 1
   - Drag = 0.5 (air resistance)

4. **Test**
   - Press Play
   - Ball should fall and bounce (or stop on ground)

**Challenge**: Make ball bounce higher! (Hint: Modify Rigidbody2D properties)

**Checkpoint**: Can you make the ball bounce? ✅

---

### **Exercise 3: Add Custom Behavior** ⏱️ 30 minutes

**Goal**: Add script to control ball behavior

**Tasks:**
1. **Create Script**
   - Assets window → Right-click → Create → C# Script
   - Name it "BallController"
   - Double-click to open in IDE

2. **Write Code**
   ```csharp
   using UnityEngine;

   public class BallController : MonoBehaviour
   {
       [Header("Bounce Settings")]
       public float bounceForce = 5f;

       private Rigidbody2D rb;

       void Start()
       {
           rb = GetComponent<Rigidbody2D>();
           if (rb == null)
           {
               Debug.LogError("BallController requires Rigidbody2D component!");
               enabled = false;
           }
       }

       void Update()
       {
           // Add bounce when Space is pressed
           if (Input.GetKeyDown(KeyCode.Space) && rb != null)
           {
               rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
           }
       }
   }
   ```

3. **Attach Script**
   - Select Ball GameObject
   - Add Component → Scripts → Ball Controller
   - Set Bounce Force = 10 in Inspector

4. **Test**
   - Press Play
   - Press Space bar to bounce ball

**Checkpoint**: Can you control the ball with Space key? ✅

---

### **Exercise 4: Player Controller** ⏱️ 45 minutes

**Goal**: Create a player character you can control

**Setup:**
1. **Create Player**
   - GameObject → 2D Object → Sprite
   - Rename to "Player"
   - Add Component → Rigidbody2D
   - Add Component → Box Collider 2D

2. **Import Script**
   - Copy `SimplePlayerController.cs` from example folder
   - Place in Assets/Scripts
   - Attach to Player GameObject

3. **Create Ground**
   - Create wide ground sprite below player
   - Add Box Collider 2D
   - Ensure player can stand on it

4. **Test Movement**
   - Press Play
   - Use Arrow Keys or WASD to move
   - Press Space to jump

**Modifications to Try:**
- Change move speed in Inspector
- Change jump force
- Add more platforms to jump on

**Checkpoint**: Can you move and jump? ✅

---

### **Exercise 5: Game Manager Setup** ⏱️ 20 minutes

**Goal**: Understand global game state management

**Setup:**
1. **Create GameManager**
   - GameObject → Create Empty
   - Rename to "GameManager"
   - Add Component → Scripts → Game Manager (from examples)

2. **Access from Player**
   - Modify PlayerController to add score when jumping:
   ```csharp
   void Jump()
   {
       rb.velocity = new Vector2(rb.velocity.x, jumpForce);

       // Access GameManager singleton
       if (GameManager.Instance != null)
       {
           GameManager.Instance.AddScore(10);
       }
   }
   ```

3. **Test**
   - Press Play
   - Jump a few times
   - Check Console for score updates

**Checkpoint**: Can you see score increasing in Console? ✅

---

## 🎯 Final Project: Complete Simple Game

**Goal**: Combine everything into a complete mini-game

**Requirements:**
1. ✅ Player can move and jump
2. ✅ Ball bounces around
3. ✅ GameManager tracks score
4. ✅ Player collects something (optional)
5. ✅ Game has win/lose condition (optional)

**Time**: 60 minutes

---

## 📊 Self-Assessment

After completing lab exercises, check:

- [ ] Can navigate Unity Editor confidently
- [ ] Understand GameObject and Component model
- [ ] Can create and attach scripts
- [ ] Understand Update() runs every frame
- [ ] Can modify Inspector properties
- [ ] Can use Play mode for testing
- [ ] Can access Console for debugging

---

## 🐛 Troubleshooting

### **Problem**: Ball doesn't bounce
**Solution**:
- Check Rigidbody2D is attached
- Check Collider2D is attached to both ball and ground
- Ensure ground has Y position below ball

### **Problem**: Player doesn't move
**Solution**:
- Check script is attached to Player GameObject
- Check Rigidbody2D component exists
- Check Console for errors (red messages)

### **Problem**: Script doesn't appear in Inspector
**Solution**:
- Ensure script file is in Assets folder
- Check script compiles (no errors in Console)
- Try re-importing script: Right-click → Reimport

---

## 💡 Tips for Success

1. **Test Frequently**: Press Play often to see changes
2. **Read Console**: Check for errors and debug messages
3. **Experiment**: Change values in Inspector, see what happens
4. **Break Things**: Remove components, see what breaks
5. **Ask Questions**: Use Unity forums, Discord, Reddit

---

## ✅ Completion Checklist

- [ ] Exercise 1: Editor Navigation complete
- [ ] Exercise 2: Bouncing Ball works
- [ ] Exercise 3: Custom script attached and working
- [ ] Exercise 4: Player can move and jump
- [ ] Exercise 5: GameManager tracks score
- [ ] Final Project: Complete mini-game created

---

## 🎓 What You Learned

1. **Unity Editor Basics**: Navigation, GameObject creation
2. **Component System**: Add/remove components
3. **Scripting**: Attach C# scripts to GameObjects
4. **Game Loop**: Update() runs every frame
5. **Physics**: Rigidbody2D and Collider2D basics
6. **Debugging**: Console messages, visual debugging

---

## 📝 Self-Assessment

After completing this lab, test your understanding with the interactive quiz:

**Take the Quiz**: [`quiz/quiz0.html`](../quiz/quiz0.html)

The quiz will help you:
- Verify your understanding of key concepts
- Identify areas that need review
- Prepare for the next lesson

---

## 🚀 Next Steps

**Congratulations!** You've completed Lesson 0. You now understand:
- How game development differs from web development
- Unity Editor basics
- Basic game development workflow
- Simple game creation

**Ready for Lesson 1?** You'll learn:
- MonoBehaviour lifecycle
- Scene management
- Prefabs and instantiation
- More advanced Unity concepts

**Proceed to**: [Lesson 1: Unity Basics](../lesson1-unity-basics/)

---

**Questions?** Check `reference/` folder for quick reference or review `theory/` for deeper understanding.
