using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

/// <summary>
/// Menu Manager for handling UI panel transitions
/// Demonstrates panel stack management and navigation
/// </summary>
public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [Header("Menu Panels")]
    public UIPanel mainMenuPanel;
    public UIPanel settingsPanel;
    public UIPanel pausePanel;
    public UIPanel gameOverPanel;

    private Stack<UIPanel> panelStack = new Stack<UIPanel>();
    private UIPanel currentPanel;

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
        // Show main menu by default
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        ShowPanel(mainMenuPanel);
    }

    public void ShowSettingsPanel()
    {
        ShowPanel(settingsPanel);
    }

    public void ShowPausePanel()
    {
        ShowPanel(pausePanel);
    }

    public void ShowGameOverPanel()
    {
        ShowPanel(gameOverPanel);
    }

    public void ShowPanel(UIPanel panel)
    {
        if (panel == null) return;

        // Hide current panel
        if (currentPanel != null)
        {
            currentPanel.Hide();
            panelStack.Push(currentPanel);
        }

        // Show new panel
        currentPanel = panel;
        panel.Show();
    }

    public void GoBack()
    {
        if (panelStack.Count > 0)
        {
            // Hide current panel
            if (currentPanel != null)
            {
                currentPanel.Hide();
            }

            // Show previous panel
            currentPanel = panelStack.Pop();
            currentPanel.Show();
        }
    }

    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
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