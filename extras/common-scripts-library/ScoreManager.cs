using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Score management system for 2D games.
/// Handles scoring, high scores, and score persistence.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    [Header("Score Settings")]
    [Tooltip("Current score")]
    public int currentScore = 0;
    
    [Tooltip("High score key for PlayerPrefs")]
    public string highScoreKey = "HighScore";
    
    [Tooltip("Score multiplier")]
    public float scoreMultiplier = 1f;
    
    [Header("Events")]
    [Tooltip("Called when score changes")]
    public UnityEvent<int> OnScoreChanged;
    
    [Tooltip("Called when high score is beaten")]
    public UnityEvent<int> OnHighScoreBeaten;
    
    [Tooltip("Called when score is reset")]
    public UnityEvent OnScoreReset;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private int highScore = 0;
    
    public static ScoreManager Instance { get; private set; }
    
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
        LoadHighScore();
    }
    
    public void AddScore(int points)
    {
        int actualPoints = Mathf.RoundToInt(points * scoreMultiplier);
        currentScore += actualPoints;
        
        OnScoreChanged?.Invoke(currentScore);
        
        // Check for high score
        if (currentScore > highScore)
        {
            highScore = currentScore;
            SaveHighScore();
            OnHighScoreBeaten?.Invoke(highScore);
        }
        
        if (showDebugInfo)
            Debug.Log($"Score added: {actualPoints}, Total: {currentScore}");
    }
    
    public void SetScore(int score)
    {
        currentScore = Mathf.Max(0, score);
        OnScoreChanged?.Invoke(currentScore);
        
        if (showDebugInfo)
            Debug.Log($"Score set to: {currentScore}");
    }
    
    public void ResetScore()
    {
        currentScore = 0;
        OnScoreChanged?.Invoke(currentScore);
        OnScoreReset?.Invoke();
        
        if (showDebugInfo)
            Debug.Log("Score reset");
    }
    
    public void SetScoreMultiplier(float multiplier)
    {
        scoreMultiplier = Mathf.Max(0f, multiplier);
        
        if (showDebugInfo)
            Debug.Log($"Score multiplier set to: {scoreMultiplier}");
    }
    
    public void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt(highScoreKey, 0);
        
        if (showDebugInfo)
            Debug.Log($"High score loaded: {highScore}");
    }
    
    public void SaveHighScore()
    {
        PlayerPrefs.SetInt(highScoreKey, highScore);
        PlayerPrefs.Save();
        
        if (showDebugInfo)
            Debug.Log($"High score saved: {highScore}");
    }
    
    public int GetCurrentScore()
    {
        return currentScore;
    }
    
    public int GetHighScore()
    {
        return highScore;
    }
    
    public float GetScoreMultiplier()
    {
        return scoreMultiplier;
    }
    
    public bool IsHighScore()
    {
        return currentScore >= highScore;
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        GUI.Label(new Rect(10, 10, 200, 20), $"Score: {currentScore}");
        GUI.Label(new Rect(10, 30, 200, 20), $"High Score: {highScore}");
        GUI.Label(new Rect(10, 50, 200, 20), $"Multiplier: {scoreMultiplier}x");
    }
}
