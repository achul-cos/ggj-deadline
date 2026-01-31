using UnityEngine;

public class SkillBehavior_MeleeAOE : MonoBehaviour
{
    // Data ini nanti dioper dari PlayerSkillManager saat spawn
    private float damage;
    private SkillData.ElementType element;

    public void Initialize(float dmg, SkillData.ElementType elem, float duration)
    {
        damage = dmg;
        element = elem;
        
        // Hancurkan diri sendiri setelah durasi habis
        Destroy(gameObject, duration);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Cek kalau kena musuh
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            // Kirim damage + elemen
            enemy.TakeDamage(damage, element); // Tambahkan parameter elemen di TakeDamage musuh nanti
            Debug.Log($"Melee Hit: {other.name} with {element}");
        }
    }
}
