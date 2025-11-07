using UnityEngine;

/// <summary>
/// Basic input handling using Unity's Legacy Input Manager
/// Demonstrates simple keyboard input for movement and actions
/// Advanced Input System will be covered in Lesson 4
/// </summary>
public class BasicInput : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    
    [Header("Debug")]
    public bool showDebugInfo = true;
    
    private Vector3 currentVelocity;
    
    void Update()
    {
        HandleMovementInput();
        HandleActionInput();
    }
    
    void HandleMovementInput()
    {
        // Read horizontal input (A/D or Left/Right arrows)
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            horizontal = -1f;
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            horizontal = 1f;
        
        // Read vertical input (W/S or Up/Down arrows)
        float vertical = 0f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            vertical = 1f;
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            vertical = -1f;
        
        // Alternative: Use Unity's default input axes
        // float horizontal = Input.GetAxis("Horizontal");
        // float vertical = Input.GetAxis("Vertical");
        
        // Calculate movement direction
        Vector3 movement = new Vector3(horizontal, vertical, 0);
        currentVelocity = movement * moveSpeed;
        
        // Apply movement
        transform.position += currentVelocity * Time.deltaTime;
        
        // Debug info
        if (showDebugInfo && movement != Vector3.zero)
        {
            Debug.Log($"Moving: {movement.normalized} at speed {moveSpeed}");
        }
    }
    
    void HandleActionInput()
    {
        // Jump action (Space)
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        
        // Attack action (Left Mouse Button or X)
        if (Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0))
        {
            Attack();
        }
        
        // Interact action (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
    
    void Jump()
    {
        Debug.Log("Jump!");
        // Jump logic will be implemented in Lesson 3 with physics
    }
    
    void Attack()
    {
        Debug.Log("Attack!");
        // Attack logic will be implemented in later lessons
    }
    
    void Interact()
    {
        Debug.Log("Interact!");
        // Interaction logic will be implemented in later lessons
    }
    
    void OnGUI()
    {
        if (showDebugInfo)
        {
            // Display input info on screen
            GUI.Label(new Rect(10, 10, 300, 20), $"Velocity: {currentVelocity}");
            GUI.Label(new Rect(10, 30, 300, 20), "Controls: WASD/Arrows - Move, Space - Jump, X/Mouse0 - Attack, E - Interact");
        }
    }
}

