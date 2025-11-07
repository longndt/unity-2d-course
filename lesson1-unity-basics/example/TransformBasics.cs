using UnityEngine;

/// <summary>
/// Basic Transform operations in Unity 2D
/// Demonstrates how to access and modify GameObject transforms
/// </summary>
public class TransformBasics : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 90f;
    public float scaleSpeed = 1f;

    [Header("Input Keys")]
    public KeyCode moveKey = KeyCode.W;
    public KeyCode rotateKey = KeyCode.E;
    public KeyCode scaleKey = KeyCode.R;

    void Start()
    {
        // Access Transform in code
        Transform myTransform = transform;
        
        // Set initial position
        myTransform.position = new Vector3(0f, 0f, 0f);
        myTransform.localScale = new Vector3(1f, 1f, 1f);
        
        Debug.Log("TransformBasics initialized at position: " + myTransform.position);
    }

    void Update()
    {
        HandleMovement();
        HandleRotation();
        HandleScaling();
    }

    void HandleMovement()
    {
        if (Input.GetKey(moveKey))
        {
            // Move forward in local space
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
    }

    void HandleRotation()
    {
        if (Input.GetKey(rotateKey))
        {
            // Rotate around Z-axis (2D rotation)
            transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleScaling()
    {
        if (Input.GetKey(scaleKey))
        {
            // Scale up uniformly
            Vector3 currentScale = transform.localScale;
            currentScale += Vector3.one * scaleSpeed * Time.deltaTime;
            transform.localScale = currentScale;
        }
    }

    void OnGUI()
    {
        // Display instructions
        GUI.Label(new Rect(10, 10, 300, 20), "Press W to move, E to rotate, R to scale");
        GUI.Label(new Rect(10, 30, 300, 20), "Position: " + transform.position);
        GUI.Label(new Rect(10, 50, 300, 20), "Rotation: " + transform.eulerAngles);
        GUI.Label(new Rect(10, 70, 300, 20), "Scale: " + transform.localScale);
    }
}