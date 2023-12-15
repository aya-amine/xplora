// Import necessary namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Declare the PauseMenu class
public class PauseMenu : MonoBehaviour
{
    // Boolean flag to track whether the game is paused
    public static bool GameIsPaused = false;

    // Reference to the pause menu UI GameObject
    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        // Check the specific button is pressed on the Oculus Touch controller -- if A is pressed
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            // Toggle between pausing and resuming the game
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Method to resume the game
    public void Resume()
    {
        // Deactivate the pause menu UI
        pauseMenuUI.SetActive(false);

        // Set the time scale to normal speed
        Time.timeScale = 1f;

        // Update the game pause status
        GameIsPaused = false;
    }

    // Method to pause the game
    void Pause()
    {
        // Activate the pause menu UI
        pauseMenuUI.SetActive(true);

        // Set the time scale to 0 to freeze the game
        Time.timeScale = 0f;

        // Update the game pause status
        GameIsPaused = true;
    }

    // Method to load the main menu scene
    public void LoadMenu()
    {
        // Set the time scale to normal speed
        Time.timeScale = 1f;

        // Load the scene with index 0 (assumed to be the main menu)
        SceneManager.LoadScene(0);
    }

    /*
    // Uncomment and implement these methods if you have specific floor loading functionality - using LevelLoader at the moment
    public void LoadFloor1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadFloor2()
    {
        SceneManager.LoadScene(2);
    }
    */

    // Method to quit the game
    public void QuitGame()
    {
        // Log a message indicating quitting (for debugging purposes)
        Debug.Log("Quit!");

        // Quit the application
        Application.Quit();
    }
}
