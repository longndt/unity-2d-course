# Lab 5: UI & Complete Game

## 🎯 Learning Objectives

- Master Unity's UI System (uGUI) and Canvas setup
- Create comprehensive menu systems and HUD
- Implement game state management and scene transitions
- Build complete game with audio, save system, and deployment
- Learn professional game development workflows

## 🎮 Playtest Criteria

**Complete when you can:**
- [ ] Create responsive UI systems with proper scaling
- [ ] Implement complete menu navigation and game flow
- [ ] Set up audio system with music and sound effects
- [ ] Create save/load system for game progress
- [ ] Build and deploy game for target platform

---

## 🚀 Quick Start

### Step 1: Prepare Your Project

1. **Open your Unity project** from previous lessons
2. **Create new scene**: `File → New Scene → 2D`
3. **Save scene** as "CompleteGameLab"
4. **Create folder structure**:
   - `Assets/UI/`
   - `Assets/Audio/`
   - `Assets/Scripts/UI/`
   - `Assets/Scripts/Game/`

### Step 2: Import Required Packages

1. **Window → Package Manager**
2. **Install packages**:
   - **TextMeshPro** (if not already installed)
   - **Input System** (if not already installed)
   - **Cinemachine** (if not already installed)

---

## 🎯 Lab Tasks

### Task 1: UI System Setup

#### **1.1 Create Canvas and UI Structure**

**Create Main Canvas:**
1. **Right-click in Hierarchy** → **UI** → **Canvas**
2. **Rename** to "MainCanvas"
3. **Configure Canvas**:
   - **Render Mode**: Screen Space - Overlay
   - **UI Scale Mode**: Scale With Screen Size
   - **Reference Resolution**: 1920 x 1080
   - **Screen Match Mode**: Match Width Or Height
   - **Match**: 0.5

**Create UI Panels:**
1. **Right-click MainCanvas** → **UI** → **Panel**
2. **Rename** to "MainMenuPanel"
3. **Create more panels**:
   - **GameplayPanel**
   - **PauseMenuPanel**
   - **GameOverPanel**
   - **SettingsPanel**

#### **1.2 Create Main Menu UI**

**Create Main Menu Elements:**
1. **Select MainMenuPanel** in Hierarchy
2. **Add UI elements**:
   - **Text (TextMeshPro)**: "Game Title"
   - **Button**: "Play Button"
   - **Button**: "Settings Button"
   - **Button**: "Quit Button"

**Configure Main Menu:**
1. **Select "Game Title"** text
2. **In Inspector**, configure:
   - **Text**: "My Game"
   - **Font Size**: 48
   - **Alignment**: Center
   - **Color**: White
3. **Position** at top center of screen

**Configure Buttons:**
1. **Select "Play Button"**
2. **In Inspector**, configure:
   - **Text**: "Play"
   - **Font Size**: 24
   - **Color**: White
3. **Position** in center of screen
4. **Repeat** for other buttons

#### **1.3 Create Gameplay HUD**

**Create HUD Elements:**
1. **Select GameplayPanel** in Hierarchy
2. **Add UI elements**:
   - **Text (TextMeshPro)**: "ScoreText"
   - **Text (TextMeshPro)**: "LivesText"
   - **Text (TextMeshPro)**: "TimeText"
   - **Image**: "HealthBar"
   - **Image**: "HealthBarFill"

**Configure HUD:**
1. **Select "ScoreText"**
2. **In Inspector**, configure:
   - **Text**: "Score: 0"
   - **Font Size**: 24
   - **Color**: White
3. **Position** at top left
4. **Repeat** for other HUD elements

**Create Health Bar:**
1. **Select "HealthBar"** image
2. **In Inspector**, configure:
   - **Image Type**: Filled
   - **Fill Method**: Horizontal
   - **Fill Amount**: 1
   - **Color**: Red
3. **Position** at top center

### Task 2: Menu System Implementation

#### **2.1 Create Menu Manager Script**

**Create Menu Manager:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "MenuManager"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject gameplayPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;
    public GameObject settingsPanel;
    
    [Header("UI Elements")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI timeText;
    public Slider healthBar;
    
    [Header("Game Settings")]
    public int startingLives = 3;
    public float gameTime = 60f;
    
    private int currentScore = 0;
    private int currentLives;
    private float currentTime;
    private bool isPaused = false;
    
    void Start()
    {
        currentLives = startingLives;
        currentTime = gameTime;
        ShowMainMenu();
    }
    
    void Update()
    {
        // Handle pause input
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
        // Update game timer
        if (currentTime > 0 && !isPaused)
        {
            currentTime -= Time.deltaTime;
            UpdateTimeDisplay();
            
            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }
    
    public void ShowMainMenu()
    {
        HideAllPanels();
        mainMenuPanel.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void StartGame()
    {
        HideAllPanels();
        gameplayPanel.SetActive(true);
        currentScore = 0;
        currentLives = startingLives;
        currentTime = gameTime;
        UpdateUI();
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    
    public void ShowSettings()
    {
        HideAllPanels();
        settingsPanel.SetActive(true);
    }
    
    public void GameOver()
    {
        HideAllPanels();
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }
    
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();
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
    
    public void UpdateHealth(float healthPercentage)
    {
        if (healthBar != null)
        {
            healthBar.value = healthPercentage;
        }
    }
    
    void HideAllPanels()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (gameplayPanel != null) gameplayPanel.SetActive(false);
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (gameOverPanel != null) gameOverPanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }
    
    void UpdateUI()
    {
        UpdateScoreDisplay();
        UpdateLivesDisplay();
        UpdateTimeDisplay();
    }
    
    void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {currentScore}";
        }
    }
    
    void UpdateLivesDisplay()
    {
        if (livesText != null)
        {
            livesText.text = $"Lives: {currentLives}";
        }
    }
    
    void UpdateTimeDisplay()
    {
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(currentTime / 60);
            int seconds = Mathf.FloorToInt(currentTime % 60);
            timeText.text = $"Time: {minutes:00}:{seconds:00}";
        }
    }
}
```

#### **2.2 Create Button Event Handlers**

**Create Button Handler Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "ButtonHandler"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    [Header("Button References")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Button resumeButton;
    public Button mainMenuButton;
    
    private MenuManager menuManager;
    
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        SetupButtonEvents();
    }
    
    void SetupButtonEvents()
    {
        if (playButton != null)
        {
            playButton.onClick.AddListener(() => menuManager.StartGame());
        }
        
        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(() => menuManager.ShowSettings());
        }
        
        if (quitButton != null)
        {
            quitButton.onClick.AddListener(() => menuManager.QuitGame());
        }
        
        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(() => menuManager.ResumeGame());
        }
        
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(() => menuManager.ShowMainMenu());
        }
    }
}
```

#### **2.3 Test Menu System**

**Setup Menu System:**
1. **Attach MenuManager script** to empty GameObject
2. **Assign UI panels** in Inspector
3. **Attach ButtonHandler script** to same GameObject
4. **Assign buttons** in Inspector
5. **Test menu navigation** with buttons

### Task 3: Game State Management

#### **3.1 Create Game State Manager**

**Create Game State Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "GameStateManager"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    [Header("Game State")]
    public GameState currentState = GameState.MainMenu;
    
    [Header("Game Settings")]
    public float gameTime = 60f;
    public int maxLives = 3;
    public int targetScore = 1000;
    
    [Header("Events")]
    public System.Action<GameState> OnStateChanged;
    public System.Action<int> OnScoreChanged;
    public System.Action<int> OnLivesChanged;
    public System.Action<float> OnTimeChanged;
    
    private int currentScore = 0;
    private int currentLives;
    private float currentTime;
    private bool isPaused = false;
    
    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Victory
    }
    
    void Start()
    {
        currentLives = maxLives;
        currentTime = gameTime;
        ChangeState(GameState.MainMenu);
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
    
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        OnStateChanged?.Invoke(currentState);
        
        Debug.Log($"Game state changed to: {currentState}");
    }
    
    public void StartGame()
    {
        currentScore = 0;
        currentLives = maxLives;
        currentTime = gameTime;
        isPaused = false;
        
        OnScoreChanged?.Invoke(currentScore);
        OnLivesChanged?.Invoke(currentLives);
        OnTimeChanged?.Invoke(currentTime);
        
        ChangeState(GameState.Playing);
    }
    
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
        ChangeState(GameState.Paused);
    }
    
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        ChangeState(GameState.Playing);
    }
    
    public void GameOver()
    {
        Time.timeScale = 0f;
        ChangeState(GameState.GameOver);
    }
    
    public void Victory()
    {
        Time.timeScale = 0f;
        ChangeState(GameState.Victory);
    }
    
    public void AddScore(int points)
    {
        currentScore += points;
        OnScoreChanged?.Invoke(currentScore);
        
        Debug.Log($"Score added: {points}. Total: {currentScore}");
    }
    
    public void LoseLife()
    {
        currentLives--;
        OnLivesChanged?.Invoke(currentLives);
        
        if (currentLives <= 0)
        {
            GameOver();
        }
        
        Debug.Log($"Life lost. Remaining: {currentLives}");
    }
    
    void UpdateGameTime()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            OnTimeChanged?.Invoke(currentTime);
            
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
    
    public int GetScore() => currentScore;
    public int GetLives() => currentLives;
    public float GetTime() => currentTime;
    public bool IsPaused() => isPaused;
}
```

#### **3.2 Create Scene Transition Manager**

**Create Scene Transition Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "SceneTransitionManager"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [Header("Transition Settings")]
    public float fadeTime = 1f;
    public CanvasGroup fadeCanvasGroup;
    
    [Header("Scene Names")]
    public string mainMenuScene = "MainMenu";
    public string gameplayScene = "Gameplay";
    public string settingsScene = "Settings";
    
    private bool isTransitioning = false;
    
    void Start()
    {
        if (fadeCanvasGroup == null)
        {
            CreateFadeCanvas();
        }
    }
    
    public void LoadScene(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToScene(sceneName));
        }
    }
    
    public void LoadMainMenu()
    {
        LoadScene(mainMenuScene);
    }
    
    public void LoadGameplay()
    {
        LoadScene(gameplayScene);
    }
    
    public void LoadSettings()
    {
        LoadScene(settingsScene);
    }
    
    IEnumerator TransitionToScene(string sceneName)
    {
        isTransitioning = true;
        
        // Fade out
        yield return StartCoroutine(FadeOut());
        
        // Load scene
        SceneManager.LoadScene(sceneName);
        
        // Fade in
        yield return StartCoroutine(FadeIn());
        
        isTransitioning = false;
    }
    
    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeTime);
            yield return null;
        }
        
        fadeCanvasGroup.alpha = 1f;
    }
    
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        
        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            yield return null;
        }
        
        fadeCanvasGroup.alpha = 0f;
    }
    
    void CreateFadeCanvas()
    {
        // Create fade canvas
        GameObject fadeCanvas = new GameObject("FadeCanvas");
        fadeCanvas.AddComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
        fadeCanvas.AddComponent<CanvasGroup>();
        
        // Create fade image
        GameObject fadeImage = new GameObject("FadeImage");
        fadeImage.transform.SetParent(fadeCanvas.transform);
        fadeImage.AddComponent<UnityEngine.UI.Image>().color = Color.black;
        
        // Set up canvas group
        fadeCanvasGroup = fadeCanvas.GetComponent<CanvasGroup>();
        fadeCanvasGroup.alpha = 0f;
    }
}
```

### Task 4: Audio System Implementation

#### **4.1 Create Audio Manager**

**Create Audio Manager Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "AudioManager"
3. **Replace content** with:

```csharp
using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource ambientSource;
    
    [Header("Audio Clips")]
    public AudioClip backgroundMusic;
    public AudioClip jumpSound;
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
    
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
        PlayBackgroundMusic();
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
    
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && musicSource != null)
        {
            musicSource.clip = backgroundMusic;
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
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
    }
    
    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }
    
    public void PauseMusic()
    {
        if (musicSource != null)
        {
            musicSource.Pause();
        }
    }
    
    public void ResumeMusic()
    {
        if (musicSource != null)
        {
            musicSource.UnPause();
        }
    }
}
```

#### **4.2 Create Audio Settings UI**

**Create Audio Settings Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "AudioSettingsUI"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsUI : MonoBehaviour
{
    [Header("Audio Sliders")]
    public Slider masterVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider sfxVolumeSlider;
    
    [Header("Audio Buttons")]
    public Button muteButton;
    public Button unmuteButton;
    
    private AudioManager audioManager;
    private bool isMuted = false;
    
    void Start()
    {
        audioManager = AudioManager.Instance;
        SetupSliders();
        SetupButtons();
    }
    
    void SetupSliders()
    {
        if (masterVolumeSlider != null)
        {
            masterVolumeSlider.value = audioManager.masterVolume;
            masterVolumeSlider.onValueChanged.AddListener(OnMasterVolumeChanged);
        }
        
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = audioManager.musicVolume;
            musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        }
        
        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = audioManager.sfxVolume;
            sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
        }
    }
    
    void SetupButtons()
    {
        if (muteButton != null)
        {
            muteButton.onClick.AddListener(MuteAudio);
        }
        
        if (unmuteButton != null)
        {
            unmuteButton.onClick.AddListener(UnmuteAudio);
        }
    }
    
    void OnMasterVolumeChanged(float value)
    {
        audioManager.SetMasterVolume(value);
    }
    
    void OnMusicVolumeChanged(float value)
    {
        audioManager.SetMusicVolume(value);
    }
    
    void OnSFXVolumeChanged(float value)
    {
        audioManager.SetSFXVolume(value);
    }
    
    void MuteAudio()
    {
        audioManager.SetMasterVolume(0f);
        isMuted = true;
        UpdateMuteButtons();
    }
    
    void UnmuteAudio()
    {
        audioManager.SetMasterVolume(1f);
        isMuted = false;
        UpdateMuteButtons();
    }
    
    void UpdateMuteButtons()
    {
        if (muteButton != null)
        {
            muteButton.gameObject.SetActive(!isMuted);
        }
        
        if (unmuteButton != null)
        {
            unmuteButton.gameObject.SetActive(isMuted);
        }
    }
}
```

### Task 5: Save System Implementation

#### **5.1 Create Save System**

**Create Save System Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "SaveSystem"
3. **Replace content** with:

```csharp
using UnityEngine;
using System.IO;
using System;

[System.Serializable]
public class GameData
{
    public int highScore;
    public int totalScore;
    public int gamesPlayed;
    public float musicVolume;
    public float sfxVolume;
    public bool firstTimePlaying;
    public string playerName;
    
    public GameData()
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
    
    private GameData gameData;
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
        catch (Exception e)
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
                gameData = JsonUtility.FromJson<GameData>(jsonData);
                Debug.Log("Game loaded successfully");
            }
            else
            {
                gameData = new GameData();
                Debug.Log("No save file found, creating new game data");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load game: {e.Message}");
            gameData = new GameData();
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
    
    public void DeleteSave()
    {
        try
        {
            if (File.Exists(savePath))
            {
                File.Delete(savePath);
                gameData = new GameData();
                Debug.Log("Save file deleted");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to delete save: {e.Message}");
        }
    }
}
```

#### **5.2 Create High Score Display**

**Create High Score UI Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "HighScoreDisplay"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class HighScoreDisplay : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI gamesPlayedText;
    public TextMeshProUGUI playerNameText;
    
    private SaveSystem saveSystem;
    
    void Start()
    {
        saveSystem = SaveSystem.Instance;
        UpdateDisplay();
    }
    
    public void UpdateDisplay()
    {
        if (saveSystem != null)
        {
            if (highScoreText != null)
            {
                highScoreText.text = $"High Score: {saveSystem.GetHighScore()}";
            }
            
            if (totalScoreText != null)
            {
                totalScoreText.text = $"Total Score: {saveSystem.GetTotalScore()}";
            }
            
            if (gamesPlayedText != null)
            {
                gamesPlayedText.text = $"Games Played: {saveSystem.GetGamesPlayed()}";
            }
            
            if (playerNameText != null)
            {
                playerNameText.text = $"Player: {saveSystem.GetPlayerName()}";
            }
        }
    }
}
```

### Task 6: Build and Deployment

#### **6.1 Configure Build Settings**

**Set Up Build Settings:**
1. **File → Build Settings**
2. **Add Open Scenes** to build
3. **Select Platform** (PC, Mac & Linux Standalone)
4. **Click "Player Settings"**

**Configure Player Settings:**
1. **In Player Settings**, configure:
   - **Company Name**: Your company name
   - **Product Name**: Your game name
   - **Version**: 1.0.0
   - **Icon**: Set game icon
   - **Resolution and Presentation**: Set default resolution
   - **Splash Image**: Set splash screen

#### **6.2 Optimize for Build**

**Create Build Optimizer Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "BuildOptimizer"
3. **Replace content** with:

```csharp
using UnityEngine;

public class BuildOptimizer : MonoBehaviour
{
    [Header("Optimization Settings")]
    public bool optimizeForBuild = true;
    public bool disableDebugLogs = true;
    public bool reduceQuality = true;
    
    void Start()
    {
        if (optimizeForBuild)
        {
            OptimizeForBuild();
        }
    }
    
    void OptimizeForBuild()
    {
        // Disable debug logs in build
        if (disableDebugLogs)
        {
            Debug.unityLogger.logEnabled = false;
        }
        
        // Reduce quality settings
        if (reduceQuality)
        {
            QualitySettings.SetQualityLevel(0); // Fastest
        }
        
        // Optimize rendering
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        
        Debug.Log("Build optimizations applied");
    }
    
    void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            // Save game when paused
            SaveSystem.Instance?.SaveGame();
        }
    }
    
    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // Save game when focus lost
            SaveSystem.Instance?.SaveGame();
        }
    }
}
```

#### **6.3 Create Build Script**

**Create Build Script:**
1. **Right-click in Project** → **Create** → **C# Script**
2. **Name it** "BuildScript"
3. **Replace content** with:

```csharp
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildScript
{
    [MenuItem("Build/Build All")]
    public static void BuildAll()
    {
        BuildWindows();
        BuildMac();
        BuildLinux();
    }
    
    [MenuItem("Build/Build Windows")]
    public static void BuildWindows()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/Windows/MyGame.exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;
        
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
    
    [MenuItem("Build/Build Mac")]
    public static void BuildMac()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/Mac/MyGame.app";
        buildPlayerOptions.target = BuildTarget.StandaloneOSX;
        buildPlayerOptions.options = BuildOptions.None;
        
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
    
    [MenuItem("Build/Build Linux")]
    public static void BuildLinux()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = GetScenePaths();
        buildPlayerOptions.locationPathName = "Builds/Linux/MyGame";
        buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
        buildPlayerOptions.options = BuildOptions.None;
        
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
    
    static string[] GetScenePaths()
    {
        string[] scenes = new string[EditorBuildSettings.scenes.Length];
        for (int i = 0; i < scenes.Length; i++)
        {
            scenes[i] = EditorBuildSettings.scenes[i].path;
        }
        return scenes;
    }
}
```

#### **6.4 Test Build Process**

**Test Build:**
1. **File → Build Settings**
2. **Click "Build"** to create executable
3. **Test the build** to ensure it works
4. **Check performance** and functionality
5. **Verify all features** work correctly

---

## ✅ Completion Checklist

### **UI System Setup**
- [ ] **Created Canvas and UI structure** with proper scaling
- [ ] **Implemented main menu** with navigation
- [ ] **Created gameplay HUD** with score, lives, and health
- [ ] **Set up UI panels** for different game states

### **Menu System Implementation**
- [ ] **Created MenuManager script** for game state management
- [ ] **Implemented button event handlers** for navigation
- [ ] **Tested menu navigation** between different screens
- [ ] **Verified UI responsiveness** on different screen sizes

### **Game State Management**
- [ ] **Implemented GameStateManager** for game flow control
- [ ] **Created scene transition system** with fade effects
- [ ] **Set up game state events** for UI updates
- [ ] **Tested complete game flow** from start to finish

### **Audio System Implementation**
- [ ] **Created AudioManager** for centralized audio control
- [ ] **Implemented audio settings UI** with volume controls
- [ ] **Set up background music** and sound effects
- [ ] **Tested audio system** with different volume levels

### **Save System Implementation**
- [ ] **Created SaveSystem** for game data persistence
- [ ] **Implemented high score tracking** and display
- [ ] **Set up audio settings** persistence
- [ ] **Tested save/load functionality** across game sessions

### **Build and Deployment**
- [ ] **Configured build settings** for target platform
- [ ] **Optimized game** for build performance
- [ ] **Created build scripts** for automated building
- [ ] **Tested final build** and verified functionality

---

## 🚨 Troubleshooting

### **Common Issues and Solutions**

#### **UI not displaying correctly**
**Possible causes:**
- Canvas settings incorrect
- UI scaling not configured
- UI elements not positioned properly

**Solutions:**
1. Check Canvas settings and scaling mode
2. Verify UI elements are positioned correctly
3. Test on different screen resolutions
4. Check UI element hierarchy

#### **Menu navigation not working**
**Possible causes:**
- Button events not assigned
- MenuManager not configured
- UI panels not set up correctly

**Solutions:**
1. Verify button event assignments
2. Check MenuManager script configuration
3. Ensure UI panels are properly set up
4. Test button functionality individually

#### **Audio not playing**
**Possible causes:**
- Audio clips not assigned
- Audio sources not configured
- Volume settings too low

**Solutions:**
1. Check audio clip assignments
2. Verify audio source configuration
3. Test volume settings
4. Check audio system setup

#### **Save system not working**
**Possible causes:**
- Save path incorrect
- JSON serialization issues
- File permissions problems

**Solutions:**
1. Verify save path and permissions
2. Check JSON serialization
3. Test save/load functionality
4. Debug save system errors

#### **Build fails or doesn't work**
**Possible causes:**
- Missing scenes in build settings
- Platform-specific issues
- Optimization problems

**Solutions:**
1. Check build settings and scene inclusion
2. Test on target platform
3. Verify optimization settings
4. Check for platform-specific issues

---

## 📝 Self-Assessment

After completing this lab, test your understanding with the interactive quiz:

**Take the Quiz**: [`quiz/quiz5.html`](../quiz/quiz5.html)

The quiz will help you:
- Verify your understanding of key concepts
- Identify areas that need review
- Prepare for advanced topics

---

## 📚 Next Steps

### **Immediate Next Steps**
1. **Complete all tasks** in this lab
2. **Test your complete game** thoroughly
3. **Build and deploy** your game
4. **Share your game** with others for feedback

### **Further Development**
1. **Add more features** to your game
2. **Improve graphics** and animations
3. **Add more levels** and gameplay
4. **Implement multiplayer** features

### **Career Development**
1. **Build a portfolio** with your games
2. **Share your work** on social media
3. **Join game development** communities
4. **Continue learning** advanced Unity features

---

## 💡 Pro Tips

### **UI Development Best Practices**
- **Use consistent styling** across all UI elements
- **Test on different screen sizes** and resolutions
- **Implement responsive design** for various devices
- **Keep UI simple** and intuitive

### **Game Development Tips**
- **Plan your game** before starting development
- **Test frequently** during development
- **Get feedback** from other players
- **Iterate and improve** based on feedback

### **Build and Deployment**
- **Test builds** on target platforms
- **Optimize performance** for smooth gameplay
- **Create multiple builds** for different platforms
- **Document your build process** for future reference

---

