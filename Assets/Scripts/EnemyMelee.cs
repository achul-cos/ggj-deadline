using UnityEngine;

public class EnemyMelee : EnemyBase
{
    private float lastAttackTime;
    private Rigidbody2D rb;

    protected override void Start()
    {
        base.Start(); 
        rb = GetComponent<Rigidbody2D>();
        
        // Safety check
        if (rb == null) Debug.LogError("Rigidbody2D hilang di musuh: " + gameObject.name);
    }

    void Update()
    {
        if (player == null || stats == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 1. Cek: Apakah Player ada di dalam Radius Deteksi?
        if (distanceToPlayer <= stats.detectionRange)
        {
            // Player terdeteksi! Sekarang cek apakah harus Lari atau Nyerang?
            
            if (distanceToPlayer > stats.attackRange)
            {
                // Jarak masih jauh -> KEJAR
                MoveTowardsPlayer();
            }
            else 
            {
                // Jarak sudah dekat -> STOP & SERANG
                StopMoving();
                
                if (Time.time >= lastAttackTime + stats.attackCooldown)
                {
                    Attack();
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            // Player di luar jangkauan -> DIAM (Idle)
            StopMoving();
        }
    }

    void MoveTowardsPlayer()
    {
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        
        // PENTING: Menggerakkan RB velocity
        if(rb != null)
        {
            rb.velocity = new Vector2(direction * stats.moveSpeed, rb.velocity.y);
        }

        // Flip Sprite
        if (direction > 0) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    void StopMoving()
    {
        if(rb != null)
        {
            // Nolkan kecepatan X, tapi biarkan Y (gravitasi) jalan
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void Attack()
    {
        Debug.Log("Jleb! Musuh Serang!");
        // Animasi & Damage code...
    }

    // VISUALISASI DEBUG (Biar kelihatan lingkaran radiusnya di Scene View)
    void OnDrawGizmosSelected()
    {
        if (stats != null)
        {
            // Lingkaran Deteksi (Kuning)
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, stats.detectionRange);

            // Lingkaran Serang (Merah)
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, stats.attackRange);
        }
    }
}
