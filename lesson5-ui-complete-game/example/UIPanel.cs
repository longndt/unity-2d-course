using UnityEngine;
using System.Collections;

/// <summary>
/// Base UI Panel class for managing panel states
/// Demonstrates panel visibility and animation handling
/// </summary>
public class UIPanel : MonoBehaviour
{
    [Header("Panel Settings")]
    public bool showOnStart = false;
    public float animationDuration = 0.3f;

    protected CanvasGroup canvasGroup;
    protected bool isVisible;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // Initialize panel state
        if (showOnStart)
            Show();
        else
            Hide();
    }

    public virtual void Show()
    {
        if (isVisible) return;

        isVisible = true;
        gameObject.SetActive(true);
        StartCoroutine(FadeIn());
        OnPanelShown();
    }

    public virtual void Hide()
    {
        if (!isVisible) return;

        isVisible = false;
        StartCoroutine(FadeOut());
        OnPanelHidden();
    }

    protected virtual void OnPanelShown()
    {
        // Override for panel-specific show logic
    }

    protected virtual void OnPanelHidden()
    {
        // Override for panel-specific hide logic
    }

    IEnumerator FadeIn()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = true;

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / animationDuration);
            yield return null;
        }

        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
    }

    IEnumerator FadeOut()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = false;

        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / animationDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
        gameObject.SetActive(false);
    }

    public bool IsVisible => isVisible;
}