using UnityEngine;
using UnityEngine.UI; // PENTING: Wajib ada untuk akses UI Image

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;

    [Header("UI Reference")]
    public Image healthBarFill; // Drag gambar 'HealthBarFill' ke sini nanti

    void Start()
    {
        // Set darah penuh saat mulai
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    // Fungsi ini dipanggil saat player kena damage
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        // Batasi biar gak minus
        if (currentHealth < 0) currentHealth = 0;

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Fungsi ini dipanggil saat player dapat heal/regen
    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        
        // Batasi biar gak lebih dari max
        if (currentHealth > maxHealth) currentHealth = maxHealth;

        UpdateHealthUI();
    }

    // Logika update tampilan bar merah
    void UpdateHealthUI()
    {
        if (healthBarFill != null)
        {
            // Rumus: Darah Sekarang / Darah Maksimal = Nilai 0.0 sampai 1.0
            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }

    void Update() // HAPUS NANTI KALAU SUDAH JADI
    {
        //if (Input.GetKeyDown(KeyCode.K)) // Tekan K buat sakitin diri sendiri
        //{
        //    TakeDamage(10);
        //}
    }    

    void Die()
    {
        Debug.Log("Player Mati!");
        // Panggil GameManager GameOver di sini nanti
        // GameManager.Instance.TriggerGameOver();
        
        // Matikan player atau restart scene
        gameObject.SetActive(false); 
    }
}
