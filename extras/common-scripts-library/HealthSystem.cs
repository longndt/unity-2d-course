using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Health system for 2D games with events and damage types.
/// Supports health bars, damage feedback, and death handling.
/// </summary>
public class HealthSystem : MonoBehaviour
{
    [Header("Health Settings")]
    [Tooltip("Maximum health value")]
    public int maxHealth = 100;
    
    [Tooltip("Current health value")]
    public int currentHealth;
    
    [Tooltip("Can this object be damaged")]
    public bool canTakeDamage = true;
    
    [Tooltip("Can this object heal")]
    public bool canHeal = true;
    
    [Header("Damage Settings")]
    [Tooltip("Invulnerability duration after taking damage")]
    public float invulnerabilityDuration = 1f;
    
    [Tooltip("Damage flash duration")]
    public float flashDuration = 0.1f;
    
    [Header("Visual Feedback")]
    [Tooltip("Sprite renderer for damage flash")]
    public SpriteRenderer spriteRenderer;
    
    [Tooltip("Damage flash color")]
    public Color damageFlashColor = Color.red;
    
    [Tooltip("Heal flash color")]
    public Color healFlashColor = Color.green;
    
    [Header("Events")]
    [Tooltip("Called when health changes")]
    public UnityEvent<int, int> OnHealthChanged;
    
    [Tooltip("Called when taking damage")]
    public UnityEvent<int> OnDamageTaken;
    
    [Tooltip("Called when healing")]
    public UnityEvent<int> OnHealed;
    
    [Tooltip("Called when health reaches zero")]
    public UnityEvent OnDeath;
    
    [Tooltip("Called when health is full")]
    public UnityEvent OnFullHealth;
    
    private Color originalColor;
    private bool isInvulnerable = false;
    private float invulnerabilityTimer = 0f;
    private float flashTimer = 0f;
    private bool isFlashing = false;
    
    void Start()
    {
        currentHealth = maxHealth;
        
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (spriteRenderer != null)
            originalColor = spriteRenderer.color;
        
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    void Update()
    {
        HandleInvulnerability();
        HandleFlashEffect();
    }
    
    void HandleInvulnerability()
    {
        if (isInvulnerable)
        {
            invulnerabilityTimer -= Time.deltaTime;
            if (invulnerabilityTimer <= 0f)
            {
                isInvulnerable = false;
            }
        }
    }
    
    void HandleFlashEffect()
    {
        if (isFlashing)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0f)
            {
                isFlashing = false;
                if (spriteRenderer != null)
                    spriteRenderer.color = originalColor;
            }
        }
    }
    
    public void TakeDamage(int damage)
    {
        if (!canTakeDamage || isInvulnerable || currentHealth <= 0)
            return;
        
        currentHealth = Mathf.Max(0, currentHealth - damage);
        
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnDamageTaken?.Invoke(damage);
        
        // Start invulnerability
        isInvulnerable = true;
        invulnerabilityTimer = invulnerabilityDuration;
        
        // Flash effect
        StartFlashEffect(damageFlashColor);
        
        // Check for death
        if (currentHealth <= 0)
        {
            OnDeath?.Invoke();
        }
    }
    
    public void Heal(int healAmount)
    {
        if (!canHeal || currentHealth >= maxHealth)
            return;
        
        int oldHealth = currentHealth;
        currentHealth = Mathf.Min(maxHealth, currentHealth + healAmount);
        int actualHeal = currentHealth - oldHealth;
        
        if (actualHeal > 0)
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            OnHealed?.Invoke(actualHeal);
            
            // Flash effect
            StartFlashEffect(healFlashColor);
            
            // Check for full health
            if (currentHealth >= maxHealth)
            {
                OnFullHealth?.Invoke();
            }
        }
    }
    
    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    public void SetHealth(int newHealth)
    {
        currentHealth = Mathf.Clamp(newHealth, 0, maxHealth);
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }
    
    public void FullHeal()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnFullHealth?.Invoke();
    }
    
    public void Kill()
    {
        currentHealth = 0;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
        OnDeath?.Invoke();
    }
    
    void StartFlashEffect(Color flashColor)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = flashColor;
            isFlashing = true;
            flashTimer = flashDuration;
        }
    }
    
    public bool IsAlive()
    {
        return currentHealth > 0;
    }
    
    public bool IsFullHealth()
    {
        return currentHealth >= maxHealth;
    }
    
    public bool IsInvulnerable()
    {
        return isInvulnerable;
    }
    
    public float GetHealthPercentage()
    {
        return (float)currentHealth / maxHealth;
    }
    
    void OnGUI()
    {
        if (Application.isEditor)
        {
            GUI.Label(new Rect(10, 10, 200, 20), $"Health: {currentHealth}/{maxHealth}");
            GUI.Label(new Rect(10, 30, 200, 20), $"Invulnerable: {isInvulnerable}");
        }
    }
}
