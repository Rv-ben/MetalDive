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

    private int i = 0;

    // storing string values of functions to be used in Invoke call
    private readonly string LoadSceneFunc = nameof(LoadNextScene);

    private readonly string UpdateSliderFunc = nameof(UpdateSlider);

    private float waitTime = 10;

    public GameObject loadingCanvas;

    public Slider progressBar;

    public TMP_Text progressText;

    public void Start()
    {
        loadingCanvas = GameObject.Find("LoadingScreen");
        loadingCanvas.SetActive(false);
    }

    /// <summary>
    /// Method called from on click event, waits for specified time
    /// to show loading canvas, then loads new scene
    /// </summary>
    public void PlayGame()
    {
        // show loading canvas with updating slider
        DisplayLoadingCanvas();


        // after waiting for specified time, load play game scene
        Invoke(LoadSceneFunc, waitTime);
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
        //Debug.Log("i: " + i);
        // calculate progress 
        float progress = 10 / (Mathf.Pow(10, 2) / (i + 1));

        // update slider value to new progress value
        progressBar.value = progress;

        // update slider text to new progress value as a percentage
        progressText.text = progress * 100f + "%";        
        
        //Debug.Log("progressBar: " + progressBar.value + ":" + progressText.text);
        i++;
    }

    /// <summary>
    /// Activates loading canvas game object
    /// side effect: invokes updateslider function after 1 second
    /// </summary>
    public void DisplayLoadingCanvas()
    {
        loadingCanvas.SetActive(true);

        Invoke(UpdateSliderFunc, 0.5f);

        Invoke(UpdateSliderFunc, 1.5f);

        Invoke(UpdateSliderFunc, 2.5f);

        Invoke(UpdateSliderFunc, 3.5f);

        Invoke(UpdateSliderFunc, 4.5f);

        Invoke(UpdateSliderFunc, 5.5f);

        Invoke(UpdateSliderFunc, 6.5f);

        Invoke(UpdateSliderFunc, 7.5f);

        Invoke(UpdateSliderFunc, 8.5f);

        Invoke(UpdateSliderFunc, 9.5f);
    }

    /// <summary>
    /// This function loads the next scene in the build settings queue
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}