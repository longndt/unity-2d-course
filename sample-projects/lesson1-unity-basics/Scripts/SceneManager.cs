using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Handles scene management and transitions between different scenes.
/// Demonstrates Unity's scene loading system and scene-specific object management.
/// Note: Named CustomSceneManager to avoid conflict with UnityEngine.SceneManagement.SceneManager
/// </summary>
public class CustomSceneManager : MonoBehaviour
{
    [Header("Scene Management")]
    [Tooltip("Name of the main menu scene")]
    public string mainMenuScene = "MainMenu";

    [Tooltip("Name of the gameplay scene")]
    public string gameplayScene = "Gameplay";

    [Tooltip("Name of the settings scene")]
    public string settingsScene = "Settings";

    [Header("Scene Transition Settings")]
    [Tooltip("Duration of fade transition between scenes")]
    public float transitionDuration = 1f;

    [Tooltip("Enable smooth transitions")]
    public bool smoothTransitions = true;

    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;

    private bool isTransitioning = false;
    private float transitionTimer = 0f;

    void Start()
    {
        if (showDebugInfo)
            Debug.Log("CustomSceneManager: Initialized scene management system");

        // Display current scene information
        DisplaySceneInfo();
    }

    void Update()
    {
        HandleInput();

        if (isTransitioning)
        {
            HandleTransition();
        }
    }

    void HandleInput()
    {
        // Scene navigation with keyboard
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            LoadScene(mainMenuScene);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            LoadScene(gameplayScene);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            LoadScene(settingsScene);
        }

        // Reload current scene
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReloadCurrentScene();
        }
    }

    public void LoadScene(string sceneName)
    {
        if (isTransitioning)
        {
            if (showDebugInfo)
                Debug.LogWarning("CustomSceneManager: Already transitioning, please wait");
            return;
        }

        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("CustomSceneManager: Scene name is null or empty");
            return;
        }

        if (showDebugInfo)
            Debug.Log($"CustomSceneManager: Loading scene: {sceneName}");

        if (smoothTransitions)
        {
            StartCoroutine(LoadSceneWithTransition(sceneName));
        }
        else
        {
            SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    System.Collections.IEnumerator LoadSceneWithTransition(string sceneName)
    {
        isTransitioning = true;
        transitionTimer = 0f;

        // Fade out
        while (transitionTimer < transitionDuration)
        {
            transitionTimer += Time.deltaTime;
            float alpha = transitionTimer / transitionDuration;
            // Here you would typically fade the screen
            yield return null;
        }

        // Load the scene
        SceneManagement.SceneManager.LoadScene(sceneName);

        // Fade in
        transitionTimer = 0f;
        while (transitionTimer < transitionDuration)
        {
            transitionTimer += Time.deltaTime;
            float alpha = 1f - (transitionTimer / transitionDuration);
            // Here you would typically fade the screen back in
            yield return null;
        }

        isTransitioning = false;
    }

    void HandleTransition()
    {
        // Handle transition effects here
        // This is where you would implement screen fade, loading bars, etc.
    }

    public void ReloadCurrentScene()
    {
        if (isTransitioning) return;

        string currentSceneName = SceneManagement.SceneManager.GetActiveScene().name;
        if (showDebugInfo)
            Debug.Log($"CustomSceneManager: Reloading current scene: {currentSceneName}");

        LoadScene(currentSceneName);
    }

    public void LoadNextScene()
    {
        string currentSceneName = SceneManagement.SceneManager.GetActiveScene().name;
        string nextScene = GetNextScene(currentSceneName);

        if (!string.IsNullOrEmpty(nextScene))
        {
            LoadScene(nextScene);
        }
        else
        {
            if (showDebugInfo)
                Debug.Log("CustomSceneManager: No next scene available");
        }
    }

    string GetNextScene(string currentScene)
    {
        switch (currentScene)
        {
            case "MainMenu":
                return gameplayScene;
            case "Gameplay":
                return settingsScene;
            case "Settings":
                return mainMenuScene;
            default:
                return mainMenuScene;
        }
    }

    void DisplaySceneInfo()
    {
        Scene currentScene = SceneManagement.SceneManager.GetActiveScene();
        if (showDebugInfo)
        {
            Debug.Log($"CustomSceneManager: Current scene: {currentScene.name}");
            Debug.Log($"CustomSceneManager: Scene path: {currentScene.path}");
            Debug.Log($"CustomSceneManager: Scene build index: {currentScene.buildIndex}");
        }
    }

    void OnGUI()
    {
        if (!showDebugInfo) return;

        // Display scene management controls
        GUI.Label(new Rect(10, 10, 300, 20), "Scene Management Controls:");
        GUI.Label(new Rect(10, 30, 300, 20), "1 - Main Menu");
        GUI.Label(new Rect(10, 50, 300, 20), "2 - Gameplay");
        GUI.Label(new Rect(10, 70, 300, 20), "3 - Settings");
        GUI.Label(new Rect(10, 90, 300, 20), "R - Reload Current Scene");

        GUI.Label(new Rect(10, 120, 300, 20), $"Current Scene: {SceneManagement.SceneManager.GetActiveScene().name}");
        GUI.Label(new Rect(10, 140, 300, 20), $"Transitioning: {isTransitioning}");
    }
}
