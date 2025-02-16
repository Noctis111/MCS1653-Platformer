using UnityEngine;
using UnityEngine.UI;  // For UI elements like Image and Sprite
using UnityEngine.SceneManagement; // For restarting the scene (game over)

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;  // Player's maximum health
    private int currentHealth; // Player's current health

    //private bool isInContactWithPunchies = false;  // To track Punchies contact
    private bool isGameOver = false;  // To track if the game is over

    // Heart UI variables
    public int numOfHearts;  // Total number of hearts (UI)
    public Image[] hearts;  // Array of heart UI elements
    public Sprite fullHeart;  // Full heart sprite
    public Sprite emptyHeart; // Empty heart sprite

    // Game Over UI
    public GameObject gameOverPanel;  // The Game Over panel (UI)

    void Start()
    {
        currentHealth = maxHealth;  // Initialize health to max at start
        UpdateHeartUI();  // Initialize the heart UI
        gameOverPanel.SetActive(false);  // Hide Game Over panel at the start
    }

    public void LoseHealth(int damage)
    {
        if (isGameOver) return;  // Prevent health loss if game is over

        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);  // Debug message when player takes damage
        UpdateHeartUI();  // Update the hearts UI with current health

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");  // Debug message when player dies
            GameOver();  // Call the Game Over function when the player dies
        }
    }

    public void TakeBulletDamage()
    {
        // Call this function when a bullet hits the player
        LoseHealth(1);  // Player loses 1 health on bullet contact
    }

    void OnCollisionEnter(Collision collision)
    {
        // When the player is hit by a bullet
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeBulletDamage();
            Destroy(collision.gameObject);  // Destroy the bullet after impact
        }
    }

    /*
    public void SetPunchiesContact(bool isInContact)
    {
        isInContactWithPunchies = isInContact;
    }

    
    
    void Update()
    {
        if (isInContactWithPunchies)
        {
            // If in contact with Punchies, continuously lose 1 health per second
            LoseHealth(1);
        }
    }
    */

    void GameOver()
    {
        // Stop all game activities and show game over screen (or restart scene)
        Time.timeScale = 0; // Pause the game
        isGameOver = true;  // Set game over flag
        gameOverPanel.SetActive(true);  // Show the Game Over UI panel
        Cursor.lockState = CursorLockMode.None;  // Unlock cursor
        Cursor.visible = true;  // Make cursor visible
        Debug.Log("Game Over!");  // Display Game Over in the console
    }

    // Update the heart UI based on current health
    void UpdateHeartUI()
    {
        // Ensure health does not exceed the number of hearts available
        if (currentHealth > numOfHearts)
        {
            currentHealth = numOfHearts;
        }

        // Loop through all hearts and update their state
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;  // Set the full heart sprite
            }
            else
            {
                hearts[i].sprite = emptyHeart;  // Set the empty heart sprite
            }

            // Enable hearts that are part of the player's UI
            if (i < numOfHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
