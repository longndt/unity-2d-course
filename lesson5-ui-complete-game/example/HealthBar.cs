using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Health bar UI component implementation
/// Demonstrates filled image usage and health visualization
/// </summary>
public class HealthBar : MonoBehaviour
{
    [Header("Health Bar Settings")]
    public Image healthFillImage;
    public Image backgroundImage;
    public TextMeshProUGUI healthText;
    public Gradient healthGradient;

    private float maxHealth;
    private float currentHealth;

    public void Initialize(float maxHealthValue)
    {
        maxHealth = maxHealthValue;
        currentHealth = maxHealthValue;
        UpdateHealthBar();
    }

    public void UpdateHealth(float newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0f, maxHealth);
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (healthFillImage != null)
        {
            // Update fill amount (0-1 range)
            float healthPercentage = currentHealth / maxHealth;
            healthFillImage.fillAmount = healthPercentage;

            // Update color based on health percentage
            if (healthGradient != null)
            {
                healthFillImage.color = healthGradient.Evaluate(healthPercentage);
            }
        }

        // Update health text
        if (healthText != null)
        {
            healthText.text = $"{currentHealth:F0}/{maxHealth:F0}";
        }
    }

    public void TakeDamage(float damage)
    {
        UpdateHealth(currentHealth - damage);
    }

    public void Heal(float healAmount)
    {
        UpdateHealth(currentHealth + healAmount);
    }
}