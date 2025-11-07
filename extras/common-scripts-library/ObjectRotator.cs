using UnityEngine;

/// <summary>
/// Object Rotator with continuous rotation and configurable axes
/// </summary>
public class ObjectRotator : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 rotationSpeed = new Vector3(0, 90, 0);
    public Space rotationSpace = Space.Self;
    public bool useUnscaledTime = false;

    [Header("Axis Control")]
    public bool rotateX = true;
    public bool rotateY = true;
    public bool rotateZ = true;

    [Header("Randomization")]
    public bool randomizeSpeed = false;
    public Vector3 minSpeed = new Vector3(0, 0, 0);
    public Vector3 maxSpeed = new Vector3(360, 360, 360);

    private Vector3 currentRotationSpeed;

    void Start()
    {
        if (randomizeSpeed)
        {
            RandomizeRotationSpeed();
        }
        else
        {
            currentRotationSpeed = rotationSpeed;
        }
    }

    void Update()
    {
        RotateObject();
    }

    void RotateObject()
    {
        float deltaTime = useUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;

        Vector3 rotation = currentRotationSpeed * deltaTime;

        // Apply axis control
        if (!rotateX) rotation.x = 0;
        if (!rotateY) rotation.y = 0;
        if (!rotateZ) rotation.z = 0;

        // Apply rotation
        transform.Rotate(rotation, rotationSpace);
    }

    public void RandomizeRotationSpeed()
    {
        currentRotationSpeed = new Vector3(
            Random.Range(minSpeed.x, maxSpeed.x),
            Random.Range(minSpeed.y, maxSpeed.y),
            Random.Range(minSpeed.z, maxSpeed.z)
        );
    }

    public void SetRotationSpeed(Vector3 newSpeed)
    {
        currentRotationSpeed = newSpeed;
    }

    public void SetRotationSpeed(float x, float y, float z)
    {
        currentRotationSpeed = new Vector3(x, y, z);
    }

    public void PauseRotation()
    {
        currentRotationSpeed = Vector3.zero;
    }

    public void ResumeRotation()
    {
        if (randomizeSpeed)
        {
            RandomizeRotationSpeed();
        }
        else
        {
            currentRotationSpeed = rotationSpeed;
        }
    }

    public void ReverseRotation()
    {
        currentRotationSpeed = -currentRotationSpeed;
    }
}
