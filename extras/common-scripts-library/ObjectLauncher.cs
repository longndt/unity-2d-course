using UnityEngine;

/// <summary>
/// Object Launcher with projectile physics and trajectory calculation
/// </summary>
public class ObjectLauncher : MonoBehaviour
{
    [Header("Launch Settings")]
    public GameObject projectilePrefab;
    public float launchForce = 10f;
    public float launchAngle = 45f;
    public float launchHeight = 1f;

    [Header("Timing")]
    public float launchInterval = 2f;
    public bool autoLaunch = false;

    [Header("Object Pooling")]
    public int poolSize = 10;
    public bool useObjectPool = true;

    private float lastLaunchTime;
    private GameObject[] projectilePool;
    private int currentPoolIndex = 0;

    void Start()
    {
        if (useObjectPool && projectilePrefab != null)
        {
            CreateObjectPool();
        }
    }

    void Update()
    {
        if (autoLaunch && Time.time - lastLaunchTime >= launchInterval)
        {
            LaunchProjectile();
        }

        // Manual launch with space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }
    }

    void CreateObjectPool()
    {
        projectilePool = new GameObject[poolSize];

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false);
            projectilePool[i] = obj;
        }
    }

    public void LaunchProjectile()
    {
        if (projectilePrefab == null) return;

        GameObject projectile;

        if (useObjectPool && projectilePool != null)
        {
            projectile = GetPooledObject();
        }
        else
        {
            projectile = Instantiate(projectilePrefab);
        }

        if (projectile != null)
        {
            // Set position
            Vector3 launchPosition = transform.position + Vector3.up * launchHeight;
            projectile.transform.position = launchPosition;
            projectile.SetActive(true);

            // Calculate launch direction
            Vector3 launchDirection = CalculateLaunchDirection();

            // Apply force
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero; // Reset velocity
                rb.AddForce(launchDirection * launchForce, ForceMode2D.Impulse);
            }

            lastLaunchTime = Time.time;
        }
    }

    Vector3 CalculateLaunchDirection()
    {
        // Convert angle to radians
        float angleInRadians = launchAngle * Mathf.Deg2Rad;

        // Calculate direction
        Vector3 direction = new Vector3(
            Mathf.Cos(angleInRadians),
            Mathf.Sin(angleInRadians),
            0
        );

        return direction;
    }

    GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            int index = (currentPoolIndex + i) % poolSize;
            if (!projectilePool[index].activeInHierarchy)
            {
                currentPoolIndex = index;
                return projectilePool[index];
            }
        }

        // If no inactive object found, use the next one in sequence
        GameObject obj = projectilePool[currentPoolIndex];
        currentPoolIndex = (currentPoolIndex + 1) % poolSize;
        return obj;
    }

    public void SetLaunchAngle(float angle)
    {
        launchAngle = angle;
    }

    public void SetLaunchForce(float force)
    {
        launchForce = force;
    }

    void OnDrawGizmosSelected()
    {
        // Draw launch direction
        Vector3 launchDirection = CalculateLaunchDirection();
        Vector3 startPos = transform.position + Vector3.up * launchHeight;
        Vector3 endPos = startPos + launchDirection * 3f;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(startPos, endPos);

        // Draw launch arc
        Gizmos.color = Color.yellow;
        for (int i = 0; i < 20; i++)
        {
            float t = (float)i / 19f;
            Vector3 pos = CalculateTrajectoryPoint(startPos, launchDirection * launchForce, t);
            if (i > 0)
            {
                Vector3 prevPos = CalculateTrajectoryPoint(startPos, launchDirection * launchForce, (float)(i - 1) / 19f);
                Gizmos.DrawLine(prevPos, pos);
            }
        }
    }

    Vector3 CalculateTrajectoryPoint(Vector3 start, Vector3 velocity, float time)
    {
        return start + velocity * time + 0.5f * Physics2D.gravity * time * time;
    }
}
