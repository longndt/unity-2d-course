# Lesson 5 Reference - UI & Complete Game

## UI Canvas Setup

### Canvas Types
- **Screen Space - Overlay**: Renders on top of everything
- **Screen Space - Camera**: Renders through specific camera
- **World Space**: 3D UI in world space

### Canvas Scaler
- **Scale With Screen Size**: Responsive UI
- **Reference Resolution**: 1920x1080 (16:9)
- **Screen Match Mode**: Match Width Or Height
- **Match**: 0.5 (balance width/height)

## UI Components

### Button
```csharp
public class UIButton : MonoBehaviour {
    [SerializeField] private Button button;
    [SerializeField] private UnityEvent onClick;

    void Start() {
        button.onClick.AddListener(() => onClick.Invoke());
    }
}
```

### Slider
```csharp
public class UISlider : MonoBehaviour {
    [SerializeField] private Slider slider;
    [SerializeField] private Text valueText;

    void Start() {
        slider.onValueChanged.AddListener(OnValueChanged);
    }

    void OnValueChanged(float value) {
        valueText.text = value.ToString("F1");
    }
}
```

### Health Bar
```csharp
public class HealthBar : MonoBehaviour {
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Image fillImage;

    public void SetHealth(float current, float max) {
        healthSlider.value = current / max;
        fillImage.color = Color.Lerp(Color.red, Color.green, current / max);
    }
}
```

## Game State Management

### Game States
```csharp
public enum GameState {
    MainMenu,
    Playing,
    Paused,
    GameOver,
    Victory
}

public class GameManager : MonoBehaviour {
    public static GameManager Instance;
    public GameState CurrentState { get; private set; }

    void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void ChangeState(GameState newState) {
        CurrentState = newState;
        OnStateChanged(newState);
    }
}
```

### Pause System
```csharp
public class PauseManager : MonoBehaviour {
    [SerializeField] private GameObject pauseMenu;

    public void PauseGame() {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        GameManager.Instance.ChangeState(GameState.Paused);
    }

    public void ResumeGame() {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        GameManager.Instance.ChangeState(GameState.Playing);
    }
}
```

## Scene Management

### Scene Loading
```csharp
public class SceneLoader : MonoBehaviour {
    [SerializeField] private string sceneName;
    [SerializeField] private float delay = 0f;

    public void LoadScene() {
        StartCoroutine(LoadSceneCoroutine());
    }

    IEnumerator LoadSceneCoroutine() {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}
```

### Async Scene Loading
```csharp
public class AsyncSceneLoader : MonoBehaviour {
    [SerializeField] private Slider loadingBar;

    public void LoadSceneAsync(string sceneName) {
        StartCoroutine(LoadSceneAsyncCoroutine(sceneName));
    }

    IEnumerator LoadSceneAsyncCoroutine(string sceneName) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone) {
            loadingBar.value = operation.progress;
            yield return null;
        }
    }
}
```

## Save/Load System

### PlayerPrefs (Simple)
```csharp
public class SaveManager : MonoBehaviour {
    public void SaveGame() {
        PlayerPrefs.SetFloat("PlayerHealth", playerHealth);
        PlayerPrefs.SetInt("PlayerScore", playerScore);
        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();
    }

    public void LoadGame() {
        playerHealth = PlayerPrefs.GetFloat("PlayerHealth", 100f);
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        playerName = PlayerPrefs.GetString("PlayerName", "Player");
    }
}
```

### JSON Save System
```csharp
[System.Serializable]
public class GameData {
    public float playerHealth;
    public int playerScore;
    public string playerName;
    public Vector3 playerPosition;
}

public class JSONSaveManager : MonoBehaviour {
    public void SaveGame(GameData data) {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.persistentDataPath + "/savegame.json", json);
    }

    public GameData LoadGame() {
        string path = Application.persistentDataPath + "/savegame.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<GameData>(json);
        }
        return new GameData();
    }
}
```

## Build Settings

### Build Configuration
```csharp
public class BuildManager : MonoBehaviour {
    [SerializeField] private string[] scenesToBuild;
    [SerializeField] private BuildTarget buildTarget;

    public void BuildGame() {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenesToBuild;
        buildPlayerOptions.locationPathName = "Builds/MyGame.exe";
        buildPlayerOptions.target = buildTarget;
        buildPlayerOptions.options = BuildOptions.None;

        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}
```

### Platform-Specific Settings
```csharp
// Android Settings
PlayerSettings.Android.bundleVersionCode = 1;
PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel21;
PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevel33;
PlayerSettings.Android.keystoreName = "user.keystore";
PlayerSettings.Android.keyaliasName = "mykey";

// iOS Settings
PlayerSettings.iOS.buildNumber = "1";
PlayerSettings.iOS.targetOSVersionString = "12.0";

// Windows Settings
PlayerSettings.SetScriptingBackend(BuildTargetGroup.Standalone, ScriptingImplementation.Mono);

// WebGL Settings
PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Gzip;
PlayerSettings.WebGL.memorySize = 16;
```

## Performance Optimization

### UI Optimization
```csharp
public class UIOptimizer : MonoBehaviour {
    [SerializeField] private CanvasGroup canvasGroup;

    void Start() {
        // Disable raycast target when not needed
        canvasGroup.blocksRaycasts = false;

        // Use object pooling for frequently created UI elements
        ObjectPooler.Instance.CreatePool(uiElementPrefab, 10);
    }
}
```

### Build Optimization
```csharp
public class BuildOptimizer : MonoBehaviour {
    void Start() {
        // Disable debug logs in builds
        #if !UNITY_EDITOR
        Debug.unityLogger.logEnabled = false;
        #endif

        // Set target frame rate
        Application.targetFrameRate = 60;

        // Enable vsync
        QualitySettings.vSyncCount = 1;
    }
}
```

## Common UI Patterns

### Menu Navigation
```csharp
public class MenuNavigation : MonoBehaviour {
    [SerializeField] private Button[] menuButtons;
    private int currentIndex = 0;

    void Update() {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            NavigateUp();
        } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
            NavigateDown();
        } else if (Input.GetKeyDown(KeyCode.Return)) {
            SelectButton();
        }
    }

    void NavigateUp() {
        currentIndex = (currentIndex - 1 + menuButtons.Length) % menuButtons.Length;
        menuButtons[currentIndex].Select();
    }
}
```

### Settings Menu
```csharp
public class SettingsMenu : MonoBehaviour {
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle fullscreenToggle;

    void Start() {
        volumeSlider.value = AudioListener.volume;
        fullscreenToggle.isOn = Screen.fullScreen;

        volumeSlider.onValueChanged.AddListener(SetVolume);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    void SetVolume(float volume) {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume);
    }
}
```

## Build Checklist

- [ ] All scenes added to Build Settings
- [ ] Player Settings configured for target platform
- [ ] Quality Settings optimized
- [ ] Build size under target limit
- [ ] Performance profiled on target device
- [ ] Input bindings saved/loaded correctly
- [ ] Save system tested
- [ ] UI scales properly on different resolutions
