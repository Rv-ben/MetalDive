using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] public DungeonCreator dungeonCreator;

    [SerializeField] public Spawner spawner;

    [SerializeField] public GameObject weaponView;

    [SerializeField] public GameObject weaponSwitchMenu;

    private WeaponSwitchMenu weaponSwitchMenuScript;

    private GameObject panel;

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
        var rooms = dungeonCreator.CreateDungeon();
        var obstacleGenerator = new ObstacleGeneration(rooms, spawner);
        player1 = new Player(rooms[0], spawner);

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
                Debug.Log(weaponSwitchMenuScript.currentWeaponIndex);
                var weaponEnum = weaponSwitchMenuScript.currentWeapon();
                Debug.Log(weaponEnum);
                SwitchPlayerWeapon(weaponEnum);
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

    public void SwitchPlayerWeapon (WeaponEnum weaponEnum)
    {
        this.player1.SwitchWeapon(weaponEnum);
    }
}
