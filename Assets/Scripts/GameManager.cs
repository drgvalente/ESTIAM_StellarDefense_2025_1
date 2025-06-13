using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic; // Library that works with Lists
using UnityEngine.UI; // Library that works with UI elements

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab; // Prefab for the player
    int playerLives = 3; // Player's lives
    Vector3 playerStartPosition = new Vector3(0f, -4f, 0f); // Starting position for the player

    int score = 0; // Player's score
    public Text scoreText; // UI Text component to display the score
    public Text livesText; // UI Text component to display the player's lives
    public Text gameOverText; // UI Text component to display the game over message    
    bool isGameOver = false; // Flag to indicate if the game is over

    public GameObject enemyPrefab; // Prefab for the enemy
    int enemiesColums = 6;// Number of enemies per line
    int enemiesRows = 3; // number of "lines" of enemies

    bool mustChangeEnenmiesDirection = false; // Flag to indicate if enemies should change direction

    float enemySpawnHorizontalRange = 8f; // Horizontal range for spawning enemies
    float enemySpawnVerticalDistance = 1.2f; // Vertical distance between enemy rows

    public List<GameObject> enemies; // List to hold all spawned enemies

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        float spacingX = enemySpawnHorizontalRange / (enemiesColums - 1); // Calculate horizontal spacing between enemies
        float startX = -enemySpawnHorizontalRange / 2f; // Calculate starting X position for spawning enemies
        for (int row = 0; row < enemiesRows; row++)
        {
            for (int col = 0; col < enemiesColums; col++)
            {
                Vector3 spawnPos = new Vector3(
                    startX + col * spacingX, 
                    row * enemySpawnVerticalDistance + 2f, 
                    0f); // Calculate spawn position for each enemy
                GameObject enemy = Instantiate(
                    enemyPrefab, 
                    spawnPos, 
                    Quaternion.identity); // Instantiate the enemy prefab at the calculated position
                enemies.Add(enemy); // Add the instantiated enemy to the list
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0) // Check if there are no enemies left
        {
            Invoke("Victory", 2f); // Call the Victory method if all enemies are defeated
            return; // Exit the Update method
        }

        foreach (GameObject enemy in enemies) // Iterate through each enemy in the list
        {
            if (enemy != null) // Check if the enemy is not destroyed
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>(); // get the EnemyController component from this enemy instance
                enemy.transform.Translate(Vector3.right * Time.deltaTime * enemyController.moveSpeed); // Move the enemy down
                   
                if (enemy.transform.position.x < -enemySpawnHorizontalRange * 0.75f || 
                    enemy.transform.position.x > enemySpawnHorizontalRange * 0.75f)
                {
                    mustChangeEnenmiesDirection = true; // Set the flag to change direction
                }
            }
        }
        if (mustChangeEnenmiesDirection)
        {
            mustChangeEnenmiesDirection = false; // Reset the flag
            foreach (GameObject enemy in enemies)
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>(); // Get the EnemyController component from this enemy instance
                enemyController.moveSpeed *= -1; // Reverse the direction of each enemy
                enemy.transform.Translate(Vector3.down * enemyController.moveDownStep); // Move enemies down
            }
        }
    }

    public void AddScore(int points)
    {
        score += points; // Add points to the player's score
        scoreText.text = "Score: " + score; // Update the UI text to display the new score
    }

    public void RemoveLife()
    {
        playerLives--; // Decrease the player's lives by one
        livesText.text = "HP: " + playerLives; // Update the UI text to display the new number of lives
        foreach (GameObject enemy in enemies) // Iterate through all enemies
        {
             enemy.transform.Translate(Vector3.up * 2f); // Move each enemy up slightly
        }

        if (playerLives <= 0)
        {
            Invoke("GameOver", 2f); // Call the GameOver method if lives reach zero
        }
        else
        {
            Invoke("RespawnPlayer", 1.5f); // Call the RespawnPlayer method after a delay of 1 second
        }

    }

    public void GameOver()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public void RestartGame()
    {
        Debug.Log("Restarting game..."); // Log message for debugging
    }

    public void RespawnPlayer()
    {
        Debug.Log("Respawning player..."); // Log message for debugging
        // Implement player respawn logic here, such as instantiating a new player object at the starting position
        // For example:
        Instantiate(playerPrefab, playerStartPosition, Quaternion.identity);
    }

    public void Victory()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Victory");
    }
}
