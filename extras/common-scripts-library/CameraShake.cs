using UnityEngine;

/// <summary>
/// Simple Camera Shake with configurable intensity and smooth return
/// </summary>
public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeIntensity = 0.5f;
    public float shakeDuration = 0.3f;
    public float shakeFrequency = 10f;

    [Header("Smooth Settings")]
    public float returnSpeed = 2f;

    private Vector3 originalPosition;
    private float shakeTimer;
    private bool isShaking = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        if (isShaking)
        {
            HandleShake();
        }
        else
        {
            ReturnToOriginalPosition();
        }
    }

    void HandleShake()
    {
        // Update timer
        shakeTimer -= Time.deltaTime;

        if (shakeTimer <= 0)
        {
            isShaking = false;
            return;
        }

        // Calculate shake strength (decreases over time)
        float strength = shakeIntensity * (shakeTimer / shakeDuration);

        // Generate random shake offset
        float x = Random.Range(-1f, 1f) * strength;
        float y = Random.Range(-1f, 1f) * strength;

        // Apply shake
        transform.localPosition = originalPosition + new Vector3(x, y, 0);
    }

    void ReturnToOriginalPosition()
    {
        // Smoothly return to original position
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, returnSpeed * Time.deltaTime);
    }

    public void StartShake()
    {
        StartShake(shakeIntensity, shakeDuration);
    }

    public void StartShake(float intensity, float duration)
    {
        shakeIntensity = intensity;
        shakeDuration = duration;
        shakeTimer = duration;
        isShaking = true;
    }

    public void StopShake()
    {
        isShaking = false;
        shakeTimer = 0;
    }

    void OnDrawGizmosSelected()
    {
        // Draw shake area
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(originalPosition, Vector3.one * shakeIntensity * 2);
    }
}
