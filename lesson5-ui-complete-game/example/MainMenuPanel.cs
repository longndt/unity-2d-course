using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Main Menu Panel implementation
/// Demonstrates a complete main menu system
/// </summary>
public class MainMenuPanel : UIPanel
{
    [Header("Menu Buttons")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Menu Title")]
    public TextMeshProUGUI titleText;
    public Image titleImage;

    protected override void Awake()
    {
        base.Awake();

        // Setup button listeners
        if (playButton != null)
            playButton.onClick.AddListener(OnPlayClicked);
        if (settingsButton != null)
            settingsButton.onClick.AddListener(OnSettingsClicked);
        if (quitButton != null)
            quitButton.onClick.AddListener(OnQuitClicked);
    }

    protected virtual void OnPlayClicked()
    {
        Debug.Log("Play button clicked");
        // Load game scene or show level select
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.LoadGameScene();
        }
    }

    protected virtual void OnSettingsClicked()
    {
        Debug.Log("Settings button clicked");
        // Show settings panel
        if (MenuManager.Instance != null)
        {
            MenuManager.Instance.ShowSettingsPanel();
        }
    }

    protected virtual void OnQuitClicked()
    {
        Debug.Log("Quit button clicked");
        // Quit application
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    protected override void OnPanelShown()
    {
        base.OnPanelShown();
        // Animate title or play main menu music
        if (titleText != null)
        {
            titleText.text = "Unity 2D Game";
        }
    }

    void OnDestroy()
    {
        // Clean up button listeners
        if (playButton != null)
            playButton.onClick.RemoveListener(OnPlayClicked);
        if (settingsButton != null)
            settingsButton.onClick.RemoveListener(OnSettingsClicked);
        if (quitButton != null)
            quitButton.onClick.RemoveListener(OnQuitClicked);
    }
}