using UnityEngine;

public class Player : MonoBehaviour
{
    float speed = 5f; // Speed of the player movement
    float xLimit = 10f; // Horizontal boundary limit
    float yLimitUpper = 0f; // Vertical boundary limit
    float yLimitLower = -5f; // Vertical boundary limit

    GameManager gameManager; // Reference to the GameManager to access the list of enemies
    public GameObject explosion; // Reference to the explosion prefab (not used in this code, but can be used for visual effects)

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // Find the GameManager in the scene
        float halfPlayerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        float halfPlayerHeight = GetComponent<SpriteRenderer>().bounds.extents.y;
        float screenHalfWidth = Camera.main.orthographicSize * Camera.main.aspect;
        float screenHalfHeight = -5f;
        xLimit = screenHalfWidth - halfPlayerWidth - 1f; // Adjust the horizontal limit based on the camera size
        yLimitLower = screenHalfHeight + halfPlayerHeight; // Adjust the lower vertical limit based on the camera size
    }

    // Update is called once per frame
    void Update()
    {
        //make horizontal movement
        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    transform.Translate(Vector3.right * Time.deltaTime * speed);
        //}
        float inputX = Input.GetAxis("Horizontal");
        //Debug.Log(inputX);
        //transform.Translate(Vector3.right * inputX * Time.deltaTime * speed);
        float newXPosition = transform.position.x + inputX * Time.deltaTime * speed;
        // Clamp the new X position to stay within the limits
        newXPosition = Mathf.Clamp(newXPosition, -xLimit, xLimit);
        //make the vertical movement
        float inputY = Input.GetAxis("Vertical");
        //transform.Translate(Vector3.up * inputY * Time.deltaTime * speed);
        float newYPosition = transform.position.y + inputY * Time.deltaTime * speed;
        // Clamp the new Y position to stay within the limits
        newYPosition = Mathf.Clamp(newYPosition, yLimitLower, yLimitUpper);
        transform.position = new Vector3(newXPosition, newYPosition, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Handle collision with enemy
            // You can add logic here to handle player damage or game over
            gameManager.RemoveLife(); // Call the RemoveLife method from GameManager
            Instantiate(explosion, transform.position, Quaternion.identity); // Instantiate the explosion effect at the player's position
            Destroy(gameObject); // Destroy the player object on collision with an enemy
        }
    }
}
