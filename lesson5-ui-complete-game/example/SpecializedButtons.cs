using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Specialized button classes for common game actions
/// Demonstrates inheritance and specific button behaviors
/// </summary>

// Play Button
public class PlayButton : UIButton
{
    [Header("Scene Settings")]
    public string gameSceneName = "GameScene";

    protected override void ExecuteButtonAction()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}

// Pause Button
public class PauseButton : UIButton
{
    protected override void ExecuteButtonAction()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PauseGame();
        }
    }
}

// Quit Button
public class QuitButton : UIButton
{
    protected override void ExecuteButtonAction()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}