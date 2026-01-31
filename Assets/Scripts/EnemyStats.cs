using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Stats", menuName = "Enemy System/Enemy Stats")]
public class EnemyStats : ScriptableObject
{
    [Header("Identity")]
    public string enemyName;
    // Pakai enum elemen yang sudah kamu buat sebelumnya
    // public SkillData.ElementType element; 

    [Header("Stats")]
    public float maxHealth = 50f;
    public float moveSpeed = 3f;
    public float attackDamage = 10f;
    
    [Header("Combat")]
    public float attackRange = 1.5f; // Jarak serang
    public float attackCooldown = 2f;
    public GameObject projectilePrefab; // Khusus Marksman (Ranged)
}
