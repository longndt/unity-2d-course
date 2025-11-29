using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Game State Manager for handling Play/Pause/GameOver states
/// </summary>
public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        Playing,
        Paused,
        GameOver,
        MainMenu
    }

    [Header("Current State")]
    public GameState currentState = GameState.Playing;

    [Header("Events")]
    public UnityEvent OnGameStart;
    public UnityEvent OnGamePause;
    public UnityEvent OnGameResume;
    public UnityEvent OnGameOver;
    public UnityEvent OnGameRestart;

    public static GameStateManager Instance { get; private set; }

    void Awake()
    {
        // Singleton pattern
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
        SetState(GameState.Playing);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Pause/Resume with Escape key
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

        // Restart with R key
        if (Input.GetKeyDown(KeyCode.R) && currentState == GameState.GameOver)
        {
            RestartGame();
        }
    }

    public void SetState(GameState newState)
    {
        if (currentState == newState) return;

        GameState previousState = currentState;
        currentState = newState;

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                OnGameStart?.Invoke();
                break;

            case GameState.Paused:
                Time.timeScale = 0f;
                OnGamePause?.Invoke();
                break;

            case GameState.GameOver:
                Time.timeScale = 0f;
                OnGameOver?.Invoke();
                break;

            case GameState.MainMenu:
                Time.timeScale = 1f;
                break;
        }

        Debug.Log($"Game State changed from {previousState} to {newState}");
    }

    public void PauseGame()
    {
        SetState(GameState.Paused);
    }

    public void ResumeGame()
    {
        SetState(GameState.Playing);
        OnGameResume?.Invoke();
    }

    public void GameOver()
    {
        SetState(GameState.GameOver);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        OnGameRestart?.Invoke();
        SetState(GameState.Playing);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
