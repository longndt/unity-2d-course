# Lesson 5: UI & Complete Game - Sample Project

## 🎯 Project Overview

This sample project demonstrates a complete 2D game with full UI system, game state management, audio integration, save system, and build process. It features a complete game loop from main menu to gameplay to game over.

### **📚 Relationship to Lesson 5 Examples**
- **Basic Examples**: `lesson5-ui-complete-game/example/` - Individual UI components and scripts
- **This Sample Project**: Complete game with all UI systems integrated
- **Learning Path**: Study individual examples first, then explore this complete game

## 🎮 Project Description

### **Objective**
Create a complete 2D game with professional UI, audio, save system, and deployment ready for release.

### **Features**
- Complete UI system with menus and HUD
- Game state management and scene transitions
- Audio system with music and sound effects
- Save/load system with high scores
- Build and deployment process
- Complete game loop with win/lose conditions

### **Controls**
- **A/D Keys**: Move character
- **Space**: Jump
- **Left Mouse**: Attack
- **Escape**: Pause menu
- **UI Navigation**: Mouse or keyboard

## 🏗️ Project Structure

```
Assets/
├── Scenes/
│   ├── MainMenu.unity          # Main menu scene
│   ├── Gameplay.unity          # Gameplay scene
│   ├── Settings.unity          # Settings scene
│   └── GameOver.unity          # Game over scene
├── Scripts/
│   ├── Game/
│   │   ├── GameManager.cs      # Main game state management
│   │   ├── GameStateManager.cs # Game state handling
│   │   └── ScoreManager.cs     # Score and high score system
│   ├── UI/
│   │   ├── MenuManager.cs      # Menu system management
│   │   ├── HUDManager.cs       # In-game HUD
│   │   ├── UIPanel.cs          # Base UI panel class
│   │   └── ButtonHandler.cs    # Button event handling
│   ├── Audio/
│   │   ├── AudioManager.cs     # Audio system management
│   │   └── AudioSettingsUI.cs  # Audio settings UI
│   ├── Save/
│   │   ├── SaveSystem.cs       # Save/load system
│   │   └── PlayerData.cs       # Player data structure
│   └── Build/
│       ├── BuildOptimizer.cs   # Build optimization
│       └── BuildScript.cs      # Build automation
├── UI/
│   ├── Prefabs/
│   │   ├── MainMenuPanel.prefab
│   │   ├── GameplayPanel.prefab
│   │   ├── PauseMenuPanel.prefab
│   │   └── GameOverPanel.prefab
│   └── Sprites/
│       ├── buttons.png
│       ├── panels.png
│       └── icons.png
├── Audio/
│   ├── Music/
│   │   ├── main_theme.ogg
│   │   └── gameplay_music.ogg
│   └── SFX/
│       ├── jump.wav
│       ├── attack.wav
│       └── collect.wav
└── Build/
    ├── Windows/
    ├── Mac/
    └── Linux/
```

## 🎯 Learning Objectives

After studying this project, you will understand:

### **UI System Implementation**
- Canvas setup and UI scaling
- UI element hierarchy and management
- Event system and button handling
- Responsive UI design

### **Game State Management**
- Game state machine implementation
- Scene transitions and management
- Pause/resume functionality
- Win/lose condition handling

### **Audio System**
- Audio manager implementation
- Music and sound effect management
- Audio settings and volume control
- Audio feedback integration

### **Save System**
- Data serialization and persistence
- High score tracking
- Settings persistence
- Cross-session data management

### **Build and Deployment**
- Build settings configuration
- Platform-specific optimization
- Build automation and scripting
- Performance optimization

## 🔧 Setup Instructions

### **1. Open Project**
1. Launch Unity Hub
2. Click "Add" and select this project folder
3. Open the project in Unity Editor
4. Wait for assets to import

### **2. Configure Build Settings**
1. **File → Build Settings**
2. **Add Open Scenes** to build
3. **Select Platform** (PC, Mac & Linux Standalone)
4. **Configure Player Settings**

### **3. Test the Game**
1. **Click Play** to start the game
2. **Navigate through menus** using mouse or keyboard
3. **Play the game** and test all features
4. **Test save/load** functionality

## 📝 Code Walkthrough

### **GameManager.cs**
```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Game Settings")]
    public int targetScore = 1000;
    public float gameTime = 60f;
    public int maxLives = 3;
    
    [Header("UI References")]
    public GameObject mainMenuPanel;
    public GameObject gameplayPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    
    [Header("Audio")]
    public AudioManager audioManager;
    
    private int currentScore = 0;
    private int currentLives;
    private float currentTime;
    private bool isPaused = false;
    private GameState currentState = GameState.MainMenu;
    
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Victory
    }
    
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
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        ChangeState(GameState.Paused);
        ShowPauseMenu();
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        ChangeState(GameState.Playing);
        ShowGameplay();
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
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();
        
        if (currentScore >= targetScore)
        {
            Victory();
        }
    }
    
    public void LoseLife()
    {
        currentLives--;
        UpdateLivesDisplay();
        
        if (currentLives <= 0)
        {
            GameOver();
        }
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
        Debug.Log($"Game state changed to: {currentState}");
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
        Debug.Log($"Score: {currentScore}");
    }
    
    void UpdateLivesDisplay()
    {
        // Update lives display in UI
        Debug.Log($"Lives: {currentLives}");
    }
    
    void UpdateTimeDisplay()
    {
        // Update time display in UI
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);
        Debug.Log($"Time: {minutes:00}:{seconds:00}");
    }
    
    public int GetScore() => currentScore;
    public int GetLives() => currentLives;
    public float GetTime() => currentTime;
    public bool IsPaused() => isPaused;
}
```

### **AudioManager.cs**
```csharp
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource ambientSource;
    
    [Header("Audio Clips")]
    public AudioClip mainMenuMusic;
    public AudioClip gameplayMusic;
    public AudioClip jumpSound;
    public AudioClip attackSound;
    public AudioClip collectSound;
    public AudioClip gameOverSound;
    public AudioClip victorySound;
    
    [Header("Volume Settings")]
    [Range(0f, 1f)]
    public float masterVolume = 1f;
    [Range(0f, 1f)]
    public float musicVolume = 0.7f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;
    
    public static AudioManager Instance { get; private set; }
    
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
        SetupAudioSources();
        LoadAudioSettings();
    }
    
    void SetupAudioSources()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = musicVolume;
        }
        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.volume = sfxVolume;
        }
        
        if (ambientSource == null)
        {
            ambientSource = gameObject.AddComponent<AudioSource>();
            ambientSource.loop = true;
            ambientSource.volume = 0.5f;
        }
    }
    
    public void PlayMainMenuMusic()
    {
        if (mainMenuMusic != null && musicSource != null)
        {
            musicSource.clip = mainMenuMusic;
            musicSource.Play();
        }
    }
    
    public void PlayGameplayMusic()
    {
        if (gameplayMusic != null && musicSource != null)
        {
            musicSource.clip = gameplayMusic;
            musicSource.Play();
        }
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }
    
    public void PlayJumpSound()
    {
        PlaySFX(jumpSound);
    }
    
    public void PlayAttackSound()
    {
        PlaySFX(attackSound);
    }
    
    public void PlayCollectSound()
    {
        PlaySFX(collectSound);
    }
    
    public void PlayGameOverSound()
    {
        PlaySFX(gameOverSound);
    }
    
    public void PlayVictorySound()
    {
        PlaySFX(victorySound);
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        AudioListener.volume = masterVolume;
        SaveAudioSettings();
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
        SaveAudioSettings();
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
        SaveAudioSettings();
    }
    
    void LoadAudioSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        
        AudioListener.volume = masterVolume;
        if (musicSource != null) musicSource.volume = musicVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;
    }
    
    void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.Save();
    }
}
```

### **SaveSystem.cs**
```csharp
using UnityEngine;
using System.IO;

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
    
    public PlayerData()
    {
        highScore = 0;
        totalScore = 0;
        gamesPlayed = 0;
        musicVolume = 0.7f;
        sfxVolume = 1f;
        firstTimePlaying = true;
        playerName = "Player";
    }
}

public class SaveSystem : MonoBehaviour
{
    [Header("Save Settings")]
    public string saveFileName = "gamedata.json";
    public bool autoSave = true;
    public float autoSaveInterval = 30f;
    
    private PlayerData gameData;
    private string savePath;
    private float autoSaveTimer;
    
    public static SaveSystem Instance { get; private set; }
    
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
    }
    
    public void SaveGame()
    {
        try
        {
            string jsonData = JsonUtility.ToJson(gameData, true);
            File.WriteAllText(savePath, jsonData);
            Debug.Log("Game saved successfully");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save game: {e.Message}");
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
                Debug.Log("Game loaded successfully");
            }
            else
            {
                gameData = new PlayerData();
                Debug.Log("No save file found, creating new game data");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load game: {e.Message}");
            gameData = new PlayerData();
        }
    }
    
    public void UpdateHighScore(int score)
    {
        if (score > gameData.highScore)
        {
            gameData.highScore = score;
            Debug.Log($"New high score: {score}");
        }
    }
    
    public void AddToTotalScore(int score)
    {
        gameData.totalScore += score;
    }
    
    public void IncrementGamesPlayed()
    {
        gameData.gamesPlayed++;
    }
    
    public void SetAudioSettings(float musicVol, float sfxVol)
    {
        gameData.musicVolume = musicVol;
        gameData.sfxVolume = sfxVol;
    }
    
    public void SetPlayerName(string name)
    {
        gameData.playerName = name;
    }
    
    public int GetHighScore() => gameData.highScore;
    public int GetTotalScore() => gameData.totalScore;
    public int GetGamesPlayed() => gameData.gamesPlayed;
    public float GetMusicVolume() => gameData.musicVolume;
    public float GetSFXVolume() => gameData.sfxVolume;
    public string GetPlayerName() => gameData.playerName;
    public bool IsFirstTime() => gameData.firstTimePlaying;
    
    public void SetFirstTimeFalse()
    {
        gameData.firstTimePlaying = false;
    }
}
```

## 🎨 Visual Elements

### **UI Elements**
- **Main Menu**: Title, play button, settings, quit
- **Gameplay HUD**: Score, lives, time, health bar
- **Pause Menu**: Resume, settings, main menu
- **Game Over**: Final score, high score, restart

### **Audio Elements**
- **Background Music**: Main menu and gameplay themes
- **Sound Effects**: Jump, attack, collect, game over
- **Audio Settings**: Volume controls and mute options

### **Visual Effects**
- **Particle Effects**: Collectibles and impacts
- **Screen Transitions**: Fade in/out between scenes
- **UI Animations**: Button hover and click effects

## 🎮 Gameplay Features

### **Complete Game Loop**
- **Main Menu**: Game start and settings
- **Gameplay**: Core game mechanics
- **Pause System**: Pause and resume functionality
- **Game Over**: Win/lose conditions and scoring

### **Save System**
- **High Score Tracking**: Persistent high scores
- **Settings Persistence**: Audio and game settings
- **Progress Tracking**: Games played and total score
- **Cross-Session Data**: Data persists between game sessions

### **Audio System**
- **Background Music**: Dynamic music based on game state
- **Sound Effects**: Audio feedback for all actions
- **Volume Control**: Master, music, and SFX volume
- **Audio Settings**: Persistent audio preferences

## 🔧 Customization Options

### **Easy Modifications**
1. **Change game objectives**: Modify target score and time
2. **Add new UI elements**: Create additional menu options
3. **Modify audio**: Add new music and sound effects
4. **Adjust difficulty**: Change game parameters

### **Advanced Features**
1. **Achievement System**: Add unlockable achievements
2. **Multiple Levels**: Create level progression system
3. **Online Leaderboards**: Add online score sharing
4. **Cloud Save**: Implement cloud save functionality

## 🐛 Common Issues

### **UI not displaying correctly**
- **Cause**: Canvas settings or UI scaling issues
- **Solution**: Check Canvas settings and UI scaling mode

### **Audio not playing**
- **Cause**: Audio clips not assigned or audio sources not configured
- **Solution**: Assign audio clips and configure audio sources

### **Save system not working**
- **Cause**: File permissions or JSON serialization issues
- **Solution**: Check file permissions and JSON data structure

### **Build fails**
- **Cause**: Missing scenes or incorrect build settings
- **Solution**: Check Build Settings and ensure all scenes are added

## 📚 Learning Exercises

### **Beginner Exercises**
1. **Modify UI elements** and test different layouts
2. **Add new sound effects** and test audio system
3. **Change game parameters** and test gameplay
4. **Test save/load** functionality

### **Intermediate Exercises**
1. **Add new menu options** and navigation
2. **Implement achievement system** with unlockable rewards
3. **Create level progression** with multiple scenes
4. **Add settings persistence** for all game options

### **Advanced Exercises**
1. **Build complete game** with all features
2. **Implement online features** like leaderboards
3. **Add cloud save** functionality
4. **Create build automation** with CI/CD

## 🎯 Next Steps

After completing this project:

1. **Study the complete system**: Understand how all components work together
2. **Experiment with features**: Try adding new functionality
3. **Build and deploy**: Test the complete build process
4. **Create your own game**: Use this as a foundation for your own project

## 💡 Pro Tips

### **Game Development Tips**
- **Plan your game** before starting development
- **Test frequently** during development
- **Get feedback** from other players
- **Iterate and improve** based on feedback

### **UI Design Tips**
- **Keep UI simple** and intuitive
- **Use consistent styling** across all elements
- **Test on different screen sizes** and resolutions
- **Provide clear feedback** for user actions

### **Audio Tips**
- **Use appropriate volume levels** for different audio types
- **Provide audio feedback** for all user actions
- **Allow users to control** audio settings
- **Test audio** on different devices and platforms

---

**Happy Learning!** This project provides a complete understanding of Unity game development from start to finish. Take your time to explore and understand how all the systems work together!
