using UnityEngine;

/// <summary>
/// Demonstrates prefab instantiation and management.
/// Shows how to create objects dynamically and manage their lifecycle.
/// </summary>
public class PrefabSpawner : MonoBehaviour
{
    [Header("Prefab References")]
    [Tooltip("Player prefab to spawn")]
    public GameObject playerPrefab;

    [Tooltip("Enemy prefab to spawn")]
    public GameObject enemyPrefab;

    [Tooltip("Collectible prefab to spawn")]
    public GameObject collectiblePrefab;

    [Header("Spawn Settings")]
    [Tooltip("Point where objects will be spawned")]
    public Transform spawnPoint;

    [Tooltip("Interval between enemy spawns")]
    public float spawnInterval = 2f;

    [Tooltip("Maximum number of enemies")]
    public int maxEnemies = 5;

    [Tooltip("Random spawn area radius")]
    public float spawnRadius = 3f;

    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;

    [Tooltip("Show spawn area gizmos")]
    public bool showGizmos = true;

    private float lastSpawnTime;
    private int enemyCount;
    private int totalSpawned = 0;

    void Start()
    {
        if (showDebugInfo)
            Debug.Log("PrefabSpawner: Initialized prefab spawning system");

        // Spawn player at start
        SpawnPlayer();

        // Initialize spawn timer
        lastSpawnTime = Time.time;
    }

    void Update()
    {
        HandleInput();
        HandleEnemySpawning();
    }

    void HandleInput()
    {
        // Spawn enemies manually
        if (Input.GetKeyDown(KeyCode.E))
        {
            SpawnEnemy();
        }

        // Spawn collectibles
        if (Input.GetKeyDown(KeyCode.C))
        {
            SpawnCollectible();
        }

        // Clear all spawned objects
        if (Input.GetKeyDown(KeyCode.X))
        {
            ClearAllSpawned();
        }
    }

    void HandleEnemySpawning()
    {
        // Spawn enemies at intervals
        if (enemyPrefab != null && enemyCount < maxEnemies)
        {
            if (Time.time - lastSpawnTime >= spawnInterval)
            {
                SpawnEnemy();
                lastSpawnTime = Time.time;
            }
        }
    }

    void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("PrefabSpawner: Player prefab not assigned!");
            return;
        }

        Vector3 spawnPos = GetSpawnPosition();
        GameObject player = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

        if (showDebugInfo)
            Debug.Log($"PrefabSpawner: Player spawned at {spawnPos}");

        totalSpawned++;
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("PrefabSpawner: Enemy prefab not assigned!");
            return;
        }

        Vector3 spawnPos = GetSpawnPosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

        // Set up enemy tracking
        enemy.name = $"Enemy_{totalSpawned}";

        if (showDebugInfo)
            Debug.Log($"PrefabSpawner: Enemy spawned at {spawnPos}");

        enemyCount++;
        totalSpawned++;
    }

    void SpawnCollectible()
    {
        if (collectiblePrefab == null)
        {
            Debug.LogError("PrefabSpawner: Collectible prefab not assigned!");
            return;
        }

        Vector3 spawnPos = GetSpawnPosition();
        GameObject collectible = Instantiate(collectiblePrefab, spawnPos, Quaternion.identity);

        // Set up collectible tracking
        collectible.name = $"Collectible_{totalSpawned}";

        if (showDebugInfo)
            Debug.Log($"PrefabSpawner: Collectible spawned at {spawnPos}");

        totalSpawned++;
    }

    Vector3 GetSpawnPosition()
    {
        if (spawnPoint != null)
        {
            // Spawn at designated spawn point with random offset
            Vector3 basePos = spawnPoint.position;
            Vector3 randomOffset = Random.insideUnitCircle * spawnRadius;
            return basePos + new Vector3(randomOffset.x, randomOffset.y, 0);
        }
        else
        {
            // Spawn at random position around spawner
            Vector3 randomPos = Random.insideUnitCircle * spawnRadius;
            return transform.position + new Vector3(randomPos.x, randomPos.y, 0);
        }
    }

    void ClearAllSpawned()
    {
        // Note: FindGameObjectsWithTag is expensive - only use when necessary
        // In production, maintain a list of spawned objects instead
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] collectibles = GameObject.FindGameObjectsWithTag("Collectible");

        foreach (GameObject obj in enemies)
        {
            if (obj != null && obj.name.StartsWith("Enemy_"))
            {
                Destroy(obj);
            }
        }

        foreach (GameObject obj in collectibles)
        {
            if (obj != null && obj.name.StartsWith("Collectible_"))
            {
                Destroy(obj);
            }
        }

        enemyCount = 0;

        if (showDebugInfo)
            Debug.Log("PrefabSpawner: All spawned objects cleared");
    }

    void OnGUI()
    {
        if (!showDebugInfo) return;

        // Display spawner information
        GUI.Label(new Rect(10, 10, 300, 20), "Prefab Spawner Controls:");
        GUI.Label(new Rect(10, 30, 300, 20), "E - Spawn Enemy");
        GUI.Label(new Rect(10, 50, 300, 20), "C - Spawn Collectible");
        GUI.Label(new Rect(10, 70, 300, 20), "X - Clear All Spawned");

        GUI.Label(new Rect(10, 100, 300, 20), $"Enemies: {enemyCount}/{maxEnemies}");
        GUI.Label(new Rect(10, 120, 300, 20), $"Total Spawned: {totalSpawned}");
        GUI.Label(new Rect(10, 140, 300, 20), $"Spawn Interval: {spawnInterval}s");
    }

    void OnDrawGizmos()
    {
        if (!showGizmos) return;

        // Draw spawn area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

        // Draw spawn point
        if (spawnPoint != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(spawnPoint.position, 0.5f);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;

        // Draw additional gizmos when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius * 1.1f);
    }
}
