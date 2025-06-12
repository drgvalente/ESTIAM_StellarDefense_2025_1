using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab for the bullet
    public Transform firePoint; // Point from where the bullet will be fired
    public Transform leftFirepoint; // Optional left fire point for double shooting
    public Transform rightFirepoint; // Optional right fire point for double shooting

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Check if the space key is pressed
        {
            Shoot(); // Call the Shoot method
        }
        if (Input.GetKeyDown(KeyCode.X)) // Check if the left shift key is pressed
        {
            DoubleShoot(); // Call the DoubleShoot method
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
}
