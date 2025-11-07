using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Interactable Object with player proximity detection and action prompts
/// </summary>
public class InteractableObject : MonoBehaviour
{
    [Header("Interaction Settings")]
    public float interactionRange = 2f;
    public KeyCode interactionKey = KeyCode.E;
    public bool requireKeyPress = true;

    [Header("UI Prompt")]
    public GameObject promptUI;
    public string promptText = "Press E to interact";

    [Header("Events")]
    public UnityEvent OnInteract;
    public UnityEvent OnEnterRange;
    public UnityEvent OnExitRange;

    private Transform player;
    private bool isInRange = false;
    private bool hasInteracted = false;

    void Start()
    {
        // Find player
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }

        // Setup prompt UI
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);
        bool wasInRange = isInRange;
        isInRange = distance <= interactionRange;

        // Handle range changes
        if (isInRange && !wasInRange)
        {
            OnEnterRange.Invoke();
            ShowPrompt();
        }
        else if (!isInRange && wasInRange)
        {
            OnExitRange.Invoke();
            HidePrompt();
        }

        // Handle interaction
        if (isInRange && requireKeyPress && Input.GetKeyDown(interactionKey))
        {
            Interact();
        }
        else if (isInRange && !requireKeyPress)
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (hasInteracted) return;

        OnInteract.Invoke();
        hasInteracted = true;

        // Hide prompt after interaction
        HidePrompt();

        Debug.Log($"Interacted with {gameObject.name}");
    }

    void ShowPrompt()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(true);
        }

        // Update prompt text if available
        var textComponent = promptUI?.GetComponentInChildren<UnityEngine.UI.Text>();
        if (textComponent != null)
        {
            textComponent.text = promptText;
        }
    }

    void HidePrompt()
    {
        if (promptUI != null)
        {
            promptUI.SetActive(false);
        }
    }

    public void ResetInteraction()
    {
        hasInteracted = false;
    }

    public void SetInteractionRange(float range)
    {
        interactionRange = range;
    }

    public void SetPromptText(string text)
    {
        promptText = text;
    }

    void OnDrawGizmosSelected()
    {
        // Draw interaction range
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);

        // Draw line to player if in range
        if (player != null && isInRange)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, player.position);
        }
    }
}
