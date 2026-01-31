using UnityEngine;

public enum ElementType { Fire, Water, Ice, Wood }

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill System/Skill Data")]
public class SkillData : ScriptableObject
{
    [Header("Identity")]
    public string skillName;
    public Sprite icon;       // Untuk UI nanti
    public ElementType element;

    [Header("Stats")]
    public float cooldownTime; // Berapa lama nunggu?
    public float damage;
    
    [Header("Visual & Effects")]
    public GameObject effectPrefab; // Prefab efek (misal bola api)
    public string animationTrigger; // Nama trigger animasi di Animator
    public float effectDuration = 1f; // Berapa lama efeknya muncul?
}
