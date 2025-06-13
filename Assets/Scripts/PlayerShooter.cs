using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component for playing sounds
    public GameObject bulletPrefab; // Prefab for the bullet
    public Transform firePoint; // Point from where the bullet will be fired
    public Transform leftFirepoint; // Optional left fire point for double shooting
    public Transform rightFirepoint; // Optional right fire point for double shooting
    bool canDoubleShoot = false; // Flag to check if double shooting is enabled
    float powerUpCooldown = 5f; // Duration for which double shooting is enabled after picking up a power-up

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component attached to the player
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Check if the space key is pressed
        {
            if (canDoubleShoot) // Check if double shooting is enabled
            {
                DoubleShoot(); // Call the method to shoot bullets from both left and right fire points
            }
            else
            {
                Shoot(); // Call the method to shoot a single bullet
            }
        }

    }

    void Shoot()
    {
        // Instantiate a bullet at the fire point's position and rotation
        GameObject bullet = Instantiate(
            bulletPrefab, 
            firePoint.position, 
            firePoint.rotation
            );
        // Play the shooting sound if the audio source is available
        if (audioSource != null)
        {
            audioSource.Play(); // Play the shooting sound
        }

    }

    void DoubleShoot()
    {
        GameObject leftBullet = Instantiate(
            bulletPrefab, 
            leftFirepoint.position, 
            leftFirepoint.rotation
            );
        GameObject rightBullet = Instantiate(
            bulletPrefab, 
            rightFirepoint.position, 
            rightFirepoint.rotation
            );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUp"))
        {
            ActivateDoubleShoot(); // Activate double shooting when the player collides with a power-up
            Destroy(collision.gameObject); // Destroy the player object on collision with an enemy bullet
        }
    }

    void ActivateDoubleShoot()
    {
        canDoubleShoot = true; // Enable double shooting
        Invoke("DeactivateDoubleShoot", powerUpCooldown); // Schedule deactivation after the cooldown period
        // Optionally, you can add visual feedback or sound effects here
    }

    void DeactivateDoubleShoot()
    {         
        canDoubleShoot = false; // Disable double shooting
        // Optionally, you can add visual feedback or sound effects here
    }
}
