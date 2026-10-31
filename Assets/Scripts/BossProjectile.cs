
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lifetime = 5f;

    private Rigidbody2D rb;
    private Vector2 moveDirection;
    private bool initialized = false;
    private bool hasHit = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction)
    {
        moveDirection = direction.normalized;
        initialized = true;

        // Rotaciona o sprite que originalmente aponta para cima.
        float angle = Mathf.Atan2(
            moveDirection.y,
            moveDirection.x
        ) * Mathf.Rad2Deg -180f;

        transform.rotation = Quaternion.Euler(
            0f, 0f, angle
        );
    }

    private void Start()
    {
        if (rb == null)
        {
            Debug.LogError("BossProjectile: Rigidbody2D não encontrado!");
            return;
        }

        if (!initialized)
        {
            Debug.LogWarning("BossProjectile: Direção não inicializada!");
            return;
        }

        rb.gravityScale = 0f;
        rb.linearVelocity = moveDirection * speed;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasHit)
            return;

        if (other.CompareTag("Player"))
        {
            hasHit = true;

            FindAnyObjectByType<GameManager>().GameOver();

            Destroy(gameObject);
        }
    }
}
