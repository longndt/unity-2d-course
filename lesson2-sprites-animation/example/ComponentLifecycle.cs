using UnityEngine;

/// <summary>
/// Demonstrates Unity component lifecycle methods
/// Shows the order and usage of common MonoBehaviour methods
/// </summary>
public class ComponentLifecycle : MonoBehaviour
{
    void Awake()    // Called when component is created
    {
        Debug.Log("Awake: Component created");
    }

    void Start()    // Called before first frame update
    {
        Debug.Log("Start: Before first frame");
    }

    void Update()   // Called once per frame
    {
        // Frame-by-frame logic
    }

    void OnEnable() // Called when object becomes active
    {
        Debug.Log("OnEnable: Object became active");
    }

    void OnDisable() // Called when object becomes inactive
    {
        Debug.Log("OnDisable: Object became inactive");
    }

    void OnDestroy() // Called when object is destroyed
    {
        Debug.Log("OnDestroy: Object destroyed");
    }
}