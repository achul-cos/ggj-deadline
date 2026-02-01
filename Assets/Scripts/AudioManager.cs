using UnityEngine;
using UnityEngine.SceneManagement; // Untuk mendeteksi pergantian scene

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Background Music List")]
    // Drag musik sesuai urutan scene atau panggil by name nanti
    public AudioClip[] sceneMusic; 

    private void Awake()
    {
        // Singleton Pattern: Memastikan hanya ada 1 AudioManager di game
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Jangan hancur saat ganti scene
        }
        else
        {
            Destroy(gameObject); // Hancurkan duplikat jika ada
        }
    }

    private void OnEnable()
    {
        // Langganan event saat scene berganti
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Dipanggil otomatis setiap kali scene selesai loading
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Logika Ganti Lagu Berdasarkan Nama/Index Scene
        // Contoh: Scene 0 (Menu) pakai lagu indeks 0, Scene 1 (Level 1) pakai lagu indeks 1
        
        int sceneIndex = scene.buildIndex;

        if (sceneIndex < sceneMusic.Length)
        {
            PlayMusic(sceneMusic[sceneIndex]);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        // Cek dulu, kalau lagunya SAMA dengan yang sedang main, jangan restart
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }

    // Panggil ini dari script lain: AudioManager.Instance.PlaySFX(clip);
    public void PlaySFX(AudioClip clip)
    {
        // PlayOneShot cocok untuk SFX yang bisa tumpang tindih (misal suara tembakan/ledakan)
        sfxSource.PlayOneShot(clip);
    }
}
