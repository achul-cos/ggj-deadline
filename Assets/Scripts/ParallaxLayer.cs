using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [Tooltip("Semakin kecil (0-1) = bergerak lebih lambat (lebih jauh). 1 = ikut kamera 1:1")]
    public float parallaxMultiplier = 0.5f;

    private Transform cam;
    private Vector3 lastCamPos;

    void Start()
    {
        cam = Camera.main.transform;
        lastCamPos = cam.position;
    }

    void LateUpdate()
    {
        Vector3 camDelta = cam.position - lastCamPos;

        // Hanya ikut gerak horizontal (X). Kalau mau juga sedikit vertikal, tambah Y.
        transform.position += new Vector3(camDelta.x * parallaxMultiplier, 0f, 0f);

        lastCamPos = cam.position;
    }
}
