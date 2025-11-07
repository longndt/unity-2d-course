using UnityEngine;
using System.IO;

/// <summary>
/// Advanced save system for game data persistence.
/// Demonstrates JSON serialization, PlayerPrefs, and cross-session data management.
/// </summary>
public class SaveSystem : MonoBehaviour
{
    [Header("Save Settings")]
    [Tooltip("Save file name")]
    public string saveFileName = "gamedata.json";
    
    [Tooltip("Enable auto save")]
    public bool autoSave = true;
    
    [Tooltip("Auto save interval in seconds")]
    public float autoSaveInterval = 30f;
    
    [Tooltip("Enable debug logging")]
    public bool enableDebugLogging = true;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private PlayerData gameData;
    private string savePath;
    private float autoSaveTimer;
    
    public static SaveSystem Instance { get; private set; }
    
    [System.Serializable]
    public class PlayerData
    {
        public int highScore;
        public int totalScore;
        public int gamesPlayed;
        public float musicVolume;
        public float sfxVolume;
        public bool firstTimePlaying;
        public string playerName;
        public System.DateTime lastSaveTime;
        
        public PlayerData()
        {
            highScore = 0;
            totalScore = 0;
            gamesPlayed = 0;
            musicVolume = 0.7f;
            sfxVolume = 1f;
            firstTimePlaying = true;
            playerName = "Player";
            lastSaveTime = System.DateTime.Now;
        }
    }
    
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
        savePath = Path.Combine(Application.persistentDataPath, saveFileName);
        LoadGame();
        
        if (showDebugInfo)
            Debug.Log("SaveSystem: Save system initialized");
    }
    
    void Update()
    {
        if (autoSave)
        {
            autoSaveTimer += Time.deltaTime;
            if (autoSaveTimer >= autoSaveInterval)
            {
                SaveGame();
                autoSaveTimer = 0f;
            }
        }
        
        HandleInput();
    }
    
    void HandleInput()
    {
        // Test save/load
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SaveGame();
        }
        
        if (Input.GetKeyDown(KeyCode.F9))
        {
            LoadGame();
        }
        
        if (Input.GetKeyDown(KeyCode.F10))
        {
            DeleteSave();
        }
    }
    
    public void SaveGame()
    {
        try
        {
            gameData.lastSaveTime = System.DateTime.Now;
            string jsonData = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(savePath, jsonData);
            
            if (enableDebugLogging)
                Debug.Log("SaveSystem: Game saved successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"SaveSystem: Failed to save game: {e.Message}");
        }
    }
    
    public void LoadGame()
    {
        try
        {
            if (File.Exists(savePath))
            {
                string jsonData = File.ReadAllText(savePath);
                gameData = JsonUtility.FromJson<PlayerData>(jsonData);
                
                if (enableDebugLogging)
                    Debug.Log("SaveSystem: Game loaded successfully");
            }
            else
            {
                gameData = new PlayerData();
                
                if (enableDebugLogging)
                    Debug.Log("SaveSystem: No save file found, creating new game data");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"SaveSystem: Failed to load game: {e.Message}");
            gameData = new PlayerData();
        }
    }
    
    public void DeleteSave()
    {
        try
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                gameData = new PlayerData();
                
                if (enableDebugLogging)
                    Debug.Log("SaveSystem: Save file deleted");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"SaveSystem: Failed to delete save: {e.Message}");
        }
    }
    
    public void UpdateHighScore(int score)
    {
        if (score > gameData.highScore)
        {
            gameData.highScore = score;
            
            if (enableDebugLogging)
                Debug.Log($"SaveSystem: New high score: {score}");
        }
    }
    
    public void AddToTotalScore(int score)
    {
        gameData.totalScore += score;
        
        if (enableDebugLogging)
            Debug.Log($"SaveSystem: Added {score} to total score");
    }
    
    public void IncrementGamesPlayed()
    {
        gameData.gamesPlayed++;
        
        if (enableDebugLogging)
            Debug.Log($"SaveSystem: Games played: {gameData.gamesPlayed}");
    }
    
    public void SetAudioSettings(float musicVol, float sfxVol)
    {
        gameData.musicVolume = musicVol;
        gameData.sfxVolume = sfxVol;
        
        if (enableDebugLogging)
            Debug.Log($"SaveSystem: Audio settings updated");
    }
    
    public void SetPlayerName(string name)
    {
        gameData.playerName = name;
        
        if (enableDebugLogging)
            Debug.Log($"SaveSystem: Player name set to {name}");
    }
    
    public void SetFirstTimeFalse()
    {
        gameData.firstTimePlaying = false;
        
        if (enableDebugLogging)
            Debug.Log("SaveSystem: First time playing set to false");
    }
    
    // Public getters
    public int GetHighScore() => gameData.highScore;
    public int GetTotalScore() => gameData.totalScore;
    public int GetGamesPlayed() => gameData.gamesPlayed;
    public float GetMusicVolume() => gameData.musicVolume;
    public float GetSFXVolume() => gameData.sfxVolume;
    public string GetPlayerName() => gameData.playerName;
    public bool IsFirstTime() => gameData.firstTimePlaying;
    public System.DateTime GetLastSaveTime() => gameData.lastSaveTime;
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display save system info
        GUI.Label(new Rect(10, 10, 300, 20), "Save System Controls:");
        GUI.Label(new Rect(10, 30, 300, 20), "F5 - Save Game");
        GUI.Label(new Rect(10, 50, 300, 20), "F9 - Load Game");
        GUI.Label(new Rect(10, 70, 300, 20), "F10 - Delete Save");
        
        GUI.Label(new Rect(10, 100, 300, 20), $"High Score: {gameData.highScore}");
        GUI.Label(new Rect(10, 120, 300, 20), $"Total Score: {gameData.totalScore}");
        GUI.Label(new Rect(10, 140, 300, 20), $"Games Played: {gameData.gamesPlayed}");
        GUI.Label(new Rect(10, 160, 300, 20), $"Player Name: {gameData.playerName}");
        GUI.Label(new Rect(10, 180, 300, 20), $"First Time: {gameData.firstTimePlaying}");
        GUI.Label(new Rect(10, 200, 300, 20), $"Last Save: {gameData.lastSaveTime:HH:mm:ss}");
    }
}
