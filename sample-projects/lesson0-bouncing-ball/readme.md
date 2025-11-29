# Lesson 0: Advanced Bouncing Ball Game

## 🎯 Project Overview

This is an **enhanced version** of the basic bouncing ball game from Lesson 0. It builds upon the simple example with advanced features including paddle control, game mechanics, and complete game loop. This demonstrates how to evolve a basic concept into a full game.

### **📚 Relationship to Lesson 0 Example**
- **Basic Example**: `lesson0-game-development-fundamentals/example/` - Simple `BouncingBall.cs` for learning fundamentals
- **This Sample Project**: Advanced implementation with `BallController.cs` and `PaddleController.cs` for complete game
- **Learning Path**: Study the basic example first, then explore this advanced version

## 🎮 Game Description

### **Objective**
Keep the ball bouncing by hitting it with the paddle. The ball gets faster over time, making the game progressively more challenging.

### **Controls**
- **A/D Keys**: Move paddle left/right
- **Space**: Launch ball (if it stops moving)
- **R**: Reset game

### **Enhanced Features** (vs. basic example)
- **Paddle Control**: Player-controlled paddle with smooth movement
- **Game Mechanics**: Score system, lives, and game states
- **Audio System**: Sound effects and music
- **Visual Effects**: Particle effects and screen shake
- **Complete Game Loop**: Menu, gameplay, game over, restart

## 🏗️ Project Structure

```
Assets/
├── Scenes/
│   └── BouncingBall.unity          # Main game scene
├── Scripts/
│   ├── BallController.cs            # Ball physics and behavior
│   ├── PaddleController.cs          # Paddle movement
│   ├── GameManager.cs               # Game state management
│   └── ScoreManager.cs              # Score tracking
├── Sprites/
│   ├── Ball.png                     # Ball sprite
│   ├── Paddle.png                   # Paddle sprite
│   └── Background.png               # Background image
├── Materials/
│   └── BouncyMaterial.physicsMaterial2D  # Physics material
└── Prefabs/
    ├── Ball.prefab                  # Ball prefab
    └── Paddle.prefab                # Paddle prefab
```

## 🎯 Learning Objectives

After studying this project, you will understand:

### **Unity Basics**
- GameObject and Component system
- Transform component usage
- Sprite Renderer configuration
- Prefab creation and usage

### **Physics System**
- Rigidbody2D component
- Collider2D setup
- Physics Material 2D
- Force and velocity manipulation

### **Collision Detection**
- OnCollisionEnter2D method
- Collision vs Trigger detection
- Layer-based collision filtering
- Physics interaction between objects

### **Game Management**
- Game state management
- Score system implementation
- Input handling
- Scene management

## 🔧 Setup Instructions

### **1. Open Project**
1. Launch Unity Hub
2. Click "Add" and select this project folder
3. Open the project in Unity Editor
4. Wait for assets to import

### **2. Verify Setup**
1. Check Console for any errors
2. Open the "BouncingBall" scene
3. Press Play to test the game
4. Verify all controls work correctly

### **3. Explore the Code**
1. Open each script in the Scripts folder
2. Read the comments to understand functionality
3. Modify values in the Inspector to see changes
4. Experiment with different settings

## 📝 Code Walkthrough

### **BallController.cs**
```csharp
public class BallController : MonoBehaviour
{
    [Header("Ball Settings")]
    public float initialSpeed = 5f;
    public float speedIncrease = 0.1f;
    public float maxSpeed = 15f;

    private Rigidbody2D rb;
    private Vector2 lastVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        LaunchBall();
    }

    void Update()
    {
        // Increase speed over time
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * (rb.velocity.magnitude + speedIncrease * Time.deltaTime);
        }

        // Check if ball is moving too slowly
        if (rb.velocity.magnitude < 1f)
        {
            LaunchBall();
        }
    }

    void LaunchBall()
    {
        // Launch ball in random direction
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        rb.velocity = randomDirection * initialSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Play bounce sound effect
        AudioSource.PlayClipAtPoint(bounceSound, transform.position);

        // Add visual effect
        Instantiate(bounceEffect, transform.position, Quaternion.identity);
    }
}
```

### **PaddleController.cs**
```csharp
public class PaddleController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float boundary = 8f;

    private float horizontalInput;

    void Update()
    {
        // Get input
        horizontalInput = Input.GetAxis("Horizontal");

        // Move paddle
        transform.Translate(Vector2.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Keep paddle within boundaries
        float clampedX = Mathf.Clamp(transform.position.x, -boundary, boundary);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
```

### **GameManager.cs**
```csharp
public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int targetScore = 100;
    public float gameTime = 60f;

    private int currentScore;
    private float timeRemaining;
    private bool gameActive;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
        if (gameActive)
        {
            timeRemaining -= Time.deltaTime;

            if (timeRemaining <= 0)
            {
                GameOver();
            }
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        ScoreManager.Instance.UpdateScore(currentScore);

        if (currentScore >= targetScore)
        {
            Victory();
        }
    }

    void StartGame()
    {
        currentScore = 0;
        timeRemaining = gameTime;
        gameActive = true;
    }

    void GameOver()
    {
        gameActive = false;
        Debug.Log("Game Over! Final Score: " + currentScore);
    }

    void Victory()
    {
        gameActive = false;
        Debug.Log("Victory! You reached the target score!");
    }
}
```

## 🎨 Visual Elements

### **Sprites**
- **Ball**: Simple circle sprite with bright color
- **Paddle**: Rectangular sprite with contrasting color
- **Background**: Subtle pattern that doesn't interfere with gameplay

### **Materials**
- **BouncyMaterial**: Physics material with high bounciness
- **Friction**: Low friction for smooth ball movement
- **Bounciness**: High bounciness for energetic gameplay

### **Effects**
- **Particle System**: Visual feedback on ball collision
- **Sound Effects**: Audio feedback for ball bouncing
- **UI Elements**: Score display and game over screen

## 🎮 Gameplay Features

### **Progressive Difficulty**
- Ball speed increases over time
- Maximum speed limit prevents impossible gameplay
- Visual and audio feedback for ball collisions

### **Score System**
- Points awarded for successful ball hits
- Target score to win the game
- Time limit for added challenge

### **Input Handling**
- Responsive paddle movement
- Smooth input processing
- Boundary checking to keep paddle on screen

### **Game States**
- Game start with ball launch
- Active gameplay with score tracking
- Game over when time runs out
- Victory when target score is reached

## 🔧 Customization Options

### **Easy Modifications**
1. **Change ball speed**: Modify `initialSpeed` and `maxSpeed` values
2. **Adjust paddle size**: Scale the paddle GameObject
3. **Modify boundaries**: Change the `boundary` value
4. **Add power-ups**: Create collectible items that affect gameplay

### **Advanced Features**
1. **Multiple balls**: Spawn additional balls at certain score thresholds
2. **Power-ups**: Add special abilities like paddle size changes
3. **Level progression**: Create multiple levels with increasing difficulty
4. **High score system**: Save and display best scores

## 🐛 Common Issues

### **Ball gets stuck**
- **Cause**: Ball velocity becomes too low
- **Solution**: Check the minimum velocity threshold in BallController

### **Paddle moves too fast/slow**
- **Cause**: Move speed value is incorrect
- **Solution**: Adjust the `moveSpeed` value in PaddleController

### **Ball doesn't bounce properly**
- **Cause**: Physics material not applied or incorrect settings
- **Solution**: Check BouncyMaterial settings and ensure it's applied to the ball

### **Score not updating**
- **Cause**: ScoreManager reference not set
- **Solution**: Ensure ScoreManager is in the scene and properly configured

## 📚 Learning Exercises

### **Beginner Exercises**
1. **Modify ball speed**: Change how fast the ball moves
2. **Adjust paddle size**: Make the paddle bigger or smaller
3. **Change colors**: Modify sprite colors for visual variety
4. **Add sound effects**: Implement audio feedback

### **Intermediate Exercises**
1. **Add power-ups**: Create collectible items that affect gameplay
2. **Implement lives system**: Give players multiple chances
3. **Create levels**: Design different difficulty levels
4. **Add particle effects**: Enhance visual feedback

### **Advanced Exercises**
1. **Multiplayer support**: Add second player with different controls
2. **AI opponent**: Create computer-controlled paddle
3. **Level editor**: Allow players to create custom levels
4. **Online leaderboards**: Implement score sharing system

## 🎯 Next Steps

After completing this project:

1. **Study the code**: Understand how each component works
2. **Experiment**: Try modifying values and adding features
3. **Move to Lesson 1**: Learn about scene management and prefabs
4. **Build your own**: Create a similar game with your own twist

## 💡 Pro Tips

### **Development Tips**
- **Test frequently**: Play the game often to catch issues early
- **Use version control**: Save your progress regularly
- **Comment your code**: Explain complex logic for future reference
- **Optimize performance**: Profile the game to ensure smooth gameplay

### **Learning Tips**
- **Read the code**: Don't just copy, understand what each line does
- **Experiment**: Try changing values to see what happens
- **Ask questions**: Don't hesitate to seek help when stuck
- **Practice**: The more you code, the better you become

---

**Happy Learning!** This project provides a solid foundation for understanding Unity's basic concepts. Take your time to explore and experiment with the code!
