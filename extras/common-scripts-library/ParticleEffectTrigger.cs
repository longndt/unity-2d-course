using UnityEngine;

/// <summary>
/// Particle Effect Trigger with effect spawning and timing control
/// </summary>
public class ParticleEffectTrigger : MonoBehaviour
{
    [Header("Effect Settings")]
    public ParticleSystem particleEffect;
    public bool playOnStart = false;
    public bool playOnTrigger = true;
    public bool playOnCollision = false;

    [Header("Timing Control")]
    public float effectDuration = 2f;
    public bool autoDestroy = true;
    public float destroyDelay = 0.5f;

    [Header("Trigger Settings")]
    public string triggerTag = "Player";
    public LayerMask triggerLayers = -1;

    private ParticleSystem currentEffect;

    void Start()
    {
        if (playOnStart && particleEffect != null)
        {
            PlayEffect();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playOnTrigger && ShouldTrigger(other))
        {
            PlayEffect();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (playOnCollision && ShouldTrigger(collision.collider))
        {
            PlayEffect();
        }
    }

    bool ShouldTrigger(Collider2D other)
    {
        // Check tag
        if (!string.IsNullOrEmpty(triggerTag) && !other.CompareTag(triggerTag))
        {
            return false;
        }

        // Check layer
        if (triggerLayers != -1 && (triggerLayers.value & (1 << other.gameObject.layer)) == 0)
        {
            return false;
        }

        return true;
    }

    public void PlayEffect()
    {
        if (particleEffect == null) return;

        // Create effect instance
        currentEffect = Instantiate(particleEffect, transform.position, transform.rotation);

        // Play effect
        currentEffect.Play();

        // Handle cleanup
        if (autoDestroy)
        {
            Destroy(currentEffect.gameObject, effectDuration + destroyDelay);
        }
    }

    public void StopEffect()
    {
        if (currentEffect != null)
        {
            currentEffect.Stop();
        }
    }

    public void SetEffect(ParticleSystem newEffect)
    {
        particleEffect = newEffect;
    }

    public void SetTriggerTag(string newTag)
    {
        triggerTag = newTag;
    }
}
