using UnityEngine;

public class SkillBehavior_ScreenNuke : MonoBehaviour
{
    public void Initialize(float damage, SkillData.ElementType element, float duration)
    {
        // Efek Visual (misal layar kedip)
        Debug.Log("SCREEN NUKE ACTIVATED!");

        // 1. Cari semua musuh di dunia
        EnemyBase[] allEnemies = FindObjectsByType<EnemyBase>(FindObjectsSortMode.None);
        
        Camera cam = Camera.main;

        foreach (EnemyBase enemy in allEnemies)
        {
            // 2. Cek apakah musuh ada di dalam layar kamera?
            Vector3 viewportPos = cam.WorldToViewportPoint(enemy.transform.position);

            // Viewport 0,0 (kiri bawah) sampai 1,1 (kanan atas)
            bool isVisible = (viewportPos.x >= 0 && viewportPos.x <= 1 && 
                              viewportPos.y >= 0 && viewportPos.y <= 1);

            if (isVisible)
            {
                enemy.TakeDamage(damage, element);
                Debug.Log($"Nuke Hit: {enemy.name}");
                
                // Opsional: Spawn efek ledakan di posisi musuh
            }
        }

        // Hancurkan objek efek visual nuke setelah durasi
        Destroy(gameObject, duration);
    }
}
