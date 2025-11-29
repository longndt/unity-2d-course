# Advanced UI Systems

This guide covers advanced UI system implementations for complete 2D games.

## Menu System with Base Panel Class

### Base UIPanel Class

```csharp
public abstract class UIPanel : MonoBehaviour
{
    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
    }
}
```

### Main Menu Panel

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
        if (playButton != null)
            playButton.onClick.RemoveListener(OnPlayClicked);
        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(OnSettingsClicked);
        if (quitButton != null)
            quitButton.onClick.RemoveListener(OnQuitClicked);
    }
}
```

### Pause Menu Panel

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
        Time.timeScale = 0f;
        AudioManager.Instance.PauseMusic();
    }

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1f;
        AudioManager.Instance.ResumeMusic();
    }

    void OnResumeClicked()
    {
        GameManager.Instance.ResumeGame();
    }

    void OnMainMenuClicked()
    {
        Time.timeScale = 1f;
        SceneTransitionManager.Instance.LoadScene("MainMenuScene");
    }

    void OnQuitClicked()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
```

## HUD Manager with Animations

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
        ShowGameHUD();
        UpdatePlayerStats();
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
        StartCoroutine(AnimateScoreText());
    }

    public void UpdateLives(int newLives)
    {
        livesText.text = $"Lives: {newLives}";

        if (newLives <= 0)
        {
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

        float duration = 0.1f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float progress = elapsed / duration;
            scoreText.transform.localScale = Vector3.Lerp(originalScale, targetScale, progress);
            yield return null;
        }

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

## Inventory System

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
        foreach (var slot in inventorySlots)
        {
            if (slot.HasItem() && slot.GetItem().itemName == item.itemName)
            {
                slot.AddQuantity(item.quantity);
                return;
            }
        }

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

For more UI examples, see `extras/common-scripts-library/`.

