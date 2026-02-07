using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    // Panggil GameManager GameOver di sini nanti
    // GameManager.Instance.TriggerGameOver();

    void Die()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }

        Respawn();
    }

    public void Respawn() 
    {
        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        Vector3 spawnPos = FindCheckpoint();

        yield return new WaitForSeconds(1.5f);

        transform.position = spawnPos;

        currentHealth = maxHealth;

        UpdateHealthUI();

        GetComponent<SpriteRenderer>().enabled = true;

        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private Vector3 FindCheckpoint()
    {
        float PlayerX = transform.position.x;

        var checkpointTerbaik = Checkpoint.allCheckpoints
            .Where(cp => cp.transform.position.x <= PlayerX)
            .OrderByDescending(cp => cp.transform.position.x)
            .FirstOrDefault();

        return checkpointTerbaik != null ? checkpointTerbaik.transform.position : transform.position;
    }
}
