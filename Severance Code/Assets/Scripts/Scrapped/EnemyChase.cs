/*
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;             // Reference to the player
    public float chaseSpeed = 5f;         // Speed at which the enemy chases the player
    public float detectionRadius = 10f;   // Radius of the sphere collider for player detection

    private bool isChasing = false;       // Whether the enemy is currently chasing the player
    private bool isAttacking = false;     // Whether the enemy is in attacking state
    private Vector3 directionToPlayer;    // The direction the enemy should move towards

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = true;
            player = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            // Prevent the Punchies object from being destroyed by Bullet tag
            if (other.gameObject.name == "Punchies")
            {
                return;  // Don't destroy the Punchies GameObject
            }
        }

        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = true;
            // Inform PlayerHealth script that the player is in contact with the Punchies object
            player.GetComponent<PlayerHealth>().SetPunchiesContact(true);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isAttacking = false;
            // Stop damage when player exits Punchies' box collider
            player.GetComponent<PlayerHealth>().SetPunchiesContact(false);
        }
    }

    void Update()
    {
        if (isChasing && !isAttacking)
        {
            // Update the direction to move towards the player
            directionToPlayer = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;
            directionToPlayer.Normalize();

            // Move the enemy toward the player in the X and Z axes only
            transform.position += directionToPlayer * chaseSpeed * Time.deltaTime;
        }

        if (isAttacking)
        {
            // If the enemy is attacking, it can stop moving (you can add attack animations here)
        }
    }

    public void StopAttacking()
    {
        isAttacking = false;
    }
}
*/