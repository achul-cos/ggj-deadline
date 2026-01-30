using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill System/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("Skill Info")]
    public string skillName;
    public Sprite icon;       // Untuk UI nanti
    public float cooldownTime; // Berapa lama nunggu?
    
    [Header("Visual & Effects")]
    public GameObject effectPrefab; // Prefab efek (misal bola api)
    public string animationTrigger; // Nama trigger animasi di Animator
    
    [Header("Behavior")]
    public float damage;
    public float effectDuration = 1f; // Berapa lama efeknya muncul?
}
