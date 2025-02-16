using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Collider attackCollider;     // Set the box collider in the inspector
    public LayerMask enemyLayer;        // Layer for enemies

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Trigger attack on spacebar press
        {
            Attack();
        }
    }

    void Attack()
    {
        // Get all colliders within the attack area (the area defined by the box collider)
        Collider[] hitEnemies = Physics.OverlapBox(attackCollider.bounds.center, attackCollider.bounds.extents, Quaternion.identity, enemyLayer);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                // Ensure the enemy is within the bounds of the collider when the attack happens
                if (attackCollider.bounds.Contains(enemy.transform.position))
                {
                    Destroy(enemy.gameObject);  // Destroy the enemy
                    Debug.Log("Enemy Eliminated: " + enemy.name);
                }
            }
        }
    }
}
