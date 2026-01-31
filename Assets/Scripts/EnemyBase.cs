using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public EnemyStats stats; // Drag data stat di sini

    protected float currentHealth;
    protected Transform player;
    
    // Virtual supaya bisa di-override (ditimpa/ditambah) sama anak-anaknya
    protected virtual void Start()
    {
        if(stats != null) currentHealth = stats.maxHealth;
        
        // Cari player otomatis
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
        if (currentHealth <= 0) Die();
    }

    protected bool IsWeakAgainst(SkillData.ElementType attackElement)
    {
        // Masukkan logika Rock-Paper-Scissors elemen di sini
        // Misal: Fire kalah sama Water
        if (stats.element == SkillData.ElementType.Fire && attackElement == SkillData.ElementType.Water) return true;
        // ... dst
        return false;
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
        // Drop item atau tambah score di sini
    }
}
