using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace to manage scenes
public class EndGame : MonoBehaviour
{
    public void BackToMenu()
    {
        // Load the main menu scene (assuming the main menu scene is named "MainMenu")
        SceneManager.LoadScene("MainMenu");
    }
}
