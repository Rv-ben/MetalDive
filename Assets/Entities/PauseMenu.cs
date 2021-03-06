using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuUI;

    [SerializeField] public GameObject ConfirmationUI;

    public static bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        // if ESC is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed!!!!!!!!");
            Debug.Log("isPaused:" + isPaused.ToString());

            if (isPaused == false)
            {
                //pause
                PauseGame();
            }
            else
            {
                //resume
                ResumeGame();
            }
        }

        
    }

    /// <summary>
    /// 
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        Debug.Log("game is currently pause");

        // TODO: logic to save current state and pause time
        Time.timeScale = 0f;
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResumeGame()
    {
        Debug.Log("resuming");
        isPaused = false;
        //PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        
    }

    /// <summary>
    /// 
    /// </summary>
    public void QuitGame()
    {
        // prompt confirmation
        //ConfirmationUI.SetActive(true);

        Debug.Log("Quiting game from pause menu");
    }

    /// <summary>
    /// 
    /// </summary>
    public void ExitToDesktop()
    {
        // prompt confirmation
        //ConfirmationUI.SetActive(true);


        Debug.Log("Exiting to desktop from pause menu");
        Application.Quit();
    }

    public bool ConfirmExit()
    {


        // keep no on first button, have user move to yes to quit
        if (Input.GetKeyDown(KeyCode.Return))
        {
            return false;
        }

        else
        {
            return true;
        }
    }
}
