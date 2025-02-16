/*
using UnityEngine;

public class Punchies : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        // Make sure Punchies does not get destroyed when hitting the player's capsule collider
        if (other.gameObject.CompareTag("Player"))
        {
            // Optionally log a debug message to confirm this part works
            Debug.Log("Punchies collided with the player.");
        }
    }

    // Only destroy Punchies if its parent Brawl is destroyed
    public void DestroyPunchiesIfBrawlDestroyed()
    {
        if (transform.parent == null) // If Brawl (the parent) is destroyed, then destroy Punchies.
        {
            Destroy(gameObject);
        }
    }
}
*/