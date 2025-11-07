using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Basic UI Button implementation
/// Demonstrates button setup and event handling
/// </summary>
public class UIButton : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        if (button == null)
        {
            Debug.LogError("UIButton requires Button component!");
            enabled = false;
            return;
        }

        // Add listener programmatically
        button.onClick.AddListener(OnButtonClick);
    }

    protected virtual void OnButtonClick()
    {
        Debug.Log($"Button {gameObject.name} clicked!");
        ExecuteButtonAction();
    }

    protected virtual void ExecuteButtonAction()
    {
        // Override in derived classes for specific functionality
    }

    void OnDestroy()
    {
        // Clean up listener
        if (button != null)
        {
            button.onClick.RemoveListener(OnButtonClick);
        }
    }
}