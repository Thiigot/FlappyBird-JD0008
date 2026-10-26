
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform firePoint;

    [Header("Shooting")]
    [SerializeField] private float fireRate = 1f;

    private float nextFireTime;

    private void Update()
    {
        bool shooting = false;

        if (Mouse.current != null &&
            Mouse.current.leftButton.isPressed)
        {
            shooting = true;
        }

        if (Keyboard.current != null &&
            Keyboard.current.enterKey.isPressed)
        {
            shooting = true;
        }

        if (shooting && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null || firePoint == null)
            return;

        Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );
    }
}
