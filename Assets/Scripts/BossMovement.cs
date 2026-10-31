
using UnityEngine;
using System.Collections;

public class BossMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 3f;

    [Header("Shooting")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 1.5f;

    private Vector3 stopPosition;
    private Transform player;

    private bool isMoving = true;
    private bool initialized = false;

    public void Initialize(Vector3 targetPosition, Transform playerTarget)
    {
        stopPosition = targetPosition;
        player = playerTarget;
        initialized = true;
    }

    private void Update()
    {
        if (!initialized)
            return;

        if (isMoving)
        {
            MoveBoss();
        }
    }

    private void MoveBoss()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            stopPosition,
            moveSpeed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, stopPosition) < 0.01f)
        {
            transform.position = stopPosition;
            isMoving = false;

            StartCoroutine(ShootRoutine());
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (true)
        {
            Shoot();

            yield return new WaitForSeconds(fireRate);
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null ||
            player == null)
            return;

        // Calcula a direção entre o ponto de disparo e o jogador
        Vector2 direction =
            ((Vector2)player.position -
            (Vector2)firePoint.position).normalized;

        // Calcula o ângulo necessário para apontar para o jogador
        float angle = Mathf.Atan2(
            direction.y,
            direction.x
        ) * Mathf.Rad2Deg;

        // Instancia o projétil já orientado para o jogador
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            Quaternion.Euler(0f, 0f, angle)
        );

        BossProjectile projectileScript =
            projectile.GetComponent<BossProjectile>();

        if (projectileScript != null)
        {
            projectileScript.Initialize(direction);
        }

        // Ignorar colisão com o próprio Boss
        Collider2D projectileCollider =
            projectile.GetComponent<Collider2D>();

        Collider2D[] bossColliders =
            GetComponentsInChildren<Collider2D>();

        if (projectileCollider != null)
        {
            foreach (Collider2D bossCollider in bossColliders)
            {
                Physics2D.IgnoreCollision(
                    projectileCollider,
                    bossCollider
                );
            }
        }
    }


}
