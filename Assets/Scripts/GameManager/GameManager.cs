using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public DungeonCreator dungeonCreator;

    [SerializeField] public Spawner spawner;

    [SerializeField] public GameObject weaponView;

    [SerializeField] public GameObject weaponSwitchMenu;

    public EnemySpawner enemySpawner;

    private WeaponSwitchMenu weaponSwitchMenuScript;

    private GameObject panel;

    private ProceedNextLevel proceedNextLevel;

    private ElevatorSwitch elevatorSwitch;

    private bool completeLevel;

    private PlayerMovement playerMovement;

    public bool isPaused = false;

    public ObstacleGeneration obstacleGenerator;

    public Player player1;


    // Start is called before the first frame update
    void Awake()
    {
        spawner.LoadPrefabs();

        panel = GameObject.Find("Panel");
        panel.SetActive(isPaused);

        weaponSwitchMenuScript = weaponSwitchMenu.GetComponent<WeaponSwitchMenu>();
        Debug.Log(weaponSwitchMenuScript);

        proceedNextLevel = this.GetComponent<ProceedNextLevel>();

        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Screen.lockCursor = false;
            panel.SetActive(!isPaused);
            weaponView.SetActive(!isPaused);

            if (isPaused)
            {
                var weaponEnum = weaponSwitchMenuScript.currentWeapon();
                SwitchPlayerWeapon(weaponEnum);
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (elevatorSwitch.playerEnter) // complete level - clear the scene
        {
            proceedNextLevel.DestroyEnvironment();
            elevatorSwitch.playerEnter = false;
            completeLevel = true;
        }
        else if (completeLevel) // complete level - generate the next level
        {
            GenerateLevel();
        }
        else if (playerMovement.isDead) // player dead
        {
            Debug.Log("END GAME");
            proceedNextLevel.DestroyEnvironment();
            playerMovement.isDead = false;
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

    public void SwitchPlayerWeapon (WeaponEnum weaponEnum)
    {
        this.player1.SwitchWeapon(weaponEnum);
    }

    private void GenerateLevel() 
    {
        completeLevel = false;
        var rooms = dungeonCreator.CreateDungeon();
        var obstacleGenerator = new ObstacleGeneration(rooms, spawner);
        player1 = new Player(rooms[0], spawner);
        elevatorSwitch = FindObjectOfType<ElevatorSwitch>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
}
