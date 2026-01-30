using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpPower = 14f;

    [Header("Ground Detection")]
    public Transform groundCheck; // Titik kosong di kaki player
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer; // Layer apa yang dianggap tanah?

    private Rigidbody2D rb;
    private float horizontalInput;
    private bool isGrounded;
    private bool isFacingRight = true;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Baca Input Pemain
        horizontalInput = Input.GetAxisRaw("Horizontal"); // Nilai -1 (kiri), 0 (diam), 1 (kanan)

        // 2. Cek Lompat
        // Kita pakai overlapCircle untuk cek apakah kaki nyentuh layer 'Ground'
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }

        // 3. Flip Karakter (Kiri/Kanan)
        FlipSprite();
    }

    void FixedUpdate()
    {
        // 4. Gerakkan Karakter (Fisika sebaiknya di FixedUpdate)
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void FlipSprite()
    {
        // Jika gerak kanan tapi hadap kiri, atau sebaliknya -> Balik badan
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
    
    // Untuk visualisasi lingkaran ground check di Scene view (biar gampang debug)
    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
