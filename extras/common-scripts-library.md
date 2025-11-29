# Unity Course - Common Scripts Library

## Essential Unity Scripts for Beginners

> **📁 Code Files**: All scripts are available in the `common-scripts-library/` folder with full implementation.

---

## Player Controllers

### 1. Simple 2D Player Controller

**File**: [Simple2DPlayerController.cs](./common-scripts-library/Simple2DPlayerController.cs)

- Left/Right movement
- Jump physics
- Animation triggers

### 2. 2D Camera Follow

**File**: [Camera2DFollow.cs](./common-scripts-library/Camera2DFollow.cs)

- Smooth following
- Boundary constraints
- Offset positioning

### 3. Simple Camera Shake

**File**: [CameraShake.cs](./common-scripts-library/CameraShake.cs)

- Impact shake effects
- Configurable intensity
- Smooth return to position

---

## Game Management

### 1. Health System

**File**: [HealthSystem.cs](./common-scripts-library/HealthSystem.cs)

- Health management
- Damage and healing
- Invulnerability frames
- Visual feedback
- Event system

### 2. Score Manager

**File**: [ScoreManager.cs](./common-scripts-library/ScoreManager.cs)

- Score tracking
- High score persistence
- Score multipliers
- Event notifications

### 3. Collectible Manager

**File**: [CollectibleManager.cs](./common-scripts-library/CollectibleManager.cs)

- Collectible spawning
- Collection handling
- Score integration
- Visual effects

### 4. Scene Manager

**File**: [SceneTransitionManager.cs](./common-scripts-library/SceneTransitionManager.cs)

- Load scenes with loading screen
- Fade transitions
- Progress tracking

### 5. Game State Manager

**File**: [GameStateManager.cs](./common-scripts-library/GameStateManager.cs)

- Play/Pause/GameOver states
- State transition logic
- Event system

### 6. Save/Load System

**File**: [SaveLoadManager.cs](./common-scripts-library/SaveLoadManager.cs)

- PlayerPrefs implementation
- JSON save data
- Settings persistence

---

## UI Systems

### 1. Main Menu Controller

**File**: [MainMenuController.cs](./common-scripts-library/MainMenuController.cs)

- Button event handling
- Panel transitions
- Settings management

### 2. HUD Manager

**File**: [HUDManager.cs](./common-scripts-library/HUDManager.cs)

- Health bars
- Score display
- Timer systems

### 3. Pause Menu

**File**: [PauseMenuController.cs](./common-scripts-library/PauseMenuController.cs)

- Game pause/resume
- Settings access
- Return to menu

---

## Audio Systems

### 1. Audio Manager

**File**: [AudioManager.cs](./common-scripts-library/AudioManager.cs)

- Sound effect playing
- Background music
- Volume controls
- Audio mixing
- Distance-based volume (2D games)
- Audio clip dictionary for easy access

---

## Physics and Movement

### 1. Physics-Based Movement

**File**: [PhysicsMovement.cs](./common-scripts-library/PhysicsMovement.cs)

- Rigidbody movement
- Force-based controls
- Air control

### 2. Platform Movement

**File**: [MovingPlatform.cs](./common-scripts-library/MovingPlatform.cs)

- Waypoint movement
- Player attachment
- Smooth transitions

### 3. Object Launcher

**File**: [ObjectLauncher.cs](./common-scripts-library/ObjectLauncher.cs)

- Projectile physics
- Trajectory calculation
- Object pooling

---

## Interaction Systems

### 1. Collectible System

**File**: [CollectibleItem.cs](./common-scripts-library/CollectibleItem.cs)

- Trigger-based collection
- Score integration
- Visual/Audio feedback

### 2. Interactable Objects

**File**: [InteractableObject.cs](./common-scripts-library/InteractableObject.cs)

- Player proximity detection
- Action prompts
- Event triggering

### 3. Inventory System

**File**: [SimpleInventory.cs](./common-scripts-library/SimpleInventory.cs)

- Item management
- UI integration
- Persistence

---

## Effects and Animation

### 1. Simple Animator Controller

**File**: [SimpleAnimationController.cs](./common-scripts-library/SimpleAnimationController.cs)

- Parameter management
- State transitions
- Event handling

### 2. Particle Effect Trigger

**File**: [ParticleEffectTrigger.cs](./common-scripts-library/ParticleEffectTrigger.cs)

- Effect spawning
- Timing control
- Cleanup management

### 3. Object Rotator

**File**: [ObjectRotator.cs](./common-scripts-library/ObjectRotator.cs)

- Continuous rotation
- Configurable axes
- Speed controls

---

## Utilities

### 1. Object Pool Manager

**File**: [ObjectPoolManager.cs](./common-scripts-library/ObjectPoolManager.cs)

- Performance optimization
- Reusable objects
- Dynamic sizing

### 2. Timer System

**File**: [TimerSystem.cs](./common-scripts-library/TimerSystem.cs)

- Countdown timers
- Event triggering
- Multiple timer support

### 3. Debug Helper

**File**: [DebugHelper.cs](./common-scripts-library/DebugHelper.cs)

- Visual debugging
- Performance monitoring
- Log management

---

## AI Systems

### 1. Simple AI Follow

**File**: [SimpleAIFollow.cs](./common-scripts-library/SimpleAIFollow.cs)

- Player following
- Obstacle avoidance
- State management

### 2. Patrol AI

**File**: [PatrolAI.cs](./common-scripts-library/PatrolAI.cs)

- Waypoint patrol
- Player detection
- Alert states

### 3. Simple Enemy AI

**File**: [SimpleEnemyAI.cs](./common-scripts-library/SimpleEnemyAI.cs)

- Chase behavior
- Attack patterns
- Health system

---

## Usage Guidelines

### Script Integration

1. Copy script to Assets/Scripts folder
2. Attach to appropriate GameObject
3. Configure public variables in Inspector
4. Test functionality in Play mode

### Customization Tips

- Modify public variables to adjust behavior
- Add Debug.Log statements to understand flow
- Extend classes to add more features
- Use inheritance to create variations

### Best Practices

- Always null-check references
- Use meaningful variable names
- Comment complex logic
- Test edge cases
- Optimize performance-critical scripts

---

## Common Issues and Solutions

### NullReferenceException

**Cause**: Missing component or GameObject reference
**Solution**: Check Inspector assignments, add null checks

### Performance Issues

**Cause**: Expensive operations in Update()
**Solution**: Use FixedUpdate, coroutines, or event systems

### Physics Problems

**Cause**: Incorrect Rigidbody settings or collision setup
**Solution**: Review physics components, check collision layers

### UI Not Responding

**Cause**: Missing EventSystem or incorrect Canvas settings
**Solution**: Add EventSystem, check Canvas setup

---

## Learning Path Recommendations

### Beginner Scripts (Lessons 0-1)

- [Simple2DPlayerController.cs](./common-scripts-library/Simple2DPlayerController.cs)
- [Camera2DFollow.cs](./common-scripts-library/Camera2DFollow.cs)
- [ObjectRotator.cs](./common-scripts-library/ObjectRotator.cs)
- [DebugHelper.cs](./common-scripts-library/DebugHelper.cs)

### Intermediate Scripts (Lessons 2-3)

- [SimpleAnimationController.cs](./common-scripts-library/SimpleAnimationController.cs)
- [CollectibleItem.cs](./common-scripts-library/CollectibleItem.cs)
- [PhysicsMovement.cs](./common-scripts-library/PhysicsMovement.cs)
- [MovingPlatform.cs](./common-scripts-library/MovingPlatform.cs)

### Advanced Scripts (Lessons 4-5)

- [GameStateManager.cs](./common-scripts-library/GameStateManager.cs)
- [AudioManager.cs](./common-scripts-library/AudioManager.cs)
- [SceneTransitionManager.cs](./common-scripts-library/SceneTransitionManager.cs)
- [SaveLoadManager.cs](./common-scripts-library/SaveLoadManager.cs)
- [ObjectPoolManager.cs](./common-scripts-library/ObjectPoolManager.cs)

---

## Extension Ideas

### For Advanced Students

1. **Networking**: MultiPlayer support
2. **Mobile Controls**: Touch input adaptation
3. **VR Integration**: VR controller support
4. **Procedural Generation**: Random level creation
5. **Advanced AI**: Machine learning integration

### Game Genre Specializations

1. **Platformer**: Advanced jump mechanics, level progression
2. **Racing**: Vehicle physics, lap timing
3. **RPG**: Stats system, equipment management
4. **Strategy**: Unit management, resource systems
5. **Puzzle**: Logic systems, solution validation
