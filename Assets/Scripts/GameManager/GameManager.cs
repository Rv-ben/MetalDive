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

    private WeaponSwitchMenu weaponSwitchMenuScript;

    private GameObject weaponPanel;

    public bool isPaused = false;

    public ObstacleGeneration obstacleGenerator;

    public Player player1;


    // Start is called before the first frame update
    void Awake()
    {
        spawner.LoadPrefabs();

        weaponPanel = GameObject.Find("WSPanel");
        weaponPanel.SetActive(isPaused);
        weaponView.SetActive(isPaused);
        weaponSwitchMenu.SetActive(isPaused);
        pauseMenu.gameObject.SetActive(isPaused);
        confirmationUI.gameObject.SetActive(isPaused);

        weaponSwitchMenuScript = weaponSwitchMenu.GetComponent<WeaponSwitchMenu>();
        Debug.Log(weaponSwitchMenuScript);

        var rooms = dungeonCreator.CreateDungeon();
        var obstacleGenerator = new ObstacleGeneration(rooms, spawner);
        player1 = new Player(rooms[0], spawner);

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
        Debug.Log("return to mainmenu from gamemanager!!!!!!!");
        SceneManager.GetSceneAt(0);
    }
}
