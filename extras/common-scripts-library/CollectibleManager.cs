using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Manages collectible items in 2D games.
/// Handles collection, spawning, and effects.
/// </summary>
public class CollectibleManager : MonoBehaviour
{
    [Header("Collectible Settings")]
    [Tooltip("Prefab for collectible items")]
    public GameObject collectiblePrefab;
    
    [Tooltip("Spawn area bounds")]
    public Bounds spawnArea;
    
    [Tooltip("Maximum number of collectibles")]
    public int maxCollectibles = 10;
    
    [Tooltip("Spawn interval in seconds")]
    public float spawnInterval = 2f;
    
    [Header("Collection Settings")]
    [Tooltip("Score value per collectible")]
    public int scoreValue = 10;
    
    [Tooltip("Health value per collectible")]
    public int healthValue = 5;
    
    [Tooltip("Collection sound effect")]
    public AudioClip collectSound;
    
    [Header("Visual Effects")]
    [Tooltip("Collection particle effect")]
    public GameObject collectEffect;
    
    [Tooltip("Collection flash duration")]
    public float flashDuration = 0.2f;
    
    [Header("Events")]
    [Tooltip("Called when collectible is collected")]
    public UnityEvent<GameObject> OnCollectibleCollected;
    
    [Tooltip("Called when all collectibles are collected")]
    public UnityEvent OnAllCollectiblesCollected;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    private int totalCollectibles = 0;
    private int collectedCount = 0;
    private float lastSpawnTime = 0f;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
        
        // Spawn initial collectibles
        SpawnInitialCollectibles();
    }
    
    void Update()
    {
        HandleSpawning();
    }
    
    void SpawnInitialCollectibles()
    {
        for (int i = 0; i < maxCollectibles; i++)
        {
            SpawnCollectible();
        }
    }
    
    void HandleSpawning()
    {
        if (Time.time - lastSpawnTime >= spawnInterval)
        {
            if (totalCollectibles < maxCollectibles)
            {
                SpawnCollectible();
            }
            lastSpawnTime = Time.time;
        }
    }
    
    public void SpawnCollectible()
    {
        if (collectiblePrefab == null) return;
        
        Vector3 spawnPosition = GetRandomSpawnPosition();
        GameObject collectible = Instantiate(collectiblePrefab, spawnPosition, Quaternion.identity);
        
        // Set up collectible
        CollectibleItem collectibleItem = collectible.GetComponent<CollectibleItem>();
        if (collectibleItem == null)
        {
            collectibleItem = collectible.AddComponent<CollectibleItem>();
        }
        
        collectibleItem.OnCollected.AddListener(OnCollectibleCollectedHandler);
        
        totalCollectibles++;
        
        if (showDebugInfo)
            Debug.Log($"Collectible spawned at {spawnPosition}");
    }
    
    Vector3 GetRandomSpawnPosition()
    {
        float x = Random.Range(spawnArea.min.x, spawnArea.max.x);
        float y = Random.Range(spawnArea.min.y, spawnArea.max.y);
        return new Vector3(x, y, 0);
    }
    
    void OnCollectibleCollectedHandler(GameObject collectible)
    {
        collectedCount++;
        
        // Play collection sound
        if (audioSource != null && collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }
        
        // Play collection effect
        if (collectEffect != null)
        {
            Instantiate(collectEffect, collectible.transform.position, Quaternion.identity);
        }
        
        // Add score
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddScore(scoreValue);
        }
        
        // Add health
        HealthSystem healthSystem = FindObjectOfType<HealthSystem>();
        if (healthSystem != null)
        {
            healthSystem.Heal(healthValue);
        }
        
        OnCollectibleCollected?.Invoke(collectible);
        
        // Check if all collected
        if (collectedCount >= totalCollectibles)
        {
            OnAllCollectiblesCollected?.Invoke();
        }
        
        if (showDebugInfo)
            Debug.Log($"Collectible collected! Total: {collectedCount}/{totalCollectibles}");
    }
    
    public void ResetCollectibles()
    {
        // Destroy all existing collectibles
        CollectibleItem[] collectibles = FindObjectsOfType<CollectibleItem>();
        foreach (CollectibleItem collectible in collectibles)
        {
            if (collectible != null)
                Destroy(collectible.gameObject);
        }
        
        totalCollectibles = 0;
        collectedCount = 0;
        
        // Spawn new collectibles
        SpawnInitialCollectibles();
        
        if (showDebugInfo)
            Debug.Log("Collectibles reset");
    }
    
    public int GetTotalCollectibles()
    {
        return totalCollectibles;
    }
    
    public int GetCollectedCount()
    {
        return collectedCount;
    }
    
    public int GetRemainingCount()
    {
        return totalCollectibles - collectedCount;
    }
    
    void OnDrawGizmos()
    {
        if (!showDebugInfo) return;
        
        // Draw spawn area
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(spawnArea.center, spawnArea.size);
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        GUI.Label(new Rect(10, 10, 200, 20), $"Collectibles: {collectedCount}/{totalCollectibles}");
        GUI.Label(new Rect(10, 30, 200, 20), $"Remaining: {GetRemainingCount()}");
    }
}
