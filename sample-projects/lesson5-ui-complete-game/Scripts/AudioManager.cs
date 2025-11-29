using UnityEngine;

/// <summary>
/// Advanced audio manager for music and sound effects.
/// Demonstrates audio system management, volume control, and audio feedback.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [Tooltip("Music audio source")]
    public AudioSource musicSource;
    
    [Tooltip("SFX audio source")]
    public AudioSource sfxSource;
    
    [Tooltip("Ambient audio source")]
    public AudioSource ambientSource;
    
    [Header("Music Clips")]
    [Tooltip("Main menu music")]
    public AudioClip mainMenuMusic;
    
    [Tooltip("Gameplay music")]
    public AudioClip gameplayMusic;
    
    [Tooltip("Victory music")]
    public AudioClip victoryMusic;
    
    [Tooltip("Game over music")]
    public AudioClip gameOverMusic;
    
    [Header("SFX Clips")]
    [Tooltip("Jump sound effect")]
    public AudioClip jumpSound;
    
    [Tooltip("Attack sound effect")]
    public AudioClip attackSound;
    
    [Tooltip("Collect sound effect")]
    public AudioClip collectSound;
    
    [Tooltip("Game over sound effect")]
    public AudioClip gameOverSound;
    
    [Tooltip("Victory sound effect")]
    public AudioClip victorySound;
    
    [Header("Volume Settings")]
    [Tooltip("Master volume")]
    [Range(0f, 1f)]
    public float masterVolume = 1f;
    
    [Tooltip("Music volume")]
    [Range(0f, 1f)]
    public float musicVolume = 0.7f;
    
    [Tooltip("SFX volume")]
    [Range(0f, 1f)]
    public float sfxVolume = 1f;
    
    [Tooltip("Ambient volume")]
    [Range(0f, 1f)]
    public float ambientVolume = 0.5f;
    
    [Header("Audio Settings")]
    [Tooltip("Enable audio")]
    public bool enableAudio = true;
    
    [Tooltip("Fade between tracks")]
    public bool enableFading = true;
    
    [Tooltip("Fade duration")]
    public float fadeDuration = 1f;
    
    [Header("Debug")]
    [Tooltip("Show debug information")]
    public bool showDebugInfo = true;
    
    public static AudioManager Instance { get; private set; }
    
    private bool isFading = false;
    private float fadeTimer = 0f;
    private AudioClip targetClip;
    private float targetVolume;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        SetupAudioSources();
        LoadAudioSettings();
        
        if (showDebugInfo)
            Debug.Log("AudioManager: Audio system initialized");
    }
    
    void Update()
    {
        HandleFading();
        HandleInput();
    }
    
    void SetupAudioSources()
    {
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
            musicSource.volume = musicVolume;
        }
        
        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
            sfxSource.loop = false;
            sfxSource.volume = sfxVolume;
        }
        
        if (ambientSource == null)
        {
            ambientSource = gameObject.AddComponent<AudioSource>();
            ambientSource.loop = true;
            ambientSource.volume = ambientVolume;
        }
    }
    
    void HandleFading()
    {
        if (!isFading) return;
        
        fadeTimer += Time.deltaTime;
        float fadeProgress = fadeTimer / fadeDuration;
        
        if (fadeProgress >= 1f)
        {
            // Fade complete
            if (musicSource != null)
            {
                musicSource.clip = targetClip;
                musicSource.volume = targetVolume;
                musicSource.Play();
            }
            
            isFading = false;
            fadeTimer = 0f;
        }
        else
        {
            // Fade in progress
            if (musicSource != null)
            {
                musicSource.volume = Mathf.Lerp(0f, targetVolume, fadeProgress);
            }
        }
    }
    
    void HandleInput()
    {
        // Test audio
        if (Input.GetKeyDown(KeyCode.M))
        {
            PlayMainMenuMusic();
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            PlayGameplayMusic();
        }
        
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlaySFX(jumpSound);
        }
    }
    
    public void PlayMainMenuMusic()
    {
        if (!enableAudio || mainMenuMusic == null) return;
        
        if (enableFading)
        {
            FadeToClip(mainMenuMusic, musicVolume);
        }
        else
        {
            if (musicSource != null)
            {
                musicSource.clip = mainMenuMusic;
                musicSource.Play();
            }
        }
        
        if (showDebugInfo)
            Debug.Log("AudioManager: Playing main menu music");
    }
    
    public void PlayGameplayMusic()
    {
        if (!enableAudio || gameplayMusic == null) return;
        
        if (enableFading)
        {
            FadeToClip(gameplayMusic, musicVolume);
        }
        else
        {
            if (musicSource != null)
            {
                musicSource.clip = gameplayMusic;
                musicSource.Play();
            }
        }
        
        if (showDebugInfo)
            Debug.Log("AudioManager: Playing gameplay music");
    }
    
    public void PlayVictoryMusic()
    {
        if (!enableAudio || victoryMusic == null) return;
        
        if (enableFading)
        {
            FadeToClip(victoryMusic, musicVolume);
        }
        else
        {
            if (musicSource != null)
            {
                musicSource.clip = victoryMusic;
                musicSource.Play();
            }
        }
        
        if (showDebugInfo)
            Debug.Log("AudioManager: Playing victory music");
    }
    
    public void PlayGameOverMusic()
    {
        if (!enableAudio || gameOverMusic == null) return;
        
        if (enableFading)
        {
            FadeToClip(gameOverMusic, musicVolume);
        }
        else
        {
            if (musicSource != null)
            {
                musicSource.clip = gameOverMusic;
                musicSource.Play();
            }
        }
        
        if (showDebugInfo)
            Debug.Log("AudioManager: Playing game over music");
    }
    
    public void PlaySFX(AudioClip clip)
    {
        if (!enableAudio || clip == null || sfxSource == null) return;
        
        sfxSource.PlayOneShot(clip);
        
        if (showDebugInfo)
            Debug.Log($"AudioManager: Playing SFX: {clip.name}");
    }
    
    public void PlayJumpSound()
    {
        PlaySFX(jumpSound);
    }
    
    public void PlayAttackSound()
    {
        PlaySFX(attackSound);
    }
    
    public void PlayCollectSound()
    {
        PlaySFX(collectSound);
    }
    
    public void PlayGameOverSound()
    {
        PlaySFX(gameOverSound);
    }
    
    public void PlayVictorySound()
    {
        PlaySFX(victorySound);
    }
    
    void FadeToClip(AudioClip clip, float volume)
    {
        if (musicSource == null) return;
        
        targetClip = clip;
        targetVolume = volume;
        isFading = true;
        fadeTimer = 0f;
        
        if (showDebugInfo)
            Debug.Log($"AudioManager: Fading to {clip.name}");
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);
        AudioListener.volume = masterVolume;
        SaveAudioSettings();
        
        if (showDebugInfo)
            Debug.Log($"AudioManager: Master volume set to {masterVolume}");
    }
    
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        if (musicSource != null)
        {
            musicSource.volume = musicVolume;
        }
        SaveAudioSettings();
        
        if (showDebugInfo)
            Debug.Log($"AudioManager: Music volume set to {musicVolume}");
    }
    
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        if (sfxSource != null)
        {
            sfxSource.volume = sfxVolume;
        }
        SaveAudioSettings();
        
        if (showDebugInfo)
            Debug.Log($"AudioManager: SFX volume set to {sfxVolume}");
    }
    
    public void SetAmbientVolume(float volume)
    {
        ambientVolume = Mathf.Clamp01(volume);
        if (ambientSource != null)
        {
            ambientSource.volume = ambientVolume;
        }
        SaveAudioSettings();
        
        if (showDebugInfo)
            Debug.Log($"AudioManager: Ambient volume set to {ambientVolume}");
    }
    
    void LoadAudioSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.7f);
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        ambientVolume = PlayerPrefs.GetFloat("AmbientVolume", 0.5f);
        
        AudioListener.volume = masterVolume;
        if (musicSource != null) musicSource.volume = musicVolume;
        if (sfxSource != null) sfxSource.volume = sfxVolume;
        if (ambientSource != null) ambientSource.volume = ambientVolume;
    }
    
    void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
        PlayerPrefs.SetFloat("AmbientVolume", ambientVolume);
        PlayerPrefs.Save();
    }
    
    void OnGUI()
    {
        if (!showDebugInfo) return;
        
        // Display audio info
        GUI.Label(new Rect(10, 10, 300, 20), "Audio Manager Controls:");
        GUI.Label(new Rect(10, 30, 300, 20), "M - Main Menu Music");
        GUI.Label(new Rect(10, 50, 300, 20), "G - Gameplay Music");
        GUI.Label(new Rect(10, 70, 300, 20), "S - Jump Sound");
        
        GUI.Label(new Rect(10, 100, 300, 20), $"Master Volume: {masterVolume:F2}");
        GUI.Label(new Rect(10, 120, 300, 20), $"Music Volume: {musicVolume:F2}");
        GUI.Label(new Rect(10, 140, 300, 20), $"SFX Volume: {sfxVolume:F2}");
        GUI.Label(new Rect(10, 160, 300, 20), $"Ambient Volume: {ambientVolume:F2}");
    }
}
