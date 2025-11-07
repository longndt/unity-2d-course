using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Debug Helper with visual debugging and performance monitoring
/// </summary>
public class DebugHelper : MonoBehaviour
{
    [Header("Debug Settings")]
    public bool showDebugInfo = true;
    public bool showFPS = true;
    public bool showMemory = true;
    public bool showPhysics = true;

    [Header("Visual Debug")]
    public bool drawGizmos = true;
    public Color gizmoColor = Color.yellow;

    private float fps;
    private float frameTime;
    private int frameCount;
    private float deltaTime;

    private List<DebugInfo> debugInfos = new List<DebugInfo>();

    [System.Serializable]
    public class DebugInfo
    {
        public string name;
        public string value;
        public Color color;

        public DebugInfo(string n, string v, Color c = Color.white)
        {
            name = n;
            value = v;
            color = c;
        }
    }

    void Start()
    {
        if (showDebugInfo)
        {
            InvokeRepeating(nameof(UpdateDebugInfo), 0f, 0.1f);
        }
    }

    void Update()
    {
        if (showFPS)
        {
            UpdateFPS();
        }
    }

    void UpdateFPS()
    {
        frameCount++;
        deltaTime += Time.deltaTime;

        if (deltaTime >= 1f)
        {
            fps = frameCount / deltaTime;
            frameTime = deltaTime / frameCount * 1000f;

            frameCount = 0;
            deltaTime = 0f;
        }
    }

    void UpdateDebugInfo()
    {
        debugInfos.Clear();

        if (showFPS)
        {
            debugInfos.Add(new DebugInfo("FPS", fps.ToString("F1"), Color.green));
            debugInfos.Add(new DebugInfo("Frame Time", frameTime.ToString("F2") + "ms", Color.yellow));
        }

        if (showMemory)
        {
            long memory = System.GC.GetTotalMemory(false);
            debugInfos.Add(new DebugInfo("Memory", (memory / 1024f / 1024f).ToString("F2") + "MB", Color.cyan));
        }

        if (showPhysics)
        {
            debugInfos.Add(new DebugInfo("Physics Objects", FindObjectsOfType<Rigidbody2D>().Length.ToString(), Color.red));
            debugInfos.Add(new DebugInfo("Colliders", FindObjectsOfType<Collider2D>().Length.ToString(), Color.magenta));
        }
    }

    void OnGUI()
    {
        if (!showDebugInfo) return;

        GUIStyle style = new GUIStyle();
        style.fontSize = 16;
        style.normal.textColor = Color.white;

        float yOffset = 10f;
        foreach (DebugInfo info in debugInfos)
        {
            style.normal.textColor = info.color;
            GUI.Label(new Rect(10, yOffset, 200, 20), $"{info.name}: {info.value}", style);
            yOffset += 20f;
        }
    }

    public void LogDebug(string message)
    {
        Debug.Log($"[DebugHelper] {message}");
    }

    public void LogWarning(string message)
    {
        Debug.LogWarning($"[DebugHelper] {message}");
    }

    public void LogError(string message)
    {
        Debug.LogError($"[DebugHelper] {message}");
    }

    public void DrawDebugLine(Vector3 start, Vector3 end, Color color, float duration = 0f)
    {
        if (drawGizmos)
        {
            Debug.DrawLine(start, end, color, duration);
        }
    }

    public void DrawDebugSphere(Vector3 center, float radius, Color color, float duration = 0f)
    {
        if (drawGizmos)
        {
            Debug.DrawRay(center, Vector3.up * radius, color, duration);
            Debug.DrawRay(center, Vector3.down * radius, color, duration);
            Debug.DrawRay(center, Vector3.left * radius, color, duration);
            Debug.DrawRay(center, Vector3.right * radius, color, duration);
        }
    }

    public void DrawDebugBox(Vector3 center, Vector3 size, Color color, float duration = 0f)
    {
        if (drawGizmos)
        {
            Vector3 halfSize = size * 0.5f;
            Vector3[] corners = {
                center + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z),
                center + new Vector3(halfSize.x, -halfSize.y, -halfSize.z),
                center + new Vector3(halfSize.x, halfSize.y, -halfSize.z),
                center + new Vector3(-halfSize.x, halfSize.y, -halfSize.z),
                center + new Vector3(-halfSize.x, -halfSize.y, halfSize.z),
                center + new Vector3(halfSize.x, -halfSize.y, halfSize.z),
                center + new Vector3(halfSize.x, halfSize.y, halfSize.z),
                center + new Vector3(-halfSize.x, halfSize.y, halfSize.z)
            };

            // Draw box edges
            for (int i = 0; i < 4; i++)
            {
                Debug.DrawLine(corners[i], corners[(i + 1) % 4], color, duration);
                Debug.DrawLine(corners[i + 4], corners[(i + 1) % 4 + 4], color, duration);
                Debug.DrawLine(corners[i], corners[i + 4], color, duration);
            }
        }
    }

    void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
    }
}
