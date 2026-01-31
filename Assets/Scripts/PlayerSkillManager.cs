using UnityEngine;

using System.Collections.Generic; 

public class PlayerSkillManager : MonoBehaviour
{
    [Header("Mask Data (Urutan: 0=Water, 1=Fire, 2=Wood, 3=Ice)")]
    public List<MaskData> availableMasks; 

    [Header("UI Slots (Urutan Harus Sama dengan Data!)")]
    public MaskSlotUI uiSlotWater; // Slot 1
    public MaskSlotUI uiSlotFire;  // Slot 2
    public MaskSlotUI uiSlotWood;  // Slot 3
    public MaskSlotUI uiSlotIce;   // Slot 4

    [Header("Swap Settings")]
    public float swapCooldownDuration = 5f;
    private float swapTimer = 0f;

    [Header("Current State")]
    public MaskData currentMask;
    private int currentMaskIndex = -1; // -1 artinya belum ada        

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
        // anim = GetComponent<Animator>();

        // // --- LOGIKA RANDOM MASK ---
        // if (availableMasks != null && availableMasks.Count > 0)
        // {
        //     // Pilih indeks acak dari 0 sampai jumlah mask
        //     int randomIndex = Random.Range(0, availableMasks.Count);
            
        //     // Pasang mask terpilih
        //     EquipMask(availableMasks[randomIndex]);
        // }
        // else
        // {
        //     Debug.LogError("List 'Available Masks' di Inspector KOSONG! Masukkan data mask dulu.");
        // }

        TrySwitchMask(0, true);
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

    // Fungsi Mencoba Ganti Mask
    public void TrySwitchMask(int targetIndex, bool ignoreCooldown = false)
    {
        // Validasi Index
        if (targetIndex < 0 || targetIndex >= availableMasks.Count) return;

        // Validasi Logic Game
        if (!ignoreCooldown)
        {
            // Jika sedang cooldown, tolak
            if (swapTimer > 0) return;

            // Jika menekan mask yang SAMA, tolak
            if (targetIndex == currentMaskIndex) return;
        }

        // --- LAKUKAN PERGANTIAN ---
        
        // 1. Set Data
        currentMaskIndex = targetIndex;
        MaskData newMask = availableMasks[currentMaskIndex];
        
        // 2. Equip Skill (Panggil fungsi EquipMask lama kamu)
        EquipMask(newMask); 

        // 3. Reset Global Cooldown
        if (!ignoreCooldown)
        {
            swapTimer = swapCooldownDuration;
        }
        
        // 4. Update Border Aktif (Visual)
        HighlightActiveSlot(currentMaskIndex);
    }

    void UpdateAllMaskUI()
    {
        // Kirim data timer yang sama ke semua slot
        if(uiSlotWater) uiSlotWater.UpdateCooldownVisual(swapTimer, swapCooldownDuration);
        if(uiSlotFire) uiSlotFire.UpdateCooldownVisual(swapTimer, swapCooldownDuration);
        if(uiSlotWood) uiSlotWood.UpdateCooldownVisual(swapTimer, swapCooldownDuration);
        if(uiSlotIce) uiSlotIce.UpdateCooldownVisual(swapTimer, swapCooldownDuration);
    }

    void HighlightActiveSlot(int index)
    {
        // Matikan semua border dulu
        if(uiSlotWater) uiSlotWater.SetActiveState(false);
        if(uiSlotFire) uiSlotFire.SetActiveState(false);
        if(uiSlotWood) uiSlotWood.SetActiveState(false);
        if(uiSlotIce) uiSlotIce.SetActiveState(false);

        // Nyalakan yang terpilih
        switch (index)
        {
            case 0: if(uiSlotWater) uiSlotWater.SetActiveState(true); break;
            case 1: if(uiSlotFire) uiSlotFire.SetActiveState(true); break;
            case 2: if(uiSlotWood) uiSlotWood.SetActiveState(true); break;
            case 3: if(uiSlotIce) uiSlotIce.SetActiveState(true); break;
        }
    }    

    void Update()
    {
        // 1. Update Timer Global Cooldown
        if (swapTimer > 0)
        {
            swapTimer -= Time.deltaTime;
        }

        // 2. Update Visual UI (Semua slot cooldownnya SAMA/SERENTAK)
        UpdateAllMaskUI();

        // 3. Input Keyboard (1, 2, 3, 4)
        if (Input.GetKeyDown(KeyCode.Alpha1)) TrySwitchMask(0); // Water
        if (Input.GetKeyDown(KeyCode.Alpha2)) TrySwitchMask(1); // Fire
        if (Input.GetKeyDown(KeyCode.Alpha3)) TrySwitchMask(2); // Wood
        if (Input.GetKeyDown(KeyCode.Alpha4)) TrySwitchMask(3); // Ice

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

        // Tentukan posisi & arah
        Vector3 spawnPos = transform.position;
        Vector2 facingDir = new Vector2(transform.localScale.x > 0 ? 1 : -1, 0);   

        // Spawn Prefab
        if (skill.effectPrefab != null)
        {
            GameObject skillObj = Instantiate(skill.effectPrefab, spawnPos, Quaternion.identity);

            // -- LOGIKA DETEKSI TIPE SKILL --
            
            // Cek apakah ini Skill 1?
            var melee = skillObj.GetComponent<SkillBehavior_MeleeAOE>();
            if (melee) 
            {
                melee.transform.parent = this.transform; // Nempel ke player
                melee.Initialize(skill.damage, skill.element, skill.effectDuration);
            }

            // Cek apakah ini Skill 2?
            var proj = skillObj.GetComponent<SkillBehavior_Projectile>();
            if (proj)
            {
                // Geser spawn dikit ke depan biar gak nabrak badan sendiri
                skillObj.transform.position += (Vector3)facingDir * 1.0f; 
                proj.Initialize(skill.damage, skill.element, skill.effectDuration, facingDir);
            }

            // Cek apakah ini Skill 3?
            var nuke = skillObj.GetComponent<SkillBehavior_ScreenNuke>();
            if (nuke)
            {
                nuke.Initialize(skill.damage, skill.element, skill.effectDuration);
            }
        }             

        // // 2. Mainkan Animasi (jika ada)
        // if (!string.IsNullOrEmpty(skill.animationTrigger) && anim != null)
        // {
        //     anim.SetTrigger(skill.animationTrigger);
        // }

        // // 3. Spawn Efek Skill (Logic Serangan)
        // // Kita spawn efek di posisi player, sedikit ke depan
        // if (skill.effectPrefab != null)
        // {
        //     Vector3 spawnPos = transform.position + (transform.right * (transform.localScale.x > 0 ? 1 : -1)); 
        //     GameObject effect = Instantiate(skill.effectPrefab, spawnPos, Quaternion.identity);
            
        //     // Hancurkan efek setelah durasi selesai
        //     Destroy(effect, skill.effectDuration);
        // }
    }
}
