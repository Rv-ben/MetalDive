using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class <c>PauseMenu</c>
/// </summary>
public class PauseMenu : MonoBehaviour
{

    public GameObject PauseMenuUI;

    public GameObject ConfirmationUI;

    //public static bool isPaused = false;

    public bool pauseStatus;



    // Update is called once per frame
    void Update()
    {
        // if ESC is pressed
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("Escape key pressed");

            //Debug.Log("pauseStatus: " + pauseStatus.ToString());
            pauseStatus = true;

            if (pauseStatus)
            {
                PauseGame();
            }
        }*/
    }

    /// <summary>
    /// Activates Pause Menu Game object and sets timeScale at 0
    /// </summary>
    public void PauseGame()
    {
        Debug.Log("game is currently paused");
        //PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resumes game by setting timeScale to 1 and deactivates menus
    /// </summary>
    public void ResumeGame()
    {
        Debug.Log("resuming");
        pauseStatus = false;
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        ConfirmationUI.SetActive(false);
    }

    
    /// <summary>
    /// loads a confirmation canvas
    /// </summary>
    public void QuitGame()
    {
        // prompt confirmation
        ConfirmationUI.SetActive(true);

        Debug.Log("Quiting game from pause menu");
    }

    /// <summary>
    /// Prompts confirmation to exit and closes application
    /// </summary>
    public void ExitToDesktop()
    {
        // prompt confirmation
        ConfirmationUI.SetActive(true);

        Debug.Log("Exiting to desktop from pause menu");
        Application.Quit();
    }
}
