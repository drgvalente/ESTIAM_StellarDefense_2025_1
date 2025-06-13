using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void BackToMenu()
    {
        // Load the main menu scene (assuming the main menu scene is named "MainMenu")
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }
}
