# Lesson 4 Example: Input & Player Controller

## 🎯 What This Example Teaches

This example demonstrates the new Input System:
- **Input Actions**: Modern input handling system
- **Player Input**: Component-based input management
- **Event vs Polling**: Different input patterns
- **Input Buffering**: Responsive input handling
- **Multi-device Support**: Keyboard and gamepad input
- **Input Comparison**: Legacy vs New Input System

## 🚀 How to Use This Example

### Step 1: Setup the Scene
1. Create a new 2D scene in Unity
2. Create a player GameObject with Rigidbody2D
3. Set up Input System package if not already installed

### Step 2: Create Input Actions
1. Create an Input Actions asset
2. Add action maps (Player, UI)
3. Add actions (Move, Jump, Dash, Pause)
4. Configure bindings for keyboard and gamepad
5. Generate C# class from the asset

### Step 3: Add the Scripts
1. **PlayerInputController.cs** → Main input handling
2. **Player2DController.cs** → Character movement
3. **AutoInputEvents.cs** → Automatic event setup
4. **InputSystemComparison.cs** → Compare input systems

### Step 4: Configure Input
1. Add PlayerInput component to player
2. Assign the Input Actions asset
3. Set up event handling in scripts
4. Configure input buffering settings

### Step 5: Test the Input
1. Press Play and test keyboard input
2. Try gamepad input if available
3. Test input buffering and responsiveness
4. Compare with legacy input system

## 🔧 Troubleshooting

**Input not working**: Check Input Actions asset is assigned
**No movement**: Verify PlayerInput component setup
**Input lag**: Check input buffering settings
**Gamepad not working**: Verify gamepad bindings
**Script errors**: Make sure Input System package is installed

## 💡 Learning Points

- **Input Actions**: Modern input configuration
- **Player Input**: Component-based input handling
- **Action Maps**: Organize input by context
- **Input Buffering**: Improve input responsiveness
- **Event Handling**: Respond to input events
- **Multi-device**: Support different input devices

---

**Next Steps**: 
1. Complete the lab exercises to practice these concepts
2. After completing the lab, test your knowledge with the [quiz](../quiz/quiz4.html).