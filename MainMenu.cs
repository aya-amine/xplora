// Import necessary namespaces
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Declare the MainMenu class
public class MainMenu : MonoBehaviour
{
    // Method to go back to the main menu scene
    public void BackMainMenu()
    {
        // Load the scene with index 0 (assumed to be the main menu)
        SceneManager.LoadScene(0);
    }

    /*
    // Uncomment and implement these methods if you have specific floor loading functionality - using levelloader at the moment
    public void PlayFloor1()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayFloor2()
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
