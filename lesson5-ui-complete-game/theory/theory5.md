# Theory: UI System & Complete 2D Game

## 🎯 Learning Objectives

After completing this lesson, students will be able to:
- Design and implement complete UI system for 2D games
- Create responsive menus and HUD elements
- Manage multiple scenes and game states
- Integrate audio system with sound effects and music
- Setup build process to publish games
- Apply game design principles for complete game experience

---

## 1. Unity UI System (uGUI) Overview

### 1.1 Canvas System

#### **Canvas Component**:
Canvas is the **root element** for all UI elements in Unity:

**Render Modes:**
- **Screen Space - Overlay**: UI rendered on top, not affected by camera
- **Screen Space - Camera**: UI rendered by specific camera, can be affected by camera effects
- **World Space**: UI elements in 3D world space

**Canvas Scaler**: Handles UI scaling across different screen resolutions

#### **Canvas Setup for 2D Games**:
```csharp
// Recommended settings for 2D games
Render Mode: Screen Space - Overlay
Canvas Scaler:
├── UI Scale Mode: Scale With Screen Size
├── Reference Resolution: 1920x1080 (or 1280x720)
├── Screen Match Mode: Match Width Or Height
└── Match: 0.5 (balanced scaling)
```

### 1.2 UI Element Hierarchy

#### **Typical UI Structure**:
```
Canvas (Screen Space - Overlay)
├── Main Menu Panel
│   ├── Title Image
│   ├── Play Button
│   ├── Settings Button
│   └── Quit Button
├── Game HUD Panel
│   ├── Health Bar
│   ├── Score Text
│   ├── Lives Counter
│   └── Pause Button
├── Pause Menu Panel
│   ├── Resume Button
│   ├── Main Menu Button
│   └── Quit Button
└── Game Over Panel
    ├── Final Score Text
    ├── Restart Button
    └── Main Menu Button
```

### 1.3 Event System

**EventSystem GameObject** handles UI input and interactions:
- **Standalone Input Module**: Keyboard/mouse input
- **Touch Input Module**: Touch/mobile input
- **Graphic Raycaster**: Detects UI element clicks

---

## 2. Core UI Components

### 2.1 Button Component

Buttons are the core of menu interaction.

#### **Button Properties** (concepts):
- **Interactable**: turn a button on/off.  
- **Transition**: visual feedback (Color Tint, Sprite Swap, Animation).  
- **Navigation**: how keyboard/controller moves focus between buttons.  
- **OnClick()**: event fired when the button is pressed.

#### **Simple Button Hook**:

```csharp
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnPlayClicked);
    }

    void OnPlayClicked()
    {
        // Play a click sound here if you have an AudioManager
        SceneManager.LoadScene("GameScene");
    }
}
```

Teaching idea:
- Use 1–2 small scripts like this in theory, then show more reusable versions (e.g. base classes) in `extras/common-scripts-library` or the Lesson 5 sample project.

### 2.2 Text Component (TextMeshPro)

#### **TextMeshPro vs Legacy Text**:
- ✅ **TextMeshPro**: Better rendering, effects, performance
- ❌ **Legacy Text**: Deprecated, limited features

#### **Text Configuration**:
```csharp
using TMPro;

public class UIText : MonoBehaviour
{
    private TextMeshProUGUI textComponent;

    [Header("Text Settings")]
    public string defaultText = "Default Text";
    public Color defaultColor = Color.white;
    public float defaultFontSize = 24f;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        InitializeText();
    }

    void InitializeText()
    {
        textComponent.text = defaultText;
        textComponent.color = defaultColor;
        textComponent.fontSize = defaultFontSize;
    }

    public void SetText(string newText)
    {
        textComponent.text = newText;
    }

    public void SetColor(Color newColor)
    {
        textComponent.color = newColor;
    }

    public void AnimateText(string newText)
    {
        StartCoroutine(TypewriterEffect(newText));
    }

    IEnumerator TypewriterEffect(string fullText)
    {
        textComponent.text = "";

        for (int i = 0; i < fullText.Length; i++)
        {
            textComponent.text += fullText[i];
            yield return new WaitForSeconds(0.05f);
        }
    }
}
```

### 2.3 Image Component

#### **Image Types**:
- **Simple**: Basic sprite display.  
- **Sliced**: 9-slice sprite for scalable UI.  
- **Tiled**: Repeating pattern.  
- **Filled**: Radial/linear fill (health bars, loading bars).

#### **Health Bar (idea, small example)**:

```csharp
public class SimpleHealthBar : MonoBehaviour
{
    public Image healthFillImage;

    public void SetHealth01(float value01)
    {
        healthFillImage.fillAmount = Mathf.Clamp01(value01);
    }
}
```

You can extend this (color gradients, animated changes, text) in the sample project and `extras/common-scripts-library`, rather than packing a full system into the theory chapter.

### 2.4 Slider Component

Sliders are often used for **settings** (volume, brightness, sensitivity).

#### **Basic Volume Slider**:

```csharp
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
    }

    void OnSliderChanged(float value)
    {
        // Map 0–1 slider to your audio system
        AudioListener.volume = value;
    }
}
```

More advanced patterns (base `UISlider` classes, saving/loading settings) fit better in the sample project and `extras` docs.

---

## 3. Menu System Design

### 3.1 Panel Management (high-level)

In a complete game you typically have several panels:
- Main Menu, Settings, Pause, Game Over.

You don’t need a big framework to explain the idea:

- Each panel is a `GameObject` with its own Canvas/Panel.  
- A simple manager shows/hides them.

Minimal example:

```csharp
public class SimpleMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject pausePanel;

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        mainMenuPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
```

More advanced patterns (base `UIPanel` class, fade animations, panel stacks) are implemented in the Lesson 5 sample project and shared UI utilities; the theory chapter only needs to introduce the concept.

### 3.2 Main Menu Implementation

#### **Main Menu Panel**:
```csharp
public class MainMenuPanel : UIPanel
{
    [Header("Menu Buttons")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Menu Title")]
    public TextMeshProUGUI titleText;
    public Image titleImage;

    override void Awake()
    {
        base.Awake();

        // Setup button listeners
        playButton.onClick.AddListener(OnPlayClicked);
        settingsButton.onClick.AddListener(OnSettingsClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    void OnPlayClicked()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        SceneTransitionManager.Instance.LoadScene("GameScene");
    }

    void OnSettingsClicked()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");
        MenuManager.Instance.ShowPanel(MenuManager.Instance.settingsPanel);
    }

    void OnQuitClicked()
    {
        AudioManager.Instance.PlaySFX("ButtonClick");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }

    void OnDestroy()
    {
        // Clean up listeners
        if (playButton != null)
            playButton.onClick.RemoveListener(OnPlayClicked);
        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(OnSettingsClicked);
        if (quitButton != null)
            quitButton.onClick.RemoveListener(OnQuitClicked);
    }
}
```

### 3.3 Pause Menu System

#### **Pause Menu Implementation**:
```csharp
public class PauseMenuPanel : UIPanel
{
    [Header("Pause Buttons")]
    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;

    override void Awake()
    {
        base.Awake();

        resumeButton.onClick.AddListener(OnResumeClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        quitButton.onClick.AddListener(OnQuitClicked);
    }

    public override void Show()
    {
        base.Show();
        Time.timeScale = 0f; // Pause game
        AudioManager.Instance.PauseMusic();
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f; // Resume game
        AudioManager.Instance.ResumeMusic();
    }

    void OnResumeClicked()
    {
        GameManager.Instance.ResumeGame();
    }

    void OnMainMenuClicked()
    {
        Time.timeScale = 1f; // Reset time scale
        SceneTransitionManager.Instance.LoadScene("MainMenuScene");
    }

    void OnQuitClicked()
    {
        Time.timeScale = 1f; // Reset time scale
        Application.Quit();
    }
}
```

---

## 4. HUD and Game UI

### 4.1 Game HUD System

#### **HUD Manager**:
```csharp
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
        // Initialize HUD
        ShowGameHUD();
        UpdatePlayerStats();

        // Setup pause button
        pauseButton.onClick.AddListener(OnPauseClicked);
    }

    public void ShowGameHUD()
    {
        gameHUD.SetActive(true);
    }

    public void HideGameHUD()
    {
        gameHUD.SetActive(false);
    }

    public void UpdateScore(int newScore)
    {
        scoreText.text = $"Score: {newScore:N0}";

        // Animate score change
        StartCoroutine(AnimateScoreText());
    }

    public void UpdateLives(int newLives)
    {
        livesText.text = $"Lives: {newLives}";

        if (newLives <= 0)
        {
            // Game over
            GameManager.Instance.GameOver();
        }
    }

    public void UpdatePlayerHealth(float currentHealth, float maxHealth)
    {
        playerHealthBar.UpdateHealth(currentHealth);
    }

    IEnumerator AnimateScoreText()
    {
        Vector3 originalScale = scoreText.transform.localScale;
        Vector3 targetScale = originalScale * 1.2f;

        // Scale up
        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            scoreText.transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            yield return null;
        }

        // Scale down
        elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            scoreText.transform.localScale = Vector3.Lerp(targetScale, originalScale, progress);
            yield return null;
        }

        scoreText.transform.localScale = originalScale;
    }

    void OnPauseClicked()
    {
        GameManager.Instance.PauseGame();
    }
}
```

### 4.2 Inventory and Item UI

#### **Inventory System**:
```csharp
[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public Sprite itemIcon;
    public int quantity;
    public string description;
}

public class InventoryUI : MonoBehaviour
{
    [Header("Inventory Settings")]
    public Transform inventoryGrid;
    public GameObject inventorySlotPrefab;
    public int maxSlots = 20;

    [Header("Item Details")]
    public Image selectedItemIcon;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemDescription;

    private List<InventorySlot> inventorySlots = new List<InventorySlot>();
    private InventoryItem selectedItem;

    void Start()
    {
        CreateInventorySlots();
    }

    void CreateInventorySlots()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slotObject = Instantiate(inventorySlotPrefab, inventoryGrid);
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            slot.Initialize(i, this);
            inventorySlots.Add(slot);
        }
    }

    public void AddItem(InventoryItem item)
    {
        // Try to stack with existing item
        foreach (var slot in inventorySlots)
        {
            if (slot.HasItem() && slot.GetItem().itemName == item.itemName)
            {
                slot.AddQuantity(item.quantity);
                return;
            }
        }

        // Find empty slot
        foreach (var slot in inventorySlots)
        {
            if (!slot.HasItem())
            {
                slot.SetItem(item);
                return;
            }
        }

        Debug.LogWarning("Inventory is full!");
    }

    public void SelectItem(InventoryItem item)
    {
        selectedItem = item;
        UpdateItemDetails();
    }

    void UpdateItemDetails()
    {
        if (selectedItem != null)
        {
            selectedItemIcon.sprite = selectedItem.itemIcon;
            selectedItemName.text = selectedItem.itemName;
            selectedItemDescription.text = selectedItem.description;
        }
    }
}

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    private InventoryItem currentItem;
    private InventoryUI inventoryUI;
    private int slotIndex;

    [Header("UI References")]
    public Image itemIcon;
    public TextMeshProUGUI quantityText;

    public void Initialize(int index, InventoryUI ui)
    {
        slotIndex = index;
        inventoryUI = ui;
        ClearSlot();
    }

    public void SetItem(InventoryItem item)
    {
        currentItem = item;
        UpdateVisuals();
    }

    public void AddQuantity(int amount)
    {
        if (currentItem != null)
        {
            currentItem.quantity += amount;
            UpdateVisuals();
        }
    }

    public InventoryItem GetItem()
    {
        return currentItem;
    }

    public bool HasItem()
    {
        return currentItem != null;
    }

    public void ClearSlot()
    {
        currentItem = null;
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        quantityText.text = "";
    }

    void UpdateVisuals()
    {
        if (currentItem != null)
        {
            itemIcon.sprite = currentItem.itemIcon;
            itemIcon.color = Color.white;
            quantityText.text = currentItem.quantity > 1 ? currentItem.quantity.ToString() : "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentItem != null)
        {
            inventoryUI.SelectItem(currentItem);
        }
    }
}
```

---

## 5. Scene Management

### 5.1 Scene Transition System

#### **Scene Transition Manager**:
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

        // Fade out
        yield return StartCoroutine(FadeOut());

        // Load new scene
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Fade in
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

### 5.2 Game State Management

#### **Game State System**:
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
    public float gameTimeLimit = 300f; // 5 minutes

    private GameState currentState;
    private int playerScore;
    private int playerLives;
    private float gameTimer;
    private bool isGameActive;

    // Events
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

        // Notify UI
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

        // Save high score
        SaveSystem.Instance.SaveHighScore(playerScore);

        // Show game over screen
        MenuManager.Instance.ShowPanel(MenuManager.Instance.gameOverPanel);
    }

    public void Victory()
    {
        isGameActive = false;
        SetGameState(GameState.Victory);

        // Bonus points for remaining time
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

    // Getters
    public GameState GetGameState() => currentState;
    public int GetScore() => playerScore;
    public int GetLives() => playerLives;
    public float GetTimeRemaining() => gameTimer;
}
```

---

## 6. Audio System Integration

### 6.1 Audio Manager

#### **Comprehensive Audio System**:
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
        // Initialize dictionaries
        musicDatabase = new Dictionary<string, AudioClipData>();
        sfxDatabase = new Dictionary<string, AudioClipData>();
        ambientDatabase = new Dictionary<string, AudioClipData>();

        // Populate dictionaries
        foreach (var clip in musicClips)
            musicDatabase[clip.name] = clip;

        foreach (var clip in sfxClips)
            sfxDatabase[clip.name] = clip;

        foreach (var clip in ambientClips)
            ambientDatabase[clip.name] = clip;
    }

    // Music Methods
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

    // SFX Methods
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

    // Volume Control
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

    // Crossfade between music tracks
    public void CrossfadeToMusic(string newTrackName, float duration = 2f)
    {
        StartCoroutine(CrossfadeRoutine(newTrackName, duration));
    }

    IEnumerator CrossfadeRoutine(string newTrackName, float duration)
    {
        float startVolume = musicSource.volume;

        // Fade out current music
        float elapsed = 0f;
        while (elapsed < duration / 2f)
        {
            elapsed += Time.deltaTime;
            musicSource.volume = Mathf.Lerp(startVolume, 0f, elapsed / (duration / 2f));
            yield return null;
        }

        // Switch to new track
        PlayMusic(newTrackName);
        musicSource.volume = 0f;

        // Fade in new music
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

### 6.2 Audio Integration Examples

#### **Player Audio Integration**:
```csharp
public class PlayerAudioIntegration : MonoBehaviour
{
    private Player2DController playerController;

    void Start()
    {
        playerController = GetComponent<Player2DController>();

        // Subscribe to player events
        playerController.OnJump += PlayJumpSound;
        playerController.OnLand += PlayLandSound;
        playerController.OnTakeDamage += PlayDamageSound;
    }

    void PlayJumpSound()
    {
        AudioManager.Instance.PlaySFX("PlayerJump");
    }

    void PlayLandSound()
    {
        AudioManager.Instance.PlaySFX("PlayerLand");
    }

    void PlayDamageSound()
    {
        AudioManager.Instance.PlaySFX("PlayerDamage");
    }

    void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.OnJump -= PlayJumpSound;
            playerController.OnLand -= PlayLandSound;
            playerController.OnTakeDamage -= PlayDamageSound;
        }
    }
}
```

---

## 7. Save System

### 7.1 Player Data Management

#### **Save System Implementation**:
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

    // Settings
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

    // High Score Methods
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

    // Settings Methods
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

---

## 8. Build and Deployment

### 8.1 Build Settings

#### **Build Configuration**:
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

        // Graphics settings for 2D
        PlayerSettings.colorSpace = ColorSpace.Linear;
        PlayerSettings.gpuSkinning = false;

        // Optimization settings
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

### 8.2 Performance Optimization

#### **Performance Profiler Integration**:
```csharp
public class PerformanceMonitor : MonoBehaviour
{
    [Header("Performance Settings")]
    public bool showFPS = true;
    public bool showMemoryUsage = false;
    public KeyCode toggleKey = KeyCode.F1;

    private float fps;
    private float memoryUsage;
    private GUIStyle style;

    void Start()
    {
        // Initialize GUI style
        style = new GUIStyle();
        style.fontSize = 20;
        style.normal.textColor = Color.white;

        InvokeRepeating(nameof(UpdatePerformanceStats), 0f, 0.5f);
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            showFPS = !showFPS;
        }
    }

    void UpdatePerformanceStats()
    {
        fps = 1f / Time.deltaTime;
        memoryUsage = System.GC.GetTotalMemory(false) / 1024f / 1024f; // MB
    }

    void OnGUI()
    {
        if (showFPS)
        {
            string fpsText = $"FPS: {fps:F1}";
            GUI.Label(new Rect(10, 10, 200, 30), fpsText, style);

            if (showMemoryUsage)
            {
                string memoryText = $"Memory: {memoryUsage:F1} MB";
                GUI.Label(new Rect(10, 40, 200, 30), memoryText, style);
            }
        }
    }

    // Optimization methods
    public void OptimizePerformance()
    {
        // Reduce target frame rate for mobile
        #if UNITY_ANDROID || UNITY_IOS
        Application.targetFrameRate = 60;
        #endif

        // Optimize quality settings
        QualitySettings.vSyncCount = 0;
        QualitySettings.antiAliasing = 0;

        // Disable unnecessary rendering
        if (Camera.main != null)
        {
            Camera.main.allowHDR = false;
            Camera.main.allowMSAA = false;
        }
    }
}
```

---

## Chapter Summary

### Core Knowledge:
1. ✅ **UI System (uGUI)**: Canvas, panels, and component hierarchy
2. ✅ **Menu Design**: Professional menu systems and navigation
3. ✅ **HUD Integration**: Real-time game interface elements
4. ✅ **Scene Management**: Smooth transitions and state handling
5. ✅ **Audio System**: Comprehensive sound and music integration
6. ✅ **Save System**: Player data persistence and settings

### Technical Skills Acquired:
- 🖼️ **Professional UI Design**: Complete menu and HUD systems
- 🎵 **Audio Integration**: Music, SFX, and ambient sound management
- 💾 **Data Persistence**: Save/load systems and player settings
- 🔄 **Scene Transitions**: Smooth loading and state management
- 📦 **Build Pipeline**: Game optimization and deployment preparation

### Game Development Complete:
- 🎮 **Complete 2D Game**: From concept to publishable product
- 🏆 **Game Polish**: Audio feedback, visual effects, and game feel
- ⚡ **Performance Optimization**: Efficient code and resource management
- 🚀 **Deployment Ready**: Build settings and distribution preparation

### Practice:
Complete **Lab 05** to create a fully functional 2D game with complete UI system, audio integration, save/load functionality, and build-ready optimization. This represents the culmination of the entire Unity 2D course!

---

## ✅ What's Next

You've completed the core Unity 2D course! Continue with advanced topics in `extras/` to further enhance your game development skills.