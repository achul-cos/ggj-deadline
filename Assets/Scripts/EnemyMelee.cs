using Unity.VisualScripting;
using UnityEngine;

public class EnemyMelee : EnemyBase
{
    private float lastAttackTime;
    private SpriteRenderer bodyRenderer;
    private Rigidbody2D rb;

    public AudioClip enemyAttackSFX;

    protected override void Start()
    {
        base.Start(); 
        rb = GetComponent<Rigidbody2D>();
        // anim = GetComponent<Animator>();
        
        // Safety check
        if (rb == null) Debug.LogError("Rigidbody2D hilang di musuh: " + gameObject.name);

        if (bodySprite != null)
        {
            bodyRenderer = bodySprite.GetComponent<SpriteRenderer>();
        }        
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

        // --- FLIP PAKAI SPRITE RENDERER (Flip X) ---
        if (bodyRenderer != null)
        {
            // Jika gerak ke KANAN (direction > 0) -> Jangan di-flip (Asumsi gambar asli menghadap kanan)
            // Jika gerak ke KIRI (direction < 0) -> Flip X aktif
            
            if (direction > 0)
                bodyRenderer.flipX = true; 
            else if (direction < 0)
                bodyRenderer.flipX = false;
        }
        // Fallback: Kalau bodyRenderer gak ketemu, pakai cara lama (Scale)
        else if (bodySprite != null)
        {
             if (direction > 0) 
                bodySprite.localScale = new Vector3(Mathf.Abs(bodySprite.localScale.x), bodySprite.localScale.y, 1);
            else 
                bodySprite.localScale = new Vector3(-Mathf.Abs(bodySprite.localScale.x), bodySprite.localScale.y, 1);
        }       
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
        if (player != null)
        {
            PlayerHealth ph = player.GetComponent<PlayerHealth>();

            if (ph != null)
            { 
                if (ph.currentHealth <= 0) return; // Player sudah mati, jangan serang lagi

                ph.TakeDamage(stats.attackDamage);

                SpawnDamagePopup(stats.attackDamage);

                AudioManager.Instance.PlaySFX(enemyAttackSFX);

            }

        }

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
