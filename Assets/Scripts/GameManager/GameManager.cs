using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] public DungeonCreator dungeonCreator;

    [SerializeField] public Spawner spawner;

    [SerializeField] public GameObject weaponView;

    [SerializeField] public GameObject weaponSwitchMenu;

    [SerializeField] public Canvas pauseMenu;

    [SerializeField] public Canvas confirmationUI;
    public EnemySpawner enemySpawner;

    private WeaponSwitchMenu weaponSwitchMenuScript;

    private GameObject weaponPanel;

    private ProceedNextLevel proceedNextLevel;

    private ElevatorSwitch elevatorSwitch;

    private GameObject transitionCanvas;

    private ElevatorTransition elevatorTransition;

    private bool completeLevel;

    private PlayerMovement playerMovement;

    public bool isPaused = false;

    public ObstacleGeneration obstacleGenerator;

    public Player player1;

    public int levelComplete;

    public int currentBitcoin;


    // Start is called before the first frame update
    void Awake()
    {
        currentBitcoin = 0;
        levelComplete = 0;
        spawner.LoadPrefabs();
        completeLevel = false;
        weaponPanel = GameObject.Find("WSPanel");
        weaponPanel.SetActive(isPaused);
        weaponView.SetActive(isPaused);
        weaponSwitchMenu.SetActive(isPaused);
        pauseMenu.gameObject.SetActive(isPaused);
        confirmationUI.gameObject.SetActive(isPaused);
        transitionCanvas = GameObject.Find("ElevatorCanvas");
        transitionCanvas.SetActive(false);
        elevatorTransition = transitionCanvas.GetComponent<ElevatorTransition>();

        Debug.Log(transitionCanvas);
        Debug.Log(elevatorTransition);

        weaponSwitchMenuScript = weaponSwitchMenu.GetComponent<WeaponSwitchMenu>();

        proceedNextLevel = this.GetComponent<ProceedNextLevel>();

        GenerateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                Screen.lockCursor = false;
                weaponPanel.SetActive(!isPaused);
                weaponView.SetActive(!isPaused);
                weaponSwitchMenu.SetActive(!isPaused);

                if (isPaused)
                {
                    Debug.Log(weaponSwitchMenuScript.currentWeaponIndex);
                    var weaponEnum = weaponSwitchMenuScript.currentWeapon();
                    SwitchPlayerWeapon(weaponEnum);
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }


            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                Screen.lockCursor = false;

                this.pauseMenu.gameObject.SetActive(!isPaused);
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
        if (elevatorSwitch.playerEnter) // complete level - clear the scene
        {
            PauseGame();
            transitionCanvas.SetActive(true);
            proceedNextLevel.DestroyEnvironment();
            elevatorSwitch.playerEnter = false;
            completeLevel = true;
            ResumeGame();
        }
        else if (completeLevel) // complete level - generate the next level
        {
            PauseGame();
            elevatorTransition.DeactivateCanvas();
            GenerateLevel();
            ResumeGame();
            
        }
        else if (playerMovement.isDead) // player dead
        {
            SceneManager.LoadScene("GameOver");
            Debug.Log("END GAME");
            playerMovement.isDead = false;
            SceneManager.LoadScene("GameOver");
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        
    }

    public void SwitchPlayerWeapon (WeaponEnum weaponEnum)
    {
        this.player1.SwitchWeapon(weaponEnum);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
    private void GenerateLevel() 
    {
        completeLevel = false;
        var rooms = dungeonCreator.CreateDungeon();
        var obstacleGenerator = new ObstacleGeneration(rooms, spawner);
        player1 = new Player(rooms[0], spawner);
        enemySpawner = new EnemySpawner(spawner, rooms, this.levelComplete + 1);
        enemySpawner.SpawnEnemies();
        elevatorSwitch = FindObjectOfType<ElevatorSwitch>();
        playerMovement = player1.prefab.GetComponent<PlayerMovement>();
        levelComplete++;
    }
}
