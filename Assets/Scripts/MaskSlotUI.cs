using UnityEngine;
using UnityEngine.UI;

public class MaskSlotUI : MonoBehaviour
{
    [Header("UI Components")]
    public GameObject borderActive;    // Drag objek Border_Water ke sini
    public Image cooldownOverlay;      // Drag CooldownOverlay
    public Text cooldownText;          // Drag CooldownText
    public Image maskIcon;             // Drag Mask_Slot_Water (Image)

    public void SetActiveState(bool isActive)
    {
        if (borderActive != null)
            borderActive.SetActive(isActive);
    }

    public void UpdateCooldownVisual(float currentTimer, float maxTime)
    {
        // 1. Overlay Lingkaran
        if (maxTime > 0)
            cooldownOverlay.fillAmount = currentTimer / maxTime;
        else
            cooldownOverlay.fillAmount = 0;

        // 2. Teks Angka
        if (currentTimer > 0)
        {
            cooldownText.text = Mathf.Ceil(currentTimer).ToString();
            cooldownText.gameObject.SetActive(true);
        }
        else
        {
            cooldownText.text = "";
            cooldownText.gameObject.SetActive(false);
        }
    }
}
