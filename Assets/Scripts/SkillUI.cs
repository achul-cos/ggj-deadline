using UnityEngine;
using UnityEngine.UI; // Wajib buat UI Image & Text
using TMPro;

public class SkillUI : MonoBehaviour
{
    [Header("UI Components")]
    public Image skillIcon;       // Gambar skill utama
    public Image cooldownOverlay; // Gambar gelap yang muter
    public TextMeshProUGUI cooldownText;     // Teks angka (Pakai TMPro jika perlu)

    public void SetSkillIcon(Sprite icon)
    {
        if (skillIcon != null && icon != null)
        {
            skillIcon.sprite = icon;
            // Tips: Kalau icon-mu transparan pinggirnya, pastikan Image di UI warnanya Putih (White) 
            // dan Alpha-nya full biar gambarnya gak jadi gelap/hilang.
            skillIcon.color = Color.white; 
        }
        // if (skillIcon != null) skillIcon.sprite = icon;
    }

    public void UpdateCooldown(float currentTimer, float maxCooldown)
    {
        // 1. Update Lingkaran Gelap (Fill Amount)
        // Jika sedang cooldown (timer > 0), fill amount dari 1.0 menuju 0.0
        // Rumus: sisa_waktu / total_waktu
        if (maxCooldown > 0)
        {
            cooldownOverlay.fillAmount = currentTimer / maxCooldown;
        }
        else
        {
            cooldownOverlay.fillAmount = 0;
        }

        // 2. Update Teks Angka
        if (currentTimer > 0)
        {
            // Tampilkan angka bulat ke atas (Mathf.Ceil), misal 0.1 jadi 1
            cooldownText.text = Mathf.Ceil(currentTimer).ToString();
            cooldownText.gameObject.SetActive(true);
        }
        else
        {
            // Hilangkan teks kalau sudah ready
            cooldownText.text = "";
            cooldownText.gameObject.SetActive(false);
        }
    }
}
