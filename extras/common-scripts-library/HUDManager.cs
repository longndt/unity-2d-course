using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// HUD Manager for health bars, score display, and timer systems
/// </summary>
public class HUDManager : MonoBehaviour
{
    [Header("Health Display")]
    public Slider healthBar;
    public Text healthText;
    public Image healthBarFill;

    [Header("Score Display")]
    public Text scoreText;
    public Text highScoreText;

    [Header("Timer Display")]
    public Text timerText;
    public bool countdown = false;
    public float timeLimit = 300f; // 5 minutes

    [Header("Other UI")]
    public Text levelText;
    public Text livesText;

    private float currentTime;
    private int currentScore;
    private int highScore;
    private int currentLives = 3;

    void Start()
    {
        // Load high score
        highScore = PlayerPrefs.GetInt("HighScore", 0);

        // Initialize timer
        currentTime = countdown ? timeLimit : 0f;

        // Update displays
        UpdateHealthDisplay();
        UpdateScoreDisplay();
        UpdateTimerDisplay();
        UpdateLivesDisplay();
    }

    void Update()
    {
        if (countdown)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                currentTime = 0;
                // Game over logic here
                Debug.Log("Time's up!");
            }
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        UpdateTimerDisplay();
    }

    public void SetHealth(float currentHealth, float maxHealth)
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth / maxHealth;
        }

        if (healthText != null)
        {
            healthText.text = $"{currentHealth:F0}/{maxHealth:F0}";
        }

        // Change color based on health percentage
        if (healthBarFill != null)
        {
            float healthPercent = currentHealth / maxHealth;
            if (healthPercent > 0.6f)
                healthBarFill.color = Color.green;
            else if (healthPercent > 0.3f)
                healthBarFill.color = Color.yellow;
            else
                healthBarFill.color = Color.red;
        }
    }

    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();

        // Check for new high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void SetScore(int score)
    {
        currentScore = score;
        UpdateScoreDisplay();
    }

    public void SetLives(int lives)
    {
        currentLives = lives;
        UpdateLivesDisplay();
    }

    public void SetLevel(int level)
    {
        if (levelText != null)
        {
            levelText.text = $"Level: {level}";
        }
    }

    void UpdateHealthDisplay()
    {
        // This would be called when health changes
        // SetHealth(currentHealth, maxHealth);
    }

    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }

        if (highScoreText != null)
        {
            highScoreText.text = $"High Score: {highScore}";
        }
    }

    void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }

    void UpdateLivesDisplay()
    {
        if (livesText != null)
        {
            livesText.text = $"Lives: {currentLives}";
        }
    }

    public float GetCurrentTime()
    {
        return currentTime;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}
