
using UnityEngine;

public class ObstacleHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHP = 3;

    private int currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }

    public void TakeDamage(int damage)
    {
        if (currentHP <= 0 || damage <= 0)
            return;

        currentHP -= damage;

        currentHP = Mathf.Max(currentHP, 0);

        if (currentHP <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        FindAnyObjectByType<GameManager>().PointUP();
        Destroy(gameObject);
    }
}
