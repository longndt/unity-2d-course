using UnityEngine;

/// <summary>
/// SpriteRenderer manipulation examples
/// Demonstrates various sprite rendering techniques
/// </summary>
public class SpriteControl : MonoBehaviour
{
    public Sprite newSprite;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("SpriteControl requires SpriteRenderer component!");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        if (sr == null) return;

        // Change sprite
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (newSprite != null)
            {
                sr.sprite = newSprite;
            }
        }

        // Flip character
        if (Input.GetKeyDown(KeyCode.F))
        {
            sr.flipX = !sr.flipX;
        }

        // Change sorting
        if (Input.GetKeyDown(KeyCode.L))
        {
            sr.sortingLayerName = "Characters";
            sr.sortingOrder = 5;
        }

        // Fade effect
        if (Input.GetKey(KeyCode.Alpha1))
        {
            sr.color = new Color(1f, 1f, 1f, 0.5f); // 50% alpha
        }
        else
        {
            sr.color = new Color(1f, 1f, 1f, 1f); // 100% alpha
        }
    }
}