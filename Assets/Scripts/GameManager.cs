using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Static Instance agar bisa dipanggil dari mana saja (Singleton)
    public static GameManager Instance { get; private set; }

    [Header("Game State")]
    public int currentScore = 0;
    public bool isGameOver = false;

    // Contoh data Mask (nanti bisa diganti dengan ScriptableObject)
    public enum ElementType { None, Fire, Water, Ice, Wood }
    [Header("Player Data")]
    public ElementType currentMaskElement = ElementType.None;
    public int playerHealth = 100;

    private void Awake()
    {
        // Implementasi Singleton Pattern
        // Jika Instance belum ada, saya lah Instance-nya
        if (Instance == null)
        {
            Instance = this;
            // Agar object ini tidak hancur saat pindah scene
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Jika sudah ada GameManager lain (misal balik lagi ke menu), hancurkan yang baru ini
            Destroy(gameObject);
        }
    }

    // Contoh Method untuk menambah Score
    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Score Updated: " + currentScore);
        // Nanti di sini kita panggil update UI
    }

    // Contoh Method Game Over
    public void TriggerGameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        // Logic restart level atau muncul panel kalah
    }
}
