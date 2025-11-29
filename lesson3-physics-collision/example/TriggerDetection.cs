using UnityEngine;

/// <summary>
/// Trigger detection system
/// Demonstrates trigger events and detection zones
/// </summary>
public class TriggerDetection : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Called when object enters trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered area");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        // Called while object stays in trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is in area");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Called when object exits trigger
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left area");
        }
    }
}