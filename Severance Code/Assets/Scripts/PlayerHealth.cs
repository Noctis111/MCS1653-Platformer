using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;
    private bool isGameOver = false;

    public int numOfHearts;
    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    public GameObject gameOverPanel;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHeartUI();
        gameOverPanel.SetActive(false);
    }

    void Update()
    {
        if (transform.position.y < -20)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void LoseHealth(int damage)
    {
        if (isGameOver) return;

        currentHealth -= damage;
        Debug.Log("Player took " + damage + " damage. Current health: " + currentHealth);
        UpdateHeartUI();

        if (currentHealth <= 0)
        {
            Debug.Log("Player died!");
            GameOver();
        }
    }

    public void TakeBulletDamage()
    {
        LoseHealth(1);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeBulletDamage();
            Destroy(collision.gameObject);
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        isGameOver = true;
        gameOverPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Game Over!");
    }

    void UpdateHeartUI()
    {
        if (currentHealth > numOfHearts)
        {
            currentHealth = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            hearts[i].enabled = i < numOfHearts;
        }
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
