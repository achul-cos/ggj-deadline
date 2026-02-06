using UnityEngine;
using TMPro;

public class DamagePopup : MonoBehaviour
{
    [Header("References")]
    public TextMeshPro textMesh;

    [Header("Settings")]
    public float destroyTime = 1f;
    public Vector3 randomOffset = new Vector3(0.5f, 0.5f, 0f);

    public void Setup(float damageAmount)
    {
        textMesh.text = Mathf.Ceil(damageAmount).ToString();

        transform.position += new Vector3(
            Random.Range(-randomOffset.x, randomOffset.x),
            Random.Range(-randomOffset.y, randomOffset.y),
            0f
        );

        Destroy(gameObject, destroyTime);
    }

    private void LateUpdate()
    {
        if (transform.parent != null)
        {
            if (transform.parent.localScale.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        
    }
}
