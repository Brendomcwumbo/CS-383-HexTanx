using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    // Audio players components
    [SerializeField] private AudioSource effectsSource;
    [SerializeField] private AudioSource musicSource;

    // Volume control
    [SerializeField] private float effectsVolume = 1f;
    [SerializeField] private float musicVolume = 1f;

    // Random pitch adjustment range
    [SerializeField] private float lowPitchRange = 0.95f;
    [SerializeField] private float highPitchRange = 1.05f;

    // Sound effect clips
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip hurtClip;
    [SerializeField] private AudioClip explodeClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip playerMoveClip;
    [SerializeField] private AudioClip playerDeathClip;
    [SerializeField] private AudioClip enemyMoveClip;
    [SerializeField] private AudioClip enemyDeathClip;

    // Music clips
    [SerializeField] private AudioClip menuMusicClip;
    [SerializeField] private AudioClip battleMusicClip;
    [SerializeField] private AudioClip pauseMusicClip;
    [SerializeField] private AudioClip winMusicClip;
    [SerializeField] private AudioClip loseMusicClip;

    // Singleton instance
    public static SoundManager Instance = null;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Ensure AudioSources are assigned
        if (effectsSource == null)
        {
            effectsSource = gameObject.AddComponent<AudioSource>();
        }
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name.ToLower();
        Debug.Log($"Scene loaded: {sceneName}. Playing corresponding music.");

        switch (sceneName)
        {
            case "mainmenu":
                MenuMusic();
                break;
            case "samplescene":
                BattleMusic();
                break;
            case "pausescene":
                PauseMusic();
                break;
            case "winscene":
                WinMusic();
                break;
            case "losescene":
                LoseMusic();
                break;
            default:
                Debug.LogWarning($"No music defined for scene '{sceneName}'. Defaulting to Menu music.");
                MenuMusic();
                break;
        }
    }

    // Volume getters
    public float GetEffectsVolume()
    {
        return effectsVolume;
    }

    public float GetMusicVolume()
    {
        return musicVolume;
    }

    // Volume setters
    public void SetEffectsVolume(float volume)
    {
        effectsVolume = Mathf.Clamp01(volume);
        Debug.Log($"Effects volume set to {effectsVolume}");
        effectsSource.volume = effectsVolume;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        Debug.Log($"Music volume set to {musicVolume}");
        musicSource.volume = musicVolume;
    }

    // Play a single clip through the sound effects source
    public void Play(AudioClip clip)
    {
        if (clip != null)
        {
            effectsSource.PlayOneShot(clip, effectsVolume);
        }
        else
        {
            Debug.LogWarning("AudioClip is null in Play!");
        }
    }

    // Play a single clip through the music source
    public void PlayMusic(AudioClip clip)
    {
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioClip is null in PlayMusic!");
        }
    }

    // Play a random clip from an array with pitch variation
    public void RandomSoundEffect(params AudioClip[] clips)
    {
        if (clips == null || clips.Length == 0) return;

        int randomIndex = Random.Range(0, clips.Length);
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        effectsSource.pitch = randomPitch;
        Play(clips[randomIndex]);
    }

    // Play a movement sound with the ability to stop it
    public void PlayMovementSound(AudioClip clip)
    {
        if (clip != null)
        {
            effectsSource.clip = clip;
            effectsSource.volume = effectsVolume;
            effectsSource.loop = false; // No looping for movement sound
            effectsSource.Play();
        }
        else
        {
            Debug.LogWarning("Movement AudioClip is null in PlayMovementSound!");
        }
    }

    public void StopMovementSound()
    {
        if (effectsSource.isPlaying)
        {
            effectsSource.Stop();
            Debug.Log("Movement sound stopped.");
        }
    }

    // Sound effect methods
    public void ShootSound()
    {
        Play(shootClip);
    }

    public void HurtSound()
    {
        Play(hurtClip);
    }

    public void ExplodeSound()
    {
        Play(explodeClip);
    }

    public void PickupSound()
    {
        Play(pickupClip);
    }

    public void PlayerMoveSound()
    {
        PlayMovementSound(playerMoveClip);
    }

    public void PlayerDeathSound()
    {
        Play(playerDeathClip);
    }

    public void EnemyMoveSound()
    {
        PlayMovementSound(enemyMoveClip);
    }

    public void EnemyDeathSound()
    {
        Play(enemyDeathClip);
    }

    // Music methods
    public void MenuMusic()
    {
        PlayMusic(menuMusicClip);
    }

    public void BattleMusic()
    {
        PlayMusic(battleMusicClip);
    }

    public void PauseMusic()
    {
        PlayMusic(pauseMusicClip);
    }

    public void WinMusic()
    {
        PlayMusic(winMusicClip);
    }

    public void LoseMusic()
    {
        PlayMusic(loseMusicClip);
    }
}