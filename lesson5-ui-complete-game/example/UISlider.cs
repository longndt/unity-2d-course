using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// UI Slider component implementation
/// Demonstrates slider configuration and value change handling
/// </summary>
public class UISlider : MonoBehaviour
{
    private Slider slider;

    [Header("Slider Settings")]
    public float minValue = 0f;
    public float maxValue = 100f;
    public float defaultValue = 50f;

    [Header("Optional Components")]
    public TextMeshProUGUI valueText;

    void Start()
    {
        slider = GetComponent<Slider>();
        if (slider == null)
        {
            Debug.LogError("UISlider requires Slider component!");
            enabled = false;
            return;
        }

        // Configure slider
        slider.minValue = minValue;
        slider.maxValue = maxValue;
        slider.value = defaultValue;

        // Add value change listener
        slider.onValueChanged.AddListener(OnSliderValueChanged);

        // Update initial display
        UpdateValueDisplay();
    }

    void OnSliderValueChanged(float value)
    {
        UpdateValueDisplay();
        OnValueChanged(value);
    }

    protected virtual void OnValueChanged(float value)
    {
        // Override in derived classes for specific functionality
        Debug.Log($"Slider {gameObject.name} value changed to: {value}");
    }

    void UpdateValueDisplay()
    {
        if (slider == null) return;

        if (valueText != null)
        {
            valueText.text = slider.value.ToString("F1");
        }
    }

    public void SetValue(float value)
    {
        if (slider == null) return;

        slider.value = Mathf.Clamp(value, minValue, maxValue);
    }

    void OnDestroy()
    {
        if (slider != null)
        {
            slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        }
    }
}