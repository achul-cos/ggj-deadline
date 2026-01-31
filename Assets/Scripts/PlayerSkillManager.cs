using UnityEngine;

using System.Collections.Generic; 

public class PlayerSkillManager : MonoBehaviour
{
    [Header("Current Mask")]
    public List<MaskData> availableMasks;
    public MaskData currentMask; // Drag mask awal di sini (misal Mask_Fire)

    private SkillData skillI; // Private aja, diisi otomatis dari mask
    private SkillData skillO;
    private SkillData skillP;

    [Header("UI References")] // TAMBAHAN BARU
    public SkillUI uiSkillI;
    public SkillUI uiSkillO;
    public SkillUI uiSkillP;    

    // Timer cooldown private
    private float cooldownTimerI;
    private float cooldownTimerO;
    private float cooldownTimerP;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();

        // --- LOGIKA RANDOM MASK ---
        if (availableMasks != null && availableMasks.Count > 0)
        {
            // Pilih indeks acak dari 0 sampai jumlah mask
            int randomIndex = Random.Range(0, availableMasks.Count);
            
            // Pasang mask terpilih
            EquipMask(availableMasks[randomIndex]);
        }
        else
        {
            Debug.LogError("List 'Available Masks' di Inspector KOSONG! Masukkan data mask dulu.");
        }           
    }
    
    public void EquipMask(MaskData newMask)
    {
        currentMask = newMask;

        // 1. Bongkar isi mask, masukkan ke slot skill aktif
        skillI = newMask.skill_I;
        skillO = newMask.skill_O;
        skillP = newMask.skill_P;

        // 2. Update UI Icon otomatis
        if(uiSkillI) uiSkillI.SetSkillIcon(skillI.icon);
        if(uiSkillO) uiSkillO.SetSkillIcon(skillO.icon);
        if(uiSkillP) uiSkillP.SetSkillIcon(skillP.icon);
        
        // 3. Reset Cooldown (Opsional: biar gak cheat ganti mask buat reset skill)
        cooldownTimerI = 0;
        cooldownTimerO = 0;
        cooldownTimerP = 0;

        Debug.Log("Ganti Mask ke: " + newMask.maskName);
    }    

    void Update()
    {
        // Update Timer Cooldown (dikurangi waktu berjalan)
        if (cooldownTimerI > 0) cooldownTimerI -= Time.deltaTime;
        if (cooldownTimerO > 0) cooldownTimerO -= Time.deltaTime;
        if (cooldownTimerP > 0) cooldownTimerP -= Time.deltaTime;

        // --- TAMBAHAN BARU: Update Visual ke UI ---
        if(uiSkillI) uiSkillI.UpdateCooldown(cooldownTimerI, skillI.cooldownTime);
        if(uiSkillO) uiSkillO.UpdateCooldown(cooldownTimerO, skillO.cooldownTime);
        if(uiSkillP) uiSkillP.UpdateCooldown(cooldownTimerP, skillP.cooldownTime);        

        // Input Skill I
        if (Input.GetKeyDown(KeyCode.I) && cooldownTimerI <= 0)
        {
            ActivateSkill(skillI, ref cooldownTimerI);
        }

        // Input Skill O
        if (Input.GetKeyDown(KeyCode.O) && cooldownTimerO <= 0)
        {
            ActivateSkill(skillO, ref cooldownTimerO);
        }

        // Input Skill P
        if (Input.GetKeyDown(KeyCode.P) && cooldownTimerP <= 0)
        {
            ActivateSkill(skillP, ref cooldownTimerP);
        }
    }

    // Fungsi helper untuk menjalankan skill
    void ActivateSkill(SkillData skill, ref float currentCooldown)
    {
        if (skill == null) return;

        Debug.Log($"Menggunakan Skill: {skill.skillName}");

        // 1. Set Cooldown
        currentCooldown = skill.cooldownTime;

        // 2. Mainkan Animasi (jika ada)
        if (!string.IsNullOrEmpty(skill.animationTrigger) && anim != null)
        {
            anim.SetTrigger(skill.animationTrigger);
        }

        // 3. Spawn Efek Skill (Logic Serangan)
        // Kita spawn efek di posisi player, sedikit ke depan
        if (skill.effectPrefab != null)
        {
            Vector3 spawnPos = transform.position + (transform.right * (transform.localScale.x > 0 ? 1 : -1)); 
            GameObject effect = Instantiate(skill.effectPrefab, spawnPos, Quaternion.identity);
            
            // Hancurkan efek setelah durasi selesai
            Destroy(effect, skill.effectDuration);
        }
    }
}
