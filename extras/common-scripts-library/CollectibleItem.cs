using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Collectible Item system with trigger-based collection and score integration
/// </summary>
public class CollectibleItem : MonoBehaviour
{
    [Header("Item Settings")]
    public int scoreValue = 10;
    public string itemName = "Collectible";

    [Header("Visual Effects")]
    public GameObject collectEffect;
    public AudioClip collectSound;

    [Header("Events")]
    public UnityEvent OnItemCollected;

    private bool isCollected = false;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            CollectItem();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isCollected)
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        if (isCollected) return;

        isCollected = true;

        // Add score
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(scoreValue);
        }

        // Play sound
        if (collectSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // Spawn effect
        if (collectEffect != null)
        {
            Instantiate(collectEffect, transform.position, Quaternion.identity);
        }

        // Invoke events
        OnItemCollected?.Invoke();

        // Destroy or disable object
        StartCoroutine(DestroyAfterSound());
    }

    System.Collections.IEnumerator DestroyAfterSound()
    {
        // Wait for sound to finish playing
        if (audioSource != null && collectSound != null)
        {
            yield return new WaitForSeconds(collectSound.length);
        }
        else
        {
            yield return new WaitForSeconds(0.1f);
        }

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}
