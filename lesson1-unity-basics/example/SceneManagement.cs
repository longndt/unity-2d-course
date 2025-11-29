using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Scene management basics
/// Demonstrates how to load scenes programmatically
/// </summary>
public class SceneManagement : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // Validate scene name
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError("SceneManagement: Scene name cannot be null or empty!");
            return;
        }

        // Load scene in code
        try
        {
            SceneManager.LoadScene(sceneName);
            Debug.Log($"Loading scene: {sceneName}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load scene '{sceneName}': {e.Message}");
        }
    }
}