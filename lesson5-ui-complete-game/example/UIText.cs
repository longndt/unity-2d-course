using UnityEngine;
using TMPro;

/// <summary>
/// Text component handling with TextMeshPro
/// Demonstrates text configuration and dynamic updates
/// </summary>
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
        if (textComponent == null)
        {
            Debug.LogError("UIText requires TextMeshProUGUI component!");
            enabled = false;
            return;
        }
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
        if (textComponent != null)
        {
            textComponent.text = newText;
        }
    }

    public void SetColor(Color newColor)
    {
        if (textComponent != null)
        {
            textComponent.color = newColor;
        }
    }

    public void SetFontSize(float newSize)
    {
        if (textComponent != null)
        {
            textComponent.fontSize = newSize;
        }
    }
}