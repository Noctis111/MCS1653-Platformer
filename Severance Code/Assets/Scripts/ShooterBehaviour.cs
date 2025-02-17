using System.Collections;
using UnityEngine;

public class ShooterBehaviour : MonoBehaviour
{
    public GameObject bulletPrefab;       // Bullet prefab to instantiate
    public Transform firePoint;           // The position where bullets spawn
    public float fireRate = 1f;           // How often bullets are fired (bullets per second)
    public float bulletSpeed = 10f;       // Speed of the bullet
    public float bulletLifetime = 3f;     // Lifetime of the bullet before despawning
    public Collider sphereCollider;      // Reference to the sphere collider
    public float rotationSpeed = 5f;      // Speed at which the Shooter rotates to face the player

    private bool isAggro = false;         // Whether the enemy is in "aggro" state
    private GameObject player;            // Reference to the player
    private Vector3 directionToPlayer;    // Direction the bullet should move in
    private Coroutine shootingCoroutine;  // Coroutine to handle continuous shooting
    private bool isShooting = false;      // Flag to track shooting state
    private bool firstBulletShot = false; // Flag to track if the first bullet has been shot

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject; // Assign player reference
            isAggro = true;             // Set aggro state to true
            if (!isShooting) // Check if already shooting
            {
                isShooting = true; // Set shooting flag
                shootingCoroutine = StartCoroutine(ShootRoutine());
            }
        }
    }

    private IEnumerator ShootRoutine()
    {
        if (!firstBulletShot)
        {
            yield return new WaitForSeconds(1f);  // Wait for 2 seconds before the first shot
            firstBulletShot = true;
        }

        while (isAggro)
        {
            Shoot(); // Call Shoot once per fire rate cycle
            yield return new WaitForSeconds(1f / fireRate); // Delay based on fire rate
        }
    }

    private void Shoot()
    {
        if (player != null && isAggro)
        {
            directionToPlayer = (player.transform.position - firePoint.position).normalized;
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.velocity = directionToPlayer * bulletSpeed;
            }

            StartCoroutine(DestroyBulletAfterTime(bullet, bulletLifetime));
        }
    }

    private IEnumerator DestroyBulletAfterTime(GameObject bullet, float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(bullet);  // Destroy the bullet after the specified time
    }

    private void Update()
    {
        if (player != null && isAggro)
        {
            // Calculate direction to player
            Vector3 directionToPlayer = player.transform.position - transform.position;

            // Calculate the angle to rotate the Shooter towards the player
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

            // Invert the angle to face the opposite direction (flip the Z-axis)
            angle += 180f;  // Flip the Z-axis by adding 180 degrees

            // Smoothly rotate the Shooter over time to face the player
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    private void OnDestroy()
    {
        StopShooting();
    }

    private void StopShooting()
    {
        isAggro = false;
        if (shootingCoroutine != null)
        {
            StopCoroutine(shootingCoroutine);
        }
        isShooting = false;  // Ensure no more bullets are spawned
    }
}
