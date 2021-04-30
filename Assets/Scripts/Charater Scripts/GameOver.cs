using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public ElevatorSwitch switcher;
    public DeathHandler death;

    // Start is called before the first frame update
    void Start()
    {
        switcher = GameObject.FindGameObjectWithTag("Elevator").GetComponent<ElevatorSwitch>();
        death = GameObject.FindGameObjectWithTag("Player").GetComponent<DeathHandler>();
    }

    void Update()
    {
        if (switcher.playerEnter || death.gameOver)
        {
            // Needs to hardcode the game over scene.  Currently, this is not the case.
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
