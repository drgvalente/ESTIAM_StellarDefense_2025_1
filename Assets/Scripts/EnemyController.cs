using UnityEngine;

public class EnemyController : MonoBehaviour
{
    float hp = 3f; // Health points of the enemy
    public float moveSpeed = 2.5f; // Speed of the enemy
    public float moveDownStep = 0.2f; // Distance to move down when reaching the edge of the screen
    float moveSpeedIncreaseFactor = 1.2f; // Factor by which the speed increases when an enemy is destroyed

    GameManager gameManager; // Reference to the GameManager to access the list of enemies

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        hp -= damage; // Reduce the enemy's health by the damage amount
        if (hp <= 0f) // Check if the enemy's health is less than or equal to zero
        {
            gameManager.enemies.Remove(gameObject); // Remove the enemy from the GameManager's list of enemies
            foreach (GameObject enemy in gameManager.enemies) // Iterate through all remaining enemies
            {
                EnemyController enemyController = enemy.GetComponent<EnemyController>(); // Get the EnemyController component from each enemy
                enemyController.IncreaseSpeed(); // Increase the speed of each remaining enemy
            }
            gameManager.AddScore(1); // Add score to the GameManager
            Destroy(gameObject); // Destroy the enemy
        }
    }

    public void IncreaseSpeed()
    {
        moveSpeed *= moveSpeedIncreaseFactor; // Increase the enemy's speed by the defined factor
    }
}
