using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Timer System with countdown timers and event triggering
/// </summary>
public class TimerSystem : MonoBehaviour
{
    [Header("Timer Settings")]
    public bool startOnAwake = true;
    public float defaultDuration = 10f;

    private List<Timer> timers = new List<Timer>();

    [System.Serializable]
    public class Timer
    {
        public string name;
        public float duration;
        public float currentTime;
        public bool isRunning;
        public bool isPaused;
        public bool loop;
        public System.Action onComplete;
        public System.Action onTick;

        public Timer(string timerName, float timerDuration, bool shouldLoop = false)
        {
            name = timerName;
            duration = timerDuration;
            currentTime = timerDuration;
            isRunning = false;
            isPaused = false;
            loop = shouldLoop;
        }
    }

    void Start()
    {
        if (startOnAwake)
        {
            CreateTimer("Default", defaultDuration);
        }
    }

    void Update()
    {
        UpdateTimers();
    }

    void UpdateTimers()
    {
        for (int i = timers.Count - 1; i >= 0; i--)
        {
            Timer timer = timers[i];

            if (timer.isRunning && !timer.isPaused)
            {
                timer.currentTime -= Time.deltaTime;

                // Call tick event
                timer.onTick?.Invoke();

                // Check if timer completed
                if (timer.currentTime <= 0)
                {
                    timer.currentTime = 0;
                    timer.isRunning = false;

                    // Call complete event
                    timer.onComplete?.Invoke();

                    // Reset timer if looping
                    if (timer.loop)
                    {
                        timer.currentTime = timer.duration;
                        timer.isRunning = true;
                    }
                }
            }
        }
    }

    public Timer CreateTimer(string name, float duration, bool loop = false)
    {
        Timer timer = new Timer(name, duration, loop);
        timers.Add(timer);
        return timer;
    }

    public void StartTimer(string name)
    {
        Timer timer = GetTimer(name);
        if (timer != null)
        {
            timer.isRunning = true;
            timer.isPaused = false;
        }
    }

    public void PauseTimer(string name)
    {
        Timer timer = GetTimer(name);
        if (timer != null)
        {
            timer.isPaused = true;
        }
    }

    public void ResumeTimer(string name)
    {
        Timer timer = GetTimer(name);
        if (timer != null)
        {
            timer.isPaused = false;
        }
    }

    public void StopTimer(string name)
    {
        Timer timer = GetTimer(name);
        if (timer != null)
        {
            timer.isRunning = false;
            timer.isPaused = false;
        }
    }

    public void ResetTimer(string name)
    {
        Timer timer = GetTimer(name);
        if (timer != null)
        {
            timer.currentTime = timer.duration;
            timer.isRunning = false;
            timer.isPaused = false;
        }
    }

    public void SetTimerDuration(string name, float duration)
    {
        Timer timer = GetTimer(name);
        if (timer != null)
        {
            timer.duration = duration;
            timer.currentTime = duration;
        }
    }

    public float GetTimerProgress(string name)
    {
        Timer timer = GetTimer(name);
        if (timer != null)
        {
            return 1f - (timer.currentTime / timer.duration);
        }
        return 0f;
    }

    public float GetTimerTime(string name)
    {
        Timer timer = GetTimer(name);
        if (timer != null)
        {
            return timer.currentTime;
        }
        return 0f;
    }

    public bool IsTimerRunning(string name)
    {
        Timer timer = GetTimer(name);
        return timer != null && timer.isRunning && !timer.isPaused;
    }

    public Timer GetTimer(string name)
    {
        return timers.Find(t => t.name == name);
    }

    public void RemoveTimer(string name)
    {
        timers.RemoveAll(t => t.name == name);
    }

    public void ClearAllTimers()
    {
        timers.Clear();
    }
}
