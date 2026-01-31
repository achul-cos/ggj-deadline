using UnityEngine;

public class SkillBehavior_Projectile : MonoBehaviour
{
    public float speed = 10f; // Kecepatan peluru

    private float damage;
    private SkillData.ElementType element;
    public Vector2 direction;

    public void Initialize(float dmg, SkillData.ElementType elem, float duration, Vector2 dir)
    {
        damage = dmg;
        element = elem;
        direction = dir;

        Destroy(gameObject, duration); // Hancur kalau kelamaan terbang
    }

    void Update()
    {
        // Gerak lurus setiap frame
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        EnemyBase enemy = other.GetComponent<EnemyBase>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage, element);
            Debug.Log($"Projectile Hit: {other.name}");
            
            // Hancurkan peluru setelah kena 1 musuh (atau biarkan tembus)
            Destroy(gameObject); 
        }
        else if (other.CompareTag("Ground")) // Hancur kena tembok
        {
            Destroy(gameObject);
        }
    }
}
