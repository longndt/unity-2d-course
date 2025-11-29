using UnityEngine;

/// <summary>
/// Simple camera shake effect demonstrating visual feedback
/// Shows coroutines and visual effects
/// </summary>
public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] private float shakeIntensity = 0.5f;
    [SerializeField] private float shakeDuration = 0.3f;

    private Vector3 originalPosition;
    private bool isShaking = false;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    public void StartShake()
    {
        StartShake(shakeIntensity, shakeDuration);
    }

    public void StartShake(float intensity, float duration)
    {
        if (isShaking) return; // Don't shake if already shaking

        StartCoroutine(ShakeCoroutine(intensity, duration));
    }

    System.Collections.IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // Random offset for shake
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null; // Wait for next frame
        }

        // Return to original position
        transform.localPosition = originalPosition;
        isShaking = false;
    }

    // Call this from other scripts:
    // CameraShake shake = Camera.main.GetComponent<CameraShake>();
    // shake.StartShake();
}
