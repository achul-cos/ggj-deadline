using UnityEngine;

[CreateAssetMenu(fileName = "New Mask", menuName = "Skill System/Mask Data")]
public class MaskData : ScriptableObject
{
    [Header("Mask Info")]
    public string maskName;
    public ElementType maskElement;
    public Sprite maskSprite; // Gambar topengnya buat UI

    [Header("Skill Set")]
    public SkillData skill_I; // Skill tombol I
    public SkillData skill_O; // Skill tombol O
    public SkillData skill_P; // Skill tombol P
}
