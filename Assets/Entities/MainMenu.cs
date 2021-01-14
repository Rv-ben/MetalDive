using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadScene()

public class MainMenu : MonoBehaviour
{
    /**
     * Method called from on click event 
     */
    public void PlayGame()
    {
        // Scene manager loads the following scene in queue
        // Queue for scenes is in Unity's Build Settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /**
     * Method called from on click event 
     */
    public void QuitGame()
    {   
        // Debug comment in console
        Debug.Log("Quitting game...");
        // Shuts down application
        Application.Quit();
    }

}
