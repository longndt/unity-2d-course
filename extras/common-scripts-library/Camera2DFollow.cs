using UnityEngine;

/// <summary>
/// 2D Camera Follow script with smooth following and boundary constraints
/// </summary>
public class Camera2DFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    public Transform target;
    public float followSpeed = 2f;
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Boundary Settings")]
    public bool useBoundaries = false;
    public float leftBoundary = -10f;
    public float rightBoundary = 10f;
    public float topBoundary = 5f;
    public float bottomBoundary = -5f;

    [Header("Smooth Settings")]
    public bool smoothFollow = true;
    public float smoothTime = 0.3f;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogWarning("Camera2DFollow should be attached to a Camera GameObject!");
        }

        // If no target assigned, try to find player
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
            else
            {
                Debug.LogWarning("Camera2DFollow: No target assigned and no Player found!");
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 targetPosition = target.position + offset;

        // Apply boundaries if enabled
        if (useBoundaries)
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, leftBoundary, rightBoundary);
            targetPosition.y = Mathf.Clamp(targetPosition.y, bottomBoundary, topBoundary);
        }

        // Smooth follow or instant follow
        if (smoothFollow)
        {
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (useBoundaries)
        {
            Gizmos.color = Color.yellow;
            Vector3 center = new Vector3(
                (leftBoundary + rightBoundary) / 2,
                (topBoundary + bottomBoundary) / 2,
                transform.position.z
            );
            Vector3 size = new Vector3(
                rightBoundary - leftBoundary,
                topBoundary - bottomBoundary,
                0
            );
            Gizmos.DrawWireCube(center, size);
        }
    }
}
