using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;  // LoadScene()
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    // specified time from Unity
    [SerializeField] private float waitTime;

    public GameObject loadingCanvas;

    public Slider progressBar;

    public TMP_Text progressText;

    public int i = 0;
    /// <summary>
    /// Method called from on click event, waits for specified time
    /// to show loading canvas, then loads new scene
    /// </summary>
    public void PlayGame()
    {
        // show loading canvas with updating slider
        DisplayLoadingCanvas();


        // after waiting for specified time, load play game scene
        Invoke("LoadNextScene", waitTime);
    }
   
    /// <summary>   
    /// Method called from on click event 
    /// </summary>
    public void QuitGame()
    {   
        // Debug comment in console
        Debug.Log("Quitting game...");

        // Shuts down application
        Application.Quit();
    }


    /// <summary>
    /// update progress bar value and text
    /// </summary>
    public void UpdateSlider()
    {
        Debug.Log("i: " + i);
        // calculate progress 
        float progress = 10 / (Mathf.Pow(10, 2) / (i + 1));

        // update slider value to new progress value
        progressBar.value = progress;

        // update slider text to new progress value as a percentage
        progressText.text = progress * 100f + "%";        
        
        Debug.Log("progressBar: " + progressBar.value + ":" + progressText.text);
        i++;
    }

    /// <summary>
    /// Activates loading canvas game object
    /// side effect: invokes updateslider function after 1 second
    /// </summary>
    public void DisplayLoadingCanvas()
    {
        loadingCanvas.SetActive(true);

        for (int i = 0; i < 10; ++i)
        {
            Invoke("UpdateSlider", 1.0f);
        }
    }

    /// <summary>
    /// Function invoked from PlayGame function after <waitTime> seconds 
    /// </summary>
    public void LoadNextScene()
    {
        // Scene manager loads the following scene in queue (from Unity build settings)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}