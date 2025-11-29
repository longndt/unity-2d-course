# Lesson 2 Example: Sprites & Animation

## 🎯 What This Example Teaches

This example demonstrates 2D visual systems:
- **Sprite Import**: Proper sprite setup and configuration
- **Animator Controller**: State machines and transitions
- **Animation Events**: Triggering code during animations
- **Character Control**: Moving and animating characters
- **Sprite Management**: Flipping and sorting sprites

## 🚀 How to Use This Example

### Step 1: Setup the Scene
1. Create a new 2D scene in Unity
2. Import character sprites (idle, walk, jump animations)
3. Create a character GameObject with SpriteRenderer

### Step 2: Add the Scripts
1. **Character2D.cs** → Main character controller
2. **AnimatorControl.cs** → Animation state management
3. **SpriteControl.cs** → Sprite flipping and sorting
4. **AnimationDebugger.cs** → Debug animation states
5. **CollisionHandler.cs** → Handle character collisions
6. **ComponentLifecycle.cs** → Reference for lifecycle methods

### Step 3: Setup Animation
1. Create an Animator Controller
2. Add animation clips (idle, walk, jump)
3. Set up transitions between states
4. Add parameters (Speed, IsGrounded, IsJumping)
5. Assign the controller to the character

### Step 4: Test the Character
1. Press Play and use arrow keys to move
2. Watch the character animate based on movement
3. Try jumping to see jump animation
4. Use AnimationDebugger to see current state

## 🔧 Troubleshooting

**No animation**: Check Animator Controller is assigned
**Character doesn't move**: Verify input handling in Character2D
**Animation doesn't play**: Check transition conditions
**Sprites don't flip**: Verify SpriteControl script is attached
**Console errors**: Check that all required components are present

## 💡 Learning Points

- **SpriteRenderer**: Displays 2D sprites
- **Animator Controller**: Manages animation states
- **Animation Events**: Trigger code during animations
- **Sorting Layers**: Control rendering order
- **Sprite Flipping**: Change character direction
- **State Machine**: Managing different animation states

---

**Next Steps**: 
1. Complete the lab exercises to practice these concepts
2. After completing the lab, test your knowledge with the [quiz](../quiz/quiz2.html).