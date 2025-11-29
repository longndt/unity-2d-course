# Advanced Physics Mechanics

This guide covers advanced physics mechanics for 2D platformer games.

## Variable Jump Height

Variable jump height allows players to control jump height by holding or releasing the jump button.

```csharp
public class VariableJump : MonoBehaviour
{
    public float jumpForce = 400f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    private Rigidbody2D rb;

    void Update()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
```

## Coyote Time Implementation

Coyote time allows players to jump shortly after leaving a platform, improving game feel.

```csharp
public class CoyoteTime : MonoBehaviour
{
    public float coyoteTimeThreshold = 0.1f;

    private bool isGrounded;
    private float lastGroundedTime;

    void Update()
    {
        bool wasGrounded = isGrounded;
        CheckGrounded();

        if (wasGrounded && !isGrounded)
        {
            lastGroundedTime = Time.time;
        }

        bool canJump = isGrounded || (Time.time - lastGroundedTime < coyoteTimeThreshold);

        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            Jump();
        }
    }
}
```

## Jump Buffering

Jump buffering stores jump input when pressed early, executing it when the player lands.

```csharp
public class JumpBuffer : MonoBehaviour
{
    public float jumpBufferTime = 0.1f;

    private float jumpBufferCounter;
    private bool isGrounded;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && isGrounded)
        {
            Jump();
            jumpBufferCounter = 0f;
        }
    }
}
```

## Moving Platforms

Moving platforms that carry the player.

```csharp
public class MovingPlatform : MonoBehaviour
{
    public Transform[] waypoints;
    public float moveSpeed = 2f;

    private int currentWaypoint = 0;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true;
    }

    void FixedUpdate()
    {
        Vector2 targetPosition = waypoints[currentWaypoint].position;
        Vector2 newPosition = Vector2.MoveTowards(rb.position, targetPosition, moveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        if (Vector2.Distance(rb.position, targetPosition) < 0.1f)
        {
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
```

## One-Way Platforms

Platforms that can be jumped through from below.

```csharp
public class OneWayPlatform : MonoBehaviour
{
    private PlatformEffector2D platformEffector;

    void Start()
    {
        platformEffector = GetComponent<PlatformEffector2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            platformEffector.rotationalOffset = 180f;
        }
        else
        {
            platformEffector.rotationalOffset = 0f;
        }
    }
}
```

---

For more physics examples, see `extras/common-scripts-library/`.

