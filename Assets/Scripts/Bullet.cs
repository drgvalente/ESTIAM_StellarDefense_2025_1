using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 10f; // Speed of the bullet

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 5f); // Destroy the bullet after 5 seconds to prevent memory leaks

        Rigidbody2D rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        //rb.linearVelocity = new Vector2(0, speed); // Set the bullet's velocity to move upwards
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
        //if (transform.position.y > 10f) // Check if the bullet has moved out of bounds
        //{
        //    Destroy(gameObject); // Destroy the bullet
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy")) // Check if the bullet collides with an enemy
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>(); // Get the EnemyController component from the enemy
            enemy.TakeDamage(1f); // Call the TakeDamage method on the enemy with 1 damage
            Destroy(gameObject); // Destroy the bullet
        }
    }
}
