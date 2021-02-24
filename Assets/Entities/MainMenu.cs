using System.Collections;
using System.Collections.Generic;
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

    /// <summary>
    /// Method called from on click event, waits for specified time
    /// to show loading canvas, then loads new scene
    /// </summary>
    public void PlayGame()
    {
        // show loading canvas
        //loadingCanvas.SetActive(true);

        //int next = SceneManager.GetActiveScene().buildIndex + 1;

        // start coroutine to wait for specified time
        StartCoroutine(LoadCanvas(waitTime));

        
        // Scene manager loads the following scene in queue (from Unity build settings)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   // ******************WORKS
    }

    
    /// <summary>
    /// 
    /// </summary>
    IEnumerator LoadCanvas(float waitTime)
    {
        float progress = 0;
        float count = 0;

        for (float i = 0; i < waitTime; ++i)
        {
            // calculate progress 
            progress = waitTime / (Mathf.Pow(waitTime, 2) / (i + 1));

            // update slider values with new progress
            this.UpdateSlider(progress);


            // if progress at MAX possible value for slider, break
            if (progress == 1)
            {
                Debug.Log("progress is 1");
                count = progress;
                break;
            }

            //yield return new WaitForSeconds(waitTime);


            //yield return null;
            //yield return new WaitForSeconds(time);
        }
        loadingCanvas.SetActive(true);
        yield return new WaitForSeconds(waitTime); // **************************WORKS
        //yield return new WaitUntil(() => count == 1);
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
    public void UpdateSlider(float progress)
    {

        Debug.Log("updating slide with progress: " + progress);

        // update slider value to new progress value
        progressBar.value = progress;
        // update slider text to new progress value as a percentage
        progressText.text = progress * 100f + "%";
        
        Debug.Log("progressBar value:text: " + progressBar.value + ":" + progressText.text);
    }

}