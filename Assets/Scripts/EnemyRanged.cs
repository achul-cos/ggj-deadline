using UnityEngine;

public class EnemyRanged : EnemyBase
{
    private float lastAttackTime;

    void Update()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);

        // Marksman diam saja kalau sudah dalam jarak tembak, atau lari kalau terlalu dekat (opsional)
        if (distance <= stats.attackRange)
        {
             if (Time.time >= lastAttackTime + stats.attackCooldown)
            {
                Shoot();
                lastAttackTime = Time.time;
            }
        }
        
        // Flip sprite selalu hadap player
        float direction = Mathf.Sign(player.position.x - transform.position.x);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * direction, transform.localScale.y, 1);
    }

    void Shoot()
    {
        Debug.Log(stats.enemyName + " Menembak!");
        if(stats.projectilePrefab != null)
        {
            Instantiate(stats.projectilePrefab, transform.position, Quaternion.identity);
            // Script peluru nanti yang jalan sendiri ke arah player
        }
    }
}
