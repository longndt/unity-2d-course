using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main game manager that handles game state, scoring, and scene transitions.
/// Demonstrates complete game loop management and state handling.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    [Tooltip("Target score to win")]
    public int targetScore = 1000;
    
    [Tooltip("Game time limit in seconds")]
    public float gameTime = 60f;
    
    [Tooltip("Maximum number of lives")]
    public int maxLives = 3;
    
    [Tooltip("Score per collectible")]
    public int scorePerCollectible = 100;
    
    [Header("UI References")]
    [Tooltip("Main menu panel")]
    public GameObject mainMenuPanel;
    
    [Tooltip("Gameplay panel")]
    public GameObject gameplayPanel;
    
    [Tooltip("Pause menu panel")]
    public GameObject pauseMenuPanel;
    
    [Tooltip("Game over panel")]
    public GameObject gameOverPanel;
    
    [Header("Audio")]
    [Tooltip("Audio manager reference")]
    public AudioManager audioManager;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Victory
    }
    
    private int currentScore = 0;
    private int currentLives;
    private float currentTime;
    private bool isPaused = false;
    private GameState currentState = GameState.MainMenu;
    
    public static GameManager Instance { get; private set; }
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        currentLives = maxLives;
        currentTime = gameTime;
        ShowMainMenu();
        
        if (showDebugInfo)
            Debug.Log("GameManager: Game manager initialized");
    }
    
    void Update()
    {
        if (currentState == GameState.Playing)
        {
            UpdateGameTime();
            CheckWinCondition();
        }
        
        HandleInput();
    }
    
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
        }
    }
    
    public void StartGame()
    {
        currentScore = 0;
        currentLives = maxLives;
        currentTime = gameTime;
        isPaused = false;
        
        ChangeState(GameState.Playing);
        ShowGameplay();
        
        if (audioManager != null)
        {
            audioManager.PlayGameplayMusic();
        }
        
        if (showDebugInfo)
            Debug.Log("GameManager: Game started");
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        ChangeState(GameState.Paused);
        ShowPauseMenu();
        
        if (showDebugInfo)
            Debug.Log("GameManager: Game paused");
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        ChangeState(GameState.Playing);
        ShowGameplay();
        
        if (showDebugInfo)
            Debug.Log("GameManager: Game resumed");
    }
    
    public void GameOver()
    {
        Time.timeScale = 0f;
        ChangeState(GameState.GameOver);
        ShowGameOver();
        
        if (audioManager != null)
        {
            audioManager.PlayGameOverSound();
        }
        
        if (showDebugInfo)
            Debug.Log("GameManager: Game over");
    }
    
    public void Victory()
    {
        Time.timeScale = 0f;
        ChangeState(GameState.Victory);
        ShowVictory();
        
        if (audioManager != null)
        {
            audioManager.PlayVictorySound();
        }
        
        if (showDebugInfo)
            Debug.Log("GameManager: Victory!");
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();
        
        if (currentScore >= targetScore)
        {
            Victory();
        }
        
        if (showDebugInfo)
            Debug.Log($"GameManager: Score added: {points}, Total: {currentScore}");
    }
    
    public void LoseLife()
    {
        currentLives--;
        UpdateLivesDisplay();
        
        if (currentLives <= 0)
        {
            GameOver();
        }
        
        if (showDebugInfo)
            Debug.Log($"GameManager: Life lost, remaining: {currentLives}");
    }
    
    void UpdateGameTime()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateTimeDisplay();
            
            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }
    
    void CheckWinCondition()
    {
        if (currentScore >= targetScore)
        {
            Victory();
        }
    }
    
    void ChangeState(GameState newState)
    {
        currentState = newState;
        
        if (showDebugInfo)
            Debug.Log($"GameManager: Game state changed to: {currentState}");
    }
    
    void ShowMainMenu()
    {
        HideAllPanels();
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
    }
    
    void ShowGameplay()
    {
        HideAllPanels();
        if (gameplayPanel != null) gameplayPanel.SetActive(true);
    }
    
    void ShowPauseMenu()
    {
        HideAllPanels();
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
    }
    
    void ShowGameOver()
    {
        HideAllPanels();
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
    
    void ShowVictory()
    {
        HideAllPanels();
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }
    
    void HideAllPanels()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
    }
    
    void UpdateScoreDisplay()
    {
        // Update score display in UI
        if (showDebugInfo)
            Debug.Log($"GameManager: Score: {currentScore}");
    }
    
    void UpdateLivesDisplay()
    {
        // Update lives display in UI
        if (showDebugInfo)
            Debug.Log($"GameManager: Lives: {currentLives}");
    }
    
    void UpdateTimeDisplay()
    {
        // Update time display in UI
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        
        if (showDebugInfo)
            Debug.Log($"GameManager: Time: {minutes:00}:{seconds:00}");
    }
    
    // Public getters
    public int GetScore() => currentScore;
    public int GetLives() => currentLives;
    public float GetTime() => currentTime;
    public bool IsPaused() => isPaused;
    public GameState GetCurrentState() => currentState;
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display game info
        GUI.Label(new Rect(10, 10, 300, 20), "Game Manager Info:");
        GUI.Label(new Rect(10, 30, 300, 20), $"State: {currentState}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Score: {currentScore}/{targetScore}");
        GUI.Label(new Rect(10, 70, 300, 20), $"Lives: {currentLives}");
        GUI.Label(new Rect(10, 90, 300, 20), $"Time: {currentTime:F1}");
        GUI.Label(new Rect(10, 110, 300, 20), $"Paused: {isPaused}");
        GUI.Label(new Rect(10, 130, 300, 20), "ESC - Pause/Resume");
    }
}
