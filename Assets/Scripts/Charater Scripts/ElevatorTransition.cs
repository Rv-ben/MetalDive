using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElevatorTransition : MonoBehaviour
{
    public TMP_Text secondsValue;

    private int seconds = 3;

    private string UpdateSecondsFunc = nameof(UpdateSeconds);

    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        this.canvas = this.gameObject.GetComponentInParent<Canvas>().gameObject;
        StartCountdown();
        
        
    }

    public void UpdateSeconds()
    {
        if (seconds >= 0)
        {
            secondsValue.text = seconds + " seconds";
            seconds--;
        }
        if (seconds < 0)
        {
            DeactivateCanvas();
        }
    }

    public void StartCountdown()
    {
        Invoke(UpdateSecondsFunc, 0.5f);
        Invoke(UpdateSecondsFunc, 1.5f);
        Invoke(UpdateSecondsFunc, 2.5f);
    }

    public void DeactivateCanvas()
    {
        this.canvas.SetActive(false);
    }
}
