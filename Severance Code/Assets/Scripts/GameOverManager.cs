using UnityEngine;
using UnityEngine.UI;  // For UI elements
using UnityEngine.SceneManagement; // For scene management

public class GameOverManager : MonoBehaviour
{
    // UI elements for the game over screen
    public GameObject gameOverPanel;  // Panel to show when the game is over
    public Button retryButton;        // Button to retry the scene
    public Button mainMenuButton;     // Button to go to the main menu
    public Button quitButton;         // Button to quit the game

    // Reference to the PlayerHealth script
    private PlayerHealth playerHealth;

    void Start()
    {
        // Assuming the PlayerHealth script is attached to the same object as the GameOverManager
        playerHealth = FindObjectOfType<PlayerHealth>();

        // Make sure the game over panel is hidden at the start
        gameOverPanel.SetActive(false);

        // Add listeners to buttons
        retryButton.onClick.AddListener(Retry);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        quitButton.onClick.AddListener(QuitGame);

        // Optionally disable buttons at the start
        retryButton.interactable = true;
        mainMenuButton.interactable = true;
        quitButton.interactable = true;
    }

    void Update()
    {
        // Check if PlayerHealth has triggered GameOver
        if (playerHealth != null && playerHealth.IsGameOver())
        {
            DisplayGameOverUI();
        }
    }

    void DisplayGameOverUI()
    {
        // Stop the game and display the game over UI
        Time.timeScale = 0;  // Pause the game
        gameOverPanel.SetActive(true);  // Show the Game Over panel
        Cursor.lockState = CursorLockMode.None;  // Unlock cursor
        Cursor.visible = true;  // Make cursor visible
    }

    void Retry()
    {
        // Reload the current scene to retry
        Time.timeScale = 1;  // Resume the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GoToMainMenu()
    {
        // Load the Main Menu scene
        Time.timeScale = 1;  // Resume the game
        SceneManager.LoadScene("MainMenu");  // Replace with your actual main menu scene name
    }

    void QuitGame()
    {
        // Quit the game (works in a built version)
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
