using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        // Load the game scene (assuming the game scene is named "Main")
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
    }

}
