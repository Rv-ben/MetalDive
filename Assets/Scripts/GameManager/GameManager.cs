using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public DungeonCreator dungeonCreator;

    [SerializeField] public Spawner spawner;

    [SerializeField] public GameObject weaponView;

    // [SerializeField] public WeaponSwitchMenu weaponSwitchMenu;

    private GameObject weaponSwitchMenu;

    public bool isPaused = false;

    public ObstacleGeneration obstacleGenerator;


    // Start is called before the first frame update
    void Awake()
    {
        spawner.LoadPrefabs();
        weaponSwitchMenu = GameObject.Find("Panel");
        weaponSwitchMenu.SetActive(isPaused);
        var rooms = dungeonCreator.CreateDungeon();
        var obstacleGenerator = new ObstacleGeneration(rooms, spawner);
        var player = new Player(rooms[0], spawner);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Screen.lockCursor = false;
            weaponSwitchMenu.SetActive(!isPaused);
            weaponView.SetActive(!isPaused);

            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        
    }
}
