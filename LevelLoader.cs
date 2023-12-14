// Import necessary namespaces
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Declare the LevelLoader class
public class LevelLoader : MonoBehaviour
{
    // Public variables accessible from the Unity Editor
    public GameObject loadingScreen; // Reference to the loading screen UI GameObject
    public Slider slider; // Reference to the slider UI 

    // Public method to initiate the asynchronous loading of a scene
    //Load scene while synchronously showing another screen
    public void LoadLevel(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    // Coroutine (for functions that can be paused and resumed/using multiple frames) for asynchronous scene loading with progress tracking
    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        // Start the asynchronous loading operation for the specified scene
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Activate the loading screen UI
        loadingScreen.SetActive(true);

        // Continue looping until the asynchronous operation is complete
        while (!operation.isDone)
        {
            // Calculate the loading progress as a value between 0 and 1
            float progress = Mathf.Clamp01(operation.progress / .9f);

            // Update the slider value to reflect the loading progress
            slider.value = progress;

            // Yield control back to the Unity engine for one frame
            yield return null; // wait a frame
        }
    }
}
