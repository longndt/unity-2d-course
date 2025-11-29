# Design Patterns in Unity 2D Development

Essential software design patterns specifically tailored for Unity 2D game development, with practical implementations and real-world examples.

---

## 🎯 **Core Design Patterns for Unity 2D**

### **1. Singleton Pattern**

The Singleton pattern ensures a class has only one instance and provides global access to it. Perfect for managers and systems.

#### **Generic Singleton Base Class**

```csharp
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    private static readonly object _lock = new object();
    private static bool _applicationIsQuitting = false;

    public static T Instance
    {
        get
        {
            if (_applicationIsQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed on application quit. Won't create again.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<T>();

                    if (FindObjectsOfType<T>().Length > 1)
                    {
                        Debug.LogError($"[Singleton] Something went really wrong - there should never be more than 1 singleton! Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = $"{typeof(T).Name} (Singleton)";

                        DontDestroyOnLoad(singleton);
                    }
                }

                return _instance;
            }
        }
    }

    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
        {
            Debug.LogWarning($"[Singleton] Destroying duplicate instance of {typeof(T).Name}");
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (_instance == this)
        {
            _instance = null;
        }
    }

    private void OnApplicationQuit()
    {
        _applicationIsQuitting = true;
    }
}
```

#### **Game Manager Implementation**

```csharp
public class GameManager : Singleton<GameManager>
{
    [Header("Game State")]
    public GameState currentState = GameState.MainMenu;

    [Header("Player Data")]
    public int score = 0;
    public int lives = 3;
    public float gameTime = 0f;

    [Header("Game Events")]
    public UnityEvent<GameState> OnGameStateChanged;
    public UnityEvent<int> OnScoreChanged;
    public UnityEvent<int> OnLivesChanged;

    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver,
        Victory
    }

    protected override void Awake()
    {
        base.Awake();

        if (OnGameStateChanged == null)
            OnGameStateChanged = new UnityEvent<GameState>();
        if (OnScoreChanged == null)
            OnScoreChanged = new UnityEvent<int>();
        if (OnLivesChanged == null)
            OnLivesChanged = new UnityEvent<int>();
    }

    void Update()
    {
        if (currentState == GameState.Playing)
        {
            gameTime += Time.deltaTime;
        }
    }

    public void ChangeState(GameState newState)
    {
        if (currentState != newState)
        {
            GameState previousState = currentState;
            currentState = newState;

            OnStateChanged(previousState, newState);
            OnGameStateChanged.Invoke(newState);
        }
    }

    private void OnStateChanged(GameState from, GameState to)
    {
        Debug.Log($"Game state changed: {from} → {to}");

        switch (to)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                SaveHighScore();
                break;
        }
    }

    public void AddScore(int points)
    {
        score += points;
        OnScoreChanged.Invoke(score);
    }

    public void LoseLife()
    {
        lives--;
        OnLivesChanged.Invoke(lives);

        if (lives <= 0)
        {
            ChangeState(GameState.GameOver);
        }
    }

    private void SaveHighScore()
    {
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }
}
```

---

### **2. Observer Pattern**

The Observer pattern defines a one-to-many dependency between objects so that when one object changes state, all dependents are notified.

#### **Event System Implementation**

```csharp
public static class EventManager
{
    private static Dictionary<System.Type, System.Delegate> eventDictionary = new Dictionary<System.Type, System.Delegate>();

    // Subscribe to an event
    public static void Subscribe<T>(System.Action<T> listener) where T : IGameEvent
    {
        System.Type eventType = typeof(T);

        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = System.Delegate.Combine(eventDictionary[eventType], listener);
        }
        else
        {
            eventDictionary[eventType] = listener;
        }
    }

    // Unsubscribe from an event
    public static void Unsubscribe<T>(System.Action<T> listener) where T : IGameEvent
    {
        System.Type eventType = typeof(T);

        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = System.Delegate.Remove(eventDictionary[eventType], listener);

            if (eventDictionary[eventType] == null)
            {
                eventDictionary.Remove(eventType);
            }
        }
    }

    // Publish an event
    public static void Publish<T>(T gameEvent) where T : IGameEvent
    {
        System.Type eventType = typeof(T);

        if (eventDictionary.ContainsKey(eventType))
        {
            var action = eventDictionary[eventType] as System.Action<T>;
            action?.Invoke(gameEvent);
        }
    }

    // Clear all events (useful for scene transitions)
    public static void Clear()
    {
        eventDictionary.Clear();
    }
}

// Base interface for all game events
public interface IGameEvent { }

// Example game events
[System.Serializable]
public struct PlayerDeathEvent : IGameEvent
{
    public Vector2 deathPosition;
    public string causeOfDeath;
}

[System.Serializable]
public struct ItemCollectedEvent : IGameEvent
{
    public string itemName;
    public int pointValue;
    public Vector2 collectionPosition;
}

[System.Serializable]
public struct EnemyDefeatedEvent : IGameEvent
{
    public string enemyType;
    public int pointReward;
    public Vector2 defeatPosition;
}
```

#### **Event Usage Examples**

```csharp
public class Player2D : MonoBehaviour
{
    [Header("Player Settings")]
    public int health = 100;

    void Start()
    {
        // Subscribe to events
        EventManager.Subscribe<ItemCollectedEvent>(OnItemCollected);
    }

    void OnDestroy()
    {
        // Always unsubscribe to prevent memory leaks
        EventManager.Unsubscribe<ItemCollectedEvent>(OnItemCollected);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Publish death event
        PlayerDeathEvent deathEvent = new PlayerDeathEvent
        {
            deathPosition = transform.position,
            causeOfDeath = "Enemy collision"
        };

        EventManager.Publish(deathEvent);

        gameObject.SetActive(false);
    }

    private void OnItemCollected(ItemCollectedEvent eventData)
    {
        // React to item collection (e.g., play sound, show effect)
        Debug.Log($"Item collected: {eventData.itemName} for {eventData.pointValue} points");
    }
}

public class Collectible : MonoBehaviour
{
    [Header("Item Settings")]
    public string itemName = "Coin";
    public int pointValue = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Publish collection event
            ItemCollectedEvent collectionEvent = new ItemCollectedEvent
            {
                itemName = itemName,
                pointValue = pointValue,
                collectionPosition = transform.position
            };

            EventManager.Publish(collectionEvent);

            Destroy(gameObject);
        }
    }
}
```

---

### **3. State Pattern**

The State pattern allows an object to alter its behavior when its internal state changes.

#### **Finite State Machine Implementation**

```csharp
// Base state class
public abstract class State<T>
{
    protected T context;

    public State(T context)
    {
        this.context = context;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
    public abstract void FixedUpdate();
}

// State machine
public class StateMachine<T>
{
    private T context;
    private State<T> currentState;
    private State<T> previousState;

    public State<T> CurrentState => currentState;
    public State<T> PreviousState => previousState;

    public StateMachine(T context)
    {
        this.context = context;
    }

    public void ChangeState(State<T> newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
            previousState = currentState;
        }

        currentState = newState;
        currentState?.Enter();
    }

    public void Update()
    {
        currentState?.Update();
    }

    public void FixedUpdate()
    {
        currentState?.FixedUpdate();
    }
}
```

#### **Enemy AI State Machine Example**

```csharp
public class EnemyAI : MonoBehaviour
{
    [Header("AI Settings")]
    public float detectionRange = 5f;
    public float attackRange = 1.5f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public Transform[] patrolPoints;

    [Header("References")]
    public Transform player;
    public Rigidbody2D rb2d;
    public Animator animator;

    private StateMachine<EnemyAI> stateMachine;
    private int currentPatrolIndex = 0;

    // Public properties for states to access
    public float DetectionRange => detectionRange;
    public float AttackRange => attackRange;
    public float PatrolSpeed => patrolSpeed;
    public float ChaseSpeed => chaseSpeed;
    public Transform Player => player;
    public Rigidbody2D Rigidbody => rb2d;
    public Animator Animator => animator;
    public Transform[] PatrolPoints => patrolPoints;
    public int CurrentPatrolIndex { get => currentPatrolIndex; set => currentPatrolIndex = value; }

    void Start()
    {
        // Initialize components
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;

        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        // Initialize state machine
        stateMachine = new StateMachine<EnemyAI>(this);
        stateMachine.ChangeState(new PatrolState(this));
    }

    void Update()
    {
        stateMachine.Update();
    }

    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public float DistanceToPlayer()
    {
        return player != null ? Vector2.Distance(transform.position, player.position) : float.MaxValue;
    }

    public Vector2 DirectionToPlayer()
    {
        return player != null ? (player.position - transform.position).normalized : Vector2.zero;
    }
}

// Patrol State
public class PatrolState : State<EnemyAI>
{
    public PatrolState(EnemyAI context) : base(context) { }

    public override void Enter()
    {
        Debug.Log("Enemy entered Patrol state");
        context.Animator.SetBool("IsPatrolling", true);
    }

    public override void Update()
    {
        // Check for player in detection range
        if (context.DistanceToPlayer() <= context.DetectionRange)
        {
            context.StateMachine.ChangeState(new ChaseState(context));
            return;
        }

        // Move towards current patrol point
        if (context.PatrolPoints.Length > 0)
        {
            Transform targetPoint = context.PatrolPoints[context.CurrentPatrolIndex];
            Vector2 direction = (targetPoint.position - context.transform.position).normalized;

            context.Rigidbody.velocity = direction * context.PatrolSpeed;

            // Check if reached patrol point
            if (Vector2.Distance(context.transform.position, targetPoint.position) < 0.5f)
            {
                context.CurrentPatrolIndex = (context.CurrentPatrolIndex + 1) % context.PatrolPoints.Length;
            }
        }
    }

    public override void FixedUpdate() { }

    public override void Exit()
    {
        context.Animator.SetBool("IsPatrolling", false);
    }
}

// Chase State
public class ChaseState : State<EnemyAI>
{
    public ChaseState(EnemyAI context) : base(context) { }

    public override void Enter()
    {
        Debug.Log("Enemy entered Chase state");
        context.Animator.SetBool("IsChasing", true);
    }

    public override void Update()
    {
        float distanceToPlayer = context.DistanceToPlayer();

        // Check if player is in attack range
        if (distanceToPlayer <= context.AttackRange)
        {
            context.StateMachine.ChangeState(new AttackState(context));
            return;
        }

        // Check if player is out of detection range
        if (distanceToPlayer > context.DetectionRange * 1.5f) // Hysteresis to prevent state flipping
        {
            context.StateMachine.ChangeState(new PatrolState(context));
            return;
        }

        // Move towards player
        Vector2 direction = context.DirectionToPlayer();
        context.Rigidbody.velocity = direction * context.ChaseSpeed;
    }

    public override void FixedUpdate() { }

    public override void Exit()
    {
        context.Animator.SetBool("IsChasing", false);
    }
}

// Attack State
public class AttackState : State<EnemyAI>
{
    private float attackCooldown = 1f;
    private float lastAttackTime = 0f;

    public AttackState(EnemyAI context) : base(context) { }

    public override void Enter()
    {
        Debug.Log("Enemy entered Attack state");
        context.Animator.SetBool("IsAttacking", true);
        context.Rigidbody.velocity = Vector2.zero; // Stop moving when attacking
    }

    public override void Update()
    {
        float distanceToPlayer = context.DistanceToPlayer();

        // Check if player moved out of attack range
        if (distanceToPlayer > context.AttackRange)
        {
            context.StateMachine.ChangeState(new ChaseState(context));
            return;
        }

        // Attack logic
        if (Time.time >= lastAttackTime + attackCooldown)
        {
            PerformAttack();
            lastAttackTime = Time.time;
        }
    }

    public override void FixedUpdate() { }

    private void PerformAttack()
    {
        Debug.Log("Enemy attacks!");
        context.Animator.SetTrigger("Attack");

        // Deal damage to player
        var player = context.Player.GetComponent<Player2D>();
        player?.TakeDamage(10);
    }

    public override void Exit()
    {
        context.Animator.SetBool("IsAttacking", false);
    }
}
```

---

### **4. Command Pattern**

The Command pattern encapsulates a request as an object, allowing you to parameterize objects with different requests and support undo operations.

#### **Input Command System**

```csharp
// Base command interface
public interface ICommand
{
    void Execute();
    void Undo();
}

// Command invoker
public class InputManager : MonoBehaviour
{
    private Dictionary<KeyCode, ICommand> keyBindings = new Dictionary<KeyCode, ICommand>();
    private Stack<ICommand> commandHistory = new Stack<ICommand>();
    private int maxHistorySize = 50;

    [Header("Player Reference")]
    public Player2DController player;

    void Start()
    {
        SetupKeyBindings();
    }

    void SetupKeyBindings()
    {
        // Movement commands
        keyBindings[KeyCode.W] = new MoveCommand(player, Vector2.up);
        keyBindings[KeyCode.S] = new MoveCommand(player, Vector2.down);
        keyBindings[KeyCode.A] = new MoveCommand(player, Vector2.left);
        keyBindings[KeyCode.D] = new MoveCommand(player, Vector2.right);

        // Action commands
        keyBindings[KeyCode.Space] = new JumpCommand(player);
        keyBindings[KeyCode.F] = new AttackCommand(player);

        // Utility commands
        keyBindings[KeyCode.Z] = new UndoCommand(this);
    }

    void Update()
    {
        foreach (var binding in keyBindings)
        {
            if (Input.GetKeyDown(binding.Key))
            {
                ExecuteCommand(binding.Value);
            }
        }
    }

    public void ExecuteCommand(ICommand command)
    {
        command.Execute();

        // Add to history for undo functionality
        commandHistory.Push(command);

        // Limit history size
        if (commandHistory.Count > maxHistorySize)
        {
            var excess = commandHistory.Count - maxHistorySize;
            var tempStack = new Stack<ICommand>();

            for (int i = 0; i < maxHistorySize; i++)
            {
                tempStack.Push(commandHistory.Pop());
            }

            commandHistory.Clear();

            while (tempStack.Count > 0)
            {
                commandHistory.Push(tempStack.Pop());
            }
        }
    }

    public void UndoLastCommand()
    {
        if (commandHistory.Count > 0)
        {
            var lastCommand = commandHistory.Pop();
            lastCommand.Undo();
        }
    }
}

// Movement command
public class MoveCommand : ICommand
{
    private Player2DController player;
    private Vector2 direction;
    private Vector2 previousPosition;

    public MoveCommand(Player2DController player, Vector2 direction)
    {
        this.player = player;
        this.direction = direction;
    }

    public void Execute()
    {
        previousPosition = player.transform.position;
        player.Move(direction);
    }

    public void Undo()
    {
        player.transform.position = previousPosition;
    }
}

// Jump command
public class JumpCommand : ICommand
{
    private Player2DController player;
    private bool wasGrounded;

    public JumpCommand(Player2DController player)
    {
        this.player = player;
    }

    public void Execute()
    {
        wasGrounded = player.IsGrounded;
        player.Jump();
    }

    public void Undo()
    {
        if (wasGrounded)
        {
            player.ResetToGround();
        }
    }
}

// Undo command (meta-command)
public class UndoCommand : ICommand
{
    private InputManager inputManager;

    public UndoCommand(InputManager inputManager)
    {
        this.inputManager = inputManager;
    }

    public void Execute()
    {
        inputManager.UndoLastCommand();
    }

    public void Undo()
    {
        // Undo of undo is redo - not implemented in this example
    }
}
```

---

### **5. Factory Pattern**

The Factory pattern creates objects without specifying their exact classes, providing flexibility in object creation.

#### **Enemy Factory System**

```csharp
// Base enemy interface
public interface IEnemy
{
    void Initialize(Vector3 spawnPosition);
    void TakeDamage(int damage);
    void Die();
}

// Enemy types
public enum EnemyType
{
    Goblin,
    Orc,
    Dragon,
    Slime
}

// Enemy factory
public class EnemyFactory : MonoBehaviour
{
    [System.Serializable]
    public class EnemyPrefabMapping
    {
        public EnemyType type;
        public GameObject prefab;
        public int poolSize = 10;
    }

    [Header("Enemy Configuration")]
    public EnemyPrefabMapping[] enemyMappings;

    private Dictionary<EnemyType, Queue<GameObject>> enemyPools;
    private Dictionary<EnemyType, GameObject> enemyPrefabs;

    void Start()
    {
        InitializePools();
    }

    void InitializePools()
    {
        enemyPools = new Dictionary<EnemyType, Queue<GameObject>>();
        enemyPrefabs = new Dictionary<EnemyType, GameObject>();

        foreach (var mapping in enemyMappings)
        {
            enemyPrefabs[mapping.type] = mapping.prefab;
            enemyPools[mapping.type] = new Queue<GameObject>();

            // Pre-populate pools
            for (int i = 0; i < mapping.poolSize; i++)
            {
                GameObject enemy = Instantiate(mapping.prefab);
                enemy.SetActive(false);
                enemyPools[mapping.type].Enqueue(enemy);
            }
        }
    }

    public GameObject CreateEnemy(EnemyType type, Vector3 position)
    {
        if (!enemyPools.ContainsKey(type))
        {
            Debug.LogError($"Enemy type {type} not found in factory!");
            return null;
        }

        GameObject enemy;

        if (enemyPools[type].Count > 0)
        {
            // Get from pool
            enemy = enemyPools[type].Dequeue();
        }
        else
        {
            // Create new if pool is empty
            enemy = Instantiate(enemyPrefabs[type]);
        }

        enemy.SetActive(true);
        enemy.transform.position = position;

        // Initialize enemy
        var enemyComponent = enemy.GetComponent<IEnemy>();
        enemyComponent?.Initialize(position);

        return enemy;
    }

    public void ReturnEnemy(EnemyType type, GameObject enemy)
    {
        enemy.SetActive(false);

        if (enemyPools.ContainsKey(type))
        {
            enemyPools[type].Enqueue(enemy);
        }
    }
}

// Specific enemy implementations
public class GoblinEnemy : MonoBehaviour, IEnemy
{
    [Header("Goblin Stats")]
    public int health = 50;
    public float speed = 3f;
    public int damage = 10;

    private Rigidbody2D rb2d;
    private Animator animator;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void Initialize(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        health = 50; // Reset health
        gameObject.SetActive(true);

        // Start AI behavior
        StartCoroutine(GoblinAI());
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        animator.SetTrigger("TakeDamage");

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        // Play death animation
        yield return new WaitForSeconds(1f);

        // Return to pool
        EnemyFactory factory = FindObjectOfType<EnemyFactory>();
        factory.ReturnEnemy(EnemyType.Goblin, gameObject);
    }

    IEnumerator GoblinAI()
    {
        while (gameObject.activeInHierarchy)
        {
            // Simple AI: move towards player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Vector2 direction = (player.transform.position - transform.position).normalized;
                rb2d.velocity = direction * speed;

                // Flip sprite based on movement direction
                if (direction.x < 0)
                    transform.localScale = new Vector3(-1, 1, 1);
                else if (direction.x > 0)
                    transform.localScale = new Vector3(1, 1, 1);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
```

---

### **6. MVC (Model-View-Controller) Pattern**

Separates application logic into three interconnected components.

#### **Player MVC Implementation**

```csharp
// Model: Data and business logic
[System.Serializable]
public class PlayerModel
{
    [Header("Stats")]
    public int maxHealth = 100;
    public int currentHealth = 100;
    public int score = 0;
    public int lives = 3;

    [Header("Abilities")]
    public float moveSpeed = 5f;
    public float jumpPower = 10f;
    public bool canDoubleJump = false;
    public bool hasDoubleJumped = false;

    // Events for when data changes
    public System.Action<int> OnHealthChanged;
    public System.Action<int> OnScoreChanged;
    public System.Action<int> OnLivesChanged;

    public void TakeDamage(int damage)
    {
        currentHealth = Mathf.Max(0, currentHealth - damage);
        OnHealthChanged?.Invoke(currentHealth);

        if (currentHealth <= 0)
        {
            LoseLife();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(maxHealth, currentHealth + amount);
        OnHealthChanged?.Invoke(currentHealth);
    }

    public void AddScore(int points)
    {
        score += points;
        OnScoreChanged?.Invoke(score);
    }

    public void LoseLife()
    {
        lives--;
        OnLivesChanged?.Invoke(lives);

        if (lives > 0)
        {
            // Reset health for new life
            currentHealth = maxHealth;
            OnHealthChanged?.Invoke(currentHealth);
        }
    }

    public bool IsAlive()
    {
        return lives > 0;
    }
}

// View: UI and visual representation
public class PlayerView : MonoBehaviour
{
    [Header("UI References")]
    public Slider healthBar;
    public Text scoreText;
    public Text livesText;

    [Header("Visual Effects")]
    public ParticleSystem damageEffect;
    public ParticleSystem healEffect;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip healSound;
    public AudioClip scoreSound;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        if (healthBar != null)
        {
            healthBar.value = (float)currentHealth / maxHealth;
        }

        // Play visual feedback
        if (damageEffect != null)
        {
            damageEffect.Play();
        }

        if (audioSource != null && damageSound != null)
        {
            audioSource.PlayOneShot(damageSound);
        }

        // Update animator
        animator.SetTrigger("TakeDamage");
    }

    public void UpdateScore(int score)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {score:N0}";
        }

        if (audioSource != null && scoreSound != null)
        {
            audioSource.PlayOneShot(scoreSound);
        }
    }

    public void UpdateLives(int lives)
    {
        if (livesText != null)
        {
            livesText.text = $"Lives: {lives}";
        }
    }

    public void ShowHealEffect()
    {
        if (healEffect != null)
        {
            healEffect.Play();
        }

        if (audioSource != null && healSound != null)
        {
            audioSource.PlayOneShot(healSound);
        }
    }
}

// Controller: Input handling and coordination
public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public PlayerModel model;
    public PlayerView view;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode interactKey = KeyCode.E;

    private Rigidbody2D rb2d;
    private bool isGrounded = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        // Subscribe to model events
        model.OnHealthChanged += view.UpdateHealth;
        model.OnScoreChanged += view.UpdateScore;
        model.OnLivesChanged += view.UpdateLives;

        // Initialize view with current model data
        view.UpdateHealth(model.currentHealth, model.maxHealth);
        view.UpdateScore(model.score);
        view.UpdateLives(model.lives);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        // Movement input
        float horizontal = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontal * model.moveSpeed, rb2d.velocity.y);
        rb2d.velocity = movement;

        // Jump input
        if (Input.GetKeyDown(jumpKey))
        {
            HandleJump();
        }

        // Interact input
        if (Input.GetKeyDown(interactKey))
        {
            HandleInteract();
        }
    }

    void HandleJump()
    {
        if (isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, model.jumpPower);
            model.hasDoubleJumped = false;
        }
        else if (model.canDoubleJump && !model.hasDoubleJumped)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, model.jumpPower);
            model.hasDoubleJumped = true;
        }
    }

    void HandleInteract()
    {
        // Check for interactable objects
        Collider2D[] nearbyObjects = Physics2D.OverlapCircleAll(transform.position, 1f);

        foreach (var obj in nearbyObjects)
        {
            var interactable = obj.GetComponent<IInteractable>();
            if (interactable != null)
            {
                interactable.Interact(this);
                break;
            }
        }
    }

    // Called by external systems to affect the player
    public void TakeDamage(int damage)
    {
        model.TakeDamage(damage);
    }

    public void Heal(int amount)
    {
        model.Heal(amount);
        view.ShowHealEffect();
    }

    public void AddScore(int points)
    {
        model.AddScore(points);
    }

    // Ground detection
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            model.hasDoubleJumped = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    void OnDestroy()
    {
        // Unsubscribe from events to prevent memory leaks
        if (model != null)
        {
            model.OnHealthChanged -= view.UpdateHealth;
            model.OnScoreChanged -= view.UpdateScore;
            model.OnLivesChanged -= view.UpdateLives;
        }
    }
}
```

---

## 🛠 **Pattern Selection Guidelines**

### **When to Use Each Pattern**

| Pattern | Use When | Avoid When |
|---------|----------|------------|
| **Singleton** | Managing global systems (GameManager, AudioManager) | Multiple instances might be needed later |
| **Observer** | Decoupling systems that react to events | Simple, direct communication is sufficient |
| **State** | Complex behavior changes (AI, player states) | Simple boolean flags are enough |
| **Command** | Undo/redo, input mapping, macro recording | Simple direct method calls work fine |
| **Factory** | Creating objects with varying types/complexity | Only one type of object is ever created |
| **MVC** | Complex UI/data relationships | Simple components with minimal data |

### **Performance Considerations**

- **Singleton**: Minimal overhead, but avoid overuse
- **Observer**: Small overhead per event, consider pooling for frequent events
- **State**: Very efficient for complex behaviors
- **Command**: Slight memory overhead for command objects
- **Factory**: Efficient with object pooling
- **MVC**: Separation overhead, but improved maintainability

---