using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// HUD Manager for in-game UI elements
/// Demonstrates game UI management and real-time updates
/// </summary>
public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance;

    [Header("HUD Elements")]
    public HealthBar playerHealthBar;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public Image[] abilityIcons;
    public Button pauseButton;

    [Header("HUD Panels")]
    public GameObject gameHUD;
    public GameObject miniMap;

    private int currentScore = 0;
    private int currentLives = 3;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        InitializeHUD();

        // Setup pause button
        if (pauseButton != null)
        {
            pauseButton.onClick.AddListener(OnPauseClicked);
        }
    }

    void InitializeHUD()
    {
        // Initialize health bar
        if (playerHealthBar != null)
        {
            playerHealthBar.Initialize(100f); // Max health = 100
        }

        // Initialize score and lives display
        UpdateScore(0);
        UpdateLives(currentLives);

        // Show HUD
        ShowGameHUD();
    }

    public void UpdateHealth(float newHealth)
    {
        if (playerHealthBar != null)
        {
            playerHealthBar.UpdateHealth(newHealth);
        }
    }

    public void UpdateScore(int newScore)
    {
        currentScore = newScore;
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }

    public void AddScore(int points)
    {
        UpdateScore(currentScore + points);
    }

    public void UpdateLives(int newLives)
    {
        currentLives = newLives;
        if (livesText != null)
        {
            livesText.text = $"Lives: {currentLives}";
        }
    }

    public void ShowGameHUD()
    {
        if (gameHUD != null)
        {
            gameHUD.SetActive(true);
        }
    }

    public void HideGameHUD()
    {
        if (gameHUD != null)
        {
            gameHUD.SetActive(false);
        }
    }

    void OnPauseClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PauseGame();
        }
    }

    void OnDestroy()
    {
        if (pauseButton != null)
        {
            pauseButton.onClick.RemoveListener(OnPauseClicked);
        }
    }
}