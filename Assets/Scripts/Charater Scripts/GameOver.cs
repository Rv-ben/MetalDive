using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public ElevatorSwitch switcher;
    public DeathHandler death;
    public bool end;

    // Start is called before the first frame update
    void Start()
    {
        switcher = GameObject.FindGameObjectWithTag("Elevator").GetComponent<ElevatorSwitch>();
        death = GameObject.FindGameObjectWithTag("Player").GetComponent<DeathHandler>();
        end = false;
    }

    void Update()
    {
        if (switcher.playerEnter || death.gameOver )
        {
            end = true;
        }
    }
}
