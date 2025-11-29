using UnityEngine;

/// <summary>
/// Collision and trigger detection examples
/// Demonstrates 2D collision handling
/// </summary>
public class CollisionHandler : MonoBehaviour
{
    private bool isGrounded = false;

    // Collision example
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            Debug.Log("Landed on ground");
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            Debug.Log("Left ground");
        }
    }

    // Trigger example
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Coin")
        {
            Debug.Log("Collected coin!");
            Destroy(other.gameObject);
        }
    }
}