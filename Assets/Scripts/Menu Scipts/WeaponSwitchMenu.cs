using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class WeaponSwitchMenu : MonoBehaviour
{
    [NonSerialized]
    private readonly List<GameObject> weapons;

    [NonSerialized]
    public int weaponIndex = 0;

    public GameObject weapon;

    public GameObject weaponView;

    private GameObject menuCanvas;

    public static bool isPaused = false;

    private void Start()
    {
        this.menuCanvas = GameObject.Find("Panel");
        menuCanvas.SetActive(isPaused);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused) 
            {
                Debug.Log("Tab key entered should resume");
                ResumeGame();
            }
            else
            {
                Debug.Log("Tab key entered should be paused");
                PauseGame();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void PauseGame()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="button"></param>
    /// <returns></returns>
    public void NextItem(GameObject button)
    {
        string buttonName = button.name;
        switch(buttonName)
        {
            case "LeftButton":
                Debug.Log("left item");
                if (weaponIndex == 0)
                {
                    weaponIndex = weapons.Count - 1;
                }
                else
                {
                    weaponIndex--;
                }
                SwitchWeapon(weaponIndex);
                break;


            case "RightButton":
                Debug.Log("right item");
                if (weaponIndex == weapons.Count - 1)
                {
                    weaponIndex = 0;
                }
                else
                {
                    weaponIndex++;
                }
                SwitchWeapon(weaponIndex);
                break;
        }

       // return currentWeaponIndex;
    } 

    public void SwitchWeapon(int index)
    {
        foreach (Transform weapon in transform)
        {
            int i = 0;
            if (i == index)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
            i++;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    public void LoadWeapon()
    {
        //weaponView.SendMessage("SwitchWeapon", weapons[weaponIndex]);
    }


}
