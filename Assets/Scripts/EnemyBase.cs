using UnityEngine;
using TMPro;

public class EnemyBase : MonoBehaviour
{
    [Header("Data & Stats")]
    public EnemyStats stats; // Drag data stat di sini
    public float currentHealth;

    [Header("Visual References")]
    public Transform bodySprite;    // Anak yang isinya gambar musuh (untuk di-flip)
    
    // Ganti Image jadi Transform atau SpriteRenderer karena ini object biasa
    public Transform healthBarFill; // Drag Object 'HealthBarFill' (Kotak Merah)
    public TextMeshPro textHealth;  // Drag Object 'HealthText'
    public TextMeshPro textName;    // Drag Object 'Text (TMP)'
    public SpriteRenderer elementBadge;

    [Header("Combat Effect")]
    public GameObject damagePopupPrefab;
    public Transform hitPosition;

    protected Transform player;

    private Vector3 initialBarScale;
    
    // Virtual supaya bisa di-override (ditimpa/ditambah) sama anak-anaknya
    protected virtual void Start()
    {
        if(stats != null) 
        {
            currentHealth = stats.maxHealth;
            if(textName != null) textName.text = stats.enemyName;
            
            // Contoh set icon elemen manual (atau lewat stats.icon)
            // if(elementBadge != null) elementBadge.sprite = stats.elementIcon;
        }

        // Catat scale awal bar merah (misal X=1 atau X=5)
        if (healthBarFill != null)
        {
            initialBarScale = healthBarFill.localScale;
        }

        UpdateHealthUI();
        
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if(p) player = p.transform;
    }

    public void TakeDamage(float damage, SkillData.ElementType attackElement)
    {
        // Logika Elemen Sederhana
        float finalDamage = damage;
        
        // Contoh Logika Lemah/Kuat (Bisa dikembangkan)
        if (IsWeakAgainst(attackElement)) 
        {
            finalDamage *= 2f; // Critical
            Debug.Log("Super Effective!");
        }
        else if (attackElement == stats.element)
        {
            finalDamage = 0; // Tidak mempan
            Debug.Log("Immune!");
            return;
        }

        currentHealth -= finalDamage;

        UpdateHealthUI(); 

        if (currentHealth <= 0) Die();
    }

    public void SpawnDamagePopup(float damageAmount)
    {
        if (damagePopupPrefab != null)
        {
            Vector3 spawnPos = player.position;

            if (hitPosition != null) spawnPos = hitPosition.position;

            GameObject popup = Instantiate(damagePopupPrefab, spawnPos, Quaternion.Euler(0, 0, 0), player);

            popup.GetComponent<DamagePopup>()?.Setup(damageAmount);
        }
    }

    protected bool IsWeakAgainst(SkillData.ElementType attackElement)
    {
        // Masukkan logika Rock-Paper-Scissors elemen di sini
        // Misal: Fire kalah sama Water
        if (stats.element == SkillData.ElementType.Fire && attackElement == SkillData.ElementType.Water) return true;
        // ... dst
        return false;
    }

    void UpdateHealthUI()
    {
        // 1. Update Bar Merah (Pakai Scale X)
        if (healthBarFill != null && stats != null)
        {
            float healthPercentage = currentHealth / stats.maxHealth;
            
            // Ubah Scale X mengecil sesuai persentase darah
            // Scale Y dan Z tetap sama kayak awal
            healthBarFill.localScale = new Vector3(
                initialBarScale.x * healthPercentage, 
                initialBarScale.y, 
                initialBarScale.z
            );
        }

        // 2. Update Angka TextMeshPro
        if (textHealth != null)
        {
            textHealth.text = Mathf.Ceil(currentHealth).ToString();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        // Drop item atau tambah score di sini
    }
}
