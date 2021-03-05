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

    public bool isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused == true)
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
        Debug.Log("pausing");

        PauseMenuUI.SetActive(true);
        // 3 options
    }

    /// <summary>
    /// 
    /// </summary>
    public void ResumeGame()
    {
        Debug.Log("resuming");
        PauseMenuUI.SetActive(false);

        
    }

    /// <summary>
    /// 
    /// </summary>
    public void QuitGame()
    {
        // prompt confirmation
        ConfirmationUI.SetActive(true);

        Debug.Log("Exiting to desktop from pause menu");
    }

    /// <summary>
    /// 
    /// </summary>
    public void ExitToDesktop()
    {
        // prompt confirmation
        ConfirmationUI.SetActive(true);


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
