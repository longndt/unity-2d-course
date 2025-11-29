# Lesson 3 Example: Physics & Collision

## 🎯 What This Example Teaches

This example demonstrates 2D physics systems:
- **Rigidbody2D**: Physics body behavior and movement
- **Colliders**: Different types and their uses
- **Physics Materials**: Friction and bounciness
- **Jump Mechanics**: Advanced jumping with coyote time
- **Force Application**: Adding forces to objects
- **Trigger Detection**: Event-based collision handling

## 🚀 How to Use This Example

### Step 1: Setup the Scene
1. Create a new 2D scene in Unity
2. Create a ground platform with BoxCollider2D
3. Create a player GameObject with Rigidbody2D and Collider2D
4. Add some obstacles and collectibles

### Step 2: Add the Scripts
1. **RigidbodyControl.cs** → Basic physics movement
2. **PlayerJump.cs** → Simple jump mechanics
3. **AdvancedJump.cs** → Coyote time and variable jump height
4. **ColliderSetup.cs** → Configure different collider types
5. **PhysicsMaterialSetup.cs** → Create physics materials
6. **ForceControl.cs** → Apply forces to objects
7. **TriggerDetection.cs** → Handle trigger events

### Step 3: Configure Physics
1. Set up physics materials for different surfaces
2. Configure collision layers and masks
3. Set up trigger colliders for collectibles
4. Adjust gravity and physics settings

### Step 4: Test the Physics
1. Press Play and use arrow keys to move
2. Try jumping on different surfaces
3. Test the advanced jump mechanics
4. Collect items to test triggers

## 🔧 Troubleshooting

**Player falls through ground**: Check collider setup and physics materials
**Jump doesn't work**: Verify ground detection and input handling
**No collision**: Check collision layers and masks
**Physics feels wrong**: Adjust gravity, drag, and physics materials
**Triggers don't work**: Verify trigger colliders are set up correctly

## 💡 Learning Points

- **Rigidbody2D**: Physics body with mass, velocity, forces
- **Colliders**: Define collision boundaries
- **Physics Materials**: Control friction and bounciness
- **FixedUpdate**: Use for physics calculations
- **Coyote Time**: Grace period for jumping
- **Variable Jump**: Jump height based on input duration
- **Triggers**: Event-based collision detection

---

**Next Steps**: 
1. Complete the lab exercises to practice these concepts
2. After completing the lab, test your knowledge with the [quiz](../quiz/quiz3.html).