# Advanced Game Systems

This guide covers advanced game systems including scene management, game state, audio, save/load, and build systems.

## Scene Transition Manager

```csharp
public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager Instance;

    [Header("Transition Settings")]
    public GameObject transitionPanel;
    public Image fadeImage;
    public float transitionDuration = 1f;

    private bool isTransitioning = false;

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

    public void LoadScene(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToScene(sceneName));
        }
    }

    public void LoadScene(int sceneIndex)
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToScene(sceneIndex));
        }
    }

    IEnumerator TransitionToScene(string sceneName)
    {
        isTransitioning = true;
        yield return StartCoroutine(FadeOut());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeIn());
        isTransitioning = false;
    }

    IEnumerator TransitionToScene(int sceneIndex)
    {
        isTransitioning = true;
        yield return StartCoroutine(FadeOut());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeIn());
        isTransitioning = false;
    }

    IEnumerator FadeOut()
    {
        transitionPanel.SetActive(true);
        float elapsed = 0f;
        Color fadeColor = fadeImage.color;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = elapsed / transitionDuration;
            fadeColor.a = alpha;
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 1f;
        fadeImage.color = fadeColor;
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color fadeColor = fadeImage.color;

        while (elapsed < transitionDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = 1f - (elapsed / transitionDuration);
            fadeColor.a = alpha;
            fadeImage.color = fadeColor;
            yield return null;
        }

        fadeColor.a = 0f;
        fadeImage.color = fadeColor;
        transitionPanel.SetActive(false);
    }
}
```

## Game State Management

```csharp
public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Victory,
    Loading
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game Settings")]
    public int maxLives = 3;
    public float gameTimeLimit = 300f;

    private GameState currentState;
    private int playerScore;
    private int playerLives;
    private float gameTimer;
    private bool isGameActive;

    public System.Action<GameState> OnGameStateChanged;
    public System.Action<int> OnScoreChanged;
    public System.Action<int> OnLivesChanged;
    public System.Action<float> OnTimeChanged;

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
        SetGameState(GameState.MainMenu);
    }

    void Update()
    {
        if (currentState == GameState.Playing)
        {
            UpdateGameTimer();
        }
    }

    public void StartNewGame()
    {
        playerScore = 0;
        playerLives = maxLives;
        gameTimer = gameTimeLimit;
        isGameActive = true;

        SetGameState(GameState.Playing);

        OnScoreChanged?.Invoke(playerScore);
        OnLivesChanged?.Invoke(playerLives);
    }

    public void PauseGame()
    {
        if (currentState == GameState.Playing)
        {
            SetGameState(GameState.Paused);
            MenuManager.Instance.ShowPanel(MenuManager.Instance.pausePanel);
        }
    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            SetGameState(GameState.Playing);
            MenuManager.Instance.HideCurrentPanel();
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        SetGameState(GameState.GameOver);

        SaveSystem.Instance.SaveHighScore(playerScore);
        MenuManager.Instance.ShowPanel(MenuManager.Instance.gameOverPanel);
    }

    public void Victory()
    {
        isGameActive = false;
        SetGameState(GameState.Victory);

        int timeBonus = Mathf.RoundToInt(gameTimer * 10);
        AddScore(timeBonus);

        SaveSystem.Instance.SaveHighScore(playerScore);
    }

    public void AddScore(int points)
    {
        playerScore += points;
        OnScoreChanged?.Invoke(playerScore);
        HUDManager.Instance.UpdateScore(playerScore);
    }

    public void LoseLife()
    {
        playerLives--;
        OnLivesChanged?.Invoke(playerLives);

        HUDManager.Instance.UpdateLives(playerLives);

        if (playerLives <= 0)
        {
            GameOver();
        }
    }

    void UpdateGameTimer()
    {
        if (isGameActive && gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
            OnTimeChanged?.Invoke(gameTimer);

            if (gameTimer <= 0)
            {
                gameTimer = 0;
                GameOver();
            }
        }
    }

    void SetGameState(GameState newState)
    {
        currentState = newState;
        OnGameStateChanged?.Invoke(currentState);
        Debug.Log($"Game state changed to: {currentState}");
    }

    public GameState GetGameState() => currentState;
    public int GetScore() => playerScore;
    public int GetLives() => playerLives;
    public float GetTimeRemaining() => gameTimer;
}
```

## Audio Manager

```csharp
[System.Serializable]
public class AudioClipData
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    [Range(0.5f, 1.5f)] public float pitch = 1f;
    public bool loop = false;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource ambientSource;

    [Header("Audio Clips")]
    public AudioClipData[] musicClips;
    public AudioClipData[] sfxClips;
    public AudioClipData[] ambientClips;

    [Header("Audio Settings")]
    [Range(0f, 1f)] public float masterVolume = 1f;
    [Range(0f, 1f)] public float musicVolume = 0.7f;
    [Range(0f, 1f)] public float sfxVolume = 1f;

    private Dictionary<string, AudioClipData> musicDatabase;
    private Dictionary<string, AudioClipData> sfxDatabase;
    private Dictionary<string, AudioClipData> ambientDatabase;

    private string currentMusicTrack;
    private bool musicPaused = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAudioDatabases();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeAudioDatabases()
    {
        musicDatabase = new Dictionary<string, AudioClipData>();
        sfxDatabase = new Dictionary<string, AudioClipData>();
        ambientDatabase = new Dictionary<string, AudioClipData>();

        foreach (var clip in musicClips)
            musicDatabase[clip.name] = clip;

        foreach (var clip in sfxClips)
            sfxDatabase[clip.name] = clip;

        foreach (var clip in ambientClips)
            ambientDatabase[clip.name] = clip;
    }

    public void PlayMusic(string trackName)
    {
        if (musicDatabase.ContainsKey(trackName))
        {
            var clipData = musicDatabase[trackName];

            musicSource.clip = clipData.clip;
            musicSource.volume = clipData.volume * musicVolume * masterVolume;
            musicSource.pitch = clipData.pitch;
            musicSource.loop = clipData.loop;
            musicSource.Play();

            currentMusicTrack = trackName;
            musicPaused = false;
        }
        else
        {
            Debug.LogWarning($"Music track '{trackName}' not found!");
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
        currentMusicTrack = "";
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
            musicPaused = true;
        }
    }

    public void ResumeMusic()
    {
        if (musicPaused)
        {
            musicSource.UnPause();
            musicPaused = false;
        }
    }

    public void PlaySFX(string sfxName)
    {
        if (sfxDatabase.ContainsKey(sfxName))
        {
            var clipData = sfxDatabase[sfxName];
            sfxSource.pitch = clipData.pitch;
            sfxSource.PlayOneShot(clipData.clip, clipData.volume * sfxVolume * masterVolume);
        }
        else
        {
            Debug.LogWarning($"SFX '{sfxName}' not found!");
        }
    }

    public void PlaySFXAtPosition(string sfxName, Vector3 position)
    {
        if (sfxDatabase.ContainsKey(sfxName))
        {
            var clipData = sfxDatabase[sfxName];
            AudioSource.PlayClipAtPoint(clipData.clip, position, clipData.volume * sfxVolume * masterVolume);
        }
    }

    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        UpdateAllVolumes();
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        musicSource.volume = musicVolume * masterVolume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
    }

    void UpdateAllVolumes()
    {
        if (musicSource.clip != null)
        {
            var clipData = musicDatabase[currentMusicTrack];
            musicSource.volume = clipData.volume * musicVolume * masterVolume;
        }
    }

    public void CrossfadeToMusic(string newTrackName, float duration = 2f)
    {
        StartCoroutine(CrossfadeRoutine(newTrackName, duration));
    }

    IEnumerator CrossfadeRoutine(string newTrackName, float duration)
    {
        float startVolume = musicSource.volume;

        float elapsed = 0f;
        while (elapsed < duration / 2f)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / (duration / 2f));
            yield return null;
        }

        PlayMusic(newTrackName);
        musicSource.volume = 0f;

        var clipData = musicDatabase[newTrackName];
        float targetVolume = clipData.volume * musicVolume * masterVolume;

        elapsed = 0f;
        while (elapsed < duration / 2f)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(0f, targetVolume, elapsed / (duration / 2f));
            yield return null;
        }

        musicSource.volume = targetVolume;
    }
}
```

## Save System

```csharp
[System.Serializable]
public class PlayerData
{
    public int highScore;
    public int totalPlayTime;
    public int gamesPlayed;
    public float masterVolume = 1f;
    public float musicVolume = 0.7f;
    public float sfxVolume = 1f;
    public bool fullscreen = true;
    public int resolutionIndex = 0;
    public string playerName = "Player";
    public KeyCode[] customKeyBindings;
}

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    private PlayerData playerData;
    private string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            savePath = Path.Combine(Application.persistentDataPath, "playerdata.json");
            LoadPlayerData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayerData()
    {
        try
        {
            string jsonData = JsonUtility.ToJson(playerData, true);
            File.WriteAllText(savePath, jsonData);
            Debug.Log("Player data saved successfully!");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save player data: {e.Message}");
        }
    }

    public void LoadPlayerData()
    {
        if (File.Exists(savePath))
        {
            try
            {
                string jsonData = File.ReadAllText(savePath);
                playerData = JsonUtility.FromJson<PlayerData>(jsonData);
                Debug.Log("Player data loaded successfully!");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load player data: {e.Message}");
                CreateDefaultPlayerData();
            }
        }
        else
        {
            CreateDefaultPlayerData();
        }
    }

    void CreateDefaultPlayerData()
    {
        playerData = new PlayerData();
        SavePlayerData();
    }

    public void SaveHighScore(int score)
    {
        if (score > playerData.highScore)
        {
            playerData.highScore = score;
            SavePlayerData();
        }
    }

    public int GetHighScore()
    {
        return playerData.highScore;
    }

    public void SaveAudioSettings(float master, float music, float sfx)
    {
        playerData.masterVolume = master;
        playerData.musicVolume = music;
        playerData.sfxVolume = sfx;
        SavePlayerData();
    }

    public void SaveGraphicsSettings(bool fullscreen, int resolutionIndex)
    {
        playerData.fullscreen = fullscreen;
        playerData.resolutionIndex = resolutionIndex;
        SavePlayerData();
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }
}
```

## Build Manager

```csharp
public class BuildManager : MonoBehaviour
{
    [Header("Build Settings")]
    public string productName = "My 2D Game";
    public string companyName = "My Game Studio";
    public string version = "1.0.0";

    [Header("Optimization")]
    public bool developmentBuild = false;
    public bool scriptDebugging = false;
    public CompressionType compressionType = CompressionType.Lz4HC;

    public void ConfigureBuildSettings()
    {
        PlayerSettings.productName = productName;
        PlayerSettings.companyName = companyName;
        PlayerSettings.bundleVersion = version;

        PlayerSettings.colorSpace = ColorSpace.Linear;
        PlayerSettings.gpuSkinning = false;

        PlayerSettings.stripEngineCode = true;
        PlayerSettings.managed.strippingLevel = ManagedStrippingLevel.Medium;

        Debug.Log("Build settings configured!");
    }

    public void BuildGame()
    {
        string[] scenes = GetScenesInBuild();
        string buildPath = "Builds/" + productName;

        BuildPlayerOptions buildOptions = new BuildPlayerOptions
        {
            scenes = scenes,
            locationPathName = buildPath,
            target = EditorUserBuildSettings.activeBuildTarget,
            options = developmentBuild ? BuildOptions.Development : BuildOptions.None
        };

        var report = BuildPipeline.BuildPlayer(buildOptions);

        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build completed successfully!");
        }
        else
        {
            Debug.LogError("Build failed!");
        }
    }

    string[] GetScenesInBuild()
    {
        List<string> sceneNames = new List<string>();

        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                sceneNames.Add(scene.path);
            }
        }

        return sceneNames.ToArray();
    }
}
```

---

For more game system examples, see `extras/common-scripts-library/`.

