using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.gravityScale = 0f;
        rb.linearVelocity = Vector2.right * speed;

        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            ObstacleHealth obstacle = other.GetComponentInParent<ObstacleHealth>();

            if (obstacle != null)
            {
                obstacle.TakeDamage(damage);
            }
        }
        if (other.CompareTag("Baloon"))
        {
            ObstacleHealth obstacle = other.GetComponentInParent<ObstacleHealth>();
            if (obstacle != null)
            {
                obstacle.TakeDamage(damage*10);
            }
        }
        Destroy(gameObject);
    }
}