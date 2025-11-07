using UnityEngine;

/// <summary>
/// Simple GameManager demonstrating singleton pattern and global game state
/// Shows how to manage game-wide state (similar to global state in web apps)
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game State")]
    [SerializeField] private int score = 0;
    [SerializeField] private int lives = 3;

    // Singleton pattern - allows global access
    public static GameManager Instance { get; private set; }

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameManager initialized!");
        }
        else
        {
            // If another GameManager exists, destroy this one
            Destroy(gameObject);
            Debug.LogWarning("Another GameManager already exists. Destroying duplicate.");
        }
    }

    void Start()
    {
        Debug.Log($"Game Started! Score: {score}, Lives: {lives}");
    }

    // Public methods to modify game state (like setState in React)
    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score updated: {score}");

        // Could trigger UI update here (like re-render in React)
        UpdateUI();
    }

    public void LoseLife()
    {
        lives--;
        Debug.Log($"Lives remaining: {lives}");

        if (lives <= 0)
        {
            GameOver();
        }
    }

    public int GetScore()
    {
        return score;
    }

    public int GetLives()
    {
        return lives;
    }

    void UpdateUI()
    {
        // In a real game, this would update UI elements
        // Similar to re-rendering UI in web frameworks
        Debug.Log("UI Updated!");
    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        // Could load game over scene here
    }
}
