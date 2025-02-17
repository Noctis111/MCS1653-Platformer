using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public GameObject victoryScreen;
    public GameObject gameOverScreen;
    //public BoxCollider endpointCollider; // BoxCollider representing the endpoint
    public GameObject player; // The player object
    /*
    //private bool gameWon = false;
    private void Update()
    {
        // Only check for victory if the game has not been won
        if (!gameWon)
        {
            CheckVictoryCondition();
        }
    }
    
    // Check if the player has reached the endpoint
    void CheckVictoryCondition()
    {
        // Check if the player has collided with the endpoint
        if (endpointCollider.bounds.Contains(player.transform.position))
        {
            Debug.Log("Player is at the endpoint.");  // Log when player is at the endpoint
            ShowVictoryScreen();
        }
    }
    
    // Show the victory screen
    void ShowVictoryScreen()
    {
        victoryScreen.SetActive(true);
        gameWon = true; // Prevent further checks after victory
        Time.timeScale = 0f; // Freeze the game when the victory screen is shown
    }
    */
    // Call this method when the player dies or fails
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        // Optionally, you can pause the game here as well
        Time.timeScale = 0f; // Freeze the game on game over
    }

    // Restart the current scene when the player chooses to restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
