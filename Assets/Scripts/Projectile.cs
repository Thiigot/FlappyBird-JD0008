using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 1f;
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
            Destroy(gameObject);
        }
        else if (other.CompareTag("Boss"))
        {
            BossHealth boss = other.GetComponent<BossHealth>();
            if(boss == null)
            {
                Debug.Log("Null!");
            }
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}